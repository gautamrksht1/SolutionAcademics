
namespace StudentCourseSubscription.Models
{
    public class CoursePaginatedModel
    {
        public Course[] Courses { get; set; }
        public int TotalPages { get; set; }
    }
}