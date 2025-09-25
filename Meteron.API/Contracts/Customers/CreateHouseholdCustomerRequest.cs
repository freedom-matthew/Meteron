namespace Meteron.API.Contracts.Customers;

public record CreateHouseholdCustomerRequest(string Email, string FirstName, string LastName, DateOnly DateOfBirth);