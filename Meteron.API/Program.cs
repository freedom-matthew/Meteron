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

app.MapGet("/customers", async (MeteronDbContext db) =>
{
    var customers = await db.Customers.ToListAsync();
    await db.SaveChangesAsync();

    List<CustomerResponse> customerResponses = [];
    foreach (var customer in customers)
    {
        customerResponses.Add(customer.ToResponse());
    }

    return customerResponses;
});

app.MapPost("/customers/household", async (CreateHouseholdCustomerRequest request, MeteronDbContext db) =>
{
    var customer = HouseholdCustomer.Create(new Email(request.Email), request.FirstName, request.LastName, request.DateOfBirth);
    await db.Customers.AddAsync(customer);
    await db.SaveChangesAsync();
    return customer.ToResponse();
});

app.MapPost("/customers/business", async (CreateBusinessCustomerRequest request, MeteronDbContext db) =>
{
    var customer = BusinessCustomer.Create(new Email(request.Email), request.Name, request.RegistrationNumber);
    await db.Customers.AddAsync(customer);
    await db.SaveChangesAsync();
    
    return customer.ToResponse();
});

#endregion

app.Run();