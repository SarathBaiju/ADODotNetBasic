using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ADODotNetBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var sqlConnection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=Learning_db");

                sqlConnection.Open();

                var sqlCommand = new SqlCommand("select top 10 * from Employee", sqlConnection);


                //To see data: DefaultView => ExpandResult
                var dataTable = new DataTable();
                var sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);

                var employees = dataTable.AsEnumerable().Select(dataRow => {
                    return new Employee
                    {
                        Id = dataRow.Field<int>("Id"),
                        Name = dataRow.Field<string>("Name"),
                        Age = dataRow.Field<int>("Age")
                    };
                }).ToList();
                sqlConnection.Close();

                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.Id);
                    Console.WriteLine(employee.Name);
                    Console.WriteLine(employee.Age);

                    Console.WriteLine("----------------------");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
