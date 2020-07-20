using StudentCourseSubscription.DataAccessLayer.Interfaces;
using StudentCourseSubscription.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace StudentCourseSubscription.DataAccessLayer
{
    public class StudentService : IStudentService
    {
        private IDbContext _dbContext;

        public StudentService(IDbContext dbcontext) {
            _dbContext = dbcontext;
        }
        public int CreateStudent(StudentVM stud)
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("CreateStudent", conn);//("select * from Album where id < 10", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FirstName", SqlDbType = SqlDbType.NVarChar, Value= stud.FirstName},
                    new SqlParameter() {ParameterName = "@LastName", SqlDbType = SqlDbType.NVarChar, Value = stud.LastName},
                    new SqlParameter() {ParameterName = "@Dob", SqlDbType = SqlDbType.DateTime, Value = stud.DOB},
                    new SqlParameter() {ParameterName = "@ContactNo", SqlDbType = SqlDbType.VarChar, Value = stud.ContactNo}
                   

                 };
                cmd.Parameters.AddRange(parms.ToArray());
                var rowsEffected=cmd.ExecuteNonQuery();
                if (rowsEffected == -1)
                    return 1;
                else
                    return 0;

            }
        }

        public int DeleteStudent(int StudId)
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DeleteStudent", conn);//("select * from Album where id < 10", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@StudId", SqlDbType = SqlDbType.NVarChar, Value= StudId},
                   
                 };
                cmd.Parameters.AddRange(parms.ToArray());
                var rowsEffected = cmd.ExecuteNonQuery();
                if (rowsEffected == -1)
                    return 1;
                else
                    return 0;

            }
        }

        public int UpdateStudent(Student stud)
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UpdateStudent", conn);//("select * from Album where id < 10", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@StudID", SqlDbType = SqlDbType.NVarChar, Value= stud.StudId},
                    new SqlParameter() {ParameterName = "@FirstName", SqlDbType = SqlDbType.NVarChar, Value= stud.FirstName},
                    new SqlParameter() {ParameterName = "@LastName", SqlDbType = SqlDbType.NVarChar, Value = stud.LastName},
                    new SqlParameter() {ParameterName = "@Dob", SqlDbType = SqlDbType.DateTime, Value = stud.DOB},
                    new SqlParameter() {ParameterName = "@ContactNo", SqlDbType = SqlDbType.VarChar, Value = stud.ContactNo}

                 };
                cmd.Parameters.AddRange(parms.ToArray());
                var rowsEffected = 0;
                try {
                    rowsEffected = cmd.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception e)
                {
                    sqlTransaction.Rollback();
                    //Write your exception handling code here
                }
                finally
                {
                    conn.Close();
                }

                if (rowsEffected == -1)
                    return 1;
                else
                    return 0;

            }
        }
        public Student[] GetStudents(int pageIndex,int pageSize,out int totalPages)
        {
            List<Student> studs = new List<Student>();
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetStudents", conn);//("select * from Album where id < 10", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Value=pageIndex},
                    new SqlParameter() {ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Value = pageSize},
                    new SqlParameter() {ParameterName = "@TotalPages", SqlDbType = SqlDbType.Int, Direction=System.Data.ParameterDirection.Output}


                 };
                cmd.Parameters.AddRange(parms.ToArray());
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student stud = new Student();
                        stud.StudId = (int)reader["StudId"];
                        stud.FirstName = (string)reader["FirstName"];
                        stud.LastName = (string)reader["LastName"];
                        stud.DOB = (DateTime)reader["Dob"];                        
                        stud.ContactNo = (string)reader["ContactNo"];                       
                        studs.Add(stud);                    

                    }
                };

                totalPages = (int)cmd.Parameters["@TotalPages"].Value;
            };
           

            return studs.ToArray();
        }

        public Student GetStudent(int studId)
        {
            Student stud=null;
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetStudent", conn);//("select * from Album where id < 10", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@StudID", SqlDbType = SqlDbType.Int, Value=studId},                   

                 };
                cmd.Parameters.AddRange(parms.ToArray());
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {     stud= new Student();
                        stud.StudId = (int)reader["StudId"];
                        stud.FirstName = (string)reader["FirstName"];
                        stud.LastName = (string)reader["LastName"];
                        stud.DOB = (DateTime)reader["Dob"];
                        stud.ContactNo = (string)reader["ContactNo"];
                        

                    }
                };

            };
            return stud;
        }
    }
}