namespace Meteron.API.Contracts.Customers;

public record CreateBusinessCustomerRequest(string Email, string Name, string RegistrationNumber);