//Sriramajayam
//orderItemCode0.js
function addToCart1_onclick(itemId, index) {
    console.log("addToCartGet1_onclick", "00000000", "ENTER!!!");
    var returnValue = true, errorMessage = "";
    var orderQty, itemIdNull;
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var returnObject = {};
    returnObject.itemId = "";
    returnObject.itemIdNull = null;
    returnObject.orderQty = "";
    if (returnValue = validateItemIdOrderQty(itemId, orderQty, returnObject, index, errorMessage)) {
        itemId = returnObject.itemId;
        itemIdNull = returnObject.itemIdNull;
        orderQty = returnObject.orderQty;
    }
    else {
        $('#loadingModal').modal('hide');
        return returnValue;
    }
    try {
        console.log(1, itemId, itemIdNull, orderQty, index, returnValue, errorMessage);
        var url = "/Home/AddToCart1/";
        $.ajax({
            url: url,
            type: "GET",
            //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
            dataType: "json",
            data: { "itemId": itemId, "orderQty": orderQty },
            async: true,
            success: function (responseData, textStatus, request) {
                $('#loadingModal').modal('hide');
                console.log("addToCartGet1_onclick", "00090000", "SUCCESS!!!");
                if (responseData.success) {
                    document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                    document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                    document.getElementById("spnMessageSuccess" + index).style.display = "block";
                    document.getElementById("spnMessageSuccessText" + index).innerHTML = orderQty;
                    document.getElementById("orderQty" + index).value = "";
                }
                else {
                    document.getElementById("divErrorMessage").innerHTML = "Error occurred";
                    document.getElementById("spnMessageError" + index).style.display = "block";
                    document.getElementById("spnMessageErrorText" + index).innerHTML = "Error occurred";
                }
                if (itemIdNull) {
                    document.getElementById("itemId" + index).innerText = "";
                    document.getElementById("spnMessageItem" + index).innerText = "Select item from below";
                }
            },
            error: function (xhr, exception) {
                $('#loadingModal').modal('hide');
                console.log("addToCartGet1_onclick", "00099000", "ERROR???");
                console.log("addToCartGet1_onclick", "00099100", exception, xhr);
                var jsonData = JSON.parse(xhr.responseText);
                document.getElementById("divErrorMessage").innerHTML = "Error occurred";
                document.getElementById("spnMessageError" + index).style.display = "block";
                document.getElementById("spnMessageErrorText" + index).innerHTML = "Error occurred";
            }
        });
    }
    catch (err) {
        console.log(err);
        $('#loadingModal').modal('hide');
        if (errorMessage === "") {
            errorMessage = "Error while adding to cart";
        }
        document.getElementById("divErrorMessage").innerHTML = errorMessage;
        document.getElementById("spnMessageError" + index).style.display = "block";
        document.getElementById("spnMessageErrorText" + index).innerHTML = errorMessage;
    }
    return false;
}
function addToCartGet2_onclick(itemId, index) {
    var returnValue = true, errorMessage = "";
    var itemIdMain = document.getElementById("itemId" + index).innerText;
    var orderQtyMain = document.getElementById("orderQty" + index).value;
    console.log("addToCartGet2_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    try {
        document.getElementById("divErrorMessage").innerHTML = "";
        document.getElementById("spnMessageSuccessText" + index).innerHTML = "";
        document.getElementById("spnMessageSuccess" + index).style.display = "none";
        document.getElementById("spnMessageErrorText" + index).innerHTML = "";
        document.getElementById("spnMessageError" + index).style.display = "none";
        var maxLength = document.getElementById("orderQty" + index).getAttribute("maxlength");
        var minValue = document.getElementById("orderQty" + index).getAttribute("min");
        var maxValue = document.getElementById("orderQty" + index).getAttribute("max");
        if ((/^\d+$/.test(itemId))) {//itemId is a number
            ;
        }
        else {
            errorMessage = "Select valid item";
            returnValue = false;
        }
        if ((/^\d+$/.test(orderQtyMain)) && orderQtyMain.length <= maxLength && orderQtyMain >= minValue && orderQtyMain <= maxValue) {//Valid orderQtyMain
            var itemBundleId, discountPercent, itemBundleItemId, quantity, itemRateBeforeDiscount, itemRateAfterDiscount, itemTypeId, orderQty;
            itemBundleId = document.getElementById("itemBundleId").innerText;
            discountPercent = document.getElementById("discountPercent").innerText;
            var jsonPostData = {};
            var shoppingCartItemBundleModels = [];
            var shoppingCartItemBundleModel, orderQtyHtmlElement, itemId, orderQty, orderComments = "";
            for (var i = 0; ; i++) {
                orderQtyHtmlElement = document.getElementById("orderQty" + i);
                if (orderQtyHtmlElement == null) {
                    break;
                }
                else {
                    orderQty = document.getElementById("orderQty" + i).value;
                    itemBundleItemId = document.getElementById("itemBundleItemId" + i).innerText;
                    quantity = document.getElementById("quantity" + i).innerText;
                    itemTypeId = document.getElementById("itemTypeId" + i).innerText;
                    if ((/^\d+$/.test(orderQty)) && orderQty.length <= document.getElementById("orderQty" + i).getAttribute("maxlength") && orderQty >= document.getElementById("orderQty" + i).getAttribute("min") && orderQty <= document.getElementById("orderQty" + i).getAttribute("max")) {
                        itemBundleId = document.getElementById("itemBundleId" + i).innerText;
                        itemBundleItemId = document.getElementById("itemBundleItemId" + i).innerText;
                        itemId = document.getElementById("itemId" + i).innerText;
                        if (itemId.trim() === "") {
                            document.getElementById("spnMessageErrorText" + index).innerHTML = "Select valid item";
                            document.getElementById("spnMessageError" + index).style.display = "block";
                            returnValue = false;
                            break;
                        }
                        else {
                            orderComments = "";//document.getElementById("orderComments" + i).value;
                            shoppingCartItemBundleModel = {};
                            shoppingCartItemBundleModel.ItemBundleId = itemBundleId;
                            shoppingCartItemBundleModel.ItemBundleItemId = itemBundleItemId;
                            shoppingCartItemBundleModel.ItemId = itemId;
                            shoppingCartItemBundleModel.ItemTypeId = itemTypeId;
                            shoppingCartItemBundleModel.OrderQty = orderQty;
                            shoppingCartItemBundleModel.OrderComments = orderComments;
                            shoppingCartItemBundleModel.Quantity = quantity;
                            shoppingCartItemBundleModels.push(shoppingCartItemBundleModel);
                        }
                    }
                }
            }
        }
        else {
            if (errorMessage != "") {
                errorMessage += "<br />";
            }
            errorMessage += "Invalid order quantity";
            returnValue = false;
        }
        if (returnValue) {
            jsonPostData.ItemBundleId = itemBundleId;
            jsonPostData.DiscountPercent = discountPercent;
            jsonPostData.ItemId = itemIdMain;
            jsonPostData.OrderQty = orderQtyMain;
            jsonPostData.OrderComments = orderComments;
            jsonPostData.ShoppingCartItemBundleModels = shoppingCartItemBundleModels;
            var jsonPostDataString = JSON.stringify(jsonPostData);
            console.log(jsonPostData);
            var url = "/Home/AddToCart2/";
            $('#loadingModal').modal('hide');
            $.ajax({
                url: url,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
                //dataType: "json",
                data: jsonPostDataString,//{ "itemId": itemId, "orderQty": orderQty },
                async: true,
                success: function (responseData, textStatus, request) {
                    $('#loadingModal').modal('hide');
                    console.log("addToCartGet2_onclick", "00090000", "SUCCESS!!!");
                    document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                    document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                    document.getElementById("orderQty" + index).value = "";
                    document.getElementById("spnMessageSuccess" + index).style.display = "block";
                    document.getElementById("spnMessageSuccessText" + index).innerHTML = orderQty;
                    resetOrderQtyToMinQty();
                },
                error: function (xhr, exception) {
                    $('#loadingModal').modal('hide');
                    console.log("addToCartGet2_onclick", "00099000", "ERROR???");
                    console.log(exception, xhr);
                    var jsonData = JSON.parse(xhr.responseText);
                    document.getElementById("divErrorMessage").innerHTML = jsonData.errorMessage;
                    document.getElementById("spnMessageError" + index).style.display = "block";
                    document.getElementById("spnMessageErrorText" + index).innerHTML = jsonData.errorMessage;
                }
            });
        }
        else {
            $('#loadingModal').modal('hide');
            document.getElementById("spnMessageError" + index).style.display = "block";
            document.getElementById("spnMessageErrorText" + index).innerHTML = errorMessage;
        }
    }
    catch (err) {
        $('#loadingModal').modal('hide');
        alert("Please fix errors to continue???");
        console.log(err);
        returnValue = false;
    }
    return returnValue;
}
function resetOrderQtyToMinQty() {
    document.getElementById("orderQty").value = "";
    for (var i = 0; ; i++) {
        orderQtyHtmlElement = document.getElementById("orderQty" + i);
        if (orderQtyHtmlElement == null) {
            break;
        }
        else {
            document.getElementById("orderQty" + i).value = document.getElementById("quantity" + i).innerText;
        }
    }
    return false;
}
function validateItemIdOrderQty(itemId, orderQty, returnObject, index, errorMessage) {
    console.log("validateItemIdOrderQty", "00000000", "ENTER!!!");
    var returnValue = true, errorMessage = "", itemIdNull;
    try {
        console.log(1, itemId, index);
        //var a = 0, b = 1, c;
        //c = b / a;
        if (itemId === '' || itemId === null || itemId === undefined) {
            itemIdNull = true;
            itemId = document.getElementById("itemId" + index).innerText;
            console.log(2, itemId, itemIdNull, index);
        }
        else {
            itemIdNull = false;
        }
        returnObject.itemIdNull = itemIdNull;
        orderQty = document.getElementById("orderQty" + index).value;
        console.log(3, itemId, index, orderQty);
        document.getElementById("divErrorMessage").innerHTML = "";
        document.getElementById("spnMessageSuccessText" + index).innerHTML = "";
        document.getElementById("spnMessageSuccess" + index).style.display = "none";
        document.getElementById("spnMessageErrorText" + index).innerHTML = "";
        document.getElementById("spnMessageError" + index).style.display = "none";
        if ((/^\d+$/.test(itemId))) {//itemId is a number
            ;
        }
        else {
            errorMessage = "Select valid item from below";
            returnValue = false;
            console.log(3.1, returnValue, errorMessage);
        }
        //Test if the input is 1. numeric 2. not exceed maxlength 3. between min and max values
        if ((/^\d+$/.test(orderQty)) && orderQty.length <= document.getElementById("orderQty" + index).getAttribute("maxlength") && orderQty >= document.getElementById("orderQty" + index).getAttribute("min") && orderQty <= document.getElementById("orderQty" + index).getAttribute("max")) {
        }
        else {
            if (errorMessage != "") {
                errorMessage += "<br />";
            }
            errorMessage += "Invalid order quantity";
            returnValue = false;
        }
        console.log(4, returnValue, errorMessage);
        if (returnValue) {
            returnObject.itemId = itemId;
            returnObject.orderQty = orderQty;
        }
        else {
            console.log("validateItemIdOrderQty", "00001000", "ERROR???", errorMessage);
            document.getElementById("divErrorMessage").innerHTML = errorMessage;
            document.getElementById("spnMessageError" + index).style.display = "block";
            document.getElementById("spnMessageErrorText" + index).innerHTML = errorMessage;
        }
    }
    catch (err) {
        alert(err);
        console.log("validateItemIdOrderQty", "00090000", "ERROR???");
        console.log(err);
        if (errorMessage === "") {
            errorMessage = "Error while adding to cart";
        }
        document.getElementById("divErrorMessage").innerHTML = errorMessage;
        document.getElementById("spnMessageError" + index).style.display = "block";
        document.getElementById("spnMessageErrorText" + index).innerHTML = errorMessage;
    }
    return returnValue;
}
function categoryId_onclick(categoryId, pageNum) {
    console.log("categoryId_onclick", "00000000", "ENTER!!!", categoryId);
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    var url = "/Home/OrderCategoryItem" + "?id=" + categoryId + "&pageNum=" + pageNum;
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("00001000", "categoryId_onclick success", responseData.processMessage);
            if (responseData.success) {
                document.getElementById("selectedCategoryDesc").innerHTML = "";
                var menuItemObject, menuItemObjectA, menuCategoryIdObject, menuCategoryIdObjectA;
                for (var i = 1; i <= categoryCount; i++) {
                    menuItemObject = document.getElementById("menuItem" + i);
                    menuItemObjectA = document.getElementById("menuItemA" + i);
                    menuCategoryIdObject = document.getElementById("menuCategoryId" + i);
                    menuCategoryIdObjectA = document.getElementById("menuCategoryIdA" + i);
                    if (menuCategoryIdObject.innerHTML == categoryId || menuCategoryIdObjectA.innerHTML == categoryId) {
                        menuItemObject.className = "active";
                        menuItemObjectA.className = "active";
                        document.getElementById("selectedCategoryDesc").innerHTML = menuItemObject.innerHTML;
                    }
                    else {
                        menuItemObject.className = "";
                        menuItemObjectA.className = "";
                    }
                }
                document.getElementById("divOrderItem").innerHTML = responseData.htmlString;
                document.getElementById("divScrollIntoView").scrollIntoView();
            }
            else {
                document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
            }
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("categoryId_onclick", "00099000", "ERROR???");
            document.getElementById("divErrorMessage").innerHTML = "Error occurred";
            console.log(xhr, exception);
        }
    });
}
function checkoutValidate_onclick() {
    console.log("00000000", "checkoutValidate_onclick", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    var url = "/Home/CheckoutValidate";
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (responseData, textStatus, request) {
            console.log(responseData);
            $('#loadingModal').modal('hide');
            console.log("00001000", "checkoutValidate_onclick success", responseData.processMessage);
            if (responseData.success) {
                url = "/Checkout";
                var id;
                try {
                    id = parseInt(document.getElementById("OrderCreatedForPersonId").value);
                    if (isNumber2(id)) {
                        url += "?id=" + id;
                    }
                    else {
                        url += "/";
                    }
                }
                catch (err) {
                    url += "/";
                }
                window.location.href = url;
                document.getElementById("divScrollIntoView").scrollIntoView();
            }
            else {
                document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
                document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                document.getElementById("divScrollIntoView").scrollIntoView();
                alert(responseData.htmlString);
            }
            console.log("00090000", "checkoutValidate_onclick success", "Exit");
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("checkoutValidate_onclick", "00099000", "ERROR???");
            document.getElementById("divErrorMessage").innerHTML = "Error during checkout";
            document.getElementById("shoppingCartItemsCount").innerHTML = "";
            document.getElementById("shoppingCartTotalAmount").innerHTML = "0.00";
            document.getElementById("divScrollIntoView").scrollIntoView();
            alert("Error during checkout");
        }
    });
    return false;
}
function orderComments_onchange(index) {
    console.log("orderComments_onchange", "00000000", "ENTER!!!");
    document.getElementById("divErrorMessage").innerHTML = "";
    try {
        var orderComments = document.getElementById("orderComments" + index).value;
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
                console.log("orderComments_onchange", "00090000", "SUCCESS!!!", responseData.success);
            },
            error: function (xhr, exception) {
                console.log("orderComments_onchange", "00099000", "ERROR???");
                console.log(exception, xhr);
                var jsonData = JSON.parse(xhr.responseText);
                document.getElementById("divErrorMessage").innerHTML = jsonData.errorMessage;
            }
        });
    }
    catch (err) {
        console.log("orderComments_onchange", "00099000", "ERROR???");
        console.log(err);
        document.getElementById("divErrorMessage").innerHTML = "Error while updating comments";
    }
    return false;
}
function orderQty_onblur(documentElementId) {
    document.getElementById(documentElementId).setAttribute("aria-expanded", false);
}
function orderQty_onfocus(documentElementId) {
    document.getElementById(documentElementId).setAttribute("aria-expanded", true);
}
function orderQty_oninput(index) {
    if (document.getElementById("orderQty" + index).value.length > document.getElementById("orderQty" + index).getAttribute("maxlength")) {
        document.getElementById("orderQty" + index).value = document.getElementById("orderQty" + index).value.substr(0, document.getElementById("orderQty" + index).getAttribute("maxlength"));
    }
}
function orderQty_oninput1() {
    var orderQty, orderQty1;
    orderQty = document.getElementById("orderQty").value;
    if ((/^\d+$/.test(orderQty))) {
        for (var i = 0; ; i++) {
            if (document.getElementById("orderQty" + i) == null) {
                break;
            }
            orderQty1 = document.getElementById("orderQty" + i).value;
            if ((/^\d+$/.test(orderQty1))) {
            }
            else {
                orderQty1 = 0;
            }
            document.getElementById("orderQty" + i).value = parseInt(orderQty1) * orderQty;
        }
    }
}
function orderQty_oninput2(orderQtyIndex, orderQtyIndexStart, orderQtyIndexFinish) {
    if (orderQtyIndexStart === '') {
        return false;
    }
    orderQtyIndexStart = parseInt(orderQtyIndexStart);
    orderQtyIndexFinish = parseInt(orderQtyIndexFinish);
    for (var i = orderQtyIndexStart; i <= orderQtyIndexFinish; i++) {
        document.getElementById("orderQty" + i).value = document.getElementById("quantity" + orderQtyIndex).innerText * document.getElementById("orderQty" + orderQtyIndex).value;
    }
}
function removeFromCart_onclick(index) {
    console.log("removeFromCart_onclick", "00000000", "ENTER!!!");
    document.getElementById("divErrorMessage").innerHTML = "";
    var jsonPostData =
    {
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
                console.log("removeFromCart_onclick", "00090000", "SUCCESS!!!");
                if (responseData.success) {
                    document.getElementById("divShoppingCartData").innerHTML = responseData.htmlString;
                    document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                    document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
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
        console.log("removeFromCart_onclick", "00099000", "ERROR???");
        console.log(err);
        document.getElementById("divErrorMessage").innerHTML = "Error while removing item";
    }
    return false;
}
function searchTermButton_onclick(searchTermElementId) {
    var indexOf = window.location.href.toUpperCase().indexOf("SEARCHRESULT");
    console.log("00000000", "searchText_onclick", searchTermElementId, "Enter");
    if (indexOf === -1) {
        window.open("/Home/SearchResult?id=" + document.getElementById(searchTermElementId).value, "_blank");
    }
    else {
        window.location.href = "/Home/SearchResult?id=" + document.getElementById(searchTermElementId).value;
    }
}
function shoppingCart_onclick() {
    console.log("showShoppingCart_onclick", "00000000", "ENTER!!!");
    var url = "/Home/ShoppingCart/";
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "json",
        //data: { "itemId": itemId, "orderQty": orderQty },
        async: true,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            console.log("showShoppingCart_onclick", "00090000", "SUCCESS!!!");
            console.log(responseData);
            document.getElementById("divShoppingCartData").innerHTML = responseData.htmlString;
            document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
            document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
            $('#divShoppingCartPopupModal').modal({ backdrop: 'static', keyboard: false });
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("showShoppingCart_onclick", "00099000", "ERROR???");
            console.log(exception, xhr);
            document.getElementById("divErrorMessage").innerHTML = "Error while getting shopping cart";
        }
    });
}
function shoppingCartSummary_onclick() {
    var url = "/Home/ShoppingCartSummary/";
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        async: true,
        success: function (responseData, textStatus, request) {
            var jsonData = JSON.parse(responseData);
            document.getElementById("shoppingCartItemsCount").innerHTML = jsonData.shoppingCartItemsCount;
            document.getElementById("shoppingCartTotalAmount").innerHTML = jsonData.shoppingCartTotalAmount;
        },
        error: function (xhr, exception) {
            console.log(43, "ERROR???", exception, xhr);
        }
    });
}
function tempFunction(itemId, itemRate, itemSpecs, itemIndex) {
    document.getElementById("spnMessageItem" + itemIndex).innerHTML = itemRate + " | " + itemSpecs;
    document.getElementById("itemId" + itemIndex).innerHTML = itemId;
    //document.getElementById("dropdownMenuButton" + itemIndex).innerHTML = itemRate + " | " + itemSpecs + '&nbsp;&nbsp;&nbsp;<span class="caret" style="color: #000000; font-size: 20px;"></span>';
}
function searchOrderCreatedForEmailAddress_onclick() {
    console.log("searchOrderCreatedForEmailAddress_onclick", "00000000", "ENTER!!!");
    var searchOrderCreatedForEmailAddressValue = document.getElementById("searchOrderCreatedForEmailAddress").value;
    searchOrderCreatedForEmailAddressValue = searchOrderCreatedForEmailAddressValue.trim();
    if (searchOrderCreatedForEmailAddressValue != "") {
        var url = "/Home/SearchForEmailAddress/" + searchOrderCreatedForEmailAddressValue;
        $.ajax({
            url: url,
            type: "GET",
            //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
            dataType: "json",
            //data: { "itemId": itemId, "orderQty": orderQty },
            async: true,
            success: function (responseData, textStatus, request) {
                $('#loadingModal').modal('hide');
                console.log("searchOrderCreatedForEmailAddress_onclick", "00001000", responseData.success, responseData.processMessage);
                if (responseData.success) {
                    document.getElementById("OrderCreatedForPersonId").innerHTML = responseData.htmlString;
                }
                else {
                    document.getElementById("divErrorMessage").innerHTML = "Error occurred";
                }
                console.log("searchOrderCreatedForEmailAddress_onclick", "00090000", "EXIT!!!");
            },
            error: function (xhr, exception) {
                $('#loadingModal').modal('hide');
                console.log("searchOrderCreatedForEmailAddress_onclick", "00099000", "ERROR???");
                var jsonData = JSON.parse(xhr.responseText);
                console.log("searchOrderCreatedForEmailAddress_onclick", "00099100", exception, xhr, jsonData);
                document.getElementById("divErrorMessage").innerHTML = "Error occurred";
            }
        });
    }
    console.log("searchOrderCreatedForEmailAddress_onclick", "00090000", "EXIT!!!");
    return false;
}
