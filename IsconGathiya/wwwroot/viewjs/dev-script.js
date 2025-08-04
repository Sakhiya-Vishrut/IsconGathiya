$(document).ready(function () {
    // sweet alert error popup
    if (sweetAlertErrorPopup != undefined && sweetAlertErrorPopup != "") {
        fnSweetMessage('error', 'Error', sweetAlertErrorPopup);
    }

    // sweet alert success popup
    if (sweetAlertSuccessPopup != undefined && sweetAlertSuccessPopup != "") {
        fnSweetMessage('success', 'Success', sweetAlertSuccessPopup);
    }

    // sweet alert warning popup
    if (sweetAlertWarningPopup != undefined && sweetAlertWarningPopup != "") {
        fnSweetMessage('warning', 'Warning', sweetAlertWarningPopup);
    }
});

function encodeIntToBase64(intValue) {
	const byteArray = new Uint8Array(4);
	const dataView = new DataView(byteArray.buffer);
	dataView.setInt32(0, intValue, true);
	return btoa(String.fromCharCode.apply(null, byteArray));
}

function GetDataWithCompanyAndDepot(Controller) {

	const selections = JSON.parse(sessionStorage.getItem("SelectedDropdowns")) || {};

	const CompanyID = selections["SelectedCompanyId"];
	const DepotID = selections["SelectedDepotId"];

	if (!CompanyID || !DepotID) {
		fnSweetMessage("warning",'Not Selected',"Please Select Company And Depot First");
		return;
	}

	const EncodedCompanyID = encodeIntToBase64(parseInt(CompanyID));
	const EncodedDepotID = encodeIntToBase64(parseInt(DepotID));

	var url = `/${Controller}?LocalStorageCompanyID=${EncodedCompanyID}&LocalStorageDepotID=${EncodedDepotID}`;
	window.location.href = url;

}
$(document).ready(function () {
    $('.select2-state, .select2-city').select2({
        width: 'resolve',
        placeholder: function () {
            return $(this).data('placeholder');
        },
        allowClear: true
    });
});