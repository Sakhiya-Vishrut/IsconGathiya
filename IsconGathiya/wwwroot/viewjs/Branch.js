function fnBranchDeletemodal(encodedBranchlId) {
    debugger;
    confirmAndDeletePopup(function () {
        $.ajax({
            type: 'POST',
            url: getBranchDelete,
            data: { encodedBranchlId: encodedBranchlId },
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

//function fnBranchDeletemodal(encodedBranchlId) {

//    // Show the confirmation popup
//    confirmAndDeletePopup(function () {
//        $.ajax({
//            type: 'POST',
//            url: getBranchDelete,
//            data: { encodedBranchlId: encodedBranchlId },
//            success: function (response) {
//                if (response.success) {
//                    Swal.fire('Deleted!', 'The Branch has been deleted.', 'success').then(function () {
//                        location.reload(); // Reload the page on successful deletion
//                    });
//                }
//            },
//            error: function () {
//                Swal.fire('Error!', 'There was an error processing your request.', 'error');
//            }
//        });
//    });
//}