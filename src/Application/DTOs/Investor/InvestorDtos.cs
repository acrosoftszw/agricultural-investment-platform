using Domain.Enums;

namespace Application.DTOs.Investor;

public record CreateInvestorProfileDto
{
    public string InvestorType { get; set; } = string.Empty;
    public string? Company { get; set; }
    public string? CompanyRegistration { get; set; }
    public decimal MinimumInvestmentAmount { get; set; }
    public decimal MaximumInvestmentAmount { get; set; }
    public string RiskAppetite { get; set; } = string.Empty;
    public List<string> PreferredCrops { get; set; } = new();
    public List<string> PreferredRegions { get; set; } = new();
    public string? FundingSource { get; set; }
}

public record UpdateInvestorProfileDto
{
    public string? RiskAppetite { get; set; }
    public List<string>? PreferredCrops { get; set; }
    public List<string>? PreferredRegions { get; set; }
    public decimal? MinimumInvestmentAmount { get; set; }
    public decimal? MaximumInvestmentAmount { get; set; }
}

public record InvestorProfileDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string InvestorType { get; set; } = string.Empty;
    public string? Company { get; set; }
    public string RiskAppetite { get; set; } = string.Empty;
    public decimal TotalInvested { get; set; }
    public int ActiveInvestments { get; set; }
    public decimal TotalReturned { get; set; }
    public bool RiskDisclosureAccepted { get; set; }
    public KycStatus KycStatus { get; set; }
}

public record AcceptRiskDisclosureDto
{
    public bool Accepted { get; set; }
}

public record CreateInvestmentDto
{
    public Guid FarmListingId { get; set; }
    public decimal InvestmentAmount { get; set; }
    public string? IdempotencyKey { get; set; }
}

public record InvestmentDto
{
    public Guid Id { get; set; }
    public Guid InvestorId { get; set; }
    public Guid FarmListingId { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
    public decimal ExpectedReturn { get; set; }
    public decimal? RealizedReturn { get; set; }
    public InvestmentStatus Status { get; set; }
    public DateTime InvestmentDate { get; set; }
    public DateTime? MaturityDate { get; set; }
    public DateTime? PayoutDate { get; set; }
    public double RoiPercentage { get; set; }
}

public record InvestmentDetailDto
{
    public Guid Id { get; set; }
    public Guid InvestorId { get; set; }
    public string InvestorName { get; set; } = string.Empty;
    public Guid FarmListingId { get; set; }
    public string FarmTitle { get; set; } = string.Empty;
    public string ReferenceNumber { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
    public decimal ExpectedReturn { get; set; }
    public decimal? RealizedReturn { get; set; }
    public decimal? PendingReturn { get; set; }
    public InvestmentStatus Status { get; set; }
    public DateTime InvestmentDate { get; set; }
    public DateTime? ActivationDate { get; set; }
    public DateTime? MaturityDate { get; set; }
    public DateTime? PayoutDate { get; set; }
    public List<MilestoneDto> Milestones { get; set; } = new();
    public ContractDto? Contract { get; set; }
}

public record MilestoneDto
{
    public Guid Id { get; set; }
    public int MilestoneNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PayoutPercentage { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedDate { get; set; }
}

public record GetRoiDto
{
    public Guid InvestmentId { get; set; }
    public decimal InvestmentAmount { get; set; }
    public decimal ExpectedReturn { get; set; }
    public decimal? RealizedReturn { get; set; }
    public decimal? PendingReturn { get; set; }
    public double ExpectedRoiPercentage { get; set; }
    public double? RealizedRoiPercentage { get; set; }
    public RoiStatus Status { get; set; }
    public RoiModel RoiModel { get; set; }
    public string FarmTitle { get; set; } = string.Empty;
    public DateTime ProjectionDate { get; set; }
    public DateTime? ActualCompletionDate { get; set; }
}

public record InvestorPortfolioDto
{
    public decimal TotalInvested { get; set; }
    public decimal TotalExpectedReturn { get; set; }
    public decimal TotalRealizedReturn { get; set; }
    public decimal TotalPendingReturn { get; set; }
    public int ActiveInvestments { get; set; }
    public int CompletedInvestments { get; set; }
    public int DefaultedInvestments { get; set; }
    public double OverallRoiPercentage { get; set; }
    public List<InvestmentDto> RecentInvestments { get; set; } = new();
}
