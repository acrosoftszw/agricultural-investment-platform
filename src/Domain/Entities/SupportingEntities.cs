using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Message : AuditableEntity, IAggregateRoot
{
    public Guid SenderId { get; set; }
    public User? Sender { get; set; }

    public Guid? ReceiverId { get; set; }
    public User? Receiver { get; set; }

    public Guid? ConversationId { get; set; }
    public Conversation? Conversation { get; set; }

    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }

    public List<string> AttachmentUrls { get; set; } = new();
    public List<string> AttachmentHashes { get; set; } = new();

    public Guid? ReplyToMessageId { get; set; }
    public Message? ReplyToMessage { get; set; }

    public string? IpAddress { get; set; }
    public string? DeviceFingerprint { get; set; }

    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void MarkAsRead()
    {
        IsRead = true;
        ReadAt = DateTime.UtcNow;
    }
}

public class Conversation : AuditableEntity, IAggregateRoot
{
    public Guid InitiatorId { get; set; }
    public User? Initiator { get; set; }

    public Guid? ParticipantId { get; set; }
    public User? Participant { get; set; }

    public Guid? InvestmentId { get; set; }
    public Investment? Investment { get; set; }

    public Guid? FarmListingId { get; set; }
    public FarmListing? FarmListing { get; set; }

    public string? Subject { get; set; }
    public string ConversationType { get; set; } = string.Empty; // Direct, Support, Investment
    public bool IsActive { get; set; } = true;

    public DateTime? LastMessageAt { get; set; }
    public int UnreadMessageCount { get; set; }

    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void Close()
    {
        IsActive = false;
    }
}

public class MarketplaceProduct : AuditableEntity, IAggregateRoot
{
    public Guid SellerId { get; set; }
    public User? Seller { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public int QuantityAvailable { get; set; }
    public int QuantitySold { get; set; }

    public string? Location { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public List<string> ImageUrls { get; set; } = new();
    public string DeliveryMethod { get; set; } = string.Empty; // Delivery, Pickup, Both

    public bool IsActive { get; set; } = true;
    public DateTime? ApprovedAt { get; set; }
    public Guid? ApprovedBy { get; set; }

    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public bool IsInStock() => QuantityAvailable > 0;

    public void RecordSale(int quantity)
    {
        if (quantity > QuantityAvailable)
            throw new InvalidOperationException("Insufficient stock");
        QuantityAvailable -= quantity;
        QuantitySold += quantity;
    }
}

public class Order : AuditableEntity, IAggregateRoot
{
    public Guid BuyerId { get; set; }
    public User? Buyer { get; set; }

    public Guid ProductId { get; set; }
    public MarketplaceProduct? Product { get; set; }

    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal FinalAmount { get; set; }

    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Shipped, Delivered, Cancelled, Returned
    public DateTime? DeliveredAt { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;
    public string? TrackingNumber { get; set; }

    public string? ReviewComment { get; set; }
    public int? ReviewRating { get; set; }
    public DateTime? ReviewedAt { get; set; }

    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void ConfirmOrder()
    {
        Status = "Confirmed";
    }

    public void ShipOrder(string trackingNumber)
    {
        Status = "Shipped";
        TrackingNumber = trackingNumber;
    }

    public void DeliverOrder()
    {
        Status = "Delivered";
        DeliveredAt = DateTime.UtcNow;
    }

    public void CancelOrder()
    {
        Status = "Cancelled";
    }
}

public class Course : AuditableEntity, IAggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructor { get; set; } = string.Empty;
    public Guid? InstructorId { get; set; }
    public User? InstructorUser { get; set; }

    public CourseStatus Status { get; set; } = CourseStatus.Draft;
    public int DurationHours { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? Category { get; set; }

    public string? PrerequisiteCourseIds { get; set; }
    public bool IsMandatory { get; set; }
    public int CompletionPercentageRequired { get; set; } = 80;

    public DateTime PublishedAt { get; set; }

    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void Publish()
    {
        Status = CourseStatus.Published;
        PublishedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = CourseStatus.Archived;
    }
}

public class Lesson : AuditableEntity, IAggregateRoot
{
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public int LessonNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public LessonType LessonType { get; set; }

    public string? VideoUrl { get; set; }
    public string? TextContent { get; set; }
    public string? PdfUrl { get; set; }
    public int DurationMinutes { get; set; }

    public string? QuizContent { get; set; }
    public int? QuizPassingScore { get; set; }

    public int SortOrder { get; set; }

    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();
}

public class CourseEnrollment : AuditableEntity, IAggregateRoot
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    public DateTime? CompletionDate { get; set; }
    public int CompletionPercentage { get; set; }

    public bool IsCompleted { get; set; }
    public string? CertificateUrl { get; set; }
    public string? CertificateHash { get; set; }

    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void MarkCompleted()
    {
        IsCompleted = true;
        CompletionDate = DateTime.UtcNow;
        CompletionPercentage = 100;
    }
}

public class LessonProgress : AuditableEntity
{
    public Guid CourseEnrollmentId { get; set; }
    public CourseEnrollment? CourseEnrollment { get; set; }

    public Guid LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? QuizScore { get; set; }
    public bool? QuizPassed { get; set; }
}

public class Notification : AuditableEntity, IAggregateRoot
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public NotificationType Type { get; set; }
    public NotificationStatus Status { get; set; } = NotificationStatus.Pending;

    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Data { get; set; }

    public DateTime ScheduledFor { get; set; } = DateTime.UtcNow;
    public DateTime? SentAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public string? FailureReason { get; set; }

    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void MarkAsSent()
    {
        Status = NotificationStatus.Sent;
        SentAt = DateTime.UtcNow;
    }

    public void MarkAsRead()
    {
        Status = NotificationStatus.Read;
        ReadAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        Status = NotificationStatus.Failed;
        FailureReason = reason;
    }
}

public class RoiRecord : AuditableEntity, IAggregateRoot
{
    public Guid InvestmentId { get; set; }
    public Investment? Investment { get; set; }

    public Guid InvestorId { get; set; }
    public InvestorProfile? Investor { get; set; }

    public Guid FarmListingId { get; set; }
    public FarmListing? FarmListing { get; set; }

    public Guid FarmerId { get; set; }
    public FarmerProfile? Farmer { get; set; }

    public RoiModel RoiModel { get; set; }
    public RoiStatus Status { get; set; } = RoiStatus.Projected;

    public decimal InvestmentAmount { get; set; }
    public decimal ExpectedReturn { get; set; }
    public decimal? RealizedReturn { get; set; }
    public decimal? PendingReturn { get; set; }

    public double ExpectedRoiPercentage { get; set; }
    public double? RealizedRoiPercentage { get; set; }

    public DateTime ProjectionDate { get; set; }
    public DateTime? ActualCompletionDate { get; set; }
    public DateTime? LastCalculatedAt { get; set; }

    public string? CalculationDetails { get; set; }
    public string? AdjustmentReason { get; set; }

    private List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();
}

public class RoiCalculationLog : AuditableEntity
{
    public Guid RoiRecordId { get; set; }
    public RoiRecord? RoiRecord { get; set; }

    public DateTime CalculationTime { get; set; } = DateTime.UtcNow;
    public string CalculationMethod { get; set; } = string.Empty;
    public decimal ResultAmount { get; set; }
    public double ResultPercentage { get; set; }
    public string? InputParameters { get; set; }
    public string? CalculationFormula { get; set; }
    public string? Status { get; set; }
}
