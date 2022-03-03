namespace barber_mvc.Models;

public class Service
{
    public int ServiceId { get; set; }
    public string? ServiceName { get; set; }
    public int ServiceDuration { get; set; }
    public int ServicePrice { get; set; }
}