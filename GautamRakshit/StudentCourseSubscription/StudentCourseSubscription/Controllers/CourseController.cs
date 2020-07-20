using StudentCourseSubscription.DataAccessLayer;
using StudentCourseSubscription.DataAccessLayer.Interfaces;
using StudentCourseSubscription.Models;

using System.Collections.Generic;

using System.Web.Http;

namespace StudentCourseSubscription.Controllers
{
    [CustomExceptionFilter]
    public class CourseController : ApiController
    {
        private ICourseService _courseService;

        public CourseController() {
            _courseService = Factory.CreateObj(typeof(CourseService));
        }
        // GET: api/Course
        [HttpGet]
        public IHttpActionResult Courses(int pageIndex, int pageSize)
        {
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));
            int totalPages;
            var students = _courseService.GetCourses(pageIndex, pageSize, out totalPages);
            if (students.Length > 0)
            {
                CoursePaginatedModel coursePaginated = new CoursePaginatedModel();
                coursePaginated.Courses = students;
                coursePaginated.TotalPages = totalPages;
                res.responseContent.Error = null;
                res.responseContent.Result = coursePaginated;
            }
            return res;
        }

        // GET: api/Course/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Course
        public IHttpActionResult Post([FromBody]CourseVM course)
        {
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));
            var s = course;
            if (course != null)
            {
                var result = _courseService.CreateCourse(course);
                if (result == 1)
                {
                    res.responseContent.Error = null;
                    res.responseContent.Result = course;
                }

            }

            return res;
        }

        // PUT: api/Course/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Course/5
        public void Delete(int id)
        {
        }
    }
}
