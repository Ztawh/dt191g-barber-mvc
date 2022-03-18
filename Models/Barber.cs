using System.ComponentModel.DataAnnotations;

namespace barber_mvc.Models;

public class Barber
{
    public int BarberId { get; set; }
    
    [Display(Name = "För- och efternamn")]
    [Required(ErrorMessage = "Ange ett för- och efternamn")]
    public string? BarberName { get; set; }
    
    [Display(Name = "Roll")]
    [Required(ErrorMessage = "Ange en roll (ex. Barberare)")]
    public string? Role { get; set; }
    
    [Display(Name = "Prisnivå")]
    [Required(ErrorMessage = "Ange ett pris")]
    [Range(1, 5, ErrorMessage = "Prisnivån måste vara mellan 1-5")]
    public float PayRate { get; set; }
}