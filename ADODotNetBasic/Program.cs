using ADODotNetBasic.Models;
using System;

namespace ADODotNetBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Choose Operation");
                Console.WriteLine("1. Get all Employee");
                Console.WriteLine("2. Get Employee by id");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee by id");
                Console.WriteLine("5. Insert Employee");

                var userSelection = Convert.ToInt32(Console.ReadLine());
                var employeeService = new EmployeeService();

                switch (userSelection)
                {
                    case (int)EmployeeOperation.GET_ALL_EMPLOYEES:
                        var employees = employeeService.GetEmployees();

                        Console.WriteLine("------Showing all the employees details---------");
                        foreach (var employeeItem in employees)
                        {
                            Console.WriteLine("Id : " + employeeItem.Id);
                            Console.WriteLine("Name : " + employeeItem.Name);
                            Console.WriteLine("Age : " + employeeItem.Age);
                            Console.WriteLine("---------------");
                            Console.WriteLine();
                        }

                        break;

                    case (int)EmployeeOperation.GET_EMPLOYEE_BY_ID:
                        Console.WriteLine("Enter Employee id");
                        var employeeId = Convert.ToInt32(Console.ReadLine());

                        var employee = employeeService.GetEmployeeById(employeeId);
                        Console.WriteLine("Id : " + employee.Id);
                        Console.WriteLine("Name : " + employee.Name);
                        Console.WriteLine("Age : " + employee.Age);
                        Console.WriteLine("---------------");

                        break;

                    case (int)EmployeeOperation.UPDATE_EMPLOYEE:
                        Console.WriteLine("Enter Employee id");
                        var id = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter Employee name");
                        var name = Console.ReadLine();

                        Console.WriteLine("Enter Employee age");
                        var age = Convert.ToInt32(Console.ReadLine());

                        var updateEmployee = new Employee
                        {
                            Id = id,
                            Name = name,
                            Age = age
                        };
                        employeeService.UpdateEmployee(updateEmployee);
                        Console.WriteLine("Update Operation done successfully");

                        break;

                    case (int)EmployeeOperation.DELETE_EMPLOYEE:
                        Console.WriteLine("Enter Employee id to Delete");
                        var employeeIdToDelete = Convert.ToInt32(Console.ReadLine());

                        employeeService.DeleteEmployeeById(employeeIdToDelete);
                        Console.WriteLine("Delete Operation done successfully");

                        break;

                    case (int)EmployeeOperation.INSERT_EMPLOYEE:

                        Console.WriteLine("Enter Employee name");
                        var employeeName = Console.ReadLine();

                        Console.WriteLine("Enter Employee age");
                        var employeeAge = Convert.ToInt32(Console.ReadLine());

                        var employeeToInsert = new Employee
                        {
                            Name = employeeName,
                            Age = employeeAge
                        };
                        employeeService.InsertEmployee(employeeToInsert);
                        Console.WriteLine("Insert Operation done successfully");

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }

}
