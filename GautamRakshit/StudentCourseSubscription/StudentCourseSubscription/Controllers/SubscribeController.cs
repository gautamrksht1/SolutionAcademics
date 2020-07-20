using StudentCourseSubscription.DataAccessLayer;
using StudentCourseSubscription.DataAccessLayer.Interfaces;
using StudentCourseSubscription.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StudentCourseSubscription.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    public class SubscribeController : ApiController
    {
        private ISubscriptionService _subscriptionService;

        public SubscribeController() {
            _subscriptionService = Factory.CreateObj(typeof(SubscriptionService));
        }
        // GET: api/Subscribe/
        [HttpGet]
        public IHttpActionResult GetStudentsAndCourses()
        {
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));
            
            var list = _subscriptionService.GetStudentAndCourseList();
            if (list!=null)
            {                
                res.responseContent.Error = null;
                res.responseContent.Result = list;
            }
            return res;
        }
        [HttpGet]
        public IHttpActionResult GetSubscriptionDetail(int pageIndex,int PageSize)
        {
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));

            var list = _subscriptionService.SubscriptionList();
            if (list != null)
            {
                res.responseContent.Error = null;
                res.responseContent.Result = list;
            }
            return res;
        }

        // POST: api/Subscribe
        [HttpPost]
        public IHttpActionResult Post([FromBody]CourseSubscription cs)
        {
            CustomResponse res = Factory.CreateObj(typeof(CustomResponse));
           
            if (cs != null)
            {
                var result = _subscriptionService.SubscribeCourse(cs);
                if (result == 1)
                {
                    res.responseContent.Error = null;
                    res.responseContent.Result = cs;
                }
                else if(result==0) {
                    res.responseContent.Error = "Selcted student or course might have been deleted";
                    res.responseContent.Result = null;
                }
                else if(result == -1)
                {
                    res.responseContent.Error = "This course is already subscribe by the specified student";
                    res.responseContent.Result = null;
                }

            }

            return res;
        }

        // PUT: api/Subscribe/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Subscribe/5
        public void Delete(int id)
        {
        }
    }
}
