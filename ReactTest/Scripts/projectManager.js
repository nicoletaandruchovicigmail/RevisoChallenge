var addProject = function (event) {
    event.preventDefault();
    var project = {
       /* $type: 'RevisoChallenge.Models.ProjectViewModel, RevisoChallenge',*/
        Name: $("#name").val(),
        Description: $("#description").val(),
        Start: $("#startdate").val(),
        End: $("#enddate").val(),
        Cost: $("#cost").val() 
    };
    $.ajax(
        {
            type: "POST",
            url: 'http://localhost:53438/api/projects/',
            data: JSON.stringify(project),
            contentType: "application/json",
            success: function (data) {
                if (data.status === "success")
                    alert("success");
                else if (data.status === "error")
                   alert(data.error_message);                
            },
            error: function (error) {
                alert(error);                
            }
        }
    );
};

$(document).ready(function () {
    $("button#btnAddProject").on("click", addProject);
});
