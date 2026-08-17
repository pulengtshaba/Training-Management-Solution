using System.ComponentModel.DataAnnotations;

namespace TrainingManagement.Api.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = "";

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Department { get; set; } = "";

    public DateTime HireDate { get; set; }

    public bool IsActive { get; set; }

    public string PhoneNumber { get; set; } = "";
}