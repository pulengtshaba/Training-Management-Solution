namespace TrainingManagement.Api.DTOs;

public class EmployeeQuery
{
    public string? Search { get; set; }

    public string? Department { get; set; }

    public bool? IsActive { get; set; }

    public string? Sort { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}