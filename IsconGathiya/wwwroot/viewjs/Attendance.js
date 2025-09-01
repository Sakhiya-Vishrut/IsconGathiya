function initializeToggleFeedback() {
    $('.toggle-active').each(function () {
        $(this).off('change').on('change', function () {
            const feedbackRow = $(this).closest('tr').next('.feedback-row');
            if ($(this).is(':checked')) {
                feedbackRow.removeClass('d-none');
            } else {
                feedbackRow.addClass('d-none');
            }
        });
    });
}

$(document).ready(function () {
    initializeToggleFeedback();
});
function setStatus(employeeId) {
    var isChecked = $('#toggle-' + employeeId).is(':checked');
    var status = isChecked ? 1 : 2;
    $('#emp-' + employeeId).val(status);
}

