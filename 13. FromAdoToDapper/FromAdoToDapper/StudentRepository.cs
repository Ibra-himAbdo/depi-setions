using Microsoft.Data.SqlClient;
using System.Data;

namespace FromAdoToDapper
{
    public class StudentRepository
    {
        private readonly SqlConnection _connection;

        public StudentRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<Student> GetStudents()
        {
            string sql = "select * from Student";

            SqlCommand command = new SqlCommand(sql, _connection);

            _connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<Student> students = new List<Student>();
            while (reader.Read())
            {
                Student std = new Student
                {
                    Id = reader.GetInt32("Id"), // (int)reader["Id"]
                    SID = reader.GetInt32("SID"),//(int)reader["SID"],
                    FNAME = reader["FNAME"].ToString(),
                    LNAME = reader["LNAME"].ToString(),
                    BDATE = reader.GetDateTime("BDATE"), //(DateTime)reader["BDATE"],
                    SALARY = reader.IsDBNull("SALARY") ? 0 : (decimal)reader["SALARY"],
                    CITY = reader.IsDBNull("CITY") ? string.Empty : reader["CITY"].ToString(),
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };

                students.Add(std);
            }

            _connection.Close();

            return students;
        }

        public int CreateStudnet(Student student)
        {
            string sql = "INSERT INTO Student (SID,FNAME,LNAME,BDATE) " +
                $"VALUES(@SID,@FNAME,@LNAME,@BDATE) " +
                $"select cast(SCOPE_IDENTITY() as int)";

            SqlCommand command = new(sql, _connection);

            command.Parameters.Add("@SID", SqlDbType.Int).Value = student.SID;
            command.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = student.FNAME;
            command.Parameters.Add("@LNAME", SqlDbType.VarChar).Value = student.LNAME;
            command.Parameters.Add("@BDATE", SqlDbType.DateTime2).Value = student.BDATE;

            _connection.Open();

            try
            {
                int affectedRows = Convert.ToInt32(command.ExecuteScalar());

                if (affectedRows > 0)
                {
                    Console.WriteLine("Added");
                }
                return affectedRows;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return 0;
        }

        public void AddWithProc(Student student)
        {
            SqlCommand command = new("AddStudent", _connection);

            command.Parameters.Add("@SID", SqlDbType.Int).Value = student.SID;
            command.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = student.FNAME;
            command.Parameters.Add("@LNAME", SqlDbType.VarChar).Value = student.LNAME;
            command.Parameters.Add("@BDATE", SqlDbType.DateTime2).Value = student.BDATE;
            command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = student.SALARY;
            command.Parameters.Add("@City", SqlDbType.VarChar).Value = student.CITY;

            command.CommandType = CommandType.StoredProcedure;

            _connection.Open();

            try
            {
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    Console.WriteLine("Added");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateName(int id, string newName)
        {
            string sql = "UPDATE Student " +
                "set FNAME = @NewName " +
                "WHERE Id = @Id";

            SqlCommand command = new(sql, _connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            command.Parameters.Add("@NewName", SqlDbType.VarChar).Value = newName;

            _connection.Open();

            try
            {
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    Console.WriteLine("Updated");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

        }
    }
}