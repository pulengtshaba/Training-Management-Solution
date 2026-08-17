using Microsoft.EntityFrameworkCore;
using TrainingManagement.Api.Data;
using TrainingManagement.Api.DTOs;
using TrainingManagement.Api.Interfaces;
using TrainingManagement.Api.Models;

namespace TrainingManagement.Api.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Employee> GetQuery()
    {
        return _context.Employees.AsQueryable();
    }

    public async Task<int> CountAsync(EmployeeQuery query)
    {
        var employees = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            employees = employees.Where(e =>
                e.FirstName.Contains(query.Search) ||
                e.LastName.Contains(query.Search) ||
                e.Email.Contains(query.Search));
        }

        if (!string.IsNullOrWhiteSpace(query.Department))
        {
            employees = employees.Where(e =>
                e.Department == query.Department);
        }

        if (query.IsActive.HasValue)
        {
            employees = employees.Where(e =>
                e.IsActive == query.IsActive.Value);
        }

        return await employees.CountAsync();
    }

    public async Task<List<Employee>> GetAllAsync(EmployeeQuery query)
    {
        var employees = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            employees = employees.Where(e =>
                e.FirstName.Contains(query.Search) ||
                e.LastName.Contains(query.Search) ||
                e.Email.Contains(query.Search));
        }

        if (!string.IsNullOrWhiteSpace(query.Department))
        {
            employees = employees.Where(e =>
                e.Department == query.Department);
        }

        if (query.IsActive.HasValue)
        {
            employees = employees.Where(e =>
                e.IsActive == query.IsActive.Value);
        }

        return await employees
            .OrderBy(e => e.Id)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}