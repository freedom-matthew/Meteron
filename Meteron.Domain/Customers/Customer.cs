using Meteron.Domain.Common;

namespace Meteron.Domain.Customers;

public abstract class Customer(CustomerId id, Email email) : EntityBase<CustomerId>(id)
{
    public Email Email { get; private set; } = email;
}