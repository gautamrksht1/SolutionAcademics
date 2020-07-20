using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCourseSubscription.Models
{
    public class StudentPaginatedModel
    {
        public Student[] Students { get; set; }
        public int TotalPages {get;set;}
    }
}