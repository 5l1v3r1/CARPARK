
$(document).ready(function () {
    $.ajax({
        url: '/Dashboard/OpenAPRDataReader',
        type: "POST",
        success: function (data) {
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                  
                }
            }

        }
    });


});



