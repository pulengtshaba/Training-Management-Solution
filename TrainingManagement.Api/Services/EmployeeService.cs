using Microsoft.EntityFrameworkCore;
using TrainingManagement.Api.Common;
using TrainingManagement.Api.Data;
using TrainingManagement.Api.DTOs;
using TrainingManagement.Api.Interfaces;
using TrainingManagement.Api.Models;

namespace TrainingManagement.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(IEmployeeRepository repository, ILogger<EmployeeService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<EmployeeDto>> GetAllAsync(
    EmployeeQuery employeeQuery)
    {
        var query = _repository.GetQuery();

        // SEARCH
        if (!string.IsNullOrWhiteSpace(employeeQuery.Search))
        {
            var search = employeeQuery.Search.Trim();

            query = query.Where(e =>
                e.FirstName.Contains(search) ||
                e.LastName.Contains(search) ||
                e.Email.Contains(search));
        }

        // FILTER BY DEPARTMENT
        if (!string.IsNullOrWhiteSpace(employeeQuery.Department))
        {
            query = query.Where(e =>
                e.Department == employeeQuery.Department);
        }

        // FILTER BY ACTIVE STATUS
        if (employeeQuery.IsActive.HasValue)
        {
            query = query.Where(e =>
                e.IsActive == employeeQuery.IsActive.Value);
        }

        // COUNT BEFORE PAGINATION
        var totalRecords = await query.CountAsync();

        // SORT
        query = employeeQuery.Sort?.ToLower() switch
        {
            "firstname" =>
                query.OrderBy(e => e.FirstName),

            "-firstname" =>
                query.OrderByDescending(e => e.FirstName),

            "lastname" =>
                query.OrderBy(e => e.LastName),

            "-lastname" =>
                query.OrderByDescending(e => e.LastName),

            "department" =>
                query.OrderBy(e => e.Department),

            "-department" =>
                query.OrderByDescending(e => e.Department),

            "hiredate" =>
                query.OrderBy(e => e.HireDate),

            "-hiredate" =>
                query.OrderByDescending(e => e.HireDate),

            _ =>
                query.OrderBy(e => e.Id)
        };

        // PAGINATION
        var employees = await query
            .Skip((employeeQuery.Page - 1) * employeeQuery.PageSize)
            .Take(employeeQuery.PageSize)
            .ToListAsync();

        // MAP TO DTO
        var employeeDtos = employees
            .Select(employee => new EmployeeDto
            {
                Id = employee.Id,
                FullName =
                    $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                Department = employee.Department
            })
            .ToList();

        var totalPages =
            (int)Math.Ceiling(
                (double)totalRecords /
                employeeQuery.PageSize);

        return new PagedResult<EmployeeDto>
        {
            Page = employeeQuery.Page,
            PageSize = employeeQuery.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            HasPreviousPage = employeeQuery.Page > 1,
            HasNextPage = employeeQuery.Page < totalPages,
            Items = employeeDtos
        };
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
        {
            _logger.LogWarning(
                "Employee {EmployeeId} was not found",
                id);

            return null;
        }

        _logger.LogInformation(
    "Retrieving employee {EmployeeId}",
    id);

        return new EmployeeDto
        {
            Id = employee.Id,
            FullName = $"{employee.FirstName} {employee.LastName}",
            Email = employee.Email,
            Department = employee.Department
        };
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Department = dto.Department,
            IsActive = true
        };

        await _repository.AddAsync(employee);
        await _repository.SaveChangesAsync();

        return new EmployeeDto
        {
            Id = employee.Id,
            FullName = $"{employee.FirstName} {employee.LastName}",
            Email = employee.Email,
            Department = employee.Department
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateEmployeeDto dto)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
            return false;

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Email = dto.Email;
        employee.Department = dto.Department;
        employee.IsActive = dto.IsActive;

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
            return false;

        await _repository.DeleteAsync(employee);

        await _repository.SaveChangesAsync();

        return true;
    }
}