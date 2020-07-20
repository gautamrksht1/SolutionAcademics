
using System.Data.SqlClient;


namespace StudentCourseSubscription.DataAccessLayer.Interfaces
{
    public interface IDbContext
    {
        SqlConnection GetConnection();
    }
}