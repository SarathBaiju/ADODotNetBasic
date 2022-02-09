using EmployeeDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeeDataAccess.Services
{
    public class EmployeeRepository
    {
        private SqlConnection _sqlConnection;

        public EmployeeRepository()
        {
            _sqlConnection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=Learning_db");
        }

        public List<Employee> GetEmployees()
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand("select * from Employee", _sqlConnection);

                var dataTable = new DataTable();
                var sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);

                var employees = dataTable.AsEnumerable().Select(dataRow =>
                {
                    return new Employee
                    {
                        Id = dataRow.Field<int>("Id"),
                        Name = dataRow.Field<string>("Name"),
                        Age = dataRow.Field<int>("Age")
                    };
                }).ToList();

                return employees;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand("select * from Employee where id = " + id, _sqlConnection);

                var dataTable = new DataTable();
                var sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);

                var employees = dataTable.AsEnumerable().Select(dataRow =>
                {
                    return new Employee
                    {
                        Id = dataRow.Field<int>("Id"),
                        Name = dataRow.Field<string>("Name"),
                        Age = dataRow.Field<int>("Age")
                    };
                }).ToList();

                return employees.First();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public int InsertEmployee(Employee employee)
        {
            try
            {
                _sqlConnection.Open();
                var insertQuery = "insert into Employee output INSERTED.ID values(@name, @age)";

                var sqlCommand = new SqlCommand(insertQuery, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("name", employee.Name);
                sqlCommand.Parameters.AddWithValue("age", employee.Age);

                var id = sqlCommand.ExecuteScalar();

                return Convert.ToInt32(id);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public int UpdateEmployee(Employee employee)
        {
            try
            {
                _sqlConnection.Open();
                var updateQuery = @"update Employee
                                            set Name = @name,
                                            Age = @age
                                            where Id=@id";

                var sqlCommand = new SqlCommand(updateQuery, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("id", employee.Id);
                sqlCommand.Parameters.AddWithValue("name", employee.Name);
                sqlCommand.Parameters.AddWithValue("age", employee.Age);

                var noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                return noOfRowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public int DeleteEmployeeById(int id)
        {
            try
            {
                _sqlConnection.Open();
                var deleteQuery = @"delete Employee where id=@id";

                var sqlCommand = new SqlCommand(deleteQuery, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("id", id);

                var noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                return noOfRowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
