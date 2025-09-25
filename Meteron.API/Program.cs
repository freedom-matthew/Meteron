using Meteron.API.Contracts.Customers;
using Meteron.Domain.Customers;
using Meteron.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MeteronDbContext>(options => options.UseSqlite(connectionString, x => x.MigrationsAssembly("Meteron.Infrastructure")));

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

#region PLAYGROUND

List<CustomerResponse> customers = [];

app.MapGet("/customers", () => customers);

app.MapPost("/customers/household", (CreateHouseholdCustomerRequest request) =>
{
    var customer = HouseholdCustomer.Create(new Email(request.Email), request.FirstName, request.LastName, request.DateOfBirth);
    customers.Add(customer.ToResponse());

    return customer.ToResponse();
});

app.MapPost("/customers/business", (CreateBusinessCustomerRequest dto) =>
{
    var customer = BusinessCustomer.Create(new Email(dto.Email), dto.Name, dto.RegistrationNumber);
    customers.Add(customer.ToResponse());
    
    return customer.ToResponse();
});

#endregion

app.Run();