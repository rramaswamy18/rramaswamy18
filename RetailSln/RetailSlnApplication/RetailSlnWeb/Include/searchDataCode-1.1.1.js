//Sriramajayam
//searchDataCode-1.1.1.js
function searchDataCityName(htmlFieldPrefix) {
    var cityNameData = document.getElementById(htmlFieldPrefix + "CityName").value;
    var searchData = '{ "SearchType":"CityName", "SearchKeyValuePairs": { "DemogInfoCountryId":"' + document.getElementById(htmlFieldPrefix + "DemogInfoCountryId").value + '", "CityName":"' + cityNameData + '"} }';
    $.ajax({
        url: "/" + "GeneralUtility" + "/" + "SearchData",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: searchData,
        dataType: "json",
        success: function (responseData, textStatus, request) {
            var html = "";
            var cityNameZipCodeDataListIdsObject = document.getElementById("cityNameZipCodeDataListIds");
            var prefixChar = "";
            cityNameZipCodeDataListIdsObject.value = "";
            for (i = 0; i < responseData.length; i++) {
                html += "<option value='" + responseData[i].CityName + " ; " + responseData[i].ZipCode + " ; " + responseData[i].SubDivisionDesc + "' />";
                cityNameZipCodeDataListIdsObject.value += prefixChar + responseData[i].DemogInfoZipId + "," + responseData[i].DemogInfoCityId;
                prefixChar = ";";
            }
            document.getElementById("cityNameDataList").innerHTML = html;
        },
        error: function (xhr, exception) {
            console.log("00099000", "cityName_oninput", "ERROR???", exception, xhr);
        }
    });
}
function searchDataZipCode(htmlFieldPrefix) {
    var zipCodeData = document.getElementById(htmlFieldPrefix + "ZipCode").value;
    var searchData = '{ "SearchType":"ZipCode", "SearchKeyValuePairs": { "DemogInfoCountryId":' + document.getElementById(htmlFieldPrefix + "DemogInfoCountryId").value + ', "ZipCode":"' + zipCodeData + '"} }';
    $.ajax({
        url: "/" + "GeneralUtility" + "/" + "SearchData",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: searchData,
        dataType: "json",
        success: function (responseData, textStatus, request) {
            var html = "";
            var cityNameZipCodeDataListIdsObject = document.getElementById("cityNameZipCodeDataListIds");
            var prefixChar = "";
            cityNameZipCodeDataListIdsObject.value = "";
            for (i = 0; i < responseData.length; i++) {
                html += "<option value='" + responseData[i].ZipCode + " ; " + responseData[i].CityName + " ; " + responseData[i].SubDivisionDesc + "' />";
                cityNameZipCodeDataListIdsObject.value += prefixChar + responseData[i].ZipCode + "$" + responseData[i].CityName + "$" + responseData[i].DemogInfoZipId + "$" + responseData[i].DemogInfoCityId;
                prefixChar = ";";
            }
            document.getElementById("zipCodeDataList").innerHTML = html;
        },
        error: function (xhr, exception) {
            console.log("00099000", "zipCode_oninput", "ERROR???", exception, xhr);
        }
    });
}
function demogInfoCountry_onchage(demogInfoCountryElement, htmlFieldPrefix) {
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
                document.getElementById(htmlFieldPrefix + "DemogInfoSubDivisionId").innerHTML = responseData.htmlString;
                //document.getElementById("PrimaryTelephoneDemogInfoCountryId").value = demogInfoCountryElement.value;
                //document.getElementById("AlternateTelephoneDemogInfoCountryId").value = demogInfoCountryElement.value;
            }
            else {
                alert("Error while loading states");
            }
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            alert("Error while loading states");
        }
    });
    return false;
}
function demogInfoSubDivision_onchage(demogInfoSubDivisionElement, htmlFieldPrefix) {
    console.log(htmlFieldPrefix);
}
function cityName_oninput(htmlFieldPrefix) {
    var i, cityNameZipCodeDataListIdsArray, cityNameZipCodeDataListIdArray;
    document.getElementById("zipCodeSuccess").style.display = "none";
    document.getElementById("zipCodeFailure").style.display = "block";
    document.getElementById("cityNameSuccess").style.display = "none";
    document.getElementById("cityNameFailure").style.display = "block";
    var cityNameData = document.getElementById(htmlFieldPrefix + "CityName").value;
    if (cityNameData.length >= 1) {
        if (cityNameData.indexOf(" ; ") > -1) {
            var indexOf = cityNameData.indexOf(" ; ");
            var cityNameValue = cityNameData.substr(0, indexOf);
            cityNameData = cityNameData.substr(indexOf + 3);
            indexOf = cityNameData.indexOf(" ; ");
            var zipCodeValue = cityNameData.substr(0, indexOf);
            var subDivisionDesc = cityNameData.substr(indexOf + 3);
            document.getElementById(htmlFieldPrefix + "ZipCode").value = zipCodeValue;
            document.getElementById(htmlFieldPrefix + "CityName").value = cityNameValue;
            var selectObject = document.getElementById(htmlFieldPrefix + "DemogInfoSubDivisionId");
            for (i = 0; i < selectObject.options.length; i++) {
                if (selectObject.options[i].text == subDivisionDesc) {
                    selectObject.selectedIndex = i;
                    document.getElementById("zipCodeSuccess").style.display = "block";
                    document.getElementById("zipCodeFailure").style.display = "none";
                    document.getElementById("cityNameSuccess").style.display = "block";
                    document.getElementById("cityNameFailure").style.display = "none";
                    break;
                }
            }
            cityNameZipCodeDataListIdsArray = document.getElementById("cityNameZipCodeDataListIds").value;
            cityNameZipCodeDataListIdsArray = cityNameZipCodeDataListIdsArray.split(";");
            for (i = 0; i < cityNameZipCodeDataListIdsArray.length; i++) {
                cityNameZipCodeDataListIdArray = cityNameZipCodeDataListIdsArray[i].split("$");
                if (cityNameZipCodeDataListIdArray[0] == cityNameValue && cityNameZipCodeDataListIdArray[1] == zipCodeValue) {
                    document.getElementById(htmlFieldPrefix + "DemogInfoZipPlusId").value = cityNameZipCodeDataListIdArray[2];
                    document.getElementById(htmlFieldPrefix + "DemogInfoZipId").value = cityNameZipCodeDataListIdArray[2];
                    document.getElementById(htmlFieldPrefix + "DemogInfoCityId").value = cityNameZipCodeDataListIdArray[3];
                }
            }
        }
        else {
            searchDataCityName(htmlFieldPrefix);
        }
    }
    else {
        //if (zipCodeData.length < 3) {
        document.getElementById("zipCodeDataList").innerHTML = "";
        //}
    }
}
function zipCode_oninput(htmlFieldPrefix) {
    var i, cityNameZipCodeDataListIdsArray, cityNameZipCodeDataListIdArray;
    document.getElementById("zipCodeSuccess").style.display = "none";
    document.getElementById("zipCodeFailure").style.display = "block";
    document.getElementById("cityNameSuccess").style.display = "none";
    document.getElementById("cityNameFailure").style.display = "block";
    var zipCodeData = document.getElementById(htmlFieldPrefix + "ZipCode").value;
    if (zipCodeData.length >= 1) {
        if (zipCodeData.indexOf(" ; ") > -1) {
            var indexOf = zipCodeData.indexOf(" ; ");
            var zipCodeValue = zipCodeData.substr(0, indexOf);
            zipCodeData = zipCodeData.substr(indexOf + 3);
            indexOf = zipCodeData.indexOf(" ; ");
            var cityNameValue = zipCodeData.substr(0, indexOf);
            var subDivisionDesc = zipCodeData.substr(indexOf + 3);
            document.getElementById(htmlFieldPrefix + "ZipCode").value = zipCodeValue;
            document.getElementById(htmlFieldPrefix + "CityName").value = cityNameValue;
            var selectObject = document.getElementById(htmlFieldPrefix + "DemogInfoSubDivisionId");
            for (i = 0; i < selectObject.options.length; i++) {
                if (selectObject.options[i].text == subDivisionDesc) {
                    selectObject.selectedIndex = i;
                    document.getElementById("zipCodeSuccess").style.display = "block";
                    document.getElementById("zipCodeFailure").style.display = "none";
                    document.getElementById("cityNameSuccess").style.display = "block";
                    document.getElementById("cityNameFailure").style.display = "none";
                    break;
                }
            }
            cityNameZipCodeDataListIdsArray = document.getElementById("cityNameZipCodeDataListIds").value;
            cityNameZipCodeDataListIdsArray = cityNameZipCodeDataListIdsArray.split(";");
            for (i = 0; i < cityNameZipCodeDataListIdsArray.length; i++) {
                cityNameZipCodeDataListIdArray = cityNameZipCodeDataListIdsArray[i].split("$");
                if (cityNameZipCodeDataListIdArray[0] == zipCodeValue) {
                    document.getElementById(htmlFieldPrefix + "DemogInfoZipPlusId").value = cityNameZipCodeDataListIdArray[2];
                    document.getElementById(htmlFieldPrefix + "DemogInfoZipId").value = cityNameZipCodeDataListIdArray[2];
                    document.getElementById(htmlFieldPrefix + "DemogInfoCityId").value = cityNameZipCodeDataListIdArray[3];
                }
            }
        }
        else {
            searchDataZipCode(htmlFieldPrefix);
        }
    }
    else {
        //if (zipCodeData.length < 3) {
            document.getElementById("zipCodeDataList").innerHTML = "";
        //}
    }
}
function zipCode_onblur(htmlFieldPrefix) {
    document.getElementById("zipCodeSuccess").style.display = "none";
    document.getElementById("zipCodeFailure").style.display = "block";
    document.getElementById("cityNameSuccess").style.display = "none";
    document.getElementById("cityNameFailure").style.display = "block";
    var zipCodeData = document.getElementById(htmlFieldPrefix + "ZipCode").value;
    if (zipCodeData == "") {
        document.getElementById(htmlFieldPrefix + "ZipCode").value = "";
        document.getElementById(htmlFieldPrefix + "CityName").value = "";
        document.getElementById(htmlFieldPrefix + "DemogInfoZipId").value = "";
        document.getElementById(htmlFieldPrefix + "DemogInfoZipPlusId").value = "";
        document.getElementById(htmlFieldPrefix + "DemogInfoCityId").value = "";
        document.getElementById(htmlFieldPrefix + "DemogInfoSubDivisionId").value = "";
    }
    else {
        var i, j;
        var searchData = '{ "SearchType":"ZipCode", "SearchKeyValuePairs": { "DemogInfoCountryId":' + document.getElementById(htmlFieldPrefix + "DemogInfoCountryId").value + ', "ZipCode":"' + zipCodeData + '"} }';
        $.ajax({
            url: "/" + "GeneralUtility" + "/" + "SearchData",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: searchData,
            dataType: "json",
            success: function (responseData, textStatus, request) {
                for (i = 0; i < responseData.length; i++) {
                    if (responseData[i].ZipCode == zipCodeData) {
                        var selectObject = document.getElementById(htmlFieldPrefix + "DemogInfoSubDivisionId");
                        for (j = 0; i < selectObject.options.length; j++) {
                            if (selectObject.options[j].text == responseData[i].SubDivisionDesc) {
                                selectObject.selectedIndex = j;
                                document.getElementById("zipCodeSuccess").style.display = "block";
                                document.getElementById("zipCodeFailure").style.display = "none";
                                document.getElementById("cityNameSuccess").style.display = "block";
                                document.getElementById("cityNameFailure").style.display = "none";
                                break;
                            }
                        }
                        document.getElementById(htmlFieldPrefix + "ZipCode").value = responseData[i].ZipCode;
                        document.getElementById(htmlFieldPrefix + "CityName").value = responseData[i].CityName;
                        document.getElementById(htmlFieldPrefix + "DemogInfoZipId").value = responseData[i].DemogInfoZipId;
                        document.getElementById(htmlFieldPrefix + "DemogInfoZipPlusId").value = responseData[i].DemogInfoZipPlusId;
                        document.getElementById(htmlFieldPrefix + "DemogInfoCityId").value = responseData[i].DemogInfoCityId;
                    }
                }
            },
            error: function (xhr, exception) {
                console.log("00099000", "zipCode_oninput", "ERROR???", exception, xhr);
            }
        });
    }
}
function cityName_onblur(htmlFieldPrefix) {
    document.getElementById("zipCodeSuccess").style.display = "none";
    document.getElementById("zipCodeFailure").style.display = "block";
    document.getElementById("cityNameSuccess").style.display = "none";
    document.getElementById("cityNameFailure").style.display = "block";
    var cityNameData = document.getElementById(htmlFieldPrefix + "CityName").value;
    if (cityNameData == "") {
        document.getElementById(htmlFieldPrefix + "ZipCode").value = "";
        document.getElementById(htmlFieldPrefix + "CityName").value = "";
        document.getElementById(htmlFieldPrefix + "DemogInfoZipPlusId").value = "";
        document.getElementById(htmlFieldPrefix + "DemogInfoZipId").value = "";
        document.getElementById(htmlFieldPrefix + "DemogInfoCityId").value = "";
        document.getElementById(htmlFieldPrefix + "DemogInfoSubDivisionId").value = "";
    }
    else {
        var i, j;
        var searchData = '{ "SearchType":"CityName", "SearchKeyValuePairs": { "DemogInfoCountryId":"' + document.getElementById(htmlFieldPrefix + "DemogInfoCountryId").value + '", "CityName":"' + cityNameData + '"} }';
        $.ajax({
            url: "/" + "GeneralUtility" + "/" + "SearchData",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: searchData,
            dataType: "json",
            success: function (responseData, textStatus, request) {
                var zipCodeData = document.getElementById(htmlFieldPrefix + "ZipCode").value;
                for (i = 0; i < responseData.length; i++) {
                    if (zipCodeData == "" && responseData[i].ZipCode != "") {
                        document.getElementById(htmlFieldPrefix + "ZipCode").value = responseData[i].ZipCode;
                        document.getElementById(htmlFieldPrefix + "DemogInfoZipId").value = responseData[i].DemogInfoZipId;
                        document.getElementById(htmlFieldPrefix + "DemogInfoZipPlusId").value = responseData[i].DemogInfoZipPlusId;
                        zipCodeData = document.getElementById(htmlFieldPrefix + "ZipCode").value;
                    }
                    if (responseData[i].CityName == cityNameData && responseData[i].ZipCode == zipCodeData) {
                        var selectObject = document.getElementById(htmlFieldPrefix + "DemogInfoSubDivisionId");
                        for (j = 0; i < selectObject.options.length; j++) {
                            if (selectObject.options[j].text == responseData[i].SubDivisionDesc) {
                                selectObject.selectedIndex = j;
                                document.getElementById("zipCodeSuccess").style.display = "block";
                                document.getElementById("zipCodeFailure").style.display = "none";
                                document.getElementById("cityNameSuccess").style.display = "block";
                                document.getElementById("cityNameFailure").style.display = "none";
                                break;
                            }
                        }
                        document.getElementById(htmlFieldPrefix + "ZipCode").value = responseData[i].ZipCode;
                        document.getElementById(htmlFieldPrefix + "CityName").value = responseData[i].CityName;
                        document.getElementById(htmlFieldPrefix + "DemogInfoZipId").value = responseData[i].DemogInfoZipId;
                        document.getElementById(htmlFieldPrefix + "DemogInfoZipPlusId").value = responseData[i].DemogInfoZipPlusId;
                        document.getElementById(htmlFieldPrefix + "DemogInfoCityId").value = responseData[i].DemogInfoCityId;
                    }
                }
            },
            error: function (xhr, exception) {
                console.log("00099000", "zipCode_oninput", "ERROR???", exception, xhr);
            }
        });
    }
}
function zipCodeCityName_oninput(htmlFieldPrefix) {
    document.getElementById("zipCodeSuccess").style.display = "none";
    document.getElementById("zipCodeFailure").style.display = "block";
    document.getElementById("cityNameSuccess").style.display = "none";
    document.getElementById("cityNameFailure").style.display = "block";
    var demogInfoCountryId = document.getElementById(htmlFieldPrefix + "DemogInfoCountryId").value
    var zipCodeData = document.getElementById(htmlFieldPrefix + "ZipCode").value;
    var cityNameData = document.getElementById(htmlFieldPrefix + "CityName").value;
    if (zipCodeData == "" && cityNameData == "") {
    }
    else {
        var i, j;
        var searchData = '{ "SearchType":"ZipCodeOrCityName", "SearchKeyValuePairs": { "DemogInfoCountryId":' + demogInfoCountryId + ', "CityName":"' + cityNameData + '", "ZipCode":"' + zipCodeData + '"} }';
        $.ajax({
            url: "/" + "GeneralUtility" + "/" + "SearchByZipCodeOrCityName",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: searchData,
            dataType: "json",
            success: function (responseData, textStatus, request) {
                console.log("00001000", "zipCodeCityName_oninput", responseData);
                if (responseData.length > 0) {
                    console.log("00002000", "zipCodeCityName_oninput");
                    document.getElementById("zipCodeSuccess").style.display = "block";
                    document.getElementById("zipCodeFailure").style.display = "none";
                    document.getElementById("cityNameSuccess").style.display = "block";
                    document.getElementById("cityNameFailure").style.display = "none";
                    document.getElementById(htmlFieldPrefix + "ZipCode").value = responseData[0].ZipCode;
                    document.getElementById(htmlFieldPrefix + "CityName").value = responseData[0].CityName;
                    document.getElementById(htmlFieldPrefix + "DemogInfoZipId").value = responseData[0].DemogInfoZipId;
                    document.getElementById(htmlFieldPrefix + "DemogInfoZipPlusId").value = responseData[0].DemogInfoZipPlusId;
                    document.getElementById(htmlFieldPrefix + "DemogInfoCityId").value = responseData[0].DemogInfoCityId;
                    document.getElementById(htmlFieldPrefix + "DemogInfoSubDivisionId").value = responseData[0].DemogInfoSubDivisionId;
                }
                else {
                    console.log("00003000", "zipCodeCityName_oninput");
                }
            },
            error: function (xhr, exception) {
                console.log("00099000", "zipCodeCityName_oninput", "ERROR???", exception, xhr);
            }
        });
    }
    return;
}
