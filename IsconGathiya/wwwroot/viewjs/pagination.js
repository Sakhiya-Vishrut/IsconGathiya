/*const { debug } = require("util");*/
function GridPagination(gridName, pageNo, urlCallBackMethodName, columnName) {
    $("#" + gridName + "PageNo").val(pageNo);
    var sortDirection = $("#" + gridName + "SortDirection").val();
    var previousSort = $("#" + gridName + "CurrentSort").val();
    if (!IsNullOrEmptyString(columnName)) {
        if (columnName == previousSort) {
            if (sortDirection == "asc") {
                $("#" + gridName + "SortDirection").val('desc');

            }
            else {
                $("#" + gridName + "SortDirection").val('asc');
            }
        }
        else {
            $("#" + gridName + "SortDirection").val('desc');
            sortDirection = 'desc';
        }
    } else {
        columnName = previousSort;
        sortDirection = 'desc';
    }
    var obj = $(".gridFilterElements");
    var result = [];
    console.log(obj);
    if (obj.length > 0) {
        for (var i = 0; i < obj.length; i++) {
            var objKeyValue = {};
            var ele = obj[i];
            objKeyValue.Text = ele.id;
            if (ele.value == 'on') {
                objKeyValue.Value = ele.checked ? "true" : "false";
            } else {
                objKeyValue.Value = ele.value;
            }
            console.log(ele);
            result.push(objKeyValue);
        }
    }
    //local storage start(For Company And Depot)
    const selections = JSON.parse(sessionStorage.getItem("SelectedDropdowns")) || {};
    const CompanyID = selections["SelectedCompanyId"];
    const DepotID = selections["SelectedDepotId"];

    if (CompanyID && DepotID) {
        result.push({ Text: "CompanyID", Value: CompanyID });
        result.push({ Text: "DepotID", Value: DepotID });
    }

    //local storage end

    var keyToFind = "isclient";
    var keyToFind1 = "isrejected";

    var foundObjs = result.filter(function (item) {
        return item.Text === keyToFind;
    });
    var foundObjs1 = result.filter(function (item) {
        return item.Text === keyToFind1;
    });

    if (foundObjs.length > 0) {
        console.log("Found objects:", foundObjs);
    } else {
        console.log("No objects found with the specified key.");
    }
    console.log(result);
    var pageIndex = IsValueUndefinedOrNull($("#" + gridName + "PageNo").val()) ? jsConfigDefaultPageNo : $("#" + gridName + "PageNo").val();
    var pageSize = IsValueUndefinedOrNull($("#" + gridName + "PageSize").val()) ? jsConfigDefaultPageSize : $("#" + gridName + "PageSize").val();
    debugger;
    fnShowMainProgress();
    //fnShowMainProgress();
    $.ajax({
        type: 'GET',
        url: urlCallBackMethodName,
        data: { pageIndex: pageIndex, pageSize: pageSize, filterObj: JSON.stringify(result), columnName: columnName, sortDirection: sortDirection },
        async: true,
        contenttype: "application/json; charset=utf-8",
        datatype: 'json',
        success: function (data, textStatus, xhr) {
            fnHideMainProgress();
            // Check if the response is a file
            var disposition = xhr.getResponseHeader('Content-Disposition');
            if (disposition && disposition.indexOf('attachment') !== -1) {
                // This means the response is a file
                var filename = "download.xlsx"; // You can also extract the filename from the header
                var blob = new Blob([data], { type: xhr.getResponseHeader('Content-Type') });
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = filename;
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            } else {
                // Handle JSON response
                $("#" + gridName).html(data);
                $("#" + gridName + "PageSize").val(pageSize);
                $("#" + gridName + "SortDirection").val(sortDirection);
                $("#" + gridName + "CurrentSort").val(columnName);

                var ele = $('#' + columnName);
                if (ele.length) { // Use length check for jQuery
                    var upperArrow = '<i id="up-caret" class="fa text-dark fa-caret-up top-up"></i>';
                    var downArrow = '<i id="down-caret" class="fa text-dark fa-caret-down"></i>';

                    if (sortDirection === "asc") {
                        $('#' + gridName + ' #' + columnName + ' .fa-caret-up').replaceWith(upperArrow);
                    } else {
                        $('#' + gridName + ' #' + columnName + ' .fa-caret-down').replaceWith(downArrow);
                    }
                }
            }

        },
        error: function (req, status, error) {
            fnHideMainProgress();
            fnShowError(error);
        }
    });
}

