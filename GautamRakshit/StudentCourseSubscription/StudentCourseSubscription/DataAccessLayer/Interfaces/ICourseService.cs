
using StudentCourseSubscription.Models;

namespace StudentCourseSubscription.DataAccessLayer.Interfaces
{
    public interface ICourseService
    {
        int CreateCourse(CourseVM course);
        int Updatecourse(Course course);
        int Deletecourse(int courseId);
        Course[] GetCourses(int pageIndex, int pageSize, out int totalPages);
        Student GetCourse(int studId);
    }
}