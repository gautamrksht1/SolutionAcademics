$( document ).ready(function() {
    $(".tabcontents .alert").hide();
    loadStudentsAndCoursesData();
    loadSubscriptionData();
    $("#courseSubscribeForm").submit(function(event){
        event.preventDefault(); 
        $(".tabcontents .alert").hide();
        var editID;     
        var form_data = new FormData(this); 
        var object={};
        form_data.forEach(function(value, key){
            object[key] = value;
        });
        if(editID){
            object.StudId=editID;
        }    
        var json = JSON.stringify(object);
    
        $.ajax({
            url : 'http://localhost:62312/api/Subscribe',
            type: editID?'put':'post',
            data : json,
            dataType: 'json',
            contentType: 'application/json',
            cache: false,
            processData:false,
            success:function(response){                
                $("#courseSubscribeForm").trigger("reset");                
                
                if(response.Error===null){
                    $(".tabcontents .success").html('<strong>Record Saved SuccessFully</strong>').show();
                    loadSubscriptionData();
                }
                else{
                    $(".tabcontents .dander").html('<strong>'+response.Error+'</strong>').show();
                }
                editID=null;
                
            },
            error:function(response){
                
                if(response && response.responseJSON){
                    var res=response.responseJSON.Error;
                    
                    $(".tabcontents .success").html('<strong>'+res+'</strong>').show();

                }
            }
        });
    }); 
});
function loadSubscriptionData(){

    $.ajax({
        url : 'http://localhost:62312/api/Subscribe/GetSubscriptionDetail',
        type: 'get',
        dataType: 'json',
        data : {pageIndex:1,pageSize:10},
        contentType: 'application/json',       
        success:callBackSubsData
    });
};
function callBackSubsData(response){
    if(response && response.Error===null){

        var subscriptions=response.Result;        
        
        $('#subscriptionList tbody').empty()
        var tbody = $('#subscriptionList tbody');

        props = ["RegId", "StudentName", "CourseName"];
            $.each(subscriptions, function(i, subscription) {
            var tr = $('<tr>');
            $.each(props, function(i, prop) {                
                    $('<td class='+'"'+prop+'"'+'>').html(subscription[prop]).appendTo(tr);
                
            });
            tbody.append(tr);
            });               
            // totalPages=response.Result.TotalPages;
            // $("#currentPageNo").val(currentPage);
            // $("#totalPages").html(response.Result.TotalPages);
        
    } 
};
function loadStudentsAndCoursesData(){    
    console.log('inside loadData');
    $.ajax({
        url : 'http://localhost:62312/api/Subscribe/GetStudentsAndCourses',
        type: 'get',
        dataType: 'json',        
        contentType: 'application/json',       
        success:callBackfuncSubs
    });

};

function callBackfuncSubs(response){
   
    console.log('respose',response.Result);            

    if(response && response.Error===null){
       
        var courses=response.Result.Courses;
        var students=response.Result.Students
        console.log('students',courses);        
        
        var option = '';
        for (var i=0;i<students.length;i++){
           option += '<option value="'+ students[i].StudId + '">' + students[i].Name + '</option>';
        }
        $("#StudId").append(option);

        option = '';
        for (var i=0;i<courses.length;i++){
           option += '<option value="'+ courses[i].CourseId + '">' + courses[i].Name + '</option>';
        }
        $("#CourseId").append(option);        
    }        

};
