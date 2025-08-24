function fnEmployeeDeletemodal(encodeEmployeeId) {
    debugger;
    confirmAndDeletePopup(function () {
        $.ajax({
            type: 'POST',
            url: getEmployeeDelete,
            data: { encodeEmployeeId: encodeEmployeeId },
            success: function (response) {
                if (response.success) {
                    Swal.fire('Deleted!', 'The Branch has been deleted.', 'success').then(function () {
                        location.reload();
                    });
                }
            },
            error: function () {
                Swal.fire('Error!', 'There was an error processing your request.', 'error');
            }
        });
    });
}

function fnEmployeeViewModal(encodeEmployeeId) {
    // Fetch data for modal content
    $.ajax({
        type: 'GET',
        url: getEmployeeViewModel,
        data: { encodeEmployeeId: encodeEmployeeId },
        success: function (data) {
            $("#EmployeeViewModelBody").empty();
            $("#EmployeeViewModelBody").html(data);
            $('#EmployeeViewModal').modal("show");
        },
        error: function () {
        },
    });
}
