using System.ComponentModel.DataAnnotations;

namespace barber_mvc.Models;

public class Barber
{
    public int BarberId { get; set; }
    
    [Display(Name = "Namn")]
    public string? BarberName { get; set; }
    
    [Display(Name = "Roll")]
    public string? Role { get; set; }
    
    [Display(Name = "Prisnivå")]
    public float PayRate { get; set; }
}