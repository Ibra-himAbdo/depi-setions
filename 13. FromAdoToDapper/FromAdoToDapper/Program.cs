using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace FromAdoToDapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Missing connection string");

            StudentRepository studentRepository = new(connectionString);

            //foreach (Student std in studentRepository.GetStudents())
            //{
            //    Console.WriteLine(std);
            //}

            //studentRepository.AddWithProc(new Student
            //{
            //    SID = 503,
            //    FNAME = "Test",
            //    LNAME = "Test 123",
            //    BDATE = DateTime.Now,
            //    SALARY=10,
            //    CITY = "Tantaw"
            //});


            //studentRepository.UpdateName(24, "Ahmed");

            // offline read

            IDbConnection _connection = new SqlConnection(connectionString);

            string sql = "select * from Student";

            //_connection.Open();

            //SqlDataAdapter _adapter = new SqlDataAdapter(sql, _connection);

            //DataTable dataTable = new DataTable();
            //_adapter.Fill(dataTable);
            //_connection.Close();

            //foreach (DataRow row in dataTable.Rows)
            //{
            //    Console.WriteLine(row["FNAME"]);
            //}

            //Dapper

            var studnets = _connection.Query<Student>(sql);

            foreach (Student student in studnets)
            {
                Console.WriteLine(student);
            }



        }
    }
}
