using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace StudentCourseSubscription.Models
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
       

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message;
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            }
            //We can log this exceptionMessage  to the file or database.  
            var customResponse = (CustomResponse)Factory.CreateObj(typeof(CustomResponse));
            customResponse.responseContent.Error = "An unhandled exception was thrown by service details as follows:"+ exceptionMessage;
            customResponse.responseContent.Result = null;
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {                
                Content = new StringContent(JsonConvert.SerializeObject(customResponse.responseContent)),
                ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
            };
            actionExecutedContext.Response = response;
        }
    }
}