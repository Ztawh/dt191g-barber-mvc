using System.ComponentModel.DataAnnotations;

namespace barber_mvc.Models;

public class Service
{
    public int ServiceId { get; set; }
    
    [Display(Name = "Tjänst")]
    [Required(ErrorMessage = "Ange en tjänst")]
    public string? ServiceName { get; set; }
    
    [Display(Name = "Långvarighet (min)")]
    [Required(ErrorMessage = "Ange en långvarighet")]
    [Range(1, 60, ErrorMessage="Långvarigheten måste vara mellan 1-60 minuter")]
    public int ServiceDuration { get; set; }
    
    [Display(Name = "Pris")]
    [Required(ErrorMessage = "Ange ett pris")]
    [Range(1, int.MaxValue, ErrorMessage="Priset måste vara minst 1kr")]
    public int ServicePrice { get; set; }
}