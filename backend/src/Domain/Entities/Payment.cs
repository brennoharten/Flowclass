namespace Domain.Entities;

public class Payment : BaseEntity
{
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = default!;
    public PaymentStatus Status { get; private set; }
    public string ExternalReference { get; private set; } = default!;

    // Navegações
    public User User { get; private set; } = default!;

    private Payment() { } // EF
    public Payment(Guid tenantId, Guid userId, decimal amount, string currency, PaymentStatus status, string externalReference)
    {
        TenantId = tenantId;
        UserId = userId;
        Amount = amount;
        Currency = currency;
        Status = status;
        ExternalReference = externalReference;
    }
}

public enum PaymentStatus
{
    Pending = 0,
    Paid = 1,
    Failed = 2
}
