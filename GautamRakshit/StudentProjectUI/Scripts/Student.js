var currentPage=1;
    var pageSize=10;
    var totalPages=0;
$( document ).ready(function() {
    console.log( "ready!" );
    $(".tabcontents .alert").hide();
    loadData(currentPage,pageSize);
    var editID;
    var that=this;
    $("#studentForm").submit(function(event){
        event.preventDefault(); 
             
        var form_data = new FormData(this); 
        var object={};
        form_data.forEach(function(value, key){
            object[key] = value;
        });
       if(isValidModel()){
        $(".tabcontents .error").html('<strong>Model Invalid</strong>').show();

       }
        if(editID){
            object.StudId=editID;
        }
        var dt=object.dob;
        object.dob=new Date(dt);

        var json = JSON.stringify(object);
    //     console.log('form_data= ',json);
    //   console.log('editID before submit', editID);
        $.ajax({
            url : 'http://localhost:62312/api/Student',
            type: editID?'put':'post',
            data : json,
            dataType: 'json',
            contentType: 'application/json',
            cache: false,
            processData:false,
            success:function(response){                
                $("#studentForm").trigger("reset");                
              
                if(response.Error===null){
                    $(".tabcontents .success").show();
                    //console.log('response.result',response.Result);
                }
                else{
                    $(".tabcontents .error").show();
                }
                editID=null;
            }
        });
    });    
    $(document ).on("click","#studList a.edit",function() {       
        editID = $(this).attr('data-id');        
        let tr = $(this).closest('tr');        
        console.log('editID=',editID);
        getStudentForEdit(editID);        
      });
      $(document ).on("click","#studList a.delete",function() {       
       var deleteID = $(this).attr('data-id');        
        let tr = $(this).closest('tr');        
        console.log('deleteID=',deleteID);
        deleteStudent(deleteID);        
      });

      $("#next").click(function(){
     if(currentPage<totalPages)
        {currentPage=currentPage+1;
        loadData(currentPage,pageSize);
        }
      });
      $("#prev").click(function(){
        if(currentPage>1)
           {currentPage=currentPage-1;
           loadData(currentPage,pageSize);
           }
         });

});
function isValid(obj){
 if(obj && obj.firstName && obj.lastName && obj.dob && obj.contactNo){
return true;
 }else
 {return 
    false;
}
};
function deleteStudent(deleteID){
    var studId={};
    studId.Id=deleteID;
    var jsonPayload=JSON.stringify(studId);
    $.ajax({
        url : 'http://localhost:62312/api/Student/DeleteStudent',
        type: 'delete',       
        data : jsonPayload,
        dataType: 'json',
        contentType: 'application/json',       
        success:function(response){
            console.log('respose',response);            
            if(response.Error===null){
                $(".tabcontents .success").html('<strong>'+response.Result+'</strong>').show();
                //console.log('response.result',response.Result);
            }
            else{
                $(".tabcontents .error").html('<strong>'+response.Error+'</strong>').show();
            }
            
        }
    });
    
};
function getStudentForEdit(studId){
    
    
    $.ajax({
        url : 'http://localhost:62312/api/Student/GetStudent',
        type: 'get',
        dataType: 'json',
        data : {studId:studId},
        contentType: 'application/json',       
        success:function(response){
            console.log('respose',response);            
            
            if(response && response.Error===null){
                var student=response.Result;
                
                   var dt=new Date(student.DOB);                  
                   var month = dt.getMonth() + 1;
                    var day = dt.getDate();
                    var year = dt.getFullYear();
                    var shortStartDate = year + "-" + month + "-" + day;
                    student.DOB=shortStartDate;
        
                    returnResult= student;
                    $('input[name="firstName"]').val(student.FirstName);
                    $('input[name="lastName"]').val(student.LastName);
                    $('input[name="dob"]').val(student.DOB);
                    $('input[name="contactNo"]').val(student.ContactNo);
                
                   
            } 
            
        }
    });
    

};
function loadData(pageInx,pageSze){
    pageInx=pageInx===0?1:pageInx;
    pageSze=pageSze===0?10:pageSze;
    console.log('inside loadData');
    $.ajax({
        url : 'http://localhost:62312/api/Student/GetStudents',
        type: 'get',
        dataType: 'json',
        data : {pageIndex:pageInx,pageSize:pageSze},
        contentType: 'application/json',       
        success:callBack
    });

};
function callBack(response){
   
        console.log('respose',response.Result);            

        if(response && response.Error===null){
            if( response.Result.TotalPages>0)
            {
            var students=response.Result.Students;
            students.forEach(element => {
               var dt=new Date(element.DOB);                  
               var month = dt.getMonth() + 1;
                var day = dt.getDate();
                var year = dt.getFullYear();
                var shortStartDate = year + "-" + month + "-" + day;
                element.DOB=shortStartDate;
            });
            console.log('students',students);
            $('#studList tbody').empty()
            var tbody = $('#studList tbody');

            props = ["StudId", "FirstName", "LastName", "DOB", "ContactNo","Actions"];
                $.each(students, function(i, student) {
                var tr = $('<tr>');
                $.each(props, function(i, prop) {
                    if(prop!=="Actions"){
                        $('<td class='+'"'+prop+'"'+'>').html(student[prop]).appendTo(tr); 
                    }else{    
                        var action="<a class='edit' data-id="+"'"+student.StudId+"'"+"href='#'>Edit</a>|<a class='delete' data-id="+"'"+student.StudId+"'"+"href='#'>Delete</a>";
                        $('<td>').html(action).appendTo(tr); 
                    }
                });
                tbody.append(tr);
                });               
                totalPages=response.Result.TotalPages;
                $("#currentPageNo").val(currentPage);
                $("#totalPages").html(response.Result.TotalPages);
            }
        }        
    
};