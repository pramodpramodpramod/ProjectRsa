var sites = [];
function urlExists(url) {
    var i = sites.length;
    while (i--) {
        if (sites[i].url === url) {
            return true;
        }
    }
    return false;
}

function getSite(url) {
    var i = sites.length;
    while (i--) {
        if (sites[i].url === url) {
            return sites[i];
        }
    }
    return null;
}

function removeSite(url) {
    var i = sites.length;
    while (i--) {
        if (sites[i].url === url) {
            sites.splice(i, 1);
            var $view = getView(url);
            $view.remove();
            var urlInBar = $('.url[data-url="' + url + '"]');
            urlInBar.remove();
            return;
        }
    }
}

function getView(url) {
    var view = $('.result-view[data-url="' + url + '"]');
    return view;
}

function showAllResults() {
    $("#results-view").empty();
    for (var i in sites) {
        var view = getResultViewForSite(sites[i]);
        $("#results-view").append(view);
    }
}

function updateView(site) {
    var $view = getView(site.url);
    if ($view.length > 0) {
        $view.replaceWith(getResultViewForSite(site));
    }
}

function updateStatus(site) {
    var urlInBar = $('.url[data-url="' + site.url + '"]');
    $(urlInBar).find('.status-span').html(site.statusText);

    if (site.completed != true) {
        $(urlInBar).addClass('inprocess');
    } else {
        var statusDiv = $(urlInBar).find('.status-div');
        statusDiv.remove();
        $(urlInBar).removeClass('inprocess').addClass('pass');
    }
}

$("#sort-by").change(function () {
    var prop = $(this).val();
    sortSites(prop);
});

function sortSites(prop) {
    if ($("#results-view").children().length) {
        sites.sort(function (a, b) {
            if (a.completed === true && b.completed == true) {
                return eval("parseInt(a.testResult.data.runs['1'].firstView." + prop + ") - parseInt(b.testResult.data.runs['1'].firstView." + prop + ")");
            } else if (a.completed == true) return -1;
            else if (b.completed == true) return 1;
            else return 0;
        });
        showAllResults();
    }
}

function showFullDetailsForSite(url) {
    var site = getSite(url);
    $("#results-view").html(getResultAndDetailViewForSite(site));
}

function getResultAndDetailViewForSite(site) {
    var temp = getResultViewForSite(site);
    temp += getDetailViewForSite(site);
    return temp;
}

function getDetailViewForSite(site) {
    if (!site.completed) return '<input id="details-pending" type="hidden" />';
    var detailsImages = config.detailTemplateImages;
    detailsImages = detailsImages.replace("{{screenShot}}", site.testResult.data.runs["1"].firstView.images.screenShot);
    detailsImages = detailsImages.replace("{{checklist}}", site.testResult.data.runs["1"].firstView.images.checklist);
    detailsImages = detailsImages.replace("{{waterfall}}", site.testResult.data.runs["1"].firstView.images.waterfall);
    detailsImages = detailsImages.replace("{{connectionView}}", site.testResult.data.runs["1"].firstView.images.connectionView);

    var requestItems = ['css', 'flash', 'font', 'html', 'image', 'js', 'other']
    var compiledRequests = "";
    for (var i in requestItems) {
        var detailsRequests = config.detailTemplateRequests;
        var breakdown = eval("site.testResult.data.runs['1'].firstView.breakdown['" + requestItems[i] + "']");
        var breakdown2 = eval("site.testResult.data.runs['1'].repeatView.breakdown['" + requestItems[i] + "']");
        var currentRequest = detailsRequests.replace("{{mimeType}}", requestItems[i]);
        currentRequest = currentRequest.replace("{{requests}}", breakdown.requests);
        currentRequest = currentRequest.replace("{{bytes}}", breakdown.bytes);
        currentRequest = currentRequest.replace("{{requests2}}", breakdown2.requests);
        currentRequest = currentRequest.replace("{{bytes2}}", breakdown2.bytes);
        compiledRequests += currentRequest;
    }

    var detailsTemplate = config.detailTemplateWrapper;
    detailsTemplate = detailsTemplate.replace("{{detailTemplateRequests}}", compiledRequests);
    detailsTemplate = detailsTemplate.replace("{{detailTemplateImages}}", detailsImages);
    return detailsTemplate;
}

