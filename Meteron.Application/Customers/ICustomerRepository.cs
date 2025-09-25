using Meteron.Domain.Customers;

namespace Meteron.Application.Customers;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer, CancellationToken ct = default);
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken ct = default);
    Task<Customer?> GetAsync(CustomerId id, CancellationToken ct = default);
}