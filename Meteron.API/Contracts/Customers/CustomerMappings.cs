using Meteron.Domain.Customers;

namespace Meteron.API.Contracts.Customers;

public static class CustomerMappings
{
    public static CustomerResponse ToResponse(this Customer customer) =>
        customer switch
        {
            HouseholdCustomer householdCustomer => new CustomerResponse(
                householdCustomer.Id.ToString(),
                householdCustomer.Email.ToString(),
                "Household",
                householdCustomer.FirstName,
                householdCustomer.LastName,
                householdCustomer.DateOfBirth,
                null,
                null
            ),
            
            BusinessCustomer businessCustomer => new CustomerResponse(
                businessCustomer.Id.ToString(),
                businessCustomer.Email.ToString(),
                "Business",
                null,
                null,
                null,
                businessCustomer.Name,
                businessCustomer.RegistrationNumber
            ),
            
            _ => throw new NotSupportedException($"Unknown customer type {customer.GetType().Name}")
        };
}