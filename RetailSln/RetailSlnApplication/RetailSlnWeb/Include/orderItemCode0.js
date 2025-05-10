//Sriramajayam
//orderItemCode0.js
function addToCart_onclick(itemMasterId, itemId, elementIdSuffix) {
    console.log("addToCart_onclick", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    for (var i = 0; i < 50000; i++);
    var orderQtyElement = document.getElementById("orderQty" + elementIdSuffix);
    var orderQty = orderQtyElement.value;
    console.log("addToCart_onclick", "00001000", "itemId", itemId, "elementIdSuffix", elementIdSuffix, "orderQty", orderQty);
    var returnValue = true, errorMessage = "";
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
        }
        else {
            if (errorMessage != "") {
                errorMessage += "<br />";
            }
            errorMessage += "Enter order quantity";
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
                type: "GET",
                //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
                dataType: "json",
                data: { "itemId": itemId, "orderQty": orderQty },
                async: true,
                success: function (responseData, textStatus, request) {
                    $('#loadingModal').modal('hide');
                    console.log("addToCart_onclick", "00090000", "SUCCESS!!!", responseData);
                    if (responseData.success) {
                        shoppingCartSummary(responseData.shoppingCartItemsCount, -1, responseData.shoppingCartTotalAmount);
                        document.getElementById("shoppingCartItemsCount").innerHTML = responseData.shoppingCartItemsCount;
                        document.getElementById("shoppingCartTotalAmount").innerHTML = responseData.shoppingCartTotalAmount;
                        document.getElementById("shoppingCartItemsCount1").innerHTML = responseData.shoppingCartItemsCount;
                        document.getElementById("shoppingCartTotalAmount1").innerHTML = responseData.shoppingCartTotalAmount;
                        document.getElementById("spnMessageSuccess" + elementIdSuffix).style.display = "block";
                        document.getElementById("spnMessageSuccessText" + elementIdSuffix).innerHTML = orderQty;
                        orderQtyElement.value = "";
                        document.getElementById("spnMessageSuccess" + elementIdSuffix).style.display = "block";
                        document.getElementById("spnMessageSuccessText" + elementIdSuffix).innerHTML = orderQty;
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
            addToCartPopup_onclick(itemMasterId);
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
function addToCartPopup_onclick(itemMasterId) {
    console.log("addToCartPopup_onclick", "00000000", "ENTER!!!");
    $('#divAddToCartPopupModal' + itemMasterId).modal({ backdrop: 'static', keyboard: false });
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
            console.log("00000500", "deliveryInfoSave_onclick success", responseData);
            if (responseData.success) {
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
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
function orderComments_onchange(index) {
    console.log("orderComments_onchange", "00000000", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
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
                $('#loadingModal').modal('hide');
                console.log("orderComments_onchange", "00090000", "SUCCESS!!!", responseData.success);
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
function orderQty_oninput1(index) {
    try {
        var orderQtyElementObject = document.getElementById("orderQty" + index);
        if (orderQtyElementObject.value.length > orderQtyElementObject.getAttribute("maxlength")) {
            orderQtyElementObject.value = orderQtyElementObject.value.substr(0, orderQtyElementObject.getAttribute("maxlength"));
        }
        //Test if the input is 1. numeric 2. not exceed maxlength 3. between min and max values
        var orderQty = orderQtyElementObject.value;
        if ((/^\d+$/.test(orderQty)) && orderQty.length <= orderQtyElementObject.getAttribute("maxlength") && orderQty >= orderQtyElementObject.getAttribute("min") && orderQty <= orderQtyElementObject.getAttribute("max")) {
        }
        else {
            orderQtyElementObject.value = "";
        }
    }
    catch (err) {
        console.log(1, "orderQty_oninput1 ERROR???", index, err);
    }
    return false;
}
function orderQty_oninput2(index, orderQtyIndex) {
    try {
        var orderQtyElementObject = document.getElementById("orderQty" + index + "_" + orderQtyIndex);
        if (orderQtyElementObject.value.length > orderQtyElementObject.getAttribute("maxlength")) {
            orderQtyElementObject.value = orderQtyElementObject.value.substr(0, orderQtyElementObject.getAttribute("maxlength"));
        }
        //Test if the input is 1. numeric 2. not exceed maxlength 3. between min and max values
        var orderQty = orderQtyElementObject.value;
        if ((/^\d+$/.test(orderQty)) && orderQty.length <= orderQtyElementObject.getAttribute("maxlength") && orderQty >= orderQtyElementObject.getAttribute("min") && orderQty <= orderQtyElementObject.getAttribute("max")) {
        }
        else {
            orderQtyElementObject.value = "";
        }
    }
    catch (err) {
        console.log(1, "orderQty_oninput2 ERROR???", index, orderQtyIndex, err);
    }
    return false;
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
    if (indexOf === -1) {
        window.open("/Home/SearchResult?id=" + document.getElementById(searchTermElementId).value, "_blank");
    }
    else {
        window.location.href = "/Home/SearchResult?id=" + document.getElementById(searchTermElementId).value;
    }
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
    if (totalItemCount == 0 || totalOrderAmount == 0) {
        document.getElementById("hrfCheckoutLink").classList.add("disabled-link");
    }
    else {
        try {
            document.getElementById("hrfCheckoutLink").classList.remove("disabled-link");
        }
        catch (err) {
            ;
        }
    }
}
