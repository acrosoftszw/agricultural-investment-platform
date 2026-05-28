namespace Application.Common.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string PhoneNumber { get; }
    string? Email { get; }
    List<string> Roles { get; }
    List<string> Permissions { get; }
    bool HasRole(string role);
    bool HasPermission(string permission);
}

public interface IAuthenticationService
{
    Task<string> GenerateAccessTokenAsync(Guid userId);
    Task<string> GenerateRefreshTokenAsync(Guid userId);
    Task<bool> ValidateRefreshTokenAsync(Guid userId, string token);
    Task RevokeRefreshTokenAsync(Guid userId, string token);
    Task RevokeAllTokensAsync(Guid userId);
    string HashPassword(string password);
    bool VerifyPasswordHash(string password, string hash, string? salt = null);
}

public interface IOtpService
{
    Task<string> GenerateOtpAsync(Guid userId, string phoneNumber);
    Task<bool> VerifyOtpAsync(Guid userId, string otpCode);
    Task InvalidateOtpAsync(Guid userId);
}

public interface ISmsSender
{
    Task<bool> SendOtpAsync(string phoneNumber, string otp);
    Task<bool> SendMessageAsync(string phoneNumber, string message);
}

public interface IEmailSender
{
    Task<bool> SendEmailAsync(string email, string subject, string body);
    Task<bool> SendHtmlEmailAsync(string email, string subject, string htmlBody);
}

public interface IEncryptionService
{
    string Encrypt(string plaintext);
    string Decrypt(string ciphertext);
}

public interface IFileStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task<Stream> DownloadFileAsync(string fileUrl);
    Task<bool> DeleteFileAsync(string fileUrl);
    Task<bool> ValidateFileAsync(Stream fileStream, string contentType, long maxSizeBytes);
    string GenerateSignedUrl(string fileUrl, TimeSpan expiresIn);
}

public interface IAuditService
{
    Task LogActionAsync(Guid? userId, string entityType, Guid entityId, string action, 
        string? oldValues, string? newValues, string? ipAddress, string? userAgent, string severity = "Info");
}

public interface INotificationService
{
    Task SendNotificationAsync(Guid userId, string title, string message, string type, string? data = null);
    Task SendMultipleNotificationsAsync(List<Guid> userIds, string title, string message, string type, string? data = null);
}
