function zipCode_oninput() {
    var zipCodeValue = document.getElementById("DeliveryAddressModel_ZipCode").value;
    if (zipCodeValue.length >= 3 && zipCodeValue.indexOf(" ; ") === -1) {
        var searchData = '{ "SearchType":"ZipCode", "SearchKeyValuePairs": { "DemogInfoCountryId":"236", "ZipCode":"' + zipCodeValue + '"} }';
        $.ajax({
            url: "/" + "GeneralUtility" + "/" + "SearchData",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: searchData,
            dataType: "json",
            success: function (responseData, textStatus, request) {
                searchResults = responseData;
                var html = "";
                for (i = 0; i < responseData.length; i++) {
                    html += "<option value='" + responseData[i].ZipCode + " ; " + responseData[i].CityName + " ; " + responseData[i].StateAbbrev + "' />";
                }
                document.getElementById("zipCodeDataList").innerHTML = html;
            },
            error: function (xhr, exception) {
                console.log("00099000", "zipCode_oninput", "ERROR???", exception, xhr);
            }
        });
    }
    else {
        if (zipCodeValue.length < 3) {
            document.getElementById("zipCodeDataList").innerHTML = "";
        }
    }
}
function zipCode_onblur() {
    var zipCodeValueOriginal = document.getElementById("DeliveryAddressModel_ZipCode").value;
    var indexOf = zipCodeValueOriginal.indexOf(" ; ");
    var zipCodeValue = zipCodeValueOriginal.substr(0, indexOf);
    zipCodeValueOriginal = zipCodeValueOriginal.substr(indexOf + 3);
    indexOf = zipCodeValueOriginal.indexOf(" ; ");
    var cityNameValue = zipCodeValueOriginal.substr(0, indexOf);
    var stateAbbrevValue = zipCodeValueOriginal.substr(indexOf + 3);
    for (i = 0; i < searchResults.length; i++) {
        if (searchResults[i].ZipCode == zipCodeValue && searchResults[i].CityName === cityNameValue && searchResults[i].StateAbbrev === stateAbbrevValue) {
            document.getElementById("DeliveryAddressModel_ZipCode").value = searchResults[i].ZipCode;
            document.getElementById("DeliveryAddressModel_CityName").value = searchResults[i].CityName;
            document.getElementById("DeliveryAddressModel_DemogInfoSubDivisionId").value = 363;//searchResults[i].DemogInfoSubDivisionId;
            break;
        }
    }
}
function cityName_oninput() {
    var cityNameValue = document.getElementById("DeliveryAddressModel_CityName").value;
    if (cityNameValue.length >= 3 && cityNameValue.indexOf(" ; ") === -1) {
        //var searchData = '{ "SearchType":"ZipCode", "SearchKeyValuePairs": { "DemogInfoCountryId":"' + document.getElementById("DeliveryAddressModel_DemogInfoCountryId").value + '", "ZipCode":"' + zipCodeValue + '"} }';
        var searchData = '{ "SearchType":"CityName", "SearchKeyValuePairs": { "DemogInfoCountryId":"236", "CityName":"' + cityNameValue + '"} }';
        $.ajax({
            url: "/" + "GeneralUtility" + "/" + "SearchData",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: searchData,
            dataType: "json",
            success: function (responseData, textStatus, request) {
                searchResults = responseData;
                var html = "";
                for (i = 0; i < responseData.length; i++) {
                    html += "<option value='" + responseData[i].CityName + " ; " + responseData[i].ZipCode + " ; " + responseData[i].StateAbbrev + "' />";
                }
                document.getElementById("cityNameDataList").innerHTML = html;
            },
            error: function (xhr, exception) {
                console.log("00099000", "cityName_oninput", "ERROR???", exception, xhr);
            }
        });
    }
    else {
        if (cityNameValue.length < 3) {
            document.getElementById("zipCodeDataList").innerHTML = "";
        }
    }
}
function cityName_onblur() {
    var cityNameValueOriginal = document.getElementById("DeliveryAddressModel_CityName").value;
    var indexOf = cityNameValueOriginal.indexOf(" ; ");
    var cityNameValue = cityNameValueOriginal.substr(0, indexOf);
    cityNameValueOriginal = cityNameValueOriginal.substr(indexOf + 3);
    indexOf = cityNameValueOriginal.indexOf(" ; ");
    var zipCodeValue = cityNameValueOriginal.substr(0, indexOf);
    var stateAbbrevValue = cityNameValueOriginal.substr(indexOf + 3);
    for (i = 0; i < searchResults.length; i++) {
        if (searchResults[i].CityName === cityNameValue && searchResults[i].ZipCode === zipCodeValue && searchResults[i].StateAbbrev === stateAbbrevValue) {
            document.getElementById("DeliveryAddressModel_CityName").value = searchResults[i].CityName;
            document.getElementById("DeliveryAddressModel_ZipCode").value = searchResults[i].ZipCode;
            document.getElementById("DeliveryAddressModel_DemogInfoSubDivisionId").value = 363;//searchResults[i].DemogInfoSubDivisionId;
            break;
        }
    }
}
