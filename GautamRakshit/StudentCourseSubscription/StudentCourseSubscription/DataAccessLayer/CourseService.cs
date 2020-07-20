using StudentCourseSubscription.DataAccessLayer.Interfaces;
using StudentCourseSubscription.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StudentCourseSubscription.DataAccessLayer
{
    public class CourseService : ICourseService
    {
        private IDbContext _dbContext;
        public CourseService(IDbContext dbcontext) { _dbContext = dbcontext; }
        public int CreateCourse(CourseVM course)
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("CreateCourse", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@CourseName", SqlDbType = SqlDbType.NVarChar, Value= course.CourseName},
                    new SqlParameter() {ParameterName = "@CourseDetail", SqlDbType = SqlDbType.NVarChar, Value = course.CourseDetail}                    

                 };
                cmd.Parameters.AddRange(parms.ToArray());
                var rowsEffected = cmd.ExecuteNonQuery();
                if (rowsEffected == -1)
                    return 1;
                else
                    return 0;

            }

        }

        public int Deletecourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(int courseId)
        {
            throw new NotImplementedException();
        }
        public Course[] GetCourses(int pageIndex, int pageSize, out int totalPages)
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetCourses", conn);//("select * from Album where id < 10", conn);
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
                        Course course = new Course();
                        course.CourseId = (int)reader["CoureId"];
                        course.CourseName = (string)reader["Name"];
                        course.CourseDetail= (string)reader["Detail"];
                       
                        courses.Add(course);

                    }
                };
                totalPages = (int)cmd.Parameters["@TotalPages"].Value;
            };
            return courses.ToArray();
        }

        public int Updatecourse(Course course)
        {
            throw new NotImplementedException();
        }

        public Student GetCourse(int studId)
        {
            throw new NotImplementedException();
        }
    }
}