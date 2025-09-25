using Meteron.API.Contracts.Customers;
using Meteron.Domain.Customers;
using Meteron.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Place SQLite DB file in the same project as MeteronDbContext (Infrastructure)
var infrastructureRoot = Path.GetFullPath("../Meteron.Infrastructure", builder.Environment.ContentRootPath);
var dbPath = Path.Combine(infrastructureRoot, "meteron.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
var connectionString = $"Data Source={dbPath}";

Console.WriteLine($"[EF] SQLite DB path: {dbPath}");

builder.Services.AddDbContext<MeteronDbContext>(options =>
    options.UseSqlite(connectionString, x => x.MigrationsAssembly("Meteron.Infrastructure")));

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

app.MapPost("/customers/business", (CreateBusinessCustomerRequest request) =>
{
    var customer = BusinessCustomer.Create(new Email(request.Email), request.Name, request.RegistrationNumber);
    customers.Add(customer.ToResponse());
    
    return customer.ToResponse();
});

#endregion

app.Run();