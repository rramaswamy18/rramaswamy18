//Sriramajayam
//orderItemCode0.js
//function addToCart_onclick(itemId, elementIdSuffix) {
//    console.log("addToCart_onclick", "00000000", "ENTER!!!");
//    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
//    for (var i = 0; i < 50000; i++);
//    var orderQtyElement = document.getElementById("orderQty" + elementIdSuffix);
//    var orderQty = orderQtyElement.value;
//    console.log("addToCart_onclick", "00001000", "itemId", itemId, "elementIdSuffix", elementIdSuffix, "orderQty", orderQty);
//    var returnValue = true, errorMessage = "";
//    var returnValue = true, errorMessage = "";
//    try {
//        if ((/^\d+$/.test(itemId))) {//itemId is a number
//            ;
//        }
//        else {
//            errorMessage = "Select valid item";
//            returnValue = false;
//            console.log("addToCart_onclick", "00003000", returnValue, errorMessage);
//        }
//        //Test if the input is 1. numeric 2. not exceed maxlength 3. between min and max values
//        if ((/^\d+$/.test(orderQty)) && orderQty.length <= orderQtyElement.getAttribute("maxlength") && orderQty >= orderQtyElement.getAttribute("min") && orderQty <= orderQtyElement.getAttribute("max")) {
//        }
//        else {
//            if (errorMessage != "") {
//                errorMessage += "<br />";
//            }
//            errorMessage += "Enter quantity";
//            returnValue = false;
//        }
//        if (returnValue) {
//            document.getElementById("divErrorMessage").innerHTML = "";
//            document.getElementById("spnMessageError" + elementIdSuffix).style.display = "none";
//            document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = "";
//            document.getElementById("spnMessageError" + elementIdSuffix).style.display = "none";
//            document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = "";
//            var url = "/Home/AddToCart/";
//            $.ajax({
//                url: url,
//                type: "GET",
//                //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
//                dataType: "json",
//                data: { "itemId": itemId, "orderQty": orderQty },
//                async: true,
//                success: function (responseData, textStatus, request) {
//                    $('#loadingModal').modal('hide');
//                    //console.log("addToCart_onclick", "00090000", "SUCCESS!!!", responseData);
//                    console.log("addToCart_onclick", "00090000", "SUCCESS!!!");
//                    if (responseData.success) {
//                        shoppingCartSummary(responseData.shoppingCartItemsCount, -1, responseData.shoppingCartTotalAmount);
//                        document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
//                        document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
//                        document.getElementById("shoppingCartItemsCount1").innerHTML = responseData.shoppingCartItemsCount;
//                        document.getElementById("shoppingCartTotalAmount1").innerHTML = responseData.shoppingCartTotalAmount;
//                        document.getElementById("spnMessageSuccess" + elementIdSuffix).style.display = "block";
//                        document.getElementById("spnMessageSuccessText" + elementIdSuffix).innerHTML = orderQty;
//                        orderQtyElement.value = "";
//                        document.getElementById("spnMessageSuccess" + elementIdSuffix).style.display = "block";
//                        document.getElementById("spnMessageSuccessText" + elementIdSuffix).innerHTML = orderQty;
//                        document.getElementById("divShoppingCart").innerHTML = responseData.htmlString;
//                    }
//                    else {
//                        document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
//                        document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
//                        document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = responseData.htmlString;
//                        document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
//                        document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = responseData.htmlString;
//                    }
//                },
//                error: function (xhr, exception) {
//                    $('#loadingModal').modal('hide');
//                    console.log("addToCart_onclick", "00099100", exception, xhr);
//                    var jsonData = JSON.parse(xhr.responseText);
//                    document.getElementById("divErrorMessage").innerHTML = "Error occurred";
//                    document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
//                    document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = "Error occurred";
//                    document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
//                    document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = "Error occurred";
//                }
//            });
//        }
//        else {
//            $('#loadingModal').modal('hide');
//            document.getElementById("divErrorMessage").innerHTML = errorMessage;
//            document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
//            document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = errorMessage;
//            document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
//            document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = errorMessage;
//            //addToCartPopup_onclick(itemMasterId);
//            return false;
//        }
//    }
//    catch (err) {
//        console.log(err);
//        $('#loadingModal').modal('hide');
//        if (errorMessage === "") {
//            errorMessage = "Error while adding to cart";
//        }
//        document.getElementById("divErrorMessage").innerHTML = errorMessage;
//        document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
//        document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = errorMessage;
//        document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
//        document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = errorMessage;
//    }
//}
//function addToCart2_onclick(index) {
//    var returnValue = true, errorMessage = "";
//    console.log("addToCartGet2_onclick", "00000000", "ENTER!!!");
//    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
//    document.getElementById("divErrorMessage").innerHTML = "";
//    try {
//        var itemId = document.getElementById("itemId" + index).value;
//        var orderQtyHtmlElement = document.getElementById("orderQty" + index);
//        var orderQty = orderQtyHtmlElement.value;
//        console.log(-9, itemId, orderQty, index);
//        document.getElementById("divErrorMessage").innerHTML = "";
//        document.getElementById("spnMessageSuccess" + index).style.display = "none";
//        document.getElementById("spnMessageSuccessText" + index).innerHTML = "";
//        document.getElementById("spnMessageError" + index).style.display = "none";
//        document.getElementById("spnMessageErrorText" + index).innerHTML = "";
//        var maxLength = orderQtyHtmlElement.getAttribute("maxlength");
//        var minValue = orderQtyHtmlElement.getAttribute("min");
//        var maxValue = orderQtyHtmlElement.getAttribute("max");
//        if ((/^\d+$/.test(itemId))) {//itemId is a number
//            ;
//        }
//        else {
//            errorMessage = "Select valid item";
//            returnValue = false;
//        }
//        if ((/^\d+$/.test(orderQty)) && orderQty.length <= maxLength && orderQty >= minValue && orderQty <= maxValue) {//Valid orderQtyMain
//            var shoppingCartItemBundleModels = [];
//            var shoppingCartItemBundleModel;
//            var jsonPostData = {};
//            jsonPostData.ItemId = itemId;
//            jsonPostData.OrderQty = orderQty;
//            for (var i = 0; ; i++) {
//                orderQtyHtmlElement = document.getElementById("orderQtyForBundle" + i);
//                if (orderQtyHtmlElement == null) {
//                    break;
//                }
//                else {
//                    orderQty = orderQtyHtmlElement.value;
//                    if ((/^\d+$/.test(orderQty)) && orderQty.length <= orderQtyHtmlElement.getAttribute("maxlength") && orderQty >= orderQtyHtmlElement.getAttribute("min") && orderQty <= orderQtyHtmlElement.getAttribute("max")) {
//                        itemId = document.getElementById("itemIdForBundle" + i).value;
//                        if (itemId.trim() === "") {
//                            document.getElementById("spnMessageErrorForBundle" + index).style.display = "block";
//                            document.getElementById("spnMessageErrorTextForBundle" + index).innerHTML = "Select valid item";
//                            returnValue = false;
//                            break;
//                        }
//                        else {
//                            shoppingCartItemBundleModel = {};
//                            shoppingCartItemBundleModel.ItemId = itemId;
//                            shoppingCartItemBundleModel.OrderQty = orderQty;
//                            shoppingCartItemBundleModels.push(shoppingCartItemBundleModel);
//                        }
//                    }
//                }
//            }
//            jsonPostData.ShoppingCartItemBundleModels = shoppingCartItemBundleModels;
//        }
//        else {
//            if (errorMessage != "") {
//                errorMessage += "<br />";
//            }
//            errorMessage += "Invalid order quantity";
//            returnValue = false;
//        }
//        console.log(returnValue,jsonPostData);
//        if (returnValue) {
//            var jsonPostDataString = JSON.stringify(jsonPostData);
//            $('#loadingModal').modal('hide');
//            var url = "/Home/AddToCart2/";
//            $.ajax({
//                url: url,
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
//                //dataType: "json",
//                data: jsonPostDataString,//{ "itemId": itemId, "orderQty": orderQty },
//                async: true,
//                success: function (responseData, textStatus, request) {
//                    $('#loadingModal').modal('hide');
//                    console.log("addToCartGet2_onclick", "00090000", "SUCCESS!!!");
//                    document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
//                    document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
//                    document.getElementById("shoppingCartItemsCount1").innerHTML = responseData.shoppingCartItemsCount;
//                    document.getElementById("shoppingCartTotalAmount1").innerHTML = responseData.shoppingCartTotalAmount;
//                    document.getElementById("orderQty" + index).value = "";
//                    document.getElementById("spnMessageSuccess" + index).style.display = "block";
//                    document.getElementById("spnMessageSuccessText" + index).innerHTML = orderQty;
//                    //resetOrderQtyToMinQty();
//                },
//                error: function (xhr, exception) {
//                    $('#loadingModal').modal('hide');
//                    console.log("addToCartGet2_onclick", "00099000", "ERROR???");
//                    console.log(exception, xhr);
//                    var jsonData = JSON.parse(xhr.responseText);
//                    document.getElementById("divErrorMessage").innerHTML = jsonData.errorMessage;
//                    document.getElementById("spnMessageError" + index).style.display = "block";
//                    document.getElementById("spnMessageErrorText" + index).innerHTML = jsonData.errorMessage;
//                }
//            });
//        }
//        else {
//            $('#loadingModal').modal('hide');
//            document.getElementById("spnMessageError" + index).style.display = "block";
//            document.getElementById("spnMessageErrorText" + index).innerHTML = errorMessage;
//        }
//    }
//    catch (err) {
//        $('#loadingModal').modal('hide');
//        alert("Please fix errors to continue???");
//        console.log(err);
//        returnValue = false;
//    }
//    return returnValue;
//}
//function addToCart2_onclickBackup(index) {
//    var returnValue = true, errorMessage = "";
//    var itemIdMain = document.getElementById("itemId" + index).innerText;
//    var orderQtyMain = document.getElementById("orderQty" + index).value;
//    console.log("addToCartGet2_onclick", "00000000", "ENTER!!!");
//    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
//    document.getElementById("divErrorMessage").innerHTML = "";
//    try {
//        document.getElementById("divErrorMessage").innerHTML = "";
//        document.getElementById("spnMessageSuccessForBundle" + index).style.display = "none";
//        document.getElementById("spnMessageSuccessTextForBundle" + index).innerHTML = "";
//        document.getElementById("spnMessageErrorForBundle" + index).style.display = "none";
//        document.getElementById("spnMessageErrorTextForBundle" + index).innerHTML = "";
//        var maxLength = document.getElementById("orderQtyForBundle" + index).getAttribute("maxlength");
//        var minValue = document.getElementById("orderQtyForBundle" + index).getAttribute("min");
//        var maxValue = document.getElementById("orderQtyForBundle" + index).getAttribute("max");
//        if ((/^\d+$/.test(itemIdMain))) {//itemIdMain is a number
//            ;
//        }
//        else {
//            errorMessage = "Select valid item";
//            returnValue = false;
//        }
//        if ((/^\d+$/.test(orderQtyMain)) && orderQtyMain.length <= maxLength && orderQtyMain >= minValue && orderQtyMain <= maxValue) {//Valid orderQtyMain
//            var itemBundleId, itemBundleItemId, quantity, itemTypeId;
//            var jsonPostData = {};
//            var shoppingCartItemBundleModels = [];
//            var shoppingCartItemBundleModel, orderQtyHtmlElement, itemId, orderQty, orderComments = "", orderQtyForBundleElement;
//            for (var i = 0; ; i++) {
//                orderQtyHtmlElement = document.getElementById("orderQtyForBundle" + i);
//                if (orderQtyHtmlElement == null) {
//                    break;
//                }
//                else {
//                    orderQty = document.getElementById("orderQtyForBundle" + i).value;
//                    itemBundleItemId = document.getElementById("itemBundleItemId" + i).innerText;
//                    quantity = document.getElementById("quantity" + i).innerText;
//                    itemTypeId = document.getElementById("itemTypeId" + i).innerText;
//                    orderQtyForBundleElement = document.getElementById("orderQtyForBundle" + i);
//                    if ((/^\d+$/.test(orderQty)) && orderQty.length <= orderQtyForBundleElement.getAttribute("maxlength") && orderQty >= orderQtyForBundleElement.getAttribute("min") && orderQty <= orderQtyForBundleElement.getAttribute("max")) {
//                        itemBundleId = document.getElementById("itemBundleId" + i).innerText;
//                        itemBundleItemId = document.getElementById("itemBundleItemId" + i).innerText;
//                        itemId = document.getElementById("itemIdForBundle" + i).innerText;
//                        if (itemId.trim() === "") {
//                            document.getElementById("spnMessageErrorText" + index).innerHTML = "Select valid item";
//                            document.getElementById("spnMessageError" + index).style.display = "block";
//                            returnValue = false;
//                            break;
//                        }
//                        else {
//                            orderComments = "";//document.getElementById("orderComments" + i).value;
//                            shoppingCartItemBundleModel = {};
//                            shoppingCartItemBundleModel.ItemBundleId = itemBundleId;
//                            shoppingCartItemBundleModel.ItemBundleItemId = itemBundleItemId;
//                            shoppingCartItemBundleModel.ItemId = itemId;
//                            shoppingCartItemBundleModel.ItemTypeId = itemTypeId;
//                            shoppingCartItemBundleModel.OrderQty = orderQty;
//                            shoppingCartItemBundleModel.OrderComments = orderComments;
//                            shoppingCartItemBundleModel.Quantity = quantity;
//                            shoppingCartItemBundleModels.push(shoppingCartItemBundleModel);
//                        }
//                    }
//                }
//            }
//        }
//        else {
//            if (errorMessage != "") {
//                errorMessage += "<br />";
//            }
//            errorMessage += "Invalid order quantity";
//            returnValue = false;
//        }
//        if (returnValue) {
//            jsonPostData.ItemBundleId = itemBundleId;
//            //jsonPostData.DiscountPercent = discountPercent;
//            jsonPostData.ItemId = itemIdMain;
//            jsonPostData.OrderQty = orderQtyMain;
//            jsonPostData.OrderComments = orderComments;
//            jsonPostData.ShoppingCartItemBundleModels = shoppingCartItemBundleModels;
//            var jsonPostDataString = JSON.stringify(jsonPostData);
//            console.log(jsonPostData);
//            $('#loadingModal').modal('hide');
//            var url = "/Home/AddToCart2/";
//            $.ajax({
//                url: url,
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
//                //dataType: "json",
//                data: jsonPostDataString,//{ "itemId": itemId, "orderQty": orderQty },
//                async: true,
//                success: function (responseData, textStatus, request) {
//                    $('#loadingModal').modal('hide');
//                    console.log("addToCartGet2_onclick", "00090000", "SUCCESS!!!");
//                    document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
//                    document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
//                    document.getElementById("shoppingCartItemsCount1").innerHTML = responseData.shoppingCartItemsCount;
//                    document.getElementById("shoppingCartTotalAmount1").innerHTML = responseData.shoppingCartTotalAmount;
//                    document.getElementById("orderQty" + index).value = "";
//                    document.getElementById("spnMessageSuccess" + index).style.display = "block";
//                    document.getElementById("spnMessageSuccessText" + index).innerHTML = orderQty;
//                    //resetOrderQtyToMinQty();
//                },
//                error: function (xhr, exception) {
//                    $('#loadingModal').modal('hide');
//                    console.log("addToCartGet2_onclick", "00099000", "ERROR???");
//                    console.log(exception, xhr);
//                    var jsonData = JSON.parse(xhr.responseText);
//                    document.getElementById("divErrorMessage").innerHTML = jsonData.errorMessage;
//                    document.getElementById("spnMessageError" + index).style.display = "block";
//                    document.getElementById("spnMessageErrorText" + index).innerHTML = jsonData.errorMessage;
//                }
//            });
//        }
//        else {
//            $('#loadingModal').modal('hide');
//            document.getElementById("spnMessageError" + index).style.display = "block";
//            document.getElementById("spnMessageErrorText" + index).innerHTML = errorMessage;
//        }
//    }
//    catch (err) {
//        $('#loadingModal').modal('hide');
//        alert("Please fix errors to continue???");
//        console.log(err);
//        returnValue = false;
//    }
//    return returnValue;
//}
//function addToCartPopup_onclick(itemMasterId) {
//    console.log("addToCartPopup_onclick", "00000000", "ENTER!!!");
//    $('#divAddToCartPopupModal' + itemMasterId).modal({ backdrop: 'static', keyboard: false });
//}
function addToCart_onclick(itemId, elementIdSuffix, defaultValue, doNotBreakBundleParm, itemBundleFlag) {
    console.log("Ummachi", itemId, elementIdSuffix, defaultValue, doNotBreakBundleParm, itemBundleFlag);
    console.log("addToCart_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var orderQtyElement = document.getElementById("orderQty" + elementIdSuffix);
    var orderQty = orderQtyElement.value;
    console.log("addToCart_onclick", "00001000", "itemId", itemId, "elementIdSuffix", elementIdSuffix, "defaultValue", defaultValue, "defaultValue=null", (defaultValue == null), "orderQty", orderQty, "maxlength", orderQtyElement.getAttribute("maxlength"), "min", orderQtyElement.getAttribute("min"), "max", orderQtyElement.getAttribute("max"), "maxlength", orderQty.length <= orderQtyElement.getAttribute("maxlength"), "min", orderQty <= orderQtyElement.getAttribute("min"), "max", orderQty <= orderQtyElement.getAttribute("max"));
    var returnValue = true, errorMessage = "";
    try {
        if ((/^\d+$/.test(itemId))) {//itemId is a number
            ;
        }
        else {
            errorMessage = "Select valid item";
            returnValue = false;
            console.log("addToCart_onclick", "00003000", returnValue, errorMessage);
        }
        //Test if the input is 1. numeric 2. not exceed maxlength 3. between min and max values
        if ((/^\d+$/.test(orderQty)) && orderQty.length <= orderQtyElement.getAttribute("maxlength") && orderQty >= orderQtyElement.getAttribute("min") && orderQty <= orderQtyElement.getAttribute("max")) {
            var shoppingCartItemBundleModels = [];
            var shoppingCartItemBundleModel;
            var jsonPostData = {};
            jsonPostData.ItemIdParm = itemId;
            jsonPostData.OrderQtyParm = orderQty;
            jsonPostData.DoNotBreakBundleParm = doNotBreakBundleParm;
            //if (!doNotBreakBundleParm) {
            if (itemBundleFlag) {
                for (var i = 0; ; i++) {
                    orderQtyHtmlElement = document.getElementById("orderQtyForBundle" + i);
                    if (orderQtyHtmlElement == null) {
                        break;
                    }
                    else {
                        if (doNotBreakBundleParm) {
                            orderQty = 1;
                            shoppingCartItemBundleModel = {};
                            shoppingCartItemBundleModel.ItemIdParm = itemId;
                            shoppingCartItemBundleModel.OrderQtyParm = orderQty;
                            shoppingCartItemBundleModels.push(shoppingCartItemBundleModel);
                        }
                        else {
                            orderQty = orderQtyHtmlElement.value;
                            if ((/^\d+$/.test(orderQty)) && orderQty.length <= orderQtyHtmlElement.getAttribute("maxlength") && orderQty >= orderQtyHtmlElement.getAttribute("min") && orderQty <= orderQtyHtmlElement.getAttribute("max")) {
                                itemId = document.getElementById("itemIdForBundle" + i).innerText;
                                if (itemId.trim() === "") {
                                    document.getElementById("spnMessageErrorForBundle" + index).style.display = "block";
                                    document.getElementById("spnMessageErrorTextForBundle" + index).innerHTML = "Select valid item";
                                    returnValue = false;
                                    break;
                                }
                                else {
                                    shoppingCartItemBundleModel = {};
                                    shoppingCartItemBundleModel.ItemIdParm = itemId;
                                    shoppingCartItemBundleModel.OrderQtyParm = orderQty;
                                    shoppingCartItemBundleModels.push(shoppingCartItemBundleModel);
                                }
                            }
                        }
                    }
                }
            }
            jsonPostData.ShoppingCartItemBundleModels = shoppingCartItemBundleModels;
            console.log(9, jsonPostData);
        }
        else {
            if (errorMessage != "") {
                errorMessage += "<br />";
            }
            errorMessage += "Enter quantity";
            console.log(9.9, errorMessage);
            returnValue = false;
        }
        if (returnValue) {
            document.getElementById("divErrorMessage").innerHTML = "";
            document.getElementById("spnMessageError" + elementIdSuffix).style.display = "none";
            document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = "";
            document.getElementById("spnMessageError" + elementIdSuffix).style.display = "none";
            document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = "";
            var url = "/Home/AddToCart/";
            $.ajax({
                url: url,
                type: "POST",
                //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
                dataType: "json",
                data: jsonPostData,//{ "itemIdParm": itemId, "orderQtyParm": orderQty },
                async: true,
                success: function (responseData, textStatus, request) {
                    $('#loadingModal').modal('hide');
                    console.log("addToCart_onclick", "00090000", "SUCCESS!!!", responseData);
                    //console.log("addToCart_onclick", "00090000", "SUCCESS!!!");
                    if (responseData.success) {
                        shoppingCartSummary(responseData.shoppingCartItemsCount, -1, responseData.shoppingCartTotalAmount);
                        document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                        document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                        document.getElementById("shoppingCartItemsCount1").innerHTML = responseData.shoppingCartItemsCount;
                        document.getElementById("shoppingCartTotalAmount1").innerHTML = responseData.shoppingCartTotalAmount;
                        document.getElementById("spnMessageSuccess" + elementIdSuffix).style.display = "block";
                        document.getElementById("spnMessageSuccessText" + elementIdSuffix).innerHTML = orderQtyElement.value;
                        if (defaultValue != null) {
                            console.log("defaultValue != null", orderQtyElement.value, orderQty)
                            orderQtyElement.value = defaultValue;
                            console.log("defaultValue != null", orderQtyElement.value, orderQty)
                        }
                        else {
                            console.log("defaultValue == null", orderQtyElement.value, orderQty);
                        }
                        document.getElementById("divShoppingCart").innerHTML = responseData.htmlString;
                    }
                    else {
                        document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
                        document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
                        document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = responseData.htmlString;
                        document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
                        document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = responseData.htmlString;
                    }
                },
                error: function (xhr, exception) {
                    $('#loadingModal').modal('hide');
                    console.log("addToCart_onclick", "00099100", exception, xhr);
                    var jsonData = JSON.parse(xhr.responseText);
                    document.getElementById("divErrorMessage").innerHTML = "Error occurred";
                    document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
                    document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = "Error occurred";
                    document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
                    document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = "Error occurred";
                }
            });
        }
        else {
            $('#loadingModal').modal('hide');
            document.getElementById("divErrorMessage").innerHTML = errorMessage;
            document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
            document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = errorMessage;
            document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
            document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = errorMessage;
            //addToCartPopup_onclick(itemMasterId);
            return false;
        }
    }
    catch (err) {
        console.log(err);
        $('#loadingModal').modal('hide');
        if (errorMessage === "") {
            errorMessage = "Error while adding to cart";
        }
        document.getElementById("divErrorMessage").innerHTML = errorMessage;
        document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
        document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = errorMessage;
        document.getElementById("spnMessageError" + elementIdSuffix).style.display = "block";
        document.getElementById("spnMessageErrorText" + elementIdSuffix).innerHTML = errorMessage;
    }
}
function categoryId_onclick(categoryId, pageNum, categoryCount) {
    //console.log("categoryId_onclick", "00000000", "ENTER!!!", categoryId, pageNum);
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    document.getElementById("selectedCategoryId").value = categoryId;
    var url = "/Home/OrderItem" + "?id=" + categoryId + "&pageNum=" + pageNum;
    $.ajax({
        url: url,
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        //data: jsonPostDataString,
        success: function (responseData, textStatus, request) {
            $('#loadingModal').modal('hide');
            //console.log("00001000", "categoryId_onclick success", responseData.processMessage);
            if (responseData.success) {
                //setCategoryIdSelected(categoryId, categoryCount);
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                document.getElementById("divScrollIntoView").scrollIntoView();
            }
            else {
                document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
            }
        },
        error: function (xhr, exception) {
            ///$('#loadingModal').modal('hide');
            console.log("categoryId_onclick", "00099000", "ERROR???");
            document.getElementById("divErrorMessage").innerHTML = "Error occurred";
            console.log(xhr, exception);
        }
    });
}
function categoryIdPagination_onclick(pageNum) {
    console.log("categoryIdPagination_onclick", "00000000", "ENTER!!!", pageNum);
    var categoryId = document.getElementById("selectedCategoryId").value;
    categoryId_onclick(categoryId, pageNum);
    console.log("categoryIdPagination_onclick", "00090000", "EXIT!!!");
}
function checkoutLoginUser_onclick() {
    console.log("00000000", "checkoutLoginUser_onclick", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/Checkout";
    var postData = $("#formLoginUserProfData").serialize();
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                console.log("00000900", "checkoutLoginUser_onclick success");
                window.location = responseData.htmlString;
            }
            else {
                console.log("00000950", "checkoutLoginUser_onclick error");
                document.getElementById("formLoginUserProfData").innerHTML = responseData.htmlString;
                document.getElementById("divScrollIntoView").scrollIntoView();
                alert("Please fix error(s) and continue!!!");
            }
            console.log("00001000", "checkoutLoginUser_onclick success", responseData.processMessage);
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("formLoginUserProfData").innerHTML = xhr.responseText;
            document.getElementById("divScrollIntoView").scrollIntoView();
            alert("Please fix error(s) and continue!!!");
            console.log("00099000", "checkoutLoginUser_onclick", "ERROR???", exception, xhr);
        }
    });
}
function deliveryInfoSave_onclick() {
    console.log("00000000", "deliveryInfoSave_onclick");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/DeliveryInfo";
    var postData = $("#formDeliveryInfoData").serialize();
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            $("#loadingModal").modal('hide');
            $("#loadingModal").removeClass("in");
            console.log("00000500", "deliveryInfoSave_onclick success", responseData);
            if (responseData.success) {
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                shoppingCart_onclick();
            }
            else {
                if (responseData == '' || responseData.htmlString == "") {
                    alert(responseData.errorMessage);
                    window.location.href = responseData.redirectUrl;
                }
                else {
                    document.getElementById("formDeliveryInfoData").innerHTML = responseData.htmlString;
                    alert("Please fix errors to continue");
                }
            }
            document.getElementById("divScrollIntoView").scrollIntoView();
            console.log("00001000", "deliveryInfoSave_onclick success", responseData.processMessage);
        },
        error: function (xhr, exception) {
            console.log(exception, xhr);
            $("#loadingModal").modal('hide');
            document.getElementById("formDeliveryInfoData").innerHTML = xhr.responseText;
            document.getElementById("divScrollIntoView").scrollIntoView();
            alert("Please fix errors to continue");
            console.log("00099000", "deliveryInfoSave_onclick error", exception, xhr);
        }
    });
}
function deliveryMethodIdPickupLocationId_onchange(deliveryMethodElement) {
    for (var i = 1; i < deliveryMethodElement.options.length; i++) {
        document.getElementById("divPickupLocationAddress" + i).style.display = "none";
    }
    document.getElementById("divPickupLocationAddress" + deliveryMethodElement.selectedIndex).style.display = "block";
}
function orderComments_onchange(index, bundleIndex) {
    console.log("orderComments_onchange", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    console.log("orderComments_onchange", "00001000", index, bundleIndex);
    try {
        if (bundleIndex != -1) {
            index = index + "_" + bundleIndex;
        }
        var orderComments = document.getElementById("orderComments" + index).value;
        var url = "/Home/ShoppingCartComments";
        $.ajax({
            url: url,
            type: "GET",
            //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { "index": index, "bundleIndex": bundleIndex, "orderComments": orderComments },
            async: true,
            success: function (responseData, textStatus, request) {
                $('#loadingModal').modal('hide');
                console.log("orderComments_onchange", "00090000", "SUCCESS!!!", responseData.success);
                if (responseData.success) {

                }
                else {
                    document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
                    alert(responseData.htmlString);
                }
            },
            error: function (xhr, exception) {
                $('#loadingModal').modal('hide');
                console.log("orderComments_onchange", "00099000", "ERROR???", exception, xhr);
                var jsonData = JSON.parse(xhr.responseText);
                document.getElementById("divErrorMessage").innerHTML = jsonData.errorMessage;
            }
        });
    }
    catch (err) {
        $('#loadingModal').modal('hide');
        console.log("orderComments_onchange", "00099000", "ERROR???", err);
        document.getElementById("divErrorMessage").innerHTML = "Error while updating comments";
    }
    return false;
}
function orderQty_oninput(orderQtyElementObject, defaultValue) {
    try {
        if (orderQtyElementObject.value.length > orderQtyElementObject.getAttribute("maxlength")) {
            orderQtyElementObject.value = orderQtyElementObject.value.substr(0, orderQtyElementObject.getAttribute("maxlength"));
        }
        //Test if the input is 1. numeric 2. not exceed maxlength 3. between min and max values
        var orderQty = orderQtyElementObject.value;
        if ((/^\d+$/.test(orderQty)) && orderQty.length <= orderQtyElementObject.getAttribute("maxlength") && orderQty >= orderQtyElementObject.getAttribute("min") && orderQty <= orderQtyElementObject.getAttribute("max")) {
        }
        else {
            orderQtyElementObject.value = defaultValue;
        }
    }
    catch (err) {
        console.log(1, "orderQty_oninput ERROR???", err);
    }
    return false;
}
function orderQty2_oninput(orderQtyElementObject, defaultValue) {
    try {
        if (orderQtyElementObject.value.length > orderQtyElementObject.getAttribute("maxlength")) {
            orderQtyElementObject.value = orderQtyElementObject.value.substr(0, orderQtyElementObject.getAttribute("maxlength"));
        }
        //Test if the input is 1. numeric 2. not exceed maxlength 3. between min and max values
        var orderQty = orderQtyElementObject.value;
        if ((/^\d+(\.\d{1,2})?$/.test(orderQty)) && orderQty.length <= orderQtyElementObject.getAttribute("maxlength") && orderQty >= orderQtyElementObject.getAttribute("min") && orderQty <= orderQtyElementObject.getAttribute("max")) {
        }
        else {
            orderQtyElementObject.value = defaultValue;
        }
    }
    catch (err) {
        console.log(1, "orderQty_oninput ERROR???", err);
    }
    return false;
}
function orderQtyBundle_oninput(orderQtyElementObject, itemBundleCount, currencySymbol) {
    try {
        if (orderQtyElementObject.value.length > orderQtyElementObject.getAttribute("maxlength")) {
            orderQtyElementObject.value = orderQtyElementObject.value.substr(0, orderQtyElementObject.getAttribute("maxlength"));
        }
        //Test if the input is 1. numeric 2. not exceed maxlength 3. between min and max values
        var orderQty = orderQtyElementObject.value.trim();
        if ((/^\d+$/.test(orderQty)) && orderQty.length <= orderQtyElementObject.getAttribute("maxlength") && orderQty >= orderQtyElementObject.getAttribute("min") && orderQty <= orderQtyElementObject.getAttribute("max")) {
            calculateItemBundleRate(itemBundleCount, currencySymbol);
        }
        else {
            orderQtyElementObject.value = 0;
        }
    }
    catch (err) {
        console.log(1, "orderQty_oninput ERROR???", err);
    }
    return false;
}
function orderQtyBundleSetQty_onclick(itemBundleCount, currencySymbol) {
    var orderQtyBundleTempObject = document.getElementById("orderQtyBundleTemp");
    var orderQtyForBundleTemp;
    try {
        orderQtyForBundleTemp = parseInt(orderQtyBundleTempObject.value);
        console.log(9, orderQtyForBundleTemp);
        if (Number.isNaN(orderQtyForBundleTemp)) {
            orderQtyForBundleTemp = "";
        }
    }
    catch (err) {
        orderQtyForBundleTemp = "";
    }
    console.log(9.9, orderQtyForBundleTemp);
    if (orderQtyBundleTempObject.value.trim() === "" || ((/^\d+$/.test(orderQtyForBundleTemp)) && orderQtyBundleTempObject.value.length <= orderQtyBundleTempObject.getAttribute("maxlength") && orderQtyForBundleTemp >= orderQtyBundleTempObject.getAttribute("min") && orderQtyForBundleTemp <= orderQtyBundleTempObject.getAttribute("max"))) {
        for (var i = 0; i < itemBundleCount; i++) {
            document.getElementById("orderQtyForBundle" + i).value = orderQtyForBundleTemp;
        }
        calculateItemBundleRate(itemBundleCount, currencySymbol);
    }
    else {
    }
    return false;
}
function calculateItemBundleRate(itemBundleCount, currencySymbol) {
    var itemBundleItemRate = 0, itemBundleItemPiecesCount = 0, itemAmountForBundle, orderQtyForBundle = 0, orderQtyForBundleObject;
    for (var i = 0; i < itemBundleCount; i++) {
        orderQtyForBundleObject = document.getElementById("orderQtyForBundle" + i);
        orderQtyForBundle = parseInt(orderQtyForBundleObject.value);
        if ((/^\d+$/.test(orderQtyForBundle)) && orderQtyForBundleObject.value.length <= orderQtyForBundleObject.getAttribute("maxlength") && orderQtyForBundle >= orderQtyForBundleObject.getAttribute("min") && orderQtyForBundle <= orderQtyForBundleObject.getAttribute("max")) {
        }
        else {
            orderQtyForBundle = 0;
        }
        itemAmountForBundle = document.getElementById("itemRateForBundle" + i).innerText * orderQtyForBundle;
        itemBundleItemPiecesCount += orderQtyForBundle;
        document.getElementById("itemAmountForBundle" + i).innerHTML = currencySymbol + itemAmountForBundle.toFixed(2);
        itemBundleItemRate += itemAmountForBundle;
    }
    document.getElementById("itemBundleItemRate").innerText = currencySymbol + itemBundleItemRate.toFixed(2);
    document.getElementById("itemBundleItemPiecesCount").innerText = itemBundleItemPiecesCount;
    console.log(currencySymbol, "itemBundleItemRate", itemBundleItemRate, "itemBundleItemPiecesCount", itemBundleItemPiecesCount);
}
function paymentInfo1Save_onclick() {
    console.log("00000000", "paymentInfo1Save_onclick");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/PaymentInfo1";
    var postData = $("#formPaymentInfo1Data").serialize();
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            console.log("00000100", "paymentInfo1Save_onclick", responseData);
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                console.log("00000200", "paymentInfo1Save_onclick");
                //document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                //document.getElementById("divShoppingCart").innerHTML = "";
                //document.getElementById("loggedInUserFullName").innerHTML = responseData.loggedInUserFullName;
                //document.getElementById("loggedInUserEmailAddress").innerHTML = responseData.loggedInUserEmailAddress;
                //document.getElementById("shoppingCartItemsCount").innerHTML = "";
                //document.getElementById("shoppingCartTotalAmount").innerHTML = "";
                window.location.href = responseData.redirectUrl;
            }
            else {
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
            }
            console.log("00001000", "paymentSave_onclick success", responseData.processMessage);
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("divOrderProcess").innerHTML = xhr.responseText;
            console.log("00099000", "paymentInfo1Save_onclick error", exception, xhr);
        }
    });
}
function paymentInfo2Save_onclick() {
    console.log("paymentInfo2Save_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/PaymentInfo2";
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        //data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            //document.getElementById("formPaymentInfoData").innerHTML = responseData.processMessage;
            console.log("paymentInfo2Save_onclick", "00001000", "SUCCESS!!!");
            console.log(9, responseData);
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                console.log("paymentInfo2Save_onclick", "00002000", responseData.success, responseData.processMessage);
                var jsonObject = JSON.parse(responseData.htmlString);
                console.log(9.9, responseData.htmlString, jsonObject);
                var options = {
                    "key": jsonObject.RazorpayKey,
                    "amount": jsonObject.Amount,
                    "currency": jsonObject.Currency,
                    "name": jsonObject.Name,
                    "description": jsonObject.Description,
                    "image": "https://avatars.githubusercontent.com/u/65504583?v=4",
                    "order_id": jsonObject.OrderId,
                    "handler": function (response) {
                        console.log(9.18, response, response.razorpay_payment_id);
                        console.log(9.27, document.getElementById('razorpay_payment_id').value);
                        //document.getElementById(("formRazorPayReturn").action = "/Home/RazorPayReturn";
                        document.getElementById('razorpay_payment_id').value = response.razorpay_payment_id;
                        document.getElementById('razorpay_order_id').value = response.razorpay_order_id;
                        document.getElementById('razorpay_signature').value = response.razorpay_signature;
                        document.getElementById('rzp-paymentresponse').style.display = "block";
                        document.getElementById('rzp-paymentresponse').click();
                    },
                    "prefill": {
                        "name": jsonObject.Name,
                        "email": jsonObject.Email,
                        "contact": jsonObject.PhoneNumber
                    },
                    "notes": {
                        "address": jsonObject.Address
                    },
                    "theme": {
                        "color": "#F37254"
                    }
                };
                var rzp1 = new Razorpay(options);
                rzp1.open();
            }
            else {
                console.log("paymentInfo2Save_onclick", "00090900", "ERROR???");
                console.log(responseData);
                document.getElementById("formPaymentInfo1Data").innerHTML = responseData.htmlString;
                alert("Error occurred in Payment Gateway call");
            }
            console.log("00001000", "paymentInfo2Save_onclick success", responseData.processMessage);
            $("#loadingModal").modal('hide');
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            //document.getElementById("formPaymentInfoData").innerHTML = xhr.responseText;
            console.log("00099000", "paymentInfo2Save_onclick error", exception, xhr);
            alert("Error occurred in Payment Gateway call");
        }
    });
}
function paymentInfo4Save_onclick(creditCardKVPKeys, creditCardKVPValues) {
    console.log("00000000", "paymentInfo4Save_onclick", "ENTER!!!");
    console.log("00001000", "paymentInfo4Save_onclick", creditCardKVPKeys, creditCardKVPValues);
    //Render the payment form template
    var container = document.getElementById('stripe-payment-container');
    container.innerHTML = document.getElementById('stripe-form-template').innerHTML;

    //Stripe logic (initialize only after form is rendered)
    //In this case there is only one client key - Will loop through if there is more
    var stripe = Stripe(creditCardKVPValues);
    var elements = stripe.elements();
    console.log("00002000", "paymentInfo4Save_onclick", elements);

    var style = {
        base: {
            fontSize: '16px',
            color: '#32325d',
            '::placeholder': {
                color: '#aab7c4'
            }
        }
    };

    var cardNumber = elements.create('cardNumber', { style: style });
    cardNumber.mount('#card-number-element');
    console.log("00002100", "paymentInfo4Save_onclick", cardNumber);

    var cardExpiry = elements.create('cardExpiry', { style: style });
    cardExpiry.mount('#card-expiry-element');
    console.log("00002200", "paymentInfo4Save_onclick", cardExpiry);

    var cardCvc = elements.create('cardCvc', { style: style });
    cardCvc.mount('#card-cvc-element');
    console.log("00002300", "paymentInfo4Save_onclick", cardCvc);

    var form = document.getElementById('payment-form');
    console.log("00003000", "paymentInfo4Save_onclick", form);
    form.addEventListener('submit', function (event) {
        event.preventDefault();
        checkLoggedInStatus("1", "Home", "Index");
        var url = '/Home/PaymentInfo4Intent';
        //var postData = {};
        fetch(url, {
            method: 'POST',
            //data: postData,
        })
            .then(function (response) {
                console.log("paymentInfo4Save_onclick", "00004000", response);
                return response.json();
            })
            .then(function (paymentIntent) {
                console.log("paymentInfo4Save_onclick", "00005000", paymentIntent);
                if (paymentIntent.success) {
                    stripe.confirmCardPayment(paymentIntent.clientSecret, {
                        payment_method: {
                            card: cardNumber,
                        }
                    })
                        .then(function (result) {
                            console.log("paymentInfo4Save_onclick", "00002000", result.error, result);
                            if (result.error) {
                                document.getElementById("divErrorMessage").innerHTML = result.error.message;
                                document.getElementById("divErrorMessage2").innerHTML = result.error.message;
                                alert('Please fix errors to continue');
                            } else {
                                if (result.paymentIntent.status === 'succeeded') {
                                    //const paymentIntent = result.paymentIntent;
                                    var url = "/Home/PaymentInfo4Success" + "?paymentIntent_status=" + result.paymentIntent.status + "&paymentIntent_payment_method" + result.paymentIntent.payment_method + "&paymentIntent_id=" + result.paymentIntent.id;
                                    window.location.href = url;
                                }
                            }
                        });
                }
                else {
                    document.getElementById("divErrorMessage").innerHTML = paymentIntent.htmlString;
                    document.getElementById("divErrorMessage2").innerHTML = paymentIntent.htmlString;
                    alert('Please fix errors to continue');
                }
            });
    });
    console.log("00006000", "paymentInfo4Save_onclick", form);
    this.disabled = true;
    document.getElementById("paymentInfo4SaveButton").innerHTML = "";
    document.getElementById("divPayment0Info").innerHTML = "";
}
function paymentInfo9Save_onclick() {
    console.log("00000000", "paymentInfo9Save_onclick");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/PaymentInfo9";
    var postData = $("#formPaymentInfo1Data").serialize();
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            console.log("00001000", "paymentInfo9Save_onclick", responseData.success, responseData.processMessage);
            $("#loadingModal").modal('hide');
            document.getElementById("divPayment0Info").innerHTML = responseData.htmlString;
            shoppingCartSummary(responseData.shoppingCartItemsCount, -1, responseData.shoppingCartTotalAmount);
            document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
            document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
            document.getElementById("shoppingCartItemsCount1").innerHTML = responseData.shoppingCartItemsCount;
            document.getElementById("shoppingCartTotalAmount1").innerHTML = responseData.shoppingCartTotalAmount;
            document.getElementById("divShoppingCart").innerHTML = responseData.htmlStringShoppingCart;
            document.getElementById("paymentInfo9SaveButton").innerHTML = "";
            console.log("paymentInfo9Save_onclick", "00090000", "EXIT!!!");
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("divPayment0Info").innerHTML = xhr.responseText;
            console.log("00099000", "paymentInfo9Save_onclick error", exception, xhr);
        }
    });
}
function paymentInfo9SaveProcess_onclick() {
    console.log("00000000", "paymentInfo9SaveProcess_onclick");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/PaymentInfo9Process";
    var postData = $("#formPaymentInfo9Data").serialize();
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            console.log("00001000", "paymentInfo9SaveProcess_onclick");
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                console.log("00002000", "paymentInfo9SaveProcess_onclick");
                window.location.href = responseData.redirectUrl;
            }
            else {
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
            }
            console.log("00003000", "paymentInfo9SaveProcess_onclick success", responseData.processMessage);
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("divOrderProcess").innerHTML = xhr.responseText;
            console.log("00099000", "paymentInfo1Save_onclick error", exception, xhr);
        }
    });
}
function removeFromCart_onclick(index) {
    console.log("removeFromCart_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
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
                $('#loadingModal').modal('hide');
                console.log("removeFromCart_onclick", "00090000", "SUCCESS!!!", responseData);
                if (responseData.success) {
                    shoppingCartSummary(responseData.shoppingCartItemsCount, -1, responseData.shoppingCartTotalAmount);
                    document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                    document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                    document.getElementById("shoppingCartItemsCount1").innerHTML = responseData.shoppingCartItemsCount;
                    document.getElementById("shoppingCartTotalAmount1").innerHTML = responseData.shoppingCartTotalAmount;
                    document.getElementById("divShoppingCart").innerHTML = responseData.htmlString;
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
    return true;
}
function searchTermButton_onclick(searchTermElementId) {
    var indexOf = window.location.href.toUpperCase().indexOf("SEARCHRESULT");
    console.log("00000000", "searchText_onclick", searchTermElementId, "Enter");
    window.location.href = "/Home/SearchResult?id=" + document.getElementById(searchTermElementId).value;
    //if (indexOf === -1) {
    //    window.open("/Home/SearchResult?id=" + document.getElementById(searchTermElementId).value, "_blank");
    //}
    //else {
    //    window.location.href = "/Home/SearchResult?id=" + document.getElementById(searchTermElementId).value;
    //}
}
function shoppingCart_onclick(ev) {
    console.log("shoppingCart_onclick", "00000000", "ENTER!!!");
    var url = "/Home/ShoppingCart/";
    $.ajax({
        url: url,
        type: "GET",
        //dataType: "html",
        async: true,
        success: function (responseData, textStatus, request) {
            document.getElementById("divShoppingCart").innerHTML = responseData.htmlString;
            shoppingCartSummary(responseData.shoppingCartItemsCount, -1, responseData.shoppingCartTotalAmount);
        },
        error: function (xhr, exception) {
            console.log(43, "ERROR???", exception, xhr);
        }
    });
    console.log("shoppingCart_onclick", "00001000", document.getElementById("divShoppingCart").offsetTop);
    try {
        ev.preventDefault();
        document.getElementById("hrfBackToTop").focus({ preventScroll: true });
        document.getElementById("divShoppingCart").scrollIntoView({ behavior: 'smooth', block: 'nearest', inline: 'start' });
        document.getElementById("hrfBackToTop").scrollTop = 50;
    }
    catch (err) {
        //console.log(err);
    }
    console.log("shoppingCart_onclick", "00000000", "EXIT!!!");
}
function shoppingCartSummary(totalItemCount, totalOrderAmount, totalOrderAmountFormatted) {
    document.getElementById("shoppingCartItemsCount").innerHTML = totalItemCount;
    document.getElementById("shoppingCartTotalAmount").innerHTML = totalOrderAmountFormatted;
    document.getElementById("shoppingCartItemsCount1").innerHTML = totalItemCount;
    document.getElementById("shoppingCartTotalAmount1").innerHTML = totalOrderAmountFormatted;
    try {
        if (totalItemCount == 0 || totalOrderAmount == 0) {
            document.getElementById("hrfCheckoutLink").classList.add("disabled-link");
        }
        else {
            document.getElementById("hrfCheckoutLink").classList.remove("disabled-link");
        }
    }
    catch (err) {
        ;
    }
}
function checkLoggedInStatus(idParm, controller, action) {
    console.log("checkLoggedInStatus() Enter", getCurrentDateTime());
    //Sleep for 5 seconds to make sure the session is cleared by Server
    //sleep(5000);
    //console.log("checkLoggedInStatus() After Sleep", getCurrentDateTime());
    fetch("/Home/CheckIsAuthenticated?id=" + idParm)
        .then(response => response.json())
        .then(data => {
            console.log(data, data.isAuthenticated);
            if (data.isAuthenticated) {
                console.log("Continue Session", getCurrentDateTime());
                //clearTimeout(checkLoggedInStatus);
                //setTimeout(checkLoggedInStatus, sessionTimeout);
            } else {
                console.log("End Session", getCurrentDateTime());
                window.location = "/" + controller + "/" + action;
            }
        });
}
