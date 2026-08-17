using TrainingManagement.Api.Common;
using TrainingManagement.Api.DTOs;
using TrainingManagement.Api.Models;

namespace TrainingManagement.Api.Interfaces;

public interface IEmployeeService
{
    Task<PagedResult<EmployeeDto>> GetAllAsync(
    EmployeeQuery query);

    Task<EmployeeDto?> GetByIdAsync(int id);

    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);

    Task<bool> UpdateAsync(int id, UpdateEmployeeDto dto);

    Task<bool> DeleteAsync(int id);
}