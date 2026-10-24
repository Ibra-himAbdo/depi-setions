using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ado_Net;

public class StudnetRepository
{
    private readonly SqlConnection _connection;

    public StudnetRepository(string ConnectionString)
    {
        _connection = new SqlConnection(ConnectionString);
    }

    public List<Student> GetAll()
    {
        string sql = "SELECT * FROM Student";
        try
        {
            SqlCommand command = new SqlCommand(sql, _connection);

            _connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<Student> students = new List<Student>();

            while (reader.Read())
            {
                var student = new Student()
                {
                    Id = reader.IsDBNull("Id") ? 0 : reader.GetInt32("Id"),
                    SID = reader.IsDBNull("SID") ? 0 : reader.GetInt32("SID"),
                    FNAME = reader.IsDBNull("FNAME") ? string.Empty : reader.GetString("FNAME"),
                    LNAME = reader.IsDBNull("LNAME") ? string.Empty : reader.GetString("LNAME"),
                    BDATE = reader.IsDBNull("BDATE") ? DateTime.MinValue : reader.GetDateTime("BDATE"),
                    SALARY = reader.IsDBNull("SALARY") ? null : reader.GetDecimal("SALARY"),
                    CITY = reader.IsDBNull("CITY") ? null : reader.GetString("CITY"),
                    CreatedAt = reader.IsDBNull("CreatedAt") ? DateTime.MinValue : reader.GetDateTime("CreatedAt")
                };

               students.Add(student);
            }

            return students;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            _connection.Close();
        }

        return new();
    }


    public void Add()
    {
        var data = new
        {
            Fname = "Ahmed2222222s",
            Lame = "Mohamed2222222222s",
            SSN = 452,
            BDate = DateTime.Now.AddYears(-12),
        };

        string sql = $"INSERT INTO Student(SID,Fname,Lname,Bdate) " +
            $"VALUES(@SID,@Fname,@Lname,@Bdate)" +
            $"select cast(scope_identity() as int)";

        SqlCommand command = new SqlCommand(sql, _connection);

        //command.Parameters.AddWithValue("@SID", data.SSN);
        //command.Parameters.AddWithValue("@Fname", data.Fname);
        //command.Parameters.AddWithValue("@Lname", data.Lame);
        //command.Parameters.AddWithValue("@Bdate", data.BDate);

        command.Parameters.Add("@SID", SqlDbType.Int).Value = data.SSN;
        command.Parameters.Add("@Fname", SqlDbType.VarChar).Value = data.Fname;
        command.Parameters.Add("@Lname", SqlDbType.VarChar).Value = data.Lame;
        command.Parameters.Add("@Bdate", SqlDbType.DateTime2).Value = data.BDate;

        _connection.Open();

        int idValue = (int)command.ExecuteScalar();

        Console.WriteLine("Added wiht {0}", idValue);

        _connection.Close();
    }

    public void Update()
    {
        string sql = "UPDATE Student SET Fname = 'NewValue' WHERE Id = 14";
        SqlCommand command = new SqlCommand(sql, _connection);

        _connection.Open();

        if (command.ExecuteNonQuery() > 0)
        {
            Console.WriteLine("Updated");
        }

        _connection.Close();
    }

    public void Delete()
    {
        string sql = "delete FROM Student WHERE Id = 14";
        SqlCommand command = new SqlCommand(sql, _connection);

        _connection.Open();

        if (command.ExecuteNonQuery() > 0)
        {
            Console.WriteLine("Deleted");
        }

        _connection.Close();
    }

    public void AddWithStored()
    {
        SqlCommand command = new SqlCommand("AddStudent", _connection);

        command.CommandType = CommandType.StoredProcedure;

        var data = new
        {
            Fname = "Ahmed2222222s",
            Lame = "Mohamed2222222222s",
            SSN = 452,
            BDate = DateTime.Now.AddYears(-12),
        };

        command.Parameters.Add("@SID", SqlDbType.Int).Value = data.SSN;
        command.Parameters.Add("@Fname", SqlDbType.VarChar).Value = data.Fname;
        command.Parameters.Add("@Lname", SqlDbType.VarChar).Value = data.Lame;
        command.Parameters.Add("@Bdate", SqlDbType.DateTime2).Value = data.BDate;
        command.Parameters.Add("@salary", SqlDbType.Decimal).Value = Random.Shared.Next(1, 900);
        command.Parameters.Add("@city", SqlDbType.VarChar).Value = "cairo";


        _connection.Open();

        if (command.ExecuteNonQuery() > 0)
        {
            Console.WriteLine("Deleted");
        }

        _connection.Close();
    }

}