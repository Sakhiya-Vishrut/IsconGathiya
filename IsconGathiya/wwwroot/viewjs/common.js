function
    fnSweetMessage(msgType, title, message, confirmButtonText = "Ok", showCancelButton = false, confirmButtonColor = '#495f8f') {
    return swal.fire({
        title: title,
        text: message,
        icon: msgType,
        buttons: {
            cancel: {
                text: "Cancel",
                visible: showCancelButton,
                className: 'btn btn-secondary',
            },
            confirm: {
                text: confirmButtonText,
                className: 'btn btn-primary',
                closeModal: true
            }
        },
        dangerMode: false,
        closeOnClickOutside: false, // Disable clicking outside the popup
        confirmButtonColor: confirmButtonColor
    });
}
function countWords(text) {
    const words = text.trim().split(/\s+/); // Split by whitespace and filter empty strings
    return words.length;
}


function confirmAndDeletePopup(onConfirm) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#25c00e',
        cancelButtonColor: '#e44141',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            onConfirm();
        }
    });
}

function confirmAndCancelPopup(onConfirm) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#25c00e',
        cancelButtonColor: '#e44141',
        confirmButtonText: 'Yes, Cancle it!',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            onConfirm(); // Call the provided callback if user confirms
        }
    });
}

function setMinValue(startId, endId) {
    let startDate = document.getElementById(startId);
    let endDate = document.getElementById(endId);
    endDate.min = startDate.value;
}
function fnShowMainProgress() {
    $(".main-loader").removeClass("loader-hide");
    $(".main-loader").addClass("loader-show");
}

function fnHideMainProgress() {
    $(".main-loader").removeClass("loader-show");
    $(".main-loader").addClass("loader-hide");
}
