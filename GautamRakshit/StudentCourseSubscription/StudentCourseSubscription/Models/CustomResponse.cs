using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace StudentCourseSubscription.Models
{
     public class CustomResponse: IHttpActionResult
        {
        public Content responseContent;

            public CustomResponse(Content content) {
            responseContent = content;
            }
       
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken token)
        {         
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseContent)),
                StatusCode= (string.IsNullOrWhiteSpace(responseContent.Error)) ? HttpStatusCode.OK : HttpStatusCode.InternalServerError
            };
            return Task.FromResult(response);
           
        }
       
    }

    public class Content
    {
        public string Error { get; set; }
        public object Result { get; set; }        

    }
}