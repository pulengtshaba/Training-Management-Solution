using TrainingManagement.Api.DTOs;
using TrainingManagement.Api.Models;

namespace TrainingManagement.Api.Interfaces;

public interface IEmployeeRepository
{
    IQueryable<Employee> GetQuery();

    Task<List<Employee>> GetAllAsync(
        EmployeeQuery query);

    Task<int> CountAsync(
        EmployeeQuery query);

    Task<Employee?> GetByIdAsync(int id);

    Task AddAsync(Employee employee);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(Employee employee);

    Task SaveChangesAsync();
}