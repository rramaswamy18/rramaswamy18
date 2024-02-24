//Sriramajayam
//orderItemCode0.js
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
        document.getElementById("shoppingCartItemsCount2").innerHTML = jsonData.shoppingCartItemsCount;
        document.getElementById("shoppingCartTotalAmount2").innerHTML = jsonData.shoppingCartTotalAmount;
    },
    error: function (xhr, exception) {
        console.log(43, "ERROR???", exception, xhr);
    }
});
function checkoutValidate_onclick() {
    console.log("00000000", "checkoutValidate_onclick", "Enter");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    document.getElementById("divErrorMessage").innerHTML = "";
    var url = "/Home/CheckoutValidate";
    $.ajax({
        url: url,
        type: "POST",
        //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (responseData, textStatus, request) {
            console.log(responseData);
            $('#loadingModal').modal('hide');
            console.log("00001000", "checkoutValidate_onclick success", responseData.processMessage);
            if (responseData.success) {
                if (responseData.success) {
                    window.location.href = "/Checkout/";
                }
                else {
                    document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
                }
            }
            else {
                document.getElementById("divErrorMessage").innerHTML = responseData.htmlString;
            }
            console.log("00090000", "checkoutValidate_onclick success", "Exit");
        },
        error: function (xhr, exception) {
            $('#loadingModal').modal('hide');
            console.log("checkoutValidate_onclick", "00099000", "ERROR???");
            document.getElementById("divErrorMessage").innerHTML = "Error while validating checkout";
        }
    });
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
