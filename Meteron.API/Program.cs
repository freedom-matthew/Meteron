using Meteron.API.Contracts.Customers;
using Meteron.Domain.Customers;
using Meteron.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var infrastructureRoot = Path.GetFullPath("../Meteron.Infrastructure", builder.Environment.ContentRootPath);
var dbPath = Path.Combine(infrastructureRoot, "meteron.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
var connectionString = $"Data Source={dbPath}";

Console.WriteLine($"[EF] SQLite DB path: {dbPath}");

builder.Services.AddDbContext<MeteronDbContext>(options => options.UseSqlite(connectionString, x => x.MigrationsAssembly("Meteron.Infrastructure")));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

#region PLAYGROUND

List<CustomerResponse> customers = [];

app.MapGet("/customers", () => customers);

app.MapPost("/customers/household", async (CreateHouseholdCustomerRequest request, MeteronDbContext db) =>
{
    var customer = HouseholdCustomer.Create(new Email(request.Email), request.FirstName, request.LastName, request.DateOfBirth);
    await db.Customers.AddAsync(customer);
    
    customers.Add(customer.ToResponse());
    return customer.ToResponse();
});

app.MapPost("/customers/business", async (CreateBusinessCustomerRequest request, MeteronDbContext db) =>
{
    var customer = BusinessCustomer.Create(new Email(request.Email), request.Name, request.RegistrationNumber);
    await db.Customers.AddAsync(customer);
    
    customers.Add(customer.ToResponse());
    return customer.ToResponse();
});

#endregion

app.Run();