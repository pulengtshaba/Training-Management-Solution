using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingManagement.Api.Common;
using TrainingManagement.Api.Data;
using TrainingManagement.Api.DTOs;
using TrainingManagement.Api.Interfaces;
using TrainingManagement.Api.Models;
using TrainingManagement.Api.Models.Common;

namespace TrainingManagement.Api.Controllers;
//This tells ASP.NET Core: This class handles API requests
[Authorize]
[ApiController]
//This defines the URL: [controller] is automatically replaced with employee since the class is named: EmployeeController
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
//This controller inherits from ControllerBase.It provides helper methods like: Ok(),NotFound(),BadRequest(),Created()
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // GET /api/employee
    [HttpGet]
    public async Task<IActionResult> GetEmployees(
        [FromQuery] EmployeeQuery query)
    {
        var result = await _employeeService.GetAllAsync(query);

        return Ok(new ApiResponse<PagedResult<EmployeeDto>>
        {
            Success = true,
            Message = "Employees retrieved successfully.",
            Data = result
        });
    }

    // GET /api/employee/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound(new ApiResponse<EmployeeDto>
            {
                Success = false,
                Message = "Employee not found.",
                Data = null
            });
        }

        return Ok(new ApiResponse<EmployeeDto>
        {
            Success = true,
            Message = "Employee retrieved successfully.",
            Data = employee
        });
    }

    // POST /api/employee
    [HttpPost]
    public async Task<IActionResult> CreateEmployee(
        CreateEmployeeDto dto)
    {
        var employee = await _employeeService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetEmployeeById),
            new { id = employee.Id },
            new ApiResponse<EmployeeDto>
            {
                Success = true,
                Message = "Employee created successfully.",
                Data = employee
            });
    }

    // PUT /api/employee/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(
        int id,
        UpdateEmployeeDto dto)
    {
        var updated = await _employeeService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Employee not found.",
                Data = null
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Employee updated successfully.",
            Data = null
        });
    }

    // DELETE /api/employee/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var deleted = await _employeeService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Employee not found.",
                Data = null
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Employee deleted successfully.",
            Data = null
        });
    }

    // GET /api/employee/test-error
    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception("This is a test exception.");
    }
}
/*

Protect by Role

Suppose only managers should create employees.

[Authorize(Roles = "Manager")]
[HttpPost]
public async Task<IActionResult> Create(...)
{
}

Only users with the Manager role can access it.

Only administrators can delete users:

[Authorize(Roles = "Admin")]

Allow both Managers and Trainers:

[Authorize(Roles = "Manager,Trainer")]

ASP.NET Core checks the role claim in the JWT automatically.



One improvement: not to use object everywhere

For learning, this is fine: ApiResponse<object>

But once DTOs are established, I'd prefer: ApiResponse<EmployeeDto>

For example:
return Ok(new ApiResponse<EmployeeDto>
{
    Success = true,
    Message = "Employee retrieved successfully.",
    Data = employee
});

For a collection: ApiResponse<List<EmployeeDto>>

For your paginated result: ApiResponse<PagedResult<EmployeeDto>>

This gives you compile-time type safety.

That's much better than: ApiResponse<object>
 */