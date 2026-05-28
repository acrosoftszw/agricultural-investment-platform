using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class FarmerProfile : AuditableEntity, IAggregateRoot
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public string FarmName { get; set; } = string.Empty;
    public string? FarmRegistrationNumber { get; set; }
    public decimal FarmSizeHectares { get; set; }
    public string LandOwnershipType { get; set; } = string.Empty; // Owned, Leased, Shared
    
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? LocationDescription { get; set; }
    
    public List<string> CropsGrown { get; set; } = new();
    public List<string> LivestockRaised { get; set; } = new();
    
    public string? BankName { get; set; }
    public string? BankAccountNumber { get; set; }
    public string? BankRoutingNumber { get; set; }
    
    public int YearsOfExperience { get; set; }
    public string? AgriculturalCertifications { get; set; }
    public string? ProductionHistory { get; set; }
    
    public bool IsVerified { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? VerificationNotes { get; set; }
    
    public bool IsSuspended { get; set; }
    public string? SuspensionReason { get; set; }
    public DateTime? SuspendedAt { get; set; }
    
    public bool IsFlagged { get; set; }
    public string? FlagReason { get; set; }
    
    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void MarkAsVerified(string verificationNotes)
    {
        IsVerified = true;
        VerifiedAt = DateTime.UtcNow;
        VerificationNotes = verificationNotes;
    }

    public void Suspend(string reason)
    {
        IsSuspended = true;
        SuspensionReason = reason;
        SuspendedAt = DateTime.UtcNow;
    }

    public void Unsuspend()
    {
        IsSuspended = false;
        SuspensionReason = null;
        SuspendedAt = null;
    }

    public void Flag(string reason)
    {
        IsFlagged = true;
        FlagReason = reason;
    }

    public void Unflag()
    {
        IsFlagged = false;
        FlagReason = null;
    }
}

public class FarmDocument : AuditableEntity
{
    public Guid FarmerProfileId { get; set; }
    public FarmerProfile? FarmerProfile { get; set; }
    
    public string DocumentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string FileHash { get; set; } = string.Empty;
    
    public bool IsVerified { get; set; }
    public string? VerificationNotes { get; set; }
    public DateTime? VerifiedAt { get; set; }
}

public class InvestorProfile : AuditableEntity, IAggregateRoot
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public string InvestorType { get; set; } = string.Empty; // Retail, Institutional, Accredited
    public string? Company { get; set; }
    public string? CompanyRegistration { get; set; }
    
    public decimal MinimumInvestmentAmount { get; set; }
    public decimal MaximumInvestmentAmount { get; set; }
    
    public string RiskAppetite { get; set; } = string.Empty; // Low, Medium, High
    public List<string> PreferredCrops { get; set; } = new();
    public List<string> PreferredRegions { get; set; } = new();
    
    public string? FundingSource { get; set; }
    public bool AccreditedStatus { get; set; }
    public DateTime? AccreditationVerifiedAt { get; set; }
    
    public bool RiskDisclosureAccepted { get; set; }
    public DateTime? RiskDisclosureAcceptedAt { get; set; }
    
    public decimal TotalInvested { get; set; }
    public int ActiveInvestments { get; set; }
    public decimal TotalReturned { get; set; }
    
    public bool IsActive { get; set; } = true;
    public bool IsSuspended { get; set; }
    public string? SuspensionReason { get; set; }
    
    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public bool CanInvest(decimal amount)
    {
        if (!IsActive || IsSuspended) return false;
        if (amount < MinimumInvestmentAmount || amount > MaximumInvestmentAmount) return false;
        return true;
    }

    public void Suspend(string reason)
    {
        IsSuspended = true;
        SuspensionReason = reason;
    }

    public void Unsuspend()
    {
        IsSuspended = false;
        SuspensionReason = null;
    }
}

public class KycVerification : AuditableEntity, IAggregateRoot
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public string FullName { get; set; } = string.Empty;
    public string? GovernmentIdType { get; set; }
    public string? GovernmentIdNumber { get; set; }
    public string? GovernmentIdImageUrl { get; set; }
    public string? GovernmentIdImageHash { get; set; }
    
    public string? SelfieImageUrl { get; set; }
    public string? SelfieImageHash { get; set; }
    public bool LivenessCheckPassed { get; set; }
    public DateTime? LivenessCheckDate { get; set; }
    
    public string? ProofOfAddressUrl { get; set; }
    public string? ProofOfAddressHash { get; set; }
    public string? AddressLine1 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    
    public KycStatus Status { get; set; } = KycStatus.Draft;
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public Guid? ReviewedBy { get; set; }
    public User? Reviewer { get; set; }
    
    public string? RejectionReason { get; set; }
    public string? ApprovalNotes { get; set; }
    
    public bool SanctionsCheckPassed { get; set; }
    public bool PepCheckPassed { get; set; }
    public string? ExternalKycProviderId { get; set; }
    public string? ExternalKycStatus { get; set; }
    
    public DateTime? ApprovedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    
    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void Submit()
    {
        Status = KycStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
    }

    public void Approve(Guid reviewedBy, string notes)
    {
        Status = KycStatus.Approved;
        ApprovedAt = DateTime.UtcNow;
        ReviewedAt = DateTime.UtcNow;
        ReviewedBy = reviewedBy;
        ApprovalNotes = notes;
        ExpiresAt = DateTime.UtcNow.AddYears(1);
    }

    public void Reject(Guid reviewedBy, string reason)
    {
        Status = KycStatus.Rejected;
        RejectionReason = reason;
        ReviewedAt = DateTime.UtcNow;
        ReviewedBy = reviewedBy;
    }

    public void RequestResubmission(string reason)
    {
        Status = KycStatus.ResubmissionRequired;
        RejectionReason = reason;
    }

    public bool IsExpired() => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt;
}
