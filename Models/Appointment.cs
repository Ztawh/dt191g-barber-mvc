using System.ComponentModel.DataAnnotations;
using NuGet.Protocol.Plugins;

namespace barber_mvc.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    
    [Display(Name = "Datum och tid")]
    [Required(ErrorMessage = "Välj datum och tid")]
    public DateTime DateAndTime { get; set; }

    // Relationer
    [Display(Name = "Barberare")]
    [Required(ErrorMessage = "Ange en barberare")]
    public int BarberId { get; set; }
    
    [Display(Name = "Barberare")]
    public Barber? Barber { get; set; }
    
    [Display(Name = "Kund")]
    [Required(ErrorMessage = "Ange en kund")]
    public int CustomerId { get; set; }
    
    [Display(Name = "Kund")]
    public Customer? Customer { get; set; }
    
    [Display(Name = "Tjänst")]
    [Required(ErrorMessage = "Ange en tjänst")]
    public int ServiceId { get; set; }
    
    [Display(Name = "Tjänst")]
    public Service? Service { get; set; }
}