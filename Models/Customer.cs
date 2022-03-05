using System.ComponentModel.DataAnnotations;

namespace barber_mvc.Models;

public class Customer
{
    public int CustomerId { get; set; }
    
    [Display(Name = "Namn")]
    public string? CustomerName { get; set; }
    
    [Display(Name = "Telefonnummer")]
    public string? Phone { get; set; }
}