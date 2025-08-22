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

//document.querySelectorAll('[

//        data - bs - toggle="tab"]').forEach(tab => {
//                tab.addEventListener('shown.bs.tab', function (e) {
//    const targetPane = document.querySelector(e.target.getAttribute('data-bs-target'));
//    const cards = targetPane.querySelectorAll('.info-card');
//    cards.forEach((card, index) => {
//        card.style.opacity = '0';
//        card.style.transform = 'translateY(20px)';
//        setTimeout(() => {
//            card.style.transition = 'all 0.4s ease';
//            card.style.opacity = '1';
//            card.style.transform = 'translateY(0)';
//        }, index * 100);
//    });
//});
//            });

//// Initial animation for overview tab
//document.addEventListener('DOMContentLoaded', () => {
//    const firstTabCards = document.querySelectorAll('#overview .info-card');
//    firstTabCards.forEach((card, index) => {
//        card.style.opacity = '0';
//        card.style.transform = 'translateY(20px)';
//        setTimeout(() => {
//            card.style.transition = 'all 0.4s ease';
//            card.style.opacity = '1';
//            card.style.transform = 'translateY(0)';
//        }, index * 100 + 300);
//    });
//});