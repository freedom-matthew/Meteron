namespace Meteron.Domain.Customers;

public class HouseholdCustomer : Customer
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    
    private HouseholdCustomer(CustomerId id, Email email, string firstName, string lastName, DateOnly dateOfBirth) : base(id, email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("Firstname cannot be null or whitespace.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Lastname cannot be null or whitespace.", nameof(lastName));
        }
        
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public static HouseholdCustomer Create(Email email, string firstName, string lastName, DateOnly dateOfBirth) => 
        new HouseholdCustomer(CustomerId.New(), email, firstName, lastName, dateOfBirth);
}