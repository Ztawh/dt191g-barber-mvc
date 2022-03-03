namespace barber_mvc.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public DateTime DateAndTime { get; set; }
    
    // Relationer
    public int BarberId { get; set; }
    public Barber? Barber { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
}