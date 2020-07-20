using StudentCourseSubscription.DataAccessLayer.Interfaces;
using System;
using System.Data.SqlClient;
namespace StudentCourseSubscription.DataAccessLayer
{
    
    public class DbContext : IDbContext
    {
        private const string connString= @"Server=GAUTAM-PC\MSSQLSERVER2014;Database=Solution;integrated security=SSPI";
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connString);
        }
    }
}