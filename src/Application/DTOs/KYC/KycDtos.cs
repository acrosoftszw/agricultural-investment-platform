using Domain.Enums;

namespace Application.DTOs.KYC;

public record SubmitKycDto
{
    public string FullName { get; set; } = string.Empty;
    public string GovernmentIdType { get; set; } = string.Empty;
    public string GovernmentIdNumber { get; set; } = string.Empty;
    public string? AddressLine1 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}

public record KycStatusDto
{
    public Guid Id { get; set; }
    public KycStatus Status { get; set; }
    public string FullName { get; set; } = string.Empty;
    public bool LivenessCheckPassed { get; set; }
    public bool SanctionsCheckPassed { get; set; }
    public bool PepCheckPassed { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
}

public record KycReviewDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public KycStatus Status { get; set; }
    public DateTime SubmittedAt { get; set; }
    public string? GovernmentIdImageUrl { get; set; }
    public string? SelfieImageUrl { get; set; }
    public string? ProofOfAddressUrl { get; set; }
    public bool SanctionsCheckPassed { get; set; }
    public bool PepCheckPassed { get; set; }
}

public record ApproveKycDto
{
    public string ApprovalNotes { get; set; } = string.Empty;
}

public record RejectKycDto
{
    public string RejectionReason { get; set; } = string.Empty;
}

public record KycDocumentUploadDto
{
    public string DocumentType { get; set; } = string.Empty;
    public IFormFile File { get; set; } = null!;
}
