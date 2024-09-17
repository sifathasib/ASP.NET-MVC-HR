using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using test.Services;

namespace test.Controllers;

public class EmployeeController : Controller{

    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    [HttpGet("/Employee")]
    public async Task<IActionResult> Index()
    {
        //var employees = await _dbContext.EMPLOYEES.ToListAsync();
        //var totalEmployees = await _Dbcontext.EMPLOYEES.CountAsync();
        //var totalPages = (int)Math.Ceiling(totalEmployees / (double)pageSize);
        // var employees = await _Dbcontext.EMPLOYEES
        //     .OrderBy(e => e.EMPLOYEE_ID) // Take only pageSize number of records
        //     .ToListAsync();
        var employees = await _employeeService.GetAllEmployeesAsync();
        return View(employees);
        // Pass data and pagination info to the view
        // ViewData["CurrentPage"] = pageNumber;
        // ViewData["TotalPages"] = totalPages;
        if (employees == null)
        {       
            return View(new List<Employee>()); // Pass an empty list if null
        }
        return View(employees);
    }

}
