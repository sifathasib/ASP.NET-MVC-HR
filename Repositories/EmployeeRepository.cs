using test.Repositories;
using test.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using test.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace test.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        // var employees = await _Dbcontext.EMPLOYEES
        //     .OrderBy(e => e.EMPLOYEE_ID) // Take only pageSize number of records
        //     .ToListAsync();
        return await _context.EMPLOYEES.ToListAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        return await _context.EMPLOYEES.FindAsync(id);
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _context.EMPLOYEES.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.EMPLOYEES.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _context.EMPLOYEES.FindAsync(id);
        if (employee != null)
        {
            _context.EMPLOYEES.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
