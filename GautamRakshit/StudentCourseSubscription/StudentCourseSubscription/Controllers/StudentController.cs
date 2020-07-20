
using StudentCourseSubscription.DataAccessLayer;
using StudentCourseSubscription.DataAccessLayer.Interfaces;
using StudentCourseSubscription.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StudentCourseSubscription.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    public class StudentController : ApiController
    {
        private IStudentService _studService;
        // GET api/<controller>
        StudentController() {
            _studService = Factory.CreateObj(typeof(StudentService));
        }
        
        [HttpGet]
        public IHttpActionResult GetStudents(int pageIndex,int pageSize)
        {            
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));
            int totalPages;
            var students= _studService.GetStudents(pageIndex,pageSize,out totalPages);
            if (students.Length>0)
            {
                StudentPaginatedModel StudentPaginated = new StudentPaginatedModel();
                StudentPaginated.Students = students;
                StudentPaginated.TotalPages = totalPages;
                res.responseContent.Error = null;
                res.responseContent.Result = StudentPaginated;
            }            
            return res;
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetStudent(int studId)
        {
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));
            var student = _studService.GetStudent(studId);
            if (student != null)
            {
                res.responseContent.Error = null;
                res.responseContent.Result = student;
            }
            else {
                res.responseContent.Error = "Requested Data not found.";
                res.responseContent.Result = null;
            }
            return res;
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]StudentVM stud)
        {
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));
            var s = stud;
            if (stud != null)
            {
               var result= _studService.CreateStudent(stud);
                if (result==1) {
                    res.responseContent.Error = null;
                    res.responseContent.Result = stud;
                }
                
            }            
            
            return res;
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put([FromBody]Student stud)
        {
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));
            
            if (stud != null)
            {
                var result = _studService.UpdateStudent(stud);
                if (result == 1)
                {
                    res.responseContent.Error = null;
                    res.responseContent.Result = "Record Updated SuccessFully";
                }

            }

            return res;
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteStudent([FromBody] StudId studId)
        {
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));
          
            if (studId.Id > 0)
            {
                var result = _studService.DeleteStudent(studId.Id);
                if (result == 1)
                {
                    res.responseContent.Error = null;
                    res.responseContent.Result = "Record Deleted SuccessFully";
                }

            }
            return res;
        }
        [HttpGet]
        public IHttpActionResult GetException()
        {

            throw new System.Exception("Test Exception");
        }
    }
}