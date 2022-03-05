using System.ComponentModel.DataAnnotations;

namespace barber_mvc.Models;

public class Service
{
    public int ServiceId { get; set; }
    
    [Display(Name = "Tjänst")]
    public string? ServiceName { get; set; }
    
    [Display(Name = "Långvarighet (min)")]
    public int ServiceDuration { get; set; }
    
    [Display(Name = "Pris")]
    public int ServicePrice { get; set; }
}