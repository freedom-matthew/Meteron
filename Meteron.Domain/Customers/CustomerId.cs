namespace Meteron.Domain.Customers;

public readonly record struct CustomerId(Guid Value)
{
    public static CustomerId New() => new CustomerId(Guid.NewGuid());
    
    public override string ToString() => Value.ToString();
}