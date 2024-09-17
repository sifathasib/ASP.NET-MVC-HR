using test.Services;
using test.Models;
using test.Repositories;
namespace test.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _employeeRepository.GetAllEmployeesAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        return await _employeeRepository.GetEmployeeByIdAsync(id);
    }

    public async Task CreateEmployeeAsync(Employee employee)
    {
        // Example of simple validation before saving employee
        if (string.IsNullOrWhiteSpace(employee.FIRST_NAME) || string.IsNullOrWhiteSpace(employee.LAST_NAME))
        {
            throw new ArgumentException("Employee must have a valid first and last name.");
        }
        
        await _employeeRepository.AddEmployeeAsync(employee);
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        await _employeeRepository.UpdateEmployeeAsync(employee);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        await _employeeRepository.DeleteEmployeeAsync(id);
    }
}
