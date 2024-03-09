﻿window.ShowToastr = (type, message) => {
    if (type == "success") {
        toastr.success(message, "Operation Successful", {timeOut: 10000});
    }
    if (type == "error") {
        toastr.error(message, "Operation Failed", {timeOut: 10000});
    }

}

window.ShowSwal = (type, message) => {
    if (type == "success") {
        Swal.fire({
            title: "Success Notification",
            message,
            icon: "success"
        });
    }
    if (type == "error") {
        Swal.fire({
            title: "Error Notification",
            message,
            icon: "error"
        });
    }

}