namespace Domain.ValueObjects;

public record PhoneNumber
{
    private const string Pattern = @"^\+?[1-9]\d{1,14}$"; // E.164 format

    public string Value { get; }

    private PhoneNumber(string value)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, Pattern))
            throw new ArgumentException("Invalid phone number format", nameof(value));
        
        Value = value;
    }

    public static PhoneNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty", nameof(value));
        
        return new PhoneNumber(value.Trim());
    }

    public static PhoneNumber FromE164(string value)
    {
        // Normalize to E.164
        var normalized = value.StartsWith("+") ? value : $"+{value}";
        return Create(normalized);
    }

    public override string ToString() => Value;

    public static bool TryCreate(string value, out PhoneNumber? phoneNumber)
    {
        try
        {
            phoneNumber = Create(value);
            return true;
        }
        catch
        {
            phoneNumber = null;
            return false;
        }
    }
}

public record Email
{
    private const string Pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public string Value { get; }

    private Email(string value)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, Pattern))
            throw new ArgumentException("Invalid email format", nameof(value));
        
        Value = value.ToLowerInvariant();
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty", nameof(value));
        
        return new Email(value.Trim());
    }

    public override string ToString() => Value;
}

public record Money
{
    private const decimal MinValue = 0;
    private const decimal MaxValue = decimal.MaxValue;

    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        if (amount < MinValue || amount > MaxValue)
            throw new ArgumentException($"Amount must be between {MinValue} and {MaxValue}", nameof(amount));
        
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new ArgumentException("Currency must be a 3-letter code", nameof(currency));
        
        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public static Money Create(decimal amount, string currency = "USD")
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
        
        return new Money(amount, currency);
    }

    public Money Add(Money other)
    {
        if (other.Currency != Currency)
            throw new InvalidOperationException($"Cannot add {other.Currency} to {Currency}");
        
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (other.Currency != Currency)
            throw new InvalidOperationException($"Cannot subtract {other.Currency} from {Currency}");
        
        var result = Amount - other.Amount;
        if (result < 0)
            throw new InvalidOperationException("Insufficient funds");
        
        return new Money(result, Currency);
    }

    public Money Multiply(decimal factor)
    {
        if (factor < 0)
            throw new ArgumentException("Multiplier cannot be negative", nameof(factor));
        
        return new Money(Amount * factor, Currency);
    }

    public override string ToString() => $"{Currency} {Amount:F2}";

    public static Money Zero(string currency = "USD") => new(0, currency);
}

public record Location
{
    public double Latitude { get; }
    public double Longitude { get; }
    public string? AddressLine1 { get; }
    public string? AddressLine2 { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }

    private Location(
        double latitude,
        double longitude,
        string? addressLine1,
        string? addressLine2,
        string city,
        string state,
        string postalCode,
        string country)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException("Latitude must be between -90 and 90", nameof(latitude));
        if (longitude < -180 || longitude > 180)
            throw new ArgumentException("Longitude must be between -180 and 180", nameof(longitude));

        Latitude = latitude;
        Longitude = longitude;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public static Location Create(
        double latitude,
        double longitude,
        string city,
        string state,
        string postalCode,
        string country,
        string? addressLine1 = null,
        string? addressLine2 = null)
    {
        return new Location(latitude, longitude, addressLine1, addressLine2, city, state, postalCode, country);
    }

    public double DistanceTo(Location other)
    {
        const double earthRadiusKm = 6371;
        var dLat = DegToRad(other.Latitude - Latitude);
        var dLon = DegToRad(other.Longitude - Longitude);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegToRad(Latitude)) * Math.Cos(DegToRad(other.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadiusKm * c;
    }

    private static double DegToRad(double degrees) => degrees * Math.PI / 180.0;
}

public record OtpCode
{
    public string Code { get; }
    public DateTime ExpiresAt { get; }
    public int AttemptsRemaining { get; }
    public const int MaxAttempts = 3;
    public const int ExpirationMinutes = 5;

    private OtpCode(string code, DateTime expiresAt, int attemptsRemaining)
    {
        Code = code;
        ExpiresAt = expiresAt;
        AttemptsRemaining = attemptsRemaining;
    }

    public static OtpCode Generate()
    {
        var code = Random.Shared.Next(100000, 999999).ToString();
        var expiresAt = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        return new OtpCode(code, expiresAt, MaxAttempts);
    }

    public bool IsExpired() => DateTime.UtcNow > ExpiresAt;

    public bool IsValid(string attemptCode) => !IsExpired() && Code == attemptCode && AttemptsRemaining > 0;

    public OtpCode DecrementAttempts() => new(Code, ExpiresAt, Math.Max(0, AttemptsRemaining - 1));
}
