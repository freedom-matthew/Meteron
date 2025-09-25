namespace Meteron.API.Contracts.Customers;

public sealed record CustomerResponse(
    string Id,
    string Email,
    string Kind,
    string? FirstName,
    string? LastName,
    DateOnly? DateOfBirth,
    string? Name,
    string? RegistrationNumber
);