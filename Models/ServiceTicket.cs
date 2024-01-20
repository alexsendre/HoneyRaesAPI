namespace HoneyRaesAPI.Models;

public class ServiceTickets
{
    public int Id { get; set; }
    public int CustomerID { get; set; }
    public int EmployeeID { get; set; }
    public string Description { get; set; }
    public bool isEmergency { get; set; }
    public DateTime DateCompleted { get; set; }
}