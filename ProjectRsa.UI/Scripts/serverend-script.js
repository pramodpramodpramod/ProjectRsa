function urlExists(url) {
    var urlInBar = $('.url[data-url="' + url + '"]');
    if (urlInBar.length > 0)
        return true;
    return false;
}

$("form.navbar-form").submit(function (e) {
    e.preventDefault();
    var url = $("#addurl").val();
    if (!urlExists(url)) {
        $("#pageurls").append("<div class='url custom-url' data-url='" + url + "'><span class='url-span'>" + url + "</span> <span title='Remove this URL' class='glyphicon glyphicon-remove-circle url-remove pull-right' onclick='removeSite(\"" + url + "\")'></span></div>");
        $("#addurl").val("");
    }
});

function removeSite(url) {
    var urlInBar = $('.url[data-url="' + url + '"]');
    urlInBar.remove();
    var view = $('.result-view[data-url="' + url + '"]');
    return view.remove();;
}


$("#submitForTest").click(function (e) {
    urls = '';
    $(".url-span").each(function () {
        urls += $(this).html() + ",";
	});
	if (urls.length == 0) {
		e.preventDefault();
		return false;
	}
    $("#UrlCsv").val(urls);
})
$("#sort-by").change(function () {
    window.location.href = "/Server?sort=" + $(this).val();
})

