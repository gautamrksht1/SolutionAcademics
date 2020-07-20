using StudentCourseSubscription.DataAccessLayer.Interfaces;
using StudentCourseSubscription.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StudentCourseSubscription.DataAccessLayer
{
    public class SubscriptionService : ISubscriptionService
    {
        private IDbContext _dbContext;
        public SubscriptionService(IDbContext dbcontext) { _dbContext = dbcontext; }
        public StudentsAndCourseList GetStudentAndCourseList()
        {
            StudentsAndCourseList studsCoursesList =null;

            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("StudentsAndCoursesList", conn);//("select * from Album where id < 10", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                List<StudentSlim> studSlims = new List<StudentSlim>();
                List<CourseSlim> courseSlims = new List<CourseSlim>();

                foreach (DataRow drCurrent in ds.Tables[0].Rows)
                {
                    StudentSlim studSlim = new StudentSlim();

                    studSlim.StudId = (int)drCurrent["StudId"];
                    studSlim.Name = drCurrent["Name"].ToString();
                    studSlims.Add(studSlim);
                }
                foreach (DataRow drCurrent in ds.Tables[1].Rows)
                {
                    CourseSlim courseSlim = new CourseSlim();

                    courseSlim.CourseId = (int)drCurrent["CoureId"];
                    courseSlim.Name = drCurrent["Name"].ToString();
                    courseSlims.Add(courseSlim);
                }
                studsCoursesList = new StudentsAndCourseList();
                studsCoursesList.Students = studSlims.ToArray();
                studsCoursesList.Courses = courseSlims.ToArray();
            };
            return studsCoursesList;
        }

        public int SubscribeCourse(CourseSubscription cs)
        {
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("CreateSubscription", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@StudId", SqlDbType = SqlDbType.NVarChar, Value= cs.StudId},
                    new SqlParameter() {ParameterName = "@CourseID", SqlDbType = SqlDbType.NVarChar, Value = cs.CourseId},
                    new SqlParameter() {ParameterName = "@StatusId", SqlDbType = SqlDbType.Int, Direction=System.Data.ParameterDirection.Output}

                 };
                cmd.Parameters.AddRange(parms.ToArray());
                cmd.ExecuteNonQuery();
                var status= (int)cmd.Parameters["@StatusId"].Value;
                return status;

            }
        }

        public Subscription[] SubscriptionList()
        {
            List<Subscription> subscriptions = new List<Subscription>();
            using (SqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetSubscriptionDetail", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Subscription subscription = new Subscription();
                        subscription.RegId = (int)reader["RegId"];
                        subscription.StudentName = (string)reader["StudentName"];
                        subscription.CourseName = (string)reader["CourseName"];

                        subscriptions.Add(subscription);

                    }
                };

            }
            return subscriptions.ToArray();

        }
    }
}