//Sriramajayam
//javaScriptCode.1.1.1.js
$(function () {
    $(document).ajaxError(function (e, xhr) {
        try {
            switch (xhr.status) {
                case 401: //Unauthorized
                    window.open(xhr.responseJSON.UnauthorizedUrl + "?ReturnUrl=" + xhr.responseJSON.ReturnUrl, "_blank");
                    break;
                case 403: //Forbidden
                    document.getElementById("forbiddenUnauthorizedHRef").href = "/Home/Forbidden";
                    document.getElementById("forbiddenUnauthorizedHRef").click();
                    window.location.href = "";
                    break;
                default:
                    //document.getElementById("forbiddenUnauthorizedHRef").href = "/Home/Forbidden";
                    //document.getElementById("forbiddenUnauthorizedHRef").click();
                    //window.location.href = "";
                    break;
            }
        }
        catch (err) {
        }
    });
});
function validateInteger(event) {
    if (event.shiftKey == true) {
        event.preventDefault();
    }

    if ((event.keyCode >= 48 && event.keyCode <= 57) ||
        (event.keyCode >= 96 && event.keyCode <= 105) ||
        event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
        event.keyCode == 39 || event.keyCode == 46 /*|| event.keyCode == 190*/) {

    } else {
        event.preventDefault();
    }
}
function validateDecimal(event, inputData) {
    var returnValue = true;
    if (event.shiftKey == true) {
        //event.preventDefault();
        returnValue = false;
    }

    if (inputData.indexOf('.') > -1 && event.keyCode == 190) {
        //event.preventDefault();
        returnValue = false;
    }

    if (returnValue) {
        if ((event.keyCode >= 48 && event.keyCode <= 57) ||
            (event.keyCode >= 96 && event.keyCode <= 105) ||
            event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
            event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {
            ;
        } else {
            //event.preventDefault();
            returnValue = false;
        }
    }
    //alert(returnValue + " " + event.keyCode + " " + inputData + " " + inputData.indexOf('.'));
    return returnValue;
}
function ajaxGet(url, queryString, ajaxUpdateTargetIdSuccess, ajaxUpdateTargetIdError, menuLinkNamePrefix, menuLinkIndex, menuLinkCount, className, selectedClassName) {
    console.log("url", url, "queryString", queryString, "ajaxUpdateTargetIdSuccess", ajaxUpdateTargetIdSuccess, "ajaxUpdateTargetIdError", ajaxUpdateTargetIdError, "menuLinkNamePrefix", menuLinkNamePrefix, "menuLinkIndex", menuLinkIndex, "menuLinkCount", menuLinkCount, "queryString", "className", className, "selectedClassName", selectedClassName);
    try {
        for (var i = 0; i < menuLinkCount; i++) {
            document.getElementById(menuLinkNamePrefix + i).removeAttribute("class");
            document.getElementById(menuLinkNamePrefix + i).setAttribute("class", className);
            if (i == menuLinkIndex) {
                document.getElementById(menuLinkNamePrefix + i).setAttribute("class", className + " " + selectedClassName);
            }
        }
    }
    catch (err) {
        ;
    }
    if (queryString == undefined) {
        queryString = "";
    }
    $.ajax({
        url: url + queryString,
        type: "GET",
        success: function (responseData, textStatus, request) {
            console.log("SUCCESS!!!", textStatus);
            document.getElementById(ajaxUpdateTargetIdSuccess).innerHTML = responseData;
        },
        error: function (xhr, exception) {
            console.log("ERROR???", exception, xhr);
            document.getElementById(ajaxUpdateTargetIdError).innerHTML = xhr.responseText;
        }
    });
}
function ajaxPost(url, queryString, ajaxUpdateTargetIdSuccess, ajaxUpdateTargetError, postData) {
    console.log("00000000", "ajaxPost", "ENTER!!!");
    $("#loadingModal").modal({ backdrop: 'static', keyboard: false });
    var url = url + queryString;
    $.ajax({
        url: url,
        type: "POST",
        //contentType: "application/x-www-form-urlencoded; charset=UTF-8",//"application/x-www-form-urlencoded; charset=UTF-8",//"text/plain; charset=UTF-8", //false, //"application/json; charset=utf-8",
        //dataType: "html",
        cache: false,
        contentType: false,
        processData: false,
        data: postData, //new FormData(document.getElementById("formItemAddEditData")),
        //async: false,
        success: function (responseData, textStatus, request) {
            $("#loadingModal").modal('hide');
            document.getElementById(ajaxUpdateTargetIdSuccess).innerHTML = responseData;
            console.log("00095000", "ajaxPost", "SUCCESS!!!");
        },
        error: function (xhr, exception) {
            $("#loadingModal").modal('hide');
            document.getElementById(ajaxUpdateTargetError).innerHTML = xhr.responseText;
            console.log("00099000", "ajaxPost", "ERROR???", exception, xhr);
        }
    });
    console.log("00090000", "ajaxPost", "EXIT!!!");
}
function isInputNumber(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode >= 48 && charCode <= 57) || charCode === 8)//0-9 or backspace
    //if ((window.event.keyCode >= 48 && window.event.keyCode <= 57) || window.event.keyCode == 8)//0-9 or backspace
    {
        return true;
    }
    else {
        return false;
    }
}
function isInputDecimal(evt, txt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
        //Check if the text already contains the . character
        if (txt.indexOf('.') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (charCode > 31 &&
            (charCode < 48 || charCode > 57))
            return false;
    }
    return true;
}
function replaceCharactersWithEmpty(inputText) {
    //When text data is pasted - replace with empty string
    //inputText = inputText.replace(/\D/g, '');
    //inputText = inputText.replace(/[^0-9.]/g, ""); - Replace except numbers and period
    //inputText = inputText.replace(/[^0-9]/g, ""); - Replace except numbers
    //inputText = inputText.replace(/[[a-z] + [^0-9\s.]+|.(?!\d)]/g, "");
    //Tried all of the above did not work - For now let me use the brute force method and replace this at a later date
    var inputChar, outputText = "";
    for (var i = 0; i < inputText.length; i++) {
        inputChar = inputText.charCodeAt(i);
        if (inputChar >= 91 && inputChar <= 100) {
            outputText += inputText.substr(i, 1);
        }
    }
    return outputText;
    /*
    $( "#textInput" ).bind( 'paste',function()
       {
           setTimeout(function()
           {
              //get the value of the input text
              var data= $( '#textInput' ).val() ;
              //replace the special characters to ''
              var dataFull = data.replace(/[^\w\s]/gi, '');
              //set the new value of the input text without special characters
              $( '#textInput' ).val(dataFull);
           });
    
        });
     */
}
function checkMaxLength(thisObject) {
    if (thisObject.value.length > thisObject.maxLength) {
        thisObject.value = thisObject.value.slice(0, thisObject.maxLength);
    }
}
//Not sure if below is needed.... Need to find the jquery code to add * for required
/*
function processAjaxOnSucces(responseObjectModel, modalFormName, showModal) {
    try {
        var responseObjectModelObject = JSON.parse(responseObjectModel.replace(/&quot;/g, '"'));
        if (responseObjectModelObject.IsSuccessStatusCode) {
            document.getElementById("responseTitle").innerHTML = responseObjectModelObject.ResponseTitle;
            document.getElementById("responseDescription").innerHTML = responseObjectModelObject.ResponseDescription;
            document.getElementById("responseMessages").innerHTML = "";
            for (var i = 0; i < responseObjectModelObject.ResponseMessages.length; i++) {
                document.getElementById("responseMessages").innerHTML += "<li>" + responseObjectModelObject.ResponseMessages[i].Value + "</li>";
            }
            if (showModal && modalFormName != undefined && modalFormName != null) {
                $("#" + modalFormName).modal({ backdrop: 'static', keyboard: false });
            }
        }
    }
    catch (err) {
        return;
    }
}
function ajaxPost(controllerName, actionName, ajaxContentElementId, ajaxUpdateTargetId) {
    var queryString = "";
    $.ajax({
        url: "/" + controllerName + "/" + actionName + queryString,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: $("#" + ajaxContentElementId).serialize(),
        dataType: "json",
        success: function (responseData, textStatus, request) {
            $("#" + ajaxUpdateTargetId).html(responseData);
        },
        error: function (xhr, exception) {
            console.log("Error " + xhr.status + " exception=" + exception + " xhr.responseText=" + xhr.responseText);
            //alert("Error " + xhr.status);
        }
    });
}
*/
function fileAttachment_onchange(files, imagePreviewDocumentElementId, fileNameDocumentElementId, fileSizeDocumentElementId, fileContentTypeDocumentElementId, fileSrcDataDocumentElementId, documentTypeIdObject, documentTypeId) {
    var file = files[0];

    document.getElementById(imagePreviewDocumentElementId).src = URL.createObjectURL(file);
    $("#" + fileNameDocumentElementId).val(file.name);
    $("#" + fileSizeDocumentElementId).val(file.size);
    $("#" + fileContentTypeDocumentElementId).val(file.type || "application/octet-stream");
    var fileReader = new FileReader();
    fileReader.onload = function () {
        fileReader.result;

        $("#" + fileSrcDataDocumentElementId).val(fileReader.result);
    };
    fileReader.readAsDataURL(file);
    if (documentTypeIdObject === null) {
        ;
    }
    else {
        documentTypeIdObject.value = documentTypeId;
    }
    //document.getElementById("closeButton").style.display = "block";
    return;
}
function getCurrentDateTime() {
    var currentDateTime = new Date();
    var temp = currentDateTime.getFullYear() + "-" + (currentDateTime.getMonth() + 1) + "-" + currentDateTime.getDate() + " " +
        currentDateTime.getHours() + ":" + currentDateTime.getMinutes() + ":" + currentDateTime.getSeconds();
    return temp;
}
function httpPostedFileBase_oninput(files, imagePreviewDocumentElementId) {
    var file = files[0];
    var objectUrl = URL.createObjectURL(file);
    img = new Image();
    img.onload = function () {
        URL.revokeObjectURL(objectUrl);
    };
    img.src = objectUrl;
    document.getElementById(imagePreviewDocumentElementId).onload = function () {
        URL.revokeObjectURL(objectUrl);
    };
    document.getElementById(imagePreviewDocumentElementId).src = objectUrl;
    var fileReader = new FileReader();
    fileReader.onload = function () {
        fileReader.result;
    };
    fileReader.readAsDataURL(file);
    return;
}
function httpPostedFileBase_reset(httpPostedFileBaseDocumentElementId, imagePreviewDocumentElementId) {
    document.getElementById(httpPostedFileBaseDocumentElementId).value = null;
    document.getElementById(imagePreviewDocumentElementId).src = null;
    return false;
}
function showHideForShow(inputElementId, typeAttributeValue) {
    $("#" + inputElementId).attr('type', typeAttributeValue);
    $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
}
function showHideForHide(inputElementId, typeAttributeValue) {
    $('#' + inputElementId).attr('type', typeAttributeValue);
    $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
}
function isNumber1(n) {
    return !isNaN(parseFloat(n)) && !isNaN(n - 0);
}
function isNumber2(n) {
    return /^-?[\d.]+(?:e-?\d+)?$/.test(n);
}
function suppressEnterKeyDown(event) {
    if (event.key === 'Enter') {
        event.preventDefault();
    }
}
function replaceEnter(element, newChar) {
    element.value = element.value.replace(/\r?\n/g, newChar);
    return false;
}
function suppressEnterKeyDown(event) {
    if (event.key === 'Enter') {
        event.preventDefault();
    }
}
function suppressEnterPaste(domElement, event) {
    const pastedText = (event.clipboardData || window.clipboardData).getData('text');
    const sanitizedText = pastedText.replace(/[\r\n]+/g, '');

    event.preventDefault();

    const selectionStart = domElement.selectionStart;
    const selectionEnd = domElement.selectionEnd;

    domElement.value = domElement.value.substring(0, selectionStart) + sanitizedText + domElement.value.substring(selectionEnd);

    domElement.selectionStart = selectionStart + sanitizedText.length;
    domElement.selectionEnd = selectionStart + sanitizedText.length;
}