function getResultViewForSite(site) {
    if (site.completed === true) {
        var temp = config.resultTemplate;
        temp = temp.replace(/{{url}}/g, site.url);
        temp = temp.replace(/{{urlTitle}}/g, site.url.length > 25 ? site.url.substring(0, 25) + "..." : site.url);

        temp = temp.replace("{{imageUrl}}", site.testResult.data.runs["1"].firstView.images.screenShot);

        temp = temp.replace("{{loadTime1}}", site.testResult.data.runs["1"].firstView.loadTime);
        temp = temp.replace("{{loadTime2}}", site.testResult.data.runs["1"].repeatView.loadTime);

        temp = temp.replace("{{firstByte1}}", site.testResult.data.runs["1"].firstView.TTFB);
        temp = temp.replace("{{firstByte2}}", site.testResult.data.runs["1"].repeatView.TTFB);

        temp = temp.replace("{{startRender1}}", site.testResult.data.runs["1"].firstView.render);
        temp = temp.replace("{{startRender2}}", site.testResult.data.runs["1"].repeatView.render);

        temp = temp.replace("{{requests1}}", site.testResult.data.runs["1"].firstView.requestsDoc);
        temp = temp.replace("{{requests2}}", site.testResult.data.runs["1"].repeatView.requestsDoc);

        temp = temp.replace("{{bytesIn1}}", site.testResult.data.runs["1"].firstView.bytesInDoc);
        temp = temp.replace("{{bytesIn2}}", site.testResult.data.runs["1"].repeatView.bytesInDoc);

        temp = temp.replace("{{speedIndex1}}", site.testResult.data.runs["1"].firstView.SpeedIndex);
        temp = temp.replace("{{speedIndex2}}", site.testResult.data.runs["1"].repeatView.SpeedIndex);

        temp = temp.replace("{{fLoadTime1}}", site.testResult.data.runs["1"].firstView.fullyLoaded);
        temp = temp.replace("{{fLoadTime2}}", site.testResult.data.runs["1"].repeatView.fullyLoaded);

        temp = temp.replace("{{fRequests1}}", site.testResult.data.runs["1"].firstView.requestsFull);
        temp = temp.replace("{{fRequests2}}", site.testResult.data.runs["1"].repeatView.requestsFull);

        temp = temp.replace("{{fBytesIn1}}", site.testResult.data.runs["1"].firstView.bytesIn);
        temp = temp.replace("{{fBytesIn2}}", site.testResult.data.runs["1"].repeatView.bytesIn);
        return temp;
    }
    else {
        var temp = config.resultPendingTemplate;
        temp = temp.replace(/{{url}}/g, site.url);
        temp = temp.replace(/{{urlTitle}}/g, site.url.length > 20 ? site.url.substring(0, 25) + "..." : site.url);
        temp = temp.replace(/{{status}}/g, site.statusText);
        return temp;
    }
}

$("form").submit(function (e) {
    e.preventDefault();
    var url = $("#addurl").val();
    if (!urlExists(url)) {
        $("#pageurls").append("<div class='url' data-url='" + url + "'><span onclick='showFullDetailsForSite(\"" + url + "\")' class='url-span'>" + url + "</span> <span title='Remove this URL' class='glyphicon glyphicon-remove-circle url-remove pull-right' onclick='removeSite(\"" + url + "\")'></span><div class='status-div'><img src='/Content/img/processing.gif' /><span class='status-span'>testing</span></div></div>");
        $("#addurl").val("");
        var site = { url: url, completed: false, statusText: 'Creating Test Request' };
        sites.push(site);
        runTest(site);
    }
});

function runTest(site) {
    var pageUrl = site.url;
    var baseUrl = "http://www.webpagetest.org/runtest.php"
    var apiKey = "A.0be73fef5b2890ddfeb1b2f9863d0254";
    var apiUrl = `${baseUrl}?url=${pageUrl}&f=json&k=${apiKey}&noopt=1`;
    $.get({ url: apiUrl, dataType: "json" }).done(function (data, textStatus, xhr) {
        site.sumbissionResult = data;
        if (data.statusCode == 200) {
            getTestResults(data, site);
        }
        else {
            alert("Please enter a valid URL." + site.url + "is not a valid internet host name");
            removeSite(site.url);
        }
    });
    site.statusText = "submitted";
    updateStatus(site);
}

function getTestResults(res, site) {
    var sitesExists = getSite(site.url);
    if (sitesExists) {
        $.get({ url: res.data.jsonUrl, dataType: "json" })
            .done(function (resData, resTextStatus, resXhr) {
                site.statusText = resData.statusText;
                site.testResult = resData;
                state = resData.statusCode;
                if (state === 200) {
                    site.testResult = resData;
                    site.completed = true;
                    updateView(site); // updates teh view with result data
                    sortSites($("#sort-by").val()); //sort the sites in order required
                } else {
                    updateView(site); //updates the view with current status
                    setTimeout(function () { getTestResults(res, site); }, 2000);
                }
                updateStatus(site);
            });
    }
}