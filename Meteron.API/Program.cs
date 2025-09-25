using Meteron.Domain.Customers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

#region PLAYGROUND

List<Customer> customers = [];

app.MapGet("/customers", () => customers);

app.MapPost("/customers/household", (HouseholdCustomerDto dto) =>
{
    var customer = HouseholdCustomer.Create(new Email(dto.Email), dto.FirstName, dto.LastName, dto.DateOfBirth);
    customers.Add(customer);
    return customer;
});

app.MapPost("/customers/business", (BusinessCustomerDto dto) =>
{
    var customer = BusinessCustomer.Create(new Email(dto.Email), dto.Name, dto.RegistrationNumber);
    customers.Add(customer);
    return customer;
});

#endregion

app.Run();

// DTO definitions
public record HouseholdCustomerDto(string Email, string FirstName, string LastName, DateOnly DateOfBirth);
public record BusinessCustomerDto(string Email, string Name, string RegistrationNumber);