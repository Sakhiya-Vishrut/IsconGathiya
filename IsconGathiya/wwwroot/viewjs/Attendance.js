//$(document).ready(function () {
//    const $shiftInputs = $('#ShiftSwitchGroup input[type=radio][name=shiftType]');

//    $shiftInputs.on('change', function () {
//        const shiftValue = $(this).val();

//        switch (shiftValue) {
//            case "":
//                alert("Please select a valid shift.");
//                break;

//            default:
//                fnShowMainProgress();

//                $.ajax({
//                    url: '@Url.Action("GetEmployeesByShift", "Attendance")',
//                    type: 'GET',
//                    data: { shiftType: shiftValue },
//                    success: function (response) {
//                        $('#EmployeeTableContainer').html(response);
//                    },
//                    error: function (xhr, status, error) {
//                        console.error("Error loading data:", xhr.status, error);
//                        alert("Error loading employee data. Please try again.");
//                    },
//                    complete: function () {
//                        fnHideMainProgress();
//                    }
//                });
//                break;
//        }
//    });

//    $shiftInputs.filter(':checked').trigger('change');
//});

//document.getElementById("changeBranchForm").addEventListener("submit", function (e) {
//    var branchSelected = document.getElementById("BranchDropDown").value;
//    var selectedEmployees = document.querySelectorAll('input[name="SelectedEmployeeIds"]:checked');

//    if (!branchSelected) {
//        e.preventDefault();
//        Swal.fire({
//            icon: 'warning',
//            title: 'Branch Required',
//            text: 'Please select a branch before submitting.'
//        });
//        return false;
//    }

//    if (selectedEmployees.length === 0) {
//        e.preventDefault();
//        Swal.fire({
//            icon: 'warning',
//            title: 'No Employee Selected',
//            text: 'Please select at least one employee before submitting.'
//        });
//        return false;
//    }
//});