$(document).ready(function () {
    var sessionPageId = sessionStorage.getItem("uniquePageId");
    if (sessionPageId == undefined || isNullOrWhiteSpace(sessionPageId)) {
        var qTabId = getParameterByKey(null, "tabId");
        var uniquePageId;
        if (isNewWindowRedirect(null)) {
            uniquePageId = qTabId;
        }
        else {
            uniquePageId = makeid(5).toString();
        }
        if (uniquePageId != undefined && !isNullOrWhiteSpace(uniquePageId)) {
            sessionStorage.setItem("uniquePageId", uniquePageId);
            sessionPageId = uniquePageId;
        }
    }

    if (sessionPageId != undefined && !isNullOrWhiteSpace(sessionPageId)) {
        var queryStringToSearchFor = "tabId=" + sessionPageId;
        if (window.location.href.toString().indexOf(queryStringToSearchFor) === -1)
            window.location.href = updateQueryStringParameter(window.location.href, "tabId", sessionPageId);

        $("a").each(function () {
            if ($(this).attr("href").charAt(0) === '/') {
                $(this).attr("href", updateQueryStringParameter($(this).attr("href"), "tabId", sessionPageId));
            }
        })
        $("form").each(function () {
            if ($(this).attr("action").charAt(0) === '/') {
                $(this).attr("action", updateQueryStringParameter($(this).attr("action"), "tabId", sessionPageId));
            }
        })
    }
})

function getParameterByKey(url, key) {
    if (!url) url = window.location.href;
    key = key.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function isNewWindowRedirect(url)
{
    if (!url) url = window.location.href;
    var isNewWindowRedirect = false;
    var cbw = getParameterByKey(url, "cbw");
    if(cbw != null && cbw === 'Y')
    {
        isNewWindowRedirect = true;
    }
    return isNewWindowRedirect;
}

function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
}

function removeQueryStringParameter(uri, key) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
}

function isNullOrWhiteSpace(obj)
{
    var checkFlag = false;
    if (obj == null || obj == '' || obj == ' ') {
        checkFlag = true;
    }
    return checkFlag;
}

function makeid(length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
}

function navigateHref (href)
{
    var sessionPageId = sessionStorage.getItem("uniquePageId");
    if (sessionPageId != undefined && !isNullOrWhiteSpace(sessionPageId)) {
        href = updateQueryStringParameter(href, "tabId", sessionPageId);
    }
    window.location.href = href;
}

function getPageSpecificUrl(url, isNewWindow) {
    var pageSpecificUrl = url;
    var sessionPageId = sessionStorage.getItem("uniquePageId");
    if (sessionPageId != undefined && !isNullOrWhiteSpace(sessionPageId)) {
        pageSpecificUrl = updateQueryStringParameter(url, "tabId", sessionPageId);
    }
    if (isNewWindow == true)
    {
        pageSpecificUrl = updateQueryStringParameter(url, "cbw", "Y");
    }
    return pageSpecificUrl
}

$("#empSummary").click(function () {
    
    var data = new FormData();
    var sessionPageId = sessionStorage.getItem("uniquePageId");
    if (sessionPageId == undefined || isNullOrWhiteSpace(sessionPageId))
    {
        var uniquePageId = makeid(5).toString();
        sessionStorage.setItem("uniquePageId", uniquePageId);
    }
    $.ajax({
        url: getPageSpecificUrl('/Home/EmployeeSummary'), type: "GET", processData: false,
        data: data,
        contentType: false,
        success: function (response) {
            navigateHref(getPageSpecificUrl('/Home/EmployeeSummary'));
        },
    });
    return false;
});

$("#removeAllIndividuals").click(function () {

    var data = new FormData();
    $.ajax({
        url: getPageSpecificUrl('/Home/RemoveAllIndividuals'), type: "GET", processData: false,
        data: data,
        contentType: false,
        success: function (response) {
            navigateHref(getPageSpecificUrl('/Home/EmployeeSummary'));
        },
    });
    return false;
});

$("#addNewEmployee").click(function () {
    navigateHref(getPageSpecificUrl('/Home/EmployeeDetails'));
});

$("#addNewEmployeeNewWindow").click(function () {
    window.open(getPageSpecificUrl('/Home/EmployeeDetails', true), '_blank', 'toolbar=0,location=0,menubar=0');
});

