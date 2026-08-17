using System.ComponentModel.DataAnnotations;

namespace TrainingManagement.Api.DTOs;

public class CreateEmployeeDto
{
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
}