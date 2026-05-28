using Domain.Enums;

namespace Application.DTOs.Contracts;

public record CreateContractDto
{
    public Guid InvestmentId { get; set; }
    public string TermsContent { get; set; } = string.Empty;
}

public record ContractDto
{
    public Guid Id { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public Guid InvestmentId { get; set; }
    public Guid FarmerId { get; set; }
    public Guid InvestorId { get; set; }
    public ContractStatus Status { get; set; }
    public decimal ContractAmount { get; set; }
    public decimal ExpectedReturn { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? SignedDate { get; set; }
    public bool IsFarmerSigned { get; set; }
    public bool IsInvestorSigned { get; set; }
    public string? PdfUrl { get; set; }
}

public record SignContractDto
{
    public Guid ContractId { get; set; }
    public string Signature { get; set; } = string.Empty;
}

public record ContractTermsDto
{
    public Guid ContractId { get; set; }
    public string TermsContent { get; set; } = string.Empty;
    public int CurrentVersion { get; set; }
    public DateTime CreatedDate { get; set; }
}
