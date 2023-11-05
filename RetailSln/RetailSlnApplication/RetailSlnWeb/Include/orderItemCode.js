//Sriramajayam
//orderItemCode.js
function addToCart_onclick(index, categoryId) {
    console.log("addToCart_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    var jsonDatas = [];
    var jsonData, jsonDataString, orderQtyHtmlElement, itemId, orderQty, orderComments;
    for (var i = 0; ; i++) {
        orderQtyHtmlElement = document.getElementById("orderQty" + i);
        if (orderQtyHtmlElement == null) {
            break;
        }
        else {
            orderQty = document.getElementById("orderQty" + i).value;
            if ((/^\d+$/.test(orderQty)) && orderQty.length <= document.getElementById("orderQty" + i).getAttribute("maxlength") && orderQty >= document.getElementById("orderQty" + i).getAttribute("min") && orderQty <= document.getElementById("orderQty" + i).getAttribute("max")) {
                itemId = document.getElementById("itemId" + i).innerText;
                orderComments = "";//document.getElementById("orderComments" + i).value;
                jsonData = {};
                jsonData.ItemId = itemId;
                jsonData.OrderQty = orderQty;
                jsonData.OrderComments = orderComments;
                jsonDatas.push(jsonData);
            }
        }
    }
    if (jsonDatas.length === 0) {
        $('#loadingModal').modal('hide');
        document.getElementById("divErrorMessage").innerHTML = "Please enter order quantity for a min of 1 of item";
        alert(document.getElementById("divErrorMessage").innerHTML);
    }
    else {
        jsonDataString = JSON.stringify(jsonDatas);
        console.log(jsonDataString);
        var url = "/Home/AddToCart/" + categoryId;
        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonDataString,
            success: function (responseData, textStatus, request) {
                $('#loadingModal').modal('hide');
                console.log("00001000", "addToCart_onclick success", responseData.processMessage);
                if (responseData.success) {
                    document.getElementById("divOrderItem").innerHTML = responseData.htmlString;
                    document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                    document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                    document.getElementById("shoppingCartItemsCount2").innerHTML = responseData.shoppingCartItemsCount;
                    document.getElementById("shoppingCartTotalAmount2").innerHTML = responseData.shoppingCartTotalAmount;
                }
                else {
                    document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
                }
            },
            error: function (xhr, exception) {
                $('#loadingModal').modal('hide');
                console.log("addToCart_onclick", "00099000", "ERROR???");
                console.log(xhr, exception);
            }
        });
    }
}
function orderComments_onchange(index) {
    console.log("orderComments_onchange", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    try {
        var orderComments = document.getElementById("orderComments" + index).value;
        if (orderComments === null || orderComments === "") {
            $('#loadingModal').modal('hide');
            return;
        }
        var url = "/Home/ShoppingCartComments";
        $.ajax({
            url: url,
            type: "GET",
            //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { "index": index, "orderComments": orderComments },
            async: true,
            success: function (responseData, textStatus, request) {
                $('#loadingModal').modal('hide');
                console.log("orderComments_onchange", "00090000", "SUCCESS!!!", responseData.success);
            },
            error: function (xhr, exception) {
                $('#loadingModal').modal('hide');
                console.log("orderComments_onchange", "00099000", "ERROR???");
                console.log(exception, xhr);
                var jsonData = JSON.parse(xhr.responseText);
                document.getElementById("divErrorMessage").innerHTML = jsonData.errorMessage;
            }
        });
    }
    catch (err) {
        alert("Please fix errors to continue???");
    }
    return false;
}
function orderQty_oninput(index) {
    if (document.getElementById("orderQty" + index).value.length > document.getElementById("orderQty" + index).getAttribute("maxlength")) {
        document.getElementById("orderQty" + index).value = document.getElementById("orderQty" + index).value.substr(0, document.getElementById("orderQty" + index).getAttribute("maxlength"));
    }
}
function removeFromCart_onclick(index) {
    console.log("removeFromCart_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    if (document.getElementById("divErrorMessage2") != null) {
        document.getElementById("divErrorMessage2").innerHTML = "";
    }
    var categoryId;
    var query = window.location.href;
    var lastIndexOf1 = query.lastIndexOf('?id=');
    if (lastIndexOf1 > -1) {
        categoryId = query.substr(lastIndexOf1 + 4);
    }
    else {
        categoryId = "";
    }
    try {
        var url = "/Home/RemoveFromCart/" + categoryId + "?index=" + index;
        $.ajax({
            url: url,
            type: "GET",
            //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (responseData, textStatus, request) {
                $('#loadingModal').modal('hide');
                console.log("removeFromCart_onclick", "00090000", "SUCCESS!!!");
                if (responseData.success) {
                    document.getElementById("divOrderItem").innerHTML = responseData.htmlString;
                    document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                    document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                    document.getElementById("shoppingCartItemsCount2").innerHTML = responseData.shoppingCartItemsCount;
                    document.getElementById("shoppingCartTotalAmount2").innerHTML = responseData.shoppingCartTotalAmount;
                }
                else {
                    document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
                }
            },
            error: function (xhr, exception) {
                $('#loadingModal').modal('hide');
                console.log("removeFromCart_onclick", "00099000", "ERROR???");
                console.log(exception, xhr);
                var jsonData = JSON.parse(xhr.responseText);
                document.getElementById("divErrorMessage").innerHTML = jsonData.errorMessage;
                if (document.getElementById("divErrorMessage2") != null) {
                    document.getElementById("divErrorMessage2").innerHTML = jsonData.errorMessage;
                }
            }
        });
    }
    catch (err) {
        alert("Please fix errors to continue???");
    }
    return false;
}
