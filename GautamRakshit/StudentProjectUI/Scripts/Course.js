var currentCoursePage=1;
    var pageCourseSize=10;
    var totalCoursePages=0;
$( document ).ready(function() {
    $(".tabcontents .alert").hide();
    console.log( "ready!" );
    loadCourseData(currentCoursePage,pageCourseSize);
   var editID;
   $("#courseForm").submit(function(event){
    $(".tabcontents .alert").hide();
    event.preventDefault();      
    var form_data = new FormData(this); 
    var object={};
    form_data.forEach(function(value, key){
        object[key] = value;
    });
    if(editID){
        object.StudId=editID;
    }    
    var json = JSON.stringify(object);
//     console.log('form_data= ',json);
//   console.log('editID before submit', editID);
    $.ajax({
        url : 'http://localhost:62312/api/Course',
        type: editID?'put':'post',
        data : json,
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        processData:false,
        success:function(response){                
            $("#courseForm").trigger("reset");                
            
            if(response.Error===null){
                $(".tabcontents .success").show();
                //console.log('response.result',response.Result);
            }
            else{
                $(".tabcontents .error").show();
            }
            editID=null;
            loadCourseData(currentCoursePage,pageCourseSize);
        }
    });
}); 

$("#nextCourse").click(function(){
    if(currentCoursePage<totalCoursePages)
       {currentCoursePage=currentCoursePage+1;
        loadCourseData(currentCoursePage,pageCourseSize);
       }
     });
     $("#prevCourse").click(function(){
       if(currentCoursePage>1)
          {currentCoursePage=currentCoursePage-1;
            loadCourseData(currentCoursePage,pageCourseSize);
          }
        });
});
function loadCourseData(pageInx,pageSze){
    pageInx=pageInx===0?1:pageInx;
    pageSze=pageSze===0?10:pageSze;
    console.log('inside loadData');
    $.ajax({
        url : 'http://localhost:62312/api/Course/GetCourses',
        type: 'get',
        dataType: 'json',
        data : {pageIndex:pageInx,pageSize:pageSze},
        contentType: 'application/json',       
        success:callBackfunc
    });

};
function callBackfunc(response){
   
        console.log('respose',response.Result);            

        if(response && response.Error===null){
            if( response.Result.TotalPages>0)
            {
            var courses=response.Result.Courses;
            
            console.log('students',courses);
            $('#courseList tbody').empty()
            var tbody = $('#courseList tbody');

            props = ["CourseId", "CourseName", "CourseDetail","Actions"];
                $.each(courses, function(i, course) {
                var tr = $('<tr>');
                $.each(props, function(i, prop) {
                    if(prop!=="Actions"){
                        $('<td class='+'"'+prop+'"'+'>').html(course[prop]).appendTo(tr); 
                    }else{    
                        var action="<a class='edit' data-id="+"'"+course.StudId+"'"+"href='#'>Edit</a>|<a class='delete' data-id="+"'"+course.StudId+"'"+"href='#'>Delete</a>";
                        $('<td>').html(action).appendTo(tr); 
                    }
                });
                tbody.append(tr);
                });               
                totalCoursePages=response.Result.TotalPages;
                $("#currentCoursePageNo").val(currentCoursePage);
                $("#totalCoursePages").html(response.Result.TotalPages);
            }
        }        
    
};