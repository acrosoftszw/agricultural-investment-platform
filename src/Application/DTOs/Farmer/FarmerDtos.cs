using Domain.Enums;

namespace Application.DTOs.Farmer;

public record CreateFarmerProfileDto
{
    public string FarmName { get; set; } = string.Empty;
    public string? FarmRegistrationNumber { get; set; }
    public decimal FarmSizeHectares { get; set; }
    public string LandOwnershipType { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? LocationDescription { get; set; }
    public List<string> CropsGrown { get; set; } = new();
    public List<string> LivestockRaised { get; set; } = new();
    public int YearsOfExperience { get; set; }
    public string? AgriculturalCertifications { get; set; }
}

public record UpdateFarmerProfileDto
{
    public string? FarmName { get; set; }
    public decimal? FarmSizeHectares { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public List<string>? CropsGrown { get; set; }
    public List<string>? LivestockRaised { get; set; }
}

public record FarmerProfileDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public bool IsSuspended { get; set; }
    public decimal FarmSizeHectares { get; set; }
    public int YearsOfExperience { get; set; }
    public List<string> CropsGrown { get; set; } = new();
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public record CreateFarmListingDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CropType { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? LocationDescription { get; set; }
    public decimal FarmSizeHectares { get; set; }
    public decimal FundingTargetAmount { get; set; }
    public decimal ExpectedReturnPercentage { get; set; }
    public decimal MinimumInvestmentAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? HarvestDate { get; set; }
    public string? RiskStatement { get; set; }
    public string? Milestones { get; set; }
}

public record FarmListingDto
{
    public Guid Id { get; set; }
    public Guid FarmerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CropType { get; set; } = string.Empty;
    public decimal FundingTargetAmount { get; set; }
    public decimal AmountRaised { get; set; }
    public decimal ExpectedReturnPercentage { get; set; }
    public FarmListingStatus Status { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public record FarmListingDetailDto
{
    public Guid Id { get; set; }
    public Guid FarmerId { get; set; }
    public string FarmerName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CropType { get; set; } = string.Empty;
    public decimal FundingTargetAmount { get; set; }
    public decimal AmountRaised { get; set; }
    public decimal FundingPercentage { get; set; }
    public decimal ExpectedReturnPercentage { get; set; }
    public decimal MinimumInvestmentAmount { get; set; }
    public FarmListingStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? HarvestDate { get; set; }
    public string? RiskStatement { get; set; }
    public string? Milestones { get; set; }
    public List<string> MediaUrls { get; set; } = new();
}
