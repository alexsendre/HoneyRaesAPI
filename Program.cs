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
    },
    new ServiceTickets()
    {
        Id = 6,
        CustomerID = 3,
        Description = "Couldn't get the car to start",
        isEmergency = false
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
    serviceTicket.Employee = employees.FirstOrDefault(e => e.Id == serviceTicket.EmployeeID);
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
    employee.ServiceTickets = serviceTickets.Where(st => st.EmployeeID == id).ToList();
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

app.MapPost("/servicetickets", (ServiceTickets serviceTicket) =>
{
    serviceTicket.Id = serviceTickets.Max(st => st.Id) + 1;
    serviceTickets.Add(serviceTicket);
    return serviceTicket;
});

app.MapDelete("/servicetickets/{id}", (int id) =>
{
    ServiceTickets serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    serviceTickets.RemoveAt(serviceTicket.Id - 1);
});

app.MapPut("/servicetickets/{ticketId}", (int ticketId, ServiceTickets serviceTicket) =>
{
    ServiceTickets ticketToUpdate = serviceTickets.FirstOrDefault(st => st.Id == ticketId);
    int ticketIndex = serviceTickets.IndexOf(ticketToUpdate);
    if (ticketToUpdate == null)
    {
        return Results.NotFound();
    }

    if (ticketId != serviceTicket.Id)
    {
        Results.BadRequest();
    }
    serviceTickets[ticketIndex] = serviceTicket;
    return Results.Ok();
});

app.MapPost("/servicetickets/{id}/complete", (int id) => 
{
    ServiceTickets ticketToComplete = serviceTickets.FirstOrDefault(st => st.Id == id);
    ticketToComplete.DateCompleted = DateTime.Today;
});

app.MapGet("/servicetickets/emergency", () =>
{
    return serviceTickets.Where(st => st.isEmergency && st.DateCompleted == null).ToList();
});

app.Run();
