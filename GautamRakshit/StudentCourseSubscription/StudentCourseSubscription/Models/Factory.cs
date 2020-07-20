using StudentCourseSubscription.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCourseSubscription.Models
{
    public static class  Factory
    {
        public static dynamic CreateObj(Type type) {
            dynamic obj=null;

            if (type.Equals(typeof(CustomResponse)))
            {
                var cont = new Content() { Error = "Server Error", Result = null };
                obj = new CustomResponse(cont) { };
            }
            else if (type.Equals(typeof(StudentService)))
            {
                var IDbContext = new DbContext();
                obj = new StudentService(IDbContext);
            }
            else if (type.Equals(typeof(CourseService)))
            {
                var IDbContext = new DbContext();
                obj = new CourseService(IDbContext);
            }
            else if (type.Equals(typeof(SubscriptionService))) {
                 var IDbContext = new DbContext();
                obj = new SubscriptionService(IDbContext);
            }
            return obj;
        }
    }
}