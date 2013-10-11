/// <reference path="jquery-1.7.2.js" />

var activeTab = false;

$(document).ready(function () {
    if (activeTab) {
        MakeActive(activeTab)
    }
});

function MakeActive(id) {
    $('#' + id).addClass("active")
}
