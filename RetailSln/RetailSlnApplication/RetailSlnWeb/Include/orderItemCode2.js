//Sriramajayam
//orderItemCode2.js
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
            console.log(responseData);
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
            }
            else {
                document.getElementById("formDeliveryInfoData").innerHTML = responseData.htmlString;
            }
            //document.getElementById("formPaymentData").scrollIntoView();
            console.log("00001000", "deliveryInfoSave_onclick success", responseData.processMessage);
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("formDeliveryInfoData").innerHTML = xhr.responseText;
            console.log("00099000", "deliveryInfoSave_onclick error", exception, xhr);
        }
    });
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
function paymentInfoSave_onclick() {
    console.log("00000000", "paymentSave_onclick");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = "/Home/PaymentInfo";
    var postData = $("#formPaymentInfoData").serialize();
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            console.log("00000100", "paymentInfoSave_onclick");
            console.log(9, responseData);
            $("#loadingModal").modal('hide');
            if (responseData.success) {
                console.log("00000200", "paymentInfoSave_onclick");
                document.getElementById("divOrderProcess").innerHTML = responseData.htmlString;
            }
            else {
                document.getElementById("formPaymentInfoData").innerHTML = responseData.htmlString;
            }
            console.log("00001000", "paymentInfoSave_onclick success", responseData.processMessage);
            $("#loadingModal").modal('hide');
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("formPaymentInfoData").innerHTML = xhr.responseText;
            console.log("00099000", "paymentInfoSave_onclick error", exception, xhr);
            $("#loadingModal").modal('hide');
        }
    });
}
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
                        //document.getElementById("shoppingCartItemsCount2").innerHTML = "";
                        //document.getElementById("shoppingCartTotalAmount2").innerHTML = "";
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
    var postData = $("#formPaymentInfoData").serialize();
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        data: postData,
        //async: false,
        success: function (responseData, textStatus, request) {
            console.log("00000100", "paymentInfo1Save_onclick");
            console.log(9, responseData);
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
                document.getElementById("formPaymentInfoData").innerHTML = responseData.htmlString;
            }
            console.log("00001000", "paymentSave_onclick success", responseData.processMessage);
            $("#loadingModal").modal('hide');
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById("formPaymentInfoData").innerHTML = xhr.responseText;
            console.log("00099000", "paymentInfo1Save_onclick error", exception, xhr);
            $("#loadingModal").modal('hide');
        }
    });
}
