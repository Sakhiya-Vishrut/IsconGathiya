//function initializeToggleFeedback() {
//    $('.toggle-active').each(function () {
//        $(this).off('change').on('change', function () {
//            const feedbackRow = $(this).closest('tr').next('.feedback-row');
//            if ($(this).is(':checked')) {
//                feedbackRow.removeClass('d-none');
//            } else {
//                feedbackRow.addClass('d-none');
//            }
//        });
//    });
//}

//$(document).ready(function () {
//    initializeToggleFeedback();
//});
//function setStatus(employeeId) {
//    var isChecked = $('#toggle-' + employeeId).is(':checked');
//    var status = isChecked ? 1 : 2;
//    $('#emp-' + employeeId).val(status);
//}

function fnAbsentViewModal(encodeAbsentid) {
    // Fetch data for modal content
    debugger;
    $.ajax({
        type: 'GET',
        url: getAbsentViewModel,
        data: { encodeAbsentid: encodeAbsentid },
        success: function (data) {
            $("#AbsentViewModelBody").empty();
            $("#AbsentViewModelBody").html(data);
            $('#AbsentViewModel').modal("show");
        },
        error: function () {
        },
    });
}