using StudentCourseSubscription.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCourseSubscription.DataAccessLayer.Interfaces
{
    public interface ISubscriptionService
    {
        StudentsAndCourseList GetStudentAndCourseList();
        int SubscribeCourse(CourseSubscription cs);

        Subscription[] SubscriptionList();

    }

}