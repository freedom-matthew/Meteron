namespace Meteron.Domain.Customers;

public class BusinessCustomer : Customer
{
    public string Name { get; private set; }
    public string RegistrationNumber { get; private set; }

    private BusinessCustomer(CustomerId id, Email email, string name, string registrationNumber) : base (id, email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            throw new ArgumentException("Registration number cannot be null or whitespace.", nameof(registrationNumber));
        }
        
        Name = name;
        RegistrationNumber = registrationNumber;
    }

    public static BusinessCustomer Create(Email email, string name, string registrationNumber) =>
        new BusinessCustomer(CustomerId.New(), email, name, registrationNumber);
}