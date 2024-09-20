using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using test.Models;
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
        Employee employee = null;

        // Get the database connection from AppDbContext
        var connection = _context.Database.GetDbConnection();

        try
        {
            // Open the connection if it's not already open
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            // Create an OracleCommand to call the stored procedure
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "EMPLOYEE_PACKAGE.GET_EMPLOYEE_BY_ID";
                command.CommandType = CommandType.StoredProcedure;

                // Add input parameter for employee ID
                var employeeIdParam = new OracleParameter("p_employee_id", OracleDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(employeeIdParam);

                // Add output parameter for the SYS_REFCURSOR
                var cursorParam = new OracleParameter("p_employee", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(cursorParam);

                // Execute the command and retrieve the result
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            employee = new Employee
                            {
                                EMPLOYEE_ID = reader.GetInt32(0),
                                FIRST_NAME = reader.GetString(1),
                                LAST_NAME = reader.GetString(2),
                                EMAIL = reader.GetString(3)
                            };
                        }
                    }
                }
            }
        }
        finally
        {
            // Ensure the connection is closed
            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }

        return employee;
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
