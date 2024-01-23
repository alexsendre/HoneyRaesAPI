using HoneyRaesAPI.Models;

List<Customer> customers = new()
{
    new Customer()
    {
        Id = 1,
        Name = "Jelly Jello",
        Address = "35 Jella Rd."
    },
    new Customer()
    {
        Id = 2,
        Name = "Pnut Butter",
        Address = "44 Peanut Dr."
    },
    new Customer()
    {
        Id = 3,
        Name = "Burnt Toast",
        Address = "892 Toaster Cir."
    }
};

List<Employee> employees = new()
{
    new Employee()
    {
        Id = 1,
        Name = "Ham Sam",
        Specialty = "Eating"
    },
    new Employee()
    {
        Id = 2,
        Name = "Dee Bogger",
        Specialty = "Debugging"
    }
};

List<ServiceTickets> serviceTickets = new()
{
    new ServiceTickets()
    {
        Id = 1,
        CustomerID = 1,
        EmployeeID = 1,
        Description = "Lost wallet walking back from the gym",
        isEmergency = true,
        DateCompleted = new DateTime(2024, 1, 3)
    },
    new ServiceTickets()
    {
        Id = 2,
        CustomerID = 2,
        EmployeeID = 2,
        Description = "Couldn't find a place to sit in the break room",
        isEmergency = false,
        DateCompleted = new DateTime(2024, 1, 20)
    },
    new ServiceTickets()
    {
        Id = 3, 
        CustomerID = 3,
        EmployeeID = 1,
        Description = "Stepped on and broke glasses",
        isEmergency = true,
    },
    new ServiceTickets()
    {
        Id = 4,
        CustomerID = 1,
        EmployeeID = 2,
        Description = "Got hacked",
        isEmergency = true,
        DateCompleted = new DateTime(2024, 1, 5)
    },
    new ServiceTickets()
    {
        Id = 5,
        CustomerID = 3,
        Description = "Couldn't get the car to start",
        isEmergency = false,
        DateCompleted = new DateTime(2023, 12, 26)
    }
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/servicetickets", () =>
{
    return serviceTickets;
});

app.MapGet("/servicetickets/{id}", (int id) =>
{
    ServiceTickets serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    if (serviceTicket == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(serviceTicket);
});

app.MapGet("/employees", () =>
{
    return employees;
});

app.MapGet("/employees/{id}", (int id) =>
{
    Employee employee = employees.FirstOrDefault(e => e.Id == id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(employee);
});

app.MapGet("/customers", () =>
{
    return customers;
});

app.MapGet("/customers/{id}", (int id) =>
{
    Customer customer = customers.FirstOrDefault(c => c.Id == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(customer);
});

app.Run();
