function loadStatesByCountry(countryId, selectedStateId = null, selectedCityId = null) {
    const $stateDropDown = $('#stateDropDown');
    const $cityDropDown = $('#cityDropDown');

    $stateDropDown.empty().append('<option value="">Loading...</option>');
    $cityDropDown.empty().append('<option value="">-- Select City --</option>');

    if (!countryId) {
        $stateDropDown.empty().append('<option value="">-- Select State --</option>');
        return;
    }

    $.getJSON('/General/GetStateByCountry', { countryId })
        .done(function (response) {
            if (response.statusCode === 200) {
                const states = response.returnData;

                $stateDropDown.empty().append('<option value="">-- Select State --</option>');

                $.each(states, function (i, state) {
                    $stateDropDown.append(`<option value="${state.value}">${state.text}</option>`);
                });

                if (selectedStateId) {
                    $stateDropDown.val(selectedStateId);
                    loadCitiesByState(selectedStateId, selectedCityId);
                }
            } else {
                alert(response.message || 'Failed to load states.');
                $stateDropDown.empty().append('<option value="">-- Select State --</option>');
            }
        })
        .fail(function () {
            alert('Error loading states.');
            $stateDropDown.empty().append('<option value="">-- Select State --</option>');
        });
}

function loadCitiesByState(stateId, selectedCityId = null) {
    const $cityDropDown = $('#cityDropDown');
    $cityDropDown.empty().append('<option value="">Loading...</option>');

    if (!stateId) {
        $cityDropDown.empty().append('<option value="">-- Select City --</option>');
        return;
    }

    $.getJSON('/General/GetCitiesByState', { stateId })
        .done(function (response) {
            if (response.statusCode === 200) {
                const cities = response.returnData;

                $cityDropDown.empty().append('<option value="">-- Select City --</option>');

                $.each(cities, function (i, city) {
                    $cityDropDown.append(`<option value="${city.value}">${city.text}</option>`);
                });

                if (selectedCityId) {
                    $cityDropDown.val(selectedCityId);
                }
            } else {
                alert(response.message || 'Failed to load cities.');
                $cityDropDown.empty().append('<option value="">-- Select City --</option>');
            }
        })
        .fail(function () {
            alert('Error loading cities.');
            $cityDropDown.empty().append('<option value="">-- Select City --</option>');
        });
}

$(document).ready(function () {
    const selectedCountryId = $('#countryDropDown').val() || 101;
    const selectedStateId = $('#StateId').val();
    const selectedCityId = $('#CityId').val();

    if (selectedCountryId) {
        loadStatesByCountry(selectedCountryId, selectedStateId, selectedCityId);
    }
});
