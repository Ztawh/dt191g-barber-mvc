using System.ComponentModel.DataAnnotations;

namespace barber_mvc.Models;

public class Customer
{
    public int CustomerId { get; set; }
    
    [Display(Name = "För- och efternamn")]
    [Required(ErrorMessage = "Ange ett för- och efternamn")]
    public string? CustomerName { get; set; }
    
    [Display(Name = "Telefonnummer")]
    [Required(ErrorMessage = "Ange ett telefonnummer")]
    public string? Phone { get; set; }
}