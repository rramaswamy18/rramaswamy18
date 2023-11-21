function demogInfoCountry_onchage(demogInfoCountryElement) {
    if (demogInfoCountryElement.value === "") {
        return false;
    }
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/GeneralUtility/DemongInfoStates/" + demogInfoCountryElement.value;
    $.ajax({
        url: url,
        type: "GET",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        dataType: "json",
        //data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                document.getElementById("DeliveryAddressModel_DemogInfoSubDivisionId").innerHTML = responseData.htmlString;
            }
            else {
                alert("Error while loading states");
            }
            //console.log("00001000", "checkoutLoginUser_onclick success", responseData.processMessage);
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            alert("Error while loading states");
            //document.getElementById("formLoginUserProfData").innerHTML = xhr.responseText;
            //console.log("00099000", "checkoutLoginUser_onclick", "ERROR???", exception, xhr);
        }
    });
    return false;
}
function cityName_oninput() {
    var i;
    var cityNameData = document.getElementById("DeliveryAddressModel_CityName").value;
    if (cityNameData.length >= 3) {
        if (cityNameData.indexOf(" ; ") > -1) {
            var indexOf = cityNameData.indexOf(" ; ");
            var cityNameValue = cityNameData.substr(0, indexOf);
            cityNameData = cityNameData.substr(indexOf + 3);
            indexOf = cityNameData.indexOf(" ; ");
            var zipCodeValue = cityNameData.substr(0, indexOf);
            var subDivisionDesc = cityNameData.substr(indexOf + 3);
            document.getElementById("DeliveryAddressModel_CityName").value = cityNameValue;
            document.getElementById("DeliveryAddressModel_ZipCode").value = zipCodeValue;
            var selectObject = document.getElementById("DeliveryAddressModel_DemogInfoSubDivisionId");
            for (i = 0; i < selectObject.options.length; i++) {
                if (selectObject.options[i].text == subDivisionDesc) {
                    selectObject.selectedIndex = i;
                    break;
                }
            }
        }
        else {
            var searchData = '{ "SearchType":"CityName", "SearchKeyValuePairs": { "DemogInfoCountryId":"' + document.getElementById("DeliveryAddressModel_DemogInfoCountryId").value + '", "CityName":"' + cityNameData + '"} }';
            $.ajax({
                url: "/" + "GeneralUtility" + "/" + "SearchData",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: searchData,
                dataType: "json",
                success: function (responseData, textStatus, request) {
                    var html = "";
                    for (i = 0; i < responseData.length; i++) {
                        html += "<option value='" + responseData[i].CityName + " ; " + responseData[i].ZipCode + " ; " + responseData[i].SubDivisionDesc + "' />";
                    }
                    document.getElementById("cityNameDataList").innerHTML = html;
                },
                error: function (xhr, exception) {
                    console.log("00099000", "cityName_oninput", "ERROR???", exception, xhr);
                }
            });
        }
    }
    else {
        if (cityNameData.length < 3) {
            document.getElementById("cityNameDataList").innerHTML = "";
        }
    }
}
function zipCode_oninput() {
    var i;
    var zipCodeData = document.getElementById("DeliveryAddressModel_ZipCode").value;
    if (zipCodeData.length >= 3) {
        if (zipCodeData.indexOf(" ; ") > -1) {
            var indexOf = zipCodeData.indexOf(" ; ");
            var zipCodeValue = zipCodeData.substr(0, indexOf);
            zipCodeData = zipCodeData.substr(indexOf + 3);
            indexOf = zipCodeData.indexOf(" ; ");
            var cityNameValue = zipCodeData.substr(0, indexOf);
            var subDivisionDesc = zipCodeData.substr(indexOf + 3);
            document.getElementById("DeliveryAddressModel_ZipCode").value = zipCodeValue;
            document.getElementById("DeliveryAddressModel_CityName").value = cityNameValue;
            var selectObject = document.getElementById("DeliveryAddressModel_DemogInfoSubDivisionId");
            for (i = 0; i < selectObject.options.length; i++) {
                if (selectObject.options[i].text == subDivisionDesc) {
                    selectObject.selectedIndex = i;
                    break;
                }
            }
        }
        else {
            var searchData = '{ "SearchType":"ZipCode", "SearchKeyValuePairs": { "DemogInfoCountryId":' + document.getElementById("DeliveryAddressModel_DemogInfoCountryId").value + ', "ZipCode":"' + zipCodeData + '"} }';
            $.ajax({
                url: "/" + "GeneralUtility" + "/" + "SearchData",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: searchData,
                dataType: "json",
                success: function (responseData, textStatus, request) {
                    var html = "";
                    for (i = 0; i < responseData.length; i++) {
                        html += "<option value='" + responseData[i].ZipCode + " ; " + responseData[i].CityName + " ; " + responseData[i].SubDivisionDesc + "' />";
                    }
                    document.getElementById("zipCodeDataList").innerHTML = html;
                },
                error: function (xhr, exception) {
                    console.log("00099000", "zipCode_oninput", "ERROR???", exception, xhr);
                }
            });
        }
    }
    else {
        if (zipCodeData.length < 3) {
            document.getElementById("zipCodeDataList").innerHTML = "";
        }
    }
}
