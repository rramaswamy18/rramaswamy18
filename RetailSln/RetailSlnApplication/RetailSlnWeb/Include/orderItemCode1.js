﻿//Sriramajayam
//orderItemCode1.js
function addToCart_onclick(categoryId, pageNum, pageSize, totalRowCount) {
    console.log("addToCart_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    var jsonPostData = {};
    var shoppingCartItemModels = [];
    var shoppingCartItemModel, orderQtyHtmlElement, itemId, orderQty, orderComments;
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
                shoppingCartItemModel = {};
                shoppingCartItemModel.ItemId = itemId;
                shoppingCartItemModel.OrderQty = orderQty;
                shoppingCartItemModel.OrderComments = orderComments;
                shoppingCartItemModels.push(shoppingCartItemModel);
            }
        }
    }
    if (shoppingCartItemModels.length === 0) {
        $('#loadingModal').modal('hide');
        document.getElementById("divErrorMessage").innerHTML = "Please enter order quantity for a min of 1 item";
        alert(document.getElementById("divErrorMessage").innerHTML);
    }
    else {
        var jsonPostDataString;
        jsonPostData.CategoryId = categoryId;
        jsonPostData.PageNum = pageNum;
        jsonPostData.PageSize = pageSize;
        jsonPostData.TotalRowCount = totalRowCount;
        jsonPostData.ShoppingCartItemModels = shoppingCartItemModels;
        jsonPostDataString = JSON.stringify(jsonPostData);
        console.log(-999, jsonPostDataString);
        var url = "/Home/AddToCart/";
        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonPostDataString,
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
function addToCartGet_onclick(index, defaultOrderQty, categoryId) {
    console.log("addToCartGet_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    //document.getElementById("addToCart" + index).innerHTML = "";
    try {
        var itemId = document.getElementById("itemId" + index).innerText;
        var orderQty = document.getElementById("orderQty" + index).value;
        //Test if the input is 1. numeric 2. not exceed maxlength 3. between min and max values
        if ((/^\d+$/.test(orderQty)) && orderQty.length <= document.getElementById("orderQty" + index).getAttribute("maxlength") && orderQty >= document.getElementById("orderQty" + index).getAttribute("min") && orderQty <= document.getElementById("orderQty" + index).getAttribute("max")) {
        }
        else {
            $('#loadingModal').modal('hide');
            document.getElementById("divErrorMessage").innerHTML = "Enter valid order quantity";
            return false;
        }
        var url = "/Home/AddToCart/" + categoryId;
        $.ajax({
            url: url,
            type: "GET",
            //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
            dataType: "json",
            data: { "itemId": itemId, "orderQty": orderQty },
            async: true,
            success: function (responseData, textStatus, request) {
                $('#loadingModal').modal('hide');
                console.log("addToCartGet_onclick", "00090000", "SUCCESS!!!");
                //var jsonData = JSON.parse(responseData);
                document.getElementById("divOrderItem").innerHTML = responseData.htmlString;
                document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                document.getElementById("orderQty" + index).value = defaultOrderQty;
            },
            error: function (xhr, exception) {
                $('#loadingModal').modal('hide');
                console.log("addToCartGet_onclick", "00099000", "ERROR???");
                console.log(exception, xhr);
                var jsonData = JSON.parse(xhr.responseText);
                document.getElementById("divErrorMessage").innerHTML = jsonData.errorMessage;
                document.getElementById("addToCart" + index).innerHTML = "<i class='fa fa-times' style='color: #ff0000; padding-right: 5px;'></i><span style='color: #ff0000;'>Error???</span><br />";
            }
        });
    }
    catch (err) {
        alert("Please fix errors to continue???");
    }
    return false;
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
    var jsonPostData =
    {
        ParentCategoryId: document.getElementById("ParentCategoryId").value,
        PageNum: document.getElementById("PageNum").value,
        PageSize: document.getElementById("PageSize").value,
        TotalRowCount: document.getElementById("TotalRowCount").value,
        RemoveFromCartIndex: index,
    };
    var jsonPostDataString = JSON.stringify(jsonPostData);
    try {
        var url = "/Home/RemoveFromCart/";
        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
            dataType: "json",
            data: jsonPostDataString,
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
            }
        });
    }
    catch (err) {
        alert("Please fix errors to continue???");
    }
    return false;
}