function GridExcelPagination(gridName, urlCallBackMethodName, columnName) {
    const selectedDropdowns = JSON.parse(sessionStorage.getItem("SelectedDropdowns") || "{}");

    // Extract companyId and depotId safely
    const companyId = selectedDropdowns.SelectedCompanyId || "";
    const depotId = selectedDropdowns.SelectedDepotId || "";

    var sortDirection = $("#" + gridName + "SortDirection").val();
    columnName = $("#" + gridName + "CurrentSort").val();

    var obj = $(".gridFilterElements");
    var result = [];

    if (obj.length > 0) {
        for (var i = 0; i < obj.length; i++) {
            var objKeyValue = {};
            var ele = obj[i];

            objKeyValue.Text = ele.id;

            if (ele.value === 'on') {
                objKeyValue.Value = ele.checked ? "true" : "false";
            } else {
                objKeyValue.Value = ele.value;
            }

            result.push(objKeyValue);
        }
    }

    $.ajax({
        type: 'GET',
        url: urlCallBackMethodName,
        data: {
            filterObj: JSON.stringify(result),
            columnName: columnName,
            sortDirection: sortDirection,
            companyId: companyId,
            depotId: depotId
        },
        xhrFields: {
            responseType: 'blob' // Expecting binary data (Excel file)
        },
        async: true,
        success: function (data, textStatus, xhr) {
            var filename = "Ventas_LeadManagement.xlsx";
            var disposition = xhr.getResponseHeader('Content-Disposition');

            if (disposition) {
                var filenameRegex = /filename[^;=\n]*=(['"]?)([^;\n]+)\1/;
                var matches = filenameRegex.exec(disposition);

                if (matches != null && matches[2]) {
                    filename = decodeURIComponent(matches[2].replace(/UTF-8''/g, ''));
                }
            }

            var blob = new Blob([data], { type: xhr.getResponseHeader('Content-Type') });
            var link = document.createElement('a');

            link.href = window.URL.createObjectURL(blob);
            link.download = filename;

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        },
        error: function (req, status, error) {
            fnShowError(error);
        }
    });
}



function ToggleSortDirectionOfGridColumn(gridName) {
    if (!IsUndefinedOrNull($("#" + gridName + "SortDirection"))) {
        if ($("#" + gridName + "SortDirection").val() == 'asc') {
            $("#" + gridName + "SortDirection").val('desc');
        }
        else {
            $("#" + gridName + "SortDirection").val('asc');
        }
    }
}

function IsUndefinedOrNull(obj) {
    if (obj == undefined || obj == null)
        return true;

    return false;
}

function IsValueUndefinedOrNull(val) {
    if (val == undefined || val == null)
        return true;

    return false;
}

function IsNullOrEmptyString(str) {
    if (str == undefined || str == "" || str == null || str.trim() == "")
        return true;

    return false;
}
//function Clearfilter() {
//    debugger
//    var obj = $(".gridFilterElements");
//    if (obj.length > 0) {
//        for (var i = 0; i < obj.length; i++) {
//            var objKeyValue = {};
//            var ele = obj[i];
//            objKeyValue.Text = ele.id;
//            ele.value = '';
//        }
//    }
//}
function Clearfilter() {
    var obj = $(".gridFilterElements");

    if (obj.length > 0) {
        for (var i = 0; i < obj.length; i++) {
            var ele = obj[i];

            // Check if the element is a Select2 dropdown
            if ($(ele).hasClass("select2-example") || $(ele).hasClass("select2-state") || $(ele).hasClass("select2-city")) {
                $(ele).val(null).trigger('change'); // Clear Select2 dropdown
            } else {
                ele.value = ''; // Clear other input elements
            }
        }
    }
}
function updatePaginationControls(totalPages, currentPage) {
    var pagination = $('.pagination');
    pagination.empty();

    if (totalPages > 1) {
        var prevDisabled = currentPage <= 1 ? 'disabled' : '';
        var nextDisabled = currentPage >= totalPages ? 'disabled' : '';

        pagination.append(`<li class="page-item ${prevDisabled}"><a class="page-link" href="#" onclick="GridPagination('yourGridName', ${currentPage - 1}, 'urlCallBackMethodName')">Previous</a></li>`);

        for (var i = 1; i <= totalPages; i++) {
            pagination.append(`<li class="page-item ${i === currentPage ? 'active' : ''}"><a class="page-link" href="#" onclick="GridPagination('yourGridName', ${i}, 'urlCallBackMethodName')">${i}</a></li>`);
        }

        pagination.append(`<li class="page-item ${nextDisabled}"><a class="page-link" href="#" onclick="GridPagination('yourGridName', ${currentPage + 1}, 'urlCallBackMethodName')">Next</a></li>`);
    }
}

function DownloadFilteredPdf(gridName, urlCallBackMethodName) {
    
    fnShowMainProgress();

    // Collect filter values
    var filters = [];
    $(".gridFilterElements").each(function () {
        var filterObj = {};
        filterObj.Text = $(this).attr("id"); // Use unique identifiers for filters
        filterObj.Value = $(this).is(":checkbox") ? ($(this).is(":checked") ? "true" : "false") : $(this).val();
        filters.push(filterObj);
    });

    // AJAX request to download filtered PDF
    $.ajax({
        type: 'GET',
        url: urlCallBackMethodName,
        data: { filterObj: JSON.stringify(filters) },
        xhrFields: {
            responseType: 'blob' // Ensures binary data is handled correctly
        },
        success: function (data, textStatus, xhr) {
            fnHideMainProgress();

            // Extract filename from header or use default
            var filename = "Digistron.pdf";
            var disposition = xhr.getResponseHeader('Content-Disposition');
            if (disposition) {
                var filenameRegex = /filename[^;=\n]*=(['"]?)([^;\n]+)\1/;
                var matches = filenameRegex.exec(disposition);
                if (matches != null && matches[2]) {
                    filename = decodeURIComponent(matches[2].replace(/UTF-8''/g, ''));
                }
            }

            // Download the file
            var blob = new Blob([data], { type: 'application/pdf' });
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = filename;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        },
        error: function (xhr, status, error) {
            fnHideMainProgress();
        }
    });
}

function ClearPaymentfilter() {
    $(".gridFilterElements").each(function () {
        // Skip clearing plumberid
        if (this.id === "plumberid") return;

        if ($(this).hasClass("select2-example")) {
            $(this).val(null).trigger('change'); // Clear Select2 dropdown
        } else {
            $(this).val(''); // Clear text or other input
        }
    });
}
function ClearComplainfilter() {
    var obj = $(".gridFilterElements");

    if (obj.length > 0) {
        for (var i = 0; i < obj.length; i++) {
            var ele = $(obj[i]);

          
            if (ele.attr("id") === "complainType") {
                continue; 
            }

            
            if (ele.hasClass("select2-example") || ele.hasClass("select2-state") || ele.hasClass("select2-city") || ele.hasClass("select2")) {
                ele.val(null).trigger('change'); 
            }
           
            else if (ele.is(":checkbox")) {
                ele.prop("checked", false);
            }
           
            else {
                ele.val('');
            }
        }
    }

}
