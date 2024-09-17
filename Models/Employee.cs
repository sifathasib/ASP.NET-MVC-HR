using System.ComponentModel.DataAnnotations;

namespace test.Models;

public class Employee
{
    [Key]
    public int EMPLOYEE_ID { get; set; }

    public string FIRST_NAME { get; set; }

    public string LAST_NAME { get; set; }

    public string EMAIL { get; set; }
}
