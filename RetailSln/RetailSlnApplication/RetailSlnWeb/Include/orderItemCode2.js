﻿//Sriramajayam
//orderItemCode2.js
function menuLink_onclick(url, queryString) {
    console.log("menuLink_onclick", "00000000", "ENTER!!!", url, queryString);
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    //document.getElementById("divErrorMessage").innerHTML = "";
    url += queryString;
    console.log("menuLink_onclick", "00001000", "url", url, "queryString", queryString);
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("00001000", "menuLink_onclick success", responseData.processMessage);
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("menuLink_onclick", "00099000", "ERROR???");
            console.log(xhr, exception);
        }
    });
}
function orderListView_onclick(orderHeaderId, invoiceTypeId) {
    console.log("orderListView_onclick", "00000000", "ENTER!!!");
    var url = "/Dashboard/OrderListView";
    url += "?id=" + orderHeaderId + "&invoiceTypeId=" + invoiceTypeId;
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("00001000", "orderListView_onclick success", responseData.processMessage);
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("orderListView_onclick", "00099000", "ERROR???");
            console.log(xhr, exception);
        }
    });
}
/*
function addEditLink_onclick(url, queryString) {
    console.log("addEditLink_onclick", "00000000", "ENTER!!!", url);
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    url += queryString;
    console.log("addEditLink_onclick", "00001000", "url", url, "queryString", queryString);
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("00001000", "addEditLink_onclick success", responseData.processMessage);
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("addEditLink_onclick", "00099000", "ERROR???");
            console.log(xhr, exception);
        }
    });
}
function btnCorpAcct_onclick() {
    window.open("/Dashboard", "_blank")
}
function btnSearchForUserResult_onclick() {
    if (document.getElementById("divSearchForUserResult").style.display == "none") {
        document.getElementById("divSearchForUserResult").style.display = "block";
    }
    else {
        document.getElementById("divSearchForUserResult").style.display = "none";
    }
}
function btnSearchForItemResult_onclick() {
    if (document.getElementById("divSearchForItemResult").style.display == "none") {
        document.getElementById("divSearchForItemResult").style.display = "block";
    }
    else {
        document.getElementById("divSearchForItemResult").style.display = "none";
    }
}
function btnUserAddEditSave_onclick() {
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    const formPostData = new FormData(formUserAddEdit);
    //console.log(formPostData);
    // Send the POST request
    fetch('/Dashboard/UserAddEdit', {
        method: "POST",
        body: formPostData,
    })
        .then((response) => response.json())
        .then((responseData) => {
            $("#loadingModal").modal('hide');
            console.log(1, responseData);
            var returnValue;
            if (responseData.success) {
                document.getElementById("divDashboard").innerHTML = responseData.htmlString;
                alert("User created successfully!!!");
                returnValue = true;
            }
            else {
                document.getElementById("formUserAddEdit").innerHTML = responseData.htmlString;
                returnValue = false;
                alert("Please fix errors to continue???");
            }
            return returnValue;
        })
        .catch(error => {
            $("#loadingModal").modal('hide');
            returnValue = false;
            alert("Error while creating user 1???");
            console.log(error);
            return false;
        });
}
function btnSearchForUser_onclick() {
    console.log("btnSearchForUser_onclick", "00000000", "ENTER!!!");
    var searchForUserValue = document.getElementById("searchForUser").value;
    searchForUserValue = searchForUserValue.trim();
    if (searchForUserValue === "" || searchForUserValue.length == 0) {
        alert("Please enter search value");
        return false;
    }
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    if (searchForUserValue != "") {
        var url = "/Home/SearchForUser/" + searchForUserValue;
        $.ajax({
            url: url,
            type: "GET",
            //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
            dataType: "json",
            //data: { "itemId": itemId, "orderQty": orderQty },
            async: true,
            success: function (responseData, textStatus, request) {
                $('#loadingModal').modal('hide');
                console.log("btnSearchForUser_onclick", "00001000", responseData.success, responseData.processMessage);
                if (responseData.success) {
                    document.getElementById("divSearchForUserResultData").innerHTML = responseData.htmlString;
                    document.getElementById("divSearchForUserResult").style.display = "block";
                }
                else if (responseData.errorCode === "RELOAD_PAGE") {
                    alert(responseData.message); // Optional: Show an alert message.
                    location.reload(); // Reload the page.
                    window.location.href = "/Home/Error";
                }
                else {
                    document.getElementById("divErrorMessage").innerHTML = "Error occurred";
                }
                console.log("btnSearchForUser_onclick", "00090000", "EXIT!!!");
            },
            error: function (xhr, exception) {
                $('#loadingModal').modal('hide');
                console.log("btnSearchForUser_onclick", "00099000", "ERROR???");
                var jsonData = JSON.parse(xhr.responseText);
                console.log("btnSearchForUser_onclick", "00099100", exception, xhr, jsonData);
                document.getElementById("divErrorMessage").innerHTML = "Error occurred";
            }
        });
    }
    else {
        $('#loadingModal').modal('hide');
        alert("Please enter search value");
    }
    console.log("btnSearchForUser_onclick", "00090000", "EXIT!!!");
    return false;
}
function btnSearchForItem_onclick() {
    console.log("btnSearchForItem_onclick", "00000000", "ENTER!!!");
    var searchForItemValue = document.getElementById("searchForItem").value;
    searchForItemValue = searchForItemValue.trim();
    if (searchForItemValue === "" || searchForItemValue.trim().length == 0) {
        alert("Please enter search value");
        return false;
    }
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/SearchForItem/" + document.getElementById("selectedCategoryId").value + "?searchText=" + searchForItemValue;
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        dataType: "json",
        //data: { "itemId": itemId, "orderQty": orderQty },
        async: true,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("btnSearchForItem_onclick", "00001000", responseData.success, responseData.processMessage);
            if (responseData.success) {
                document.getElementById("divSearchForItemResultData").innerHTML = responseData.htmlString;
                document.getElementById("divSearchForItemResult").style.display = "block";
            }
            else if (responseData.errorCode === "RELOAD_PAGE") {
                alert(responseData.message); // Optional: Show an alert message.
                location.reload(); // Reload the page.
                window.location.href = "/Home/Error";
            }
            else {
                document.getElementById("divErrorMessage").innerHTML = "Error occurred";
            }
            console.log("btnSearchForItem_onclick", "00090000", "EXIT!!!");
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("btnSearchForItem_onclick", "00099000", "ERROR???");
            var jsonData = JSON.parse(xhr.responseText);
            console.log("btnSearchForItem_onclick", "00099100", exception, xhr, jsonData);
            document.getElementById("divErrorMessage").innerHTML = "Error occurred";
        }
    });
    console.log("searchItem_onclick", "00090000", "EXIT!!!");
    return false;
}
function categoryId_onchange(categoryId, pageNum, rowCount) {
    console.log("categoryId_onchange", "00000000", "ENTER!!!", categoryId, pageNum, rowCount);
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("lblSelectedCategoryDesc").innerHTML = document.getElementById("categoryId").options[document.getElementById("categoryId").selectedIndex].innerHTML;
    document.getElementById("categoryId").selectedIndex = 0;
    document.getElementById("divErrorMessage").innerHTML = "";
    document.getElementById("selectedCategoryId").value = categoryId;
    var url = "/Dashboard/OrderItemData" + "?id=" + categoryId + "&pageNum=" + pageNum + "&rowCount=" + rowCount;
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            if (responseData.success) {
                document.getElementById("divOrderItem").innerHTML = responseData.htmlString;
                document.getElementById("divScrollIntoView").scrollIntoView();
            }
            else {
                document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
            }
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("category_onchange", "00099000", "ERROR???");
            document.getElementById("divErrorMessage").innerHTML = "Error occurred";
            console.log(xhr, exception);
        }
    });
}
function categoryHier_onclick(parentCategoryId) {
    console.log("categoryHier_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    url = "/Dashboard/CategoryHierList)";
    url += "/" + parentCategoryId;
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("00001000", "categoryHier_onclick success", responseData.processMessage);
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("categoryHier_onclick", "00099000", "ERROR???");
            console.log(xhr, exception);
        }
    });
}
function hrfHamburgerMenu_onclick() {
    if (document.getElementById("divHamburgerMenu").style.display === "none") {
        document.getElementById("divHamburgerMenu").style.display = "block";
    }
    else {
        document.getElementById("divHamburgerMenu").style.display = "none";
    }
}
function hrfCloseHamburgerMenu_onclick() {
    document.getElementById("divHamburgerMenu").style.display = "none";
}
function itemMasterInfo_onclick(itemMasterId) {
    console.log("itemMasterInfo_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    url = "/Dashboard/ItemMasterInfo/" + itemMasterId;
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("00001000", "itemMasterInfo_onclick success", responseData.processMessage);
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
            jQueryTextEditorInit();
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("itemMasterInfo_onclick", "00099000", "ERROR???");
            console.log(xhr, exception);
            document.getElementById("divErrorMessage").innerHTML = "Error in loading Item Master Spec";
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
        }
    });
}
function itemMasterInfoSave_onclick() {
    console.log("itemMasterInfoSave_onclick", "00000000", "ENTER!!!");
    url = "/Dashboard/ItemMasterInfo/";
    var postData = $("#formItemMasterInfo").serialize();
    //var jsonData = {};
    //jsonData.ItemMasterId = document.getElementById("ItemMasterId").value;
    //console.log(9, postData);
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "json",
        data: postData,
        success: function (responseData, textStatus, request) {
            console.log("00001000", "itemMasterInfoSave_onclick success", responseData.processMessage);
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
            jQueryTextEditorInit();
        },
        error: function (xhr, exception) {
            console.log("itemMasterInfoSave_onclick", "00099000", "ERROR???");
            console.log(xhr, exception);
        }
    });
}
function jQueryTextEditorInit() {
    $('.jqte-test').jqte();
    $(".jqte_fontsize").each(function () {
        var sizeValue = $(this).attr("jqte-styleval");
        $(this).text(sizeValue);
        $(this).css({
            "font-size": "14px",
            "text-decoration": "none"
        });
    });
    $(".jqte_fontsizes").css(
        {
            "width": "45px",
            "min-height": "210px",
            "text-align": "center"
        });
    $(".jqte_tool ").css(
        {
            "border": "1px solid #abb2b9",
            "padding": "2px 8px",
            "border-radius": "4px",
            "margin-right": "6px",
        });
    // settings of status
    var jqteStatus = true;
    $(".status").click(function () {
        jqteStatus = jqteStatus ? false : true;
        $('.jqte-test').jqte({ "status": jqteStatus })
    });
}
function menuLink_onclick(url, queryString) {
    console.log("menuLink_onclick", "00000000", "ENTER!!!", url, queryString);
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    //document.getElementById("divErrorMessage").innerHTML = "";
    url += queryString;
    console.log("menuLink_onclick", "00001000", "url", url, "queryString", queryString);
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("00001000", "menuLink_onclick success", responseData.processMessage);
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("menuLink_onclick", "00099000", "ERROR???");
            console.log(xhr, exception);
        }
    });
}
function menuLinkHamburger_onclick(url, queryString) {
    console.log("menuLinkHamburger_onclick", "00000000", "ENTER!!!", url, queryString);
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    menuLink_onclick(url, queryString);
    document.getElementById("divHamburgerMenu").style.display = "none";
    $('#loadingModal').modal('hide');
    console.log("menuLinkHamburger_onclick", "00090000", "EXIT!!!");
}
function corpAcctSave_onclick() {
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    console.log("corpAcctSave_onclick", "00000000", "ENTER!!!");
    const formPostData = new FormData(formCorpAcct);
    fetch('/Dashboard/CorpAcct', {
        method: "POST",
        body: formPostData,
    })
        .then((response) => response.json())
        .then((responseData) => {
            $("#loadingModal").modal('hide');
            console.log(1, responseData);
            var returnValue;
            if (responseData.success) {
                alert("Corp acct created successfully!!!");
                document.getElementById("divDashboard").innerHTML = responseData.htmlString;
                returnValue = true;
            }
            else {
                returnValue = false;
                document.getElementById("formCorpAcct").innerHTML = responseData.htmlString;
                alert("Please fix errors to continue???");
            }
            return returnValue;
        })
        .catch(error => {
            $("#loadingModal").modal('hide');
            returnValue = false;
            alert("Error while creating user???");
            console.log(error);
            return false;
        });
}
function corpAcctLocationSave_onclick() {
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    console.log("corpAcctLocationSave_onclick", "00000000", "ENTER!!!");
    const formPostData = new FormData(formCorpAcctLocation);
    fetch('/Dashboard/CorpAcctLocation', {
        method: "POST",
        body: formPostData,
    })
        .then((response) => response.json())
        .then((responseData) => {
            $("#loadingModal").modal('hide');
            console.log("corpAcctLocationSave_onclick", "00001000", responseData);
            var returnValue;
            if (responseData.success) {
                alert("Corp acct location created successfully!!!");
                document.getElementById("divDashboard").innerHTML = responseData.htmlString;
                returnValue = true;
            }
            else {
                returnValue = false;
                document.getElementById("formCorpAcctLocation").innerHTML = responseData.htmlString;
                alert("Please fix errors to continue???");
            }
            return returnValue;
        })
        .catch(error => {
            $("#loadingModal").modal('hide');
            returnValue = false;
            alert("Error while creating user???");
            console.log(error);
            return false;
        });
}
function corpAcctLocationSave_onclick_Old() {
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    console.log("corpAcctLocationSave_onclick", "00000000", "ENTER!!!");
    var postDataJsonString = JSON.stringify($("#formCorpAcctLocation").serialize());
    console.log(postDataJsonString);
    $.ajax({
        url: "/Dashboard/CorpAcctLocation",
        type: "POST",
        //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: postDataJsonString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("00001000", "corpAcctLocationSave_onclick success", responseData.processMessage);
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("00099000", "corpAcctLocationSave_onclick", "ERROR???");
            //console.log(xhr.responseText);
            console.log(xhr, exception);
        }
    });
}
function corpAcctLocationSave_onclick_Backup() {
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    console.log("corpAcctLocationSave_onclick", "00000000", "ENTER!!!");
    var postData = $("#formCorpAcctLocation").serialize();
    //console.log(postData);
    //const formPostData = new FormData(formCorpAcctLocation);
    //console.log(JSON.stringify(formPostData));
    fetch('/Dashboard/CorpAcctLocation', {
        method: "POST",
        body: postData,
    })
        .then((response) => response.json())
        .then((responseData) => {
            $("#loadingModal").modal('hide');
            console.log(1, responseData);
            var returnValue;
            if (responseData.success) {
                alert("Corp acct location created successfully!!!");
                document.getElementById("formCorpAcctLocation").innerHTML = responseData.htmlString;
                returnValue = true;
            }
            else {
                returnValue = false;
                document.getElementById("formCorpAcctLocation").innerHTML = responseData.htmlString;
                alert("Please fix errors to continue???");
            }
            return returnValue;
        })
        .catch(error => {
            $("#loadingModal").modal('hide');
            returnValue = false;
            alert("Error while creating user???");
            console.log(error);
            return false;
        });
}
function invoiceType_onchange() {
    console.log("invoiceType_onchange", "00000000", "ENTER!!!");
    var url = "/Dashboard/InvoiceType" + "?id=" + document.getElementById("invoiceTypeId").value;
    fetch(url, {
        method: "GET",
    })
        .then((response) => response.json())
        .then((responseData) => {
            $("#loadingModal").modal('hide');
            console.log(1, responseData);
            console.log("00001000", "invoiceType_onchange success", responseData.processMessage);
            if (responseData.success) {
                $("#loadingModal").modal('hide');
            }
            else {
                $("#loadingModal").modal('hide');
                alert("Invoice Type error???");
            }
            return false;
        })
        .catch(error => {
            $("#loadingModal").modal('hide');
            alert("Invoice Type error???");
            console.log(error);
            return false;
        });
    return false;
}
function createdFor_onclick() {
    var i, locationIndex = -1, personIndex = -1;
    for (i = 0; ; i++) {
        try {
            if (document.getElementById("createdForLocationSelect" + i).checked) {
                locationIndex = i;
                break;
            }
        }
        catch (err) {
            break;
        }
    }
    for (i = 0; ; i++) {
        try {
            if (document.getElementById("createdForPersonSelect" + i).checked) {
                personIndex = i;
                break;
            }
        }
        catch (err) {
            break;
        }
    }
    if (locationIndex == -1 || personIndex == -1) {
        alert("Select both location and person");
        return false;
    }
    //alert(locationIndex);
    //alert(personIndex);
    document.getElementById("createdForPersonSelected").innerHTML = document.getElementById("createdForPerson" + personIndex).innerHTML;
    document.getElementById("createdForLocationSelected").innerHTML = document.getElementById("createdForCorpAcctLocation" + locationIndex).innerHTML;
    var personId, corpAcctLocationId;
    personId = document.getElementById("createdForPersonId" + personIndex).value;
    corpAcctLocationId = document.getElementById("createdForCorpAcctLocationId" + locationIndex).value;
    //alert(corpAcctLocationId);
    //alert(personId);
    var url = "/Dashboard/OrderCreatedFor" + "?id=" + personId + "&corpAcctLocnId=" + corpAcctLocationId + "&invTypeId=" + document.getElementById("invoiceTypeId").value;
    fetch(url, {
        method: "GET",
    })
        .then((response) => response.json())
        .then((responseData) => {
            $("#loadingModal").modal('hide');
            console.log(1, responseData);
            //document.getElementById("formUserAddEdit").innerHTML = responseData.htmlString;
            var returnValue;
            if (responseData.success) {
                $("#loadingModal").modal('hide');
                document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                document.getElementById("shoppingCartItemsCount1").innerHTML = responseData.shoppingCartItemsCount;
                document.getElementById("shoppingCartTotalAmount1").innerHTML = responseData.shoppingCartTotalAmount;
                btnSearchForUserResult_onclick();
                returnValue = true;
            }
            else {
                $("#loadingModal").modal('hide');
                returnValue = false;
                alert("Create for user error???");
            }
            return returnValue;
        })
        .catch(error => {
            $("#loadingModal").modal('hide');
            returnValue = false;
            alert("Create for user error???");
            console.log(error);
            return false;
        });
    return false;
}
//function shoppingCart_onclick() {
//    console.log("showShoppingCart_onclick", "00000000", "ENTER!!!");
//    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
//    var url = "/Home/ShoppingCart/";
//    $.ajax({
//        url: url,
//        type: "GET",
//        //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
//        //dataType: "json",
//        //data: { "itemId": itemId, "orderQty": orderQty },
//        async: true,
//        success: function (responseData, textStatus, request) {
//            $('#loadingModal').modal('hide');
//            console.log("showShoppingCart_onclick", "00090000", "SUCCESS!!!");
//            console.log(responseData);
//            document.getElementById("divShoppingCartData").innerHTML = responseData.htmlString;
//            document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
//            document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
//            document.getElementById("shoppingCartItemsCount1").innerHTML = responseData.shoppingCartItemsCount;
//            document.getElementById("shoppingCartTotalAmount1").innerHTML = responseData.shoppingCartTotalAmount;
//            $('#divShoppingCartPopupModal').modal({ backdrop: 'static', keyboard: false });
//        },
//        error: function (xhr, exception) {
//            $('#loadingModal').modal('hide');
//            console.log("showShoppingCart_onclick", "00099000", "ERROR???");
//            console.log(exception, xhr);
//            document.getElementById("divErrorMessage").innerHTML = "Error while getting shopping cart";
//        }
//    });
//}
function swapItem(fromIndex, toIndex) {
    var tempValue;
    console.log(fromIndex, toIndex, document.getElementById("categoryDesc" + fromIndex).innerHTML, document.getElementById("categoryDesc" + toIndex).innerHTML);

    tempValue = document.getElementById("seqNum" + fromIndex).innerHTML;
    document.getElementById("seqNum" + fromIndex).innerHTML = document.getElementById("seqNum" + toIndex).innerHTML;
    document.getElementById("seqNum" + toIndex).innerHTML = tempValue;

    tempValue = document.getElementById("categoryId" + fromIndex).innerHTML;
    document.getElementById("categoryId" + fromIndex).innerHTML = document.getElementById("categoryId" + toIndex).innerHTML;
    document.getElementById("categoryId" + toIndex).innerHTML = tempValue;

    tempValue = document.getElementById("categoryNameDesc" + fromIndex).innerHTML;
    document.getElementById("categoryNameDesc" + fromIndex).innerHTML = document.getElementById("categoryNameDesc" + toIndex).innerHTML;
    document.getElementById("categoryNameDesc" + toIndex).innerHTML = tempValue;

    tempValue = document.getElementById("categoryNameDesc" + fromIndex).innerHTML;
    document.getElementById("categoryNameDesc" + fromIndex).innerHTML = document.getElementById("categoryNameDesc" + toIndex).innerHTML;
    document.getElementById("categoryNameDesc" + toIndex).innerHTML = tempValue;

    tempValue = document.getElementById("categoryDesc" + fromIndex).innerHTML;
    document.getElementById("categoryDesc" + fromIndex).innerHTML = document.getElementById("categoryDesc" + toIndex).innerHTML;
    document.getElementById("categoryDesc" + toIndex).innerHTML = tempValue;

    tempValue = document.getElementById("CategoryItemHierModels_" + fromIndex + "__CategoryModel_CategoryId").value;
    document.getElementById("CategoryItemHierModels_" + fromIndex + "__CategoryModel_CategoryId").value = document.getElementById("CategoryItemHierModels_" + toIndex + "__CategoryModel_CategoryId").value;
    document.getElementById("CategoryItemHierModels_" + toIndex + "__CategoryModel_CategoryId").value = tempValue;
}
function orderListView_onclick(orderHeaderId, invoiceTypeId) {
    console.log("orderListView_onclick", "00000000", "ENTER!!!");
    var url = "/Dashboard/OrderListView";
    url += "?id=" + orderHeaderId + "&invoiceTypeId=" + invoiceTypeId;
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("00001000", "orderListView_onclick success", responseData.processMessage);
            document.getElementById("divDashboard").innerHTML = responseData.htmlString;
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("orderListView_onclick", "00099000", "ERROR???");
            console.log(xhr, exception);
        }
    });
}
*/
