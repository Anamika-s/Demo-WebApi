using DemoWebApi.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsingADO.Models;

using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DemoWebApi.Repository
{
    public class StudentRepo : IStudentRepo

    {
        private readonly IConfiguration _config;
        SqlConnection connection = null;
        
        public StudentRepo(IConfiguration config)
        {
            _config = config;
            
        }
        public SqlConnection GetConnection()
        {
           string connectionString =  _config.GetConnectionString("MyConnection").ToString();
            connection = new SqlConnection(connectionString);
            return connection;
        }
        public void Delete(int id)
        {
            using (SqlConnection connection  =  GetConnection())
            {
                using (SqlCommand command = new SqlCommand("Delete from students where studentId =@id"))
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public List<Student> Get()
        {
            List<Student> listStudents = null;
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "Select * from students";
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        listStudents = new List<Student>();
                        while (reader.Read())
                        {
                            listStudents.Add(new Student()
                            {
                                StudentId = (int)reader["StudentId"],
                                Name = reader["Name"].ToString(),
                                Batch = reader["Batch"].ToString(),
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                Marks = (int)reader["Marks"]
                            }
                            );

                        }
                    }
                        reader.Close();
                        connection.Close();

                    }
                }

                return listStudents;
                
            }
        

        public Student Get(int id)
        {

             Student student = null;
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "Select * from students where studentId=@id";
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();

                        student = new Student()
                        {

                            StudentId = (int)reader["StudentId"],
                            Name = reader["Name"].ToString(),
                            Batch = reader["Batch"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Marks = (int)reader["Marks"]
                        };
                           

                        }
                           reader.Close();
                    connection.Close();

                }
            }

            return student;

        }

        public void Post(Student student)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "Insert into Students(Name , Batch, DateOfBirth, Marks) " +
                        "values (@name , @batch, @dateOfBirth,@marks)";
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@name", student.Name);
                    command.Parameters.AddWithValue("@batch", student.Batch);
                    command.Parameters.AddWithValue("@dateofbirth", student.DateOfBirth);
                    command.Parameters.AddWithValue("@marks", student.Marks);
                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }
        }

        public void Put(int id, Student student)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "update Students set batch=@batch, marks = @marks, dateofbirth=@datebirth where studentId =@id";
                      command.Connection = connection;
                     command.Parameters.AddWithValue("@batch", student.Batch);
                    command.Parameters.AddWithValue("@dateofbirth", student.DateOfBirth);
                    command.Parameters.AddWithValue("@marks", student.Marks);
                    command.Parameters.AddWithValue("@studentid", id);
                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }

        }
    }
}
