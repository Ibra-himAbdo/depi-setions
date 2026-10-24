using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ado_Net;

    
internal class Program
{

    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Empty Connection String");


        StudnetRepository studnetRepositroy = new StudnetRepository(connectionString);

        //studnetRepositroy.Add();

        //studnetRepositroy.Update();

        //studnetRepositroy.Delete();

        //studnetRepositroy.AddWithStored();


        //var sttudnets = studnetRepositroy.GetAll();

        //foreach (var item in sttudnets)
        //{
        //    Console.WriteLine(item.FNAME);
        //}


        //IDbConnection connection = new SqlConnection(connectionString);

        //var students = connection.Query<Student>("SELECT * FROM Student");

        //foreach (var item in students)
        //{
        //    Console.WriteLine(item.FNAME);
        //}

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        using SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Student", connection);


        DataTable dataTable = new DataTable();

        dataAdapter.Fill(dataTable);



        foreach (DataRow row in dataTable.Rows)
        {
            Console.WriteLine(row["Fname"]);
        }



    }
}
