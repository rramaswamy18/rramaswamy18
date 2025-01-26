//Sriramajayam
//orderItemCode1.js
function checkoutLoginUserProfGuest_onclick() {
    console.log("00000000", "loginUserProfGuestSave_onclick", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/CheckoutGuest";
    var postData = $("#formOTPData").serialize();
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
                //document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                //document.getElementById("loggedInUserFullName").innerHTML = responseData.loggedInUserFullName;
                //document.getElementById("loggedInUserEmailAddress").innerHTML = responseData.loggedInUserEmailAddress;
                window.location = responseData.htmlString;
            }
            else {
                document.getElementById("formOTPData").innerHTML = responseData.htmlString;
            }
            console.log("00001000", "checkoutLoginUserProfGuest_onclick success", responseData.processMessage);
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("formLoginUserProfGuestData").innerHTML = xhr.responseText;
            console.log("00099000", "checkoutLoginUserProfGuest_onclick", "ERROR???", exception, xhr);
        }
    });
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
                //document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                //document.getElementById("divScrollIntoView").scrollIntoView();
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
            if (responseData.success) {
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
            }
            else {
                if (responseData.htmlString == "") {
                    window.location.href = "/Home/Index";
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
function giftCertBalance_onclick() {
    console.log("00000000", "giftCertBalance_onclick", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/GiftCertBalance?";
    var jsonData, jsonDataString;
    //var queryString = "giftCertNumber=" + document.getElementById("GiftCertNumber").value + "&giftCertKey=" + document.getElementById("GiftCertKey").value;
    jsonData = {};
    jsonData.GiftCertNumber = document.getElementById("GiftCertNumber").value;
    jsonData.GiftCertKey = document.getElementById("GiftCertKey").value;
    jsonDataString = JSON.stringify(jsonData);
    $.ajax({
        url: url,//+ queryString,
        type: "POST",
        contentType: "application/json; charset=utf-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        dataType: "json",
        data: jsonDataString,
        //async: false,
        success: function (responseData, textStatus, request) {
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                document.getElementById("giftCertBalanceAmount").innerHTML = responseData.giftCertBalanceAmount;
            }
            else {
                document.getElementById("giftCertBalanceAmount").innerHTML = responseData.errorMessage;
            }
            console.log("00090000", "giftCertBalance_onclick", "SUCCESS!!!");
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("giftCertBalanceAmount").innerHTML = xhr.responseText.errorMessage;
            console.log("00099000", "giftCertBalance_onclick", "ERROR???", exception, xhr);
        }
    });
    return false;
}
//function paymentInfoSave_onclick() {
//    console.log("00000000", "paymentSave_onclick");
//    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
//    var url = "/Home/PaymentInfo";
//    var postData = $("#formPaymentInfoData").serialize();
//    $.ajax({
//        url: url,
//        type: "POST",
//        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
//        //dataType: "html",
//        data: postData,
//        //async: false,
//        success: function (responseData, textStatus, request) {
//            console.log("00000100", "paymentInfoSave_onclick");
//            console.log(9, responseData);
//            $("#loadingModal").modal('hide');
//            if (responseData.success) {
//                console.log("00000200", "paymentInfoSave_onclick");
//                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
//            }
//            else {
//                document.getElementById("formPaymentInfoData").innerHTML = responseData.htmlString;
//            }
//            console.log("00001000", "paymentInfoSave_onclick success", responseData.processMessage);
//            $("#loadingModal").modal('hide');
//        },
//        error: function (xhr, exception) {
//            $("#loadingModal").modal('hide');
//            document.getElementById("formPaymentInfoData").innerHTML = xhr.responseText;
//            console.log("00099000", "paymentInfoSave_onclick error", exception, xhr);
//            $("#loadingModal").modal('hide');
//        }
//    });
//}
function paymentSave_onclick() {
    console.log("00000000", "paymentSave_onclick");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/Payment";
    var postData = $("#formPaymentData").serialize();
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            console.log("00000100", "paymentSave_onclick");
            console.log(9, responseData);
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                console.log("00000200", "paymentSave_onclick");
                switch (responseData.creditCardProcessor) {
                    case "NONE":
                        break;
                    case "RAZORPAYTEST":
                        console.log("00000300", "paymentSave_onclick");
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
                                console.log(9.18, response);
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
                        break;
                    case "TESTMODE":
                    case "NUVEITEST":
                    case "NUVEIPROD":
                        document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                        document.getElementById("loggedInUserFullName").innerHTML = responseData.loggedInUserFullName;
                        document.getElementById("loggedInUserEmailAddress").innerHTML = responseData.loggedInUserEmailAddress;
                        document.getElementById("shoppingCartItemsCount").innerHTML = "";
                        document.getElementById("shoppingCartTotalAmount").innerHTML = "";
                        break;
                }
            }
            else {
                document.getElementById("formPaymentData").innerHTML = responseData.htmlString;
            }
            console.log("00001000", "paymentSave_onclick success", responseData.processMessage);
            $("#loadingModal").modal('hide');
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("formPaymentData").innerHTML = xhr.responseText;
            console.log("00099000", "paymentSave_onclick error", exception, xhr);
            $("#loadingModal").modal('hide');
        }
    });
}
function paymentInfo1Save_onclick() {
    console.log("00000000", "paymentInfo1Save_onclick");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/PaymentInfo1";
    //var postData = $("#formPaymentInfoData").serialize();
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        //data: null,
        //async: false,
        success: function (responseData, textStatus, request) {
            console.log("00000100", "paymentInfo1Save_onclick", responseData);
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                console.log("00000200", "paymentInfo1Save_onclick");
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                document.getElementById("loggedInUserFullName").innerHTML = responseData.loggedInUserFullName;
                document.getElementById("loggedInUserEmailAddress").innerHTML = responseData.loggedInUserEmailAddress;
                document.getElementById("shoppingCartItemsCount").innerHTML = "";
                document.getElementById("shoppingCartTotalAmount").innerHTML = "";
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
                //document.getElementById("formPaymentInfoData").innerHTML = responseData.htmlString;
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
//function paymentInfo2SaveBackup_onclick() {
//    console.log("00000000", "paymentInfo2Save_onclick");
//    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
//    var url = "/Home/PaymentInfo2";
//    var postData = $("#formPaymentInfoData").serialize();
//    $.ajax({
//        url: url,
//        type: "POST",
//        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
//        //dataType: "html",
//        data: postData,
//        //async: false,
//        success: function (responseData, textStatus, request) {
//            console.log("00000100", "paymentInfo2Save_onclick");
//            console.log(9, responseData);
//            $("#loadingModal").modal('hide');
//            if (responseData.success) {
//                console.log("00000200", "paymentInfo2Save_onclick");
//                var jsonObject = JSON.parse(responseData.htmlString);
//                console.log(9.9, responseData.htmlString, jsonObject);
//                var options = {
//                    "key": jsonObject.RazorpayKey,
//                    "amount": jsonObject.Amount,
//                    "currency": jsonObject.Currency,
//                    "name": jsonObject.Name,
//                    "description": jsonObject.Description,
//                    "image": "https://avatars.githubusercontent.com/u/65504583?v=4",
//                    "order_id": jsonObject.OrderId,
//                    "handler": function (response) {
//                        console.log(9.18, response);
//                        //document.getElementById(("formRazorPayReturn").action = "/Home/RazorPayReturn";
//                        document.getElementById('razorpay_payment_id').value = response.razorpay_payment_id;
//                        document.getElementById('razorpay_order_id').value = response.razorpay_order_id;
//                        document.getElementById('razorpay_signature').value = response.razorpay_signature;
//                        document.getElementById('rzp-paymentresponse').style.display = "block";
//                        document.getElementById('rzp-paymentresponse').click();
//                    },
//                    "prefill": {
//                        "name": jsonObject.Name,
//                        "email": jsonObject.Email,
//                        "contact": jsonObject.PhoneNumber
//                    },
//                    "notes": {
//                        "address": jsonObject.Address
//                    },
//                    "theme": {
//                        "color": "#F37254"
//                    }
//                };
//                var rzp1 = new Razorpay(options);
//                rzp1.open();
//            }
//            else {
//                document.getElementById("formPaymentInfoData").innerHTML = responseData.htmlString;
//            }
//            console.log("00001000", "paymentInfo2Save_onclick success", responseData.processMessage);
//            $("#loadingModal").modal('hide');
//        },
//        error: function (xhr, exception) {
//            $("#loadingModal").modal('hide');
//            document.getElementById("formPaymentInfoData").innerHTML = xhr.responseText;
//            console.log("00099000", "paymentInfo2Save_onclick error", exception, xhr);
//            $("#loadingModal").modal('hide');
//        }
//    });
//}
function paymentInfo4Save_onclick() {
    console.log("00000000", "paymentInfo4Save_onclick");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/PaymentInfo4";
    //var postData = $("#formPaymentInfoData").serialize();
    //console.log(postData);
    $.ajax({
        url: url,
        type: "GET",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        //data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            console.log("00000100", "paymentInfo4Save_onclick");
            console.log(9, responseData);
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                console.log("00000200", "paymentInfo4Save_onclick");
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                document.getElementById("CreditCardNumber").value = "4111111111111111";
                document.getElementById("CVV").value = "123";
                document.getElementById("CardHolderName").value = "John Miller";
                document.getElementById("CardExpiryMM").value = "09";
                document.getElementById("CardExpiryYYYY").value = "2025";
                //document.getElementById("loggedInUserFullName").innerHTML = responseData.loggedInUserFullName;
                //document.getElementById("loggedInUserEmailAddress").innerHTML = responseData.loggedInUserEmailAddress;
                //document.getElementById("shoppingCartItemsCount").innerHTML = "";
                //document.getElementById("shoppingCartTotalAmount").innerHTML = "";
            }
            else {
                //document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                alert("Error in response???");
                document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
            }
            console.log("00001000", "paymentInfo4Save_onclick success", responseData.processMessage);
            $("#loadingModal").modal('hide');
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            console.log("00099000", "paymentInfo4Save_onclick error", exception, xhr);
            alert("Error occurred???");
            //document.getElementById("divOrderProcess").innerHTML = xhr.responseText;
            $("#loadingModal").modal('hide');
        }
    });
}
function paymentInfo5Save_onclick() {
    console.log("00000000", "paymentInfo5Save_onclick");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/PaymentInfo5";
    var postData = $("#formCreditCardPaymentData").serialize();
    console.log(postData);
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            console.log("00000100", "paymentInfo5Save_onclick");
            //console.log(9, responseData);
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                console.log("00001000", "paymentInfo5Save_onclick", responseData.success, responseData.processMessage);
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
                //    document.getElementById("loggedInUserFullName").innerHTML = responseData.loggedInUserFullName;
                //    document.getElementById("loggedInUserEmailAddress").innerHTML = responseData.loggedInUserEmailAddress;
                //    document.getElementById("shoppingCartItemsCount").innerHTML = "";
                //    document.getElementById("shoppingCartTotalAmount").innerHTML = "";
            }
            else {
                console.log("00002000", "paymentInfo5Save_onclick", responseData.success, responseData.processMessage);
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
            }
            console.log("00003000", "paymentInfo5Save_onclick success", responseData.success, responseData.processMessage);
            $("#loadingModal").modal('hide');
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            console.log("00099000", "paymentInfo5Save_onclick error", exception, xhr);
            document.getElementById("divOrderProcess").innerHTML = xhr.responseText;
        }
    });
}
