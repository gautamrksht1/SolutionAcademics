using StudentCourseSubscription.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCourseSubscription.DataAccessLayer.Interfaces
{
    public interface IStudentService
    {
        int CreateStudent(StudentVM stud);
        int UpdateStudent(Student stud);
        int DeleteStudent(int StudId);

        Student[] GetStudents(int pageIndex,int pageSize,out int totalPages);

        Student GetStudent(int studId);
    }
}