function wireupHelpTooltipEvents(ondemand) {
    if (ondemand) {
        wireupHelpTooltipEventsOnDemand();
    }
    else {
        wireupHelpTooltipEventsPreFetch();
    }
}

function wireupHelpTooltipEventsPreFetch() {
    var hovertimeout;

    $(".helplink").hover(
        function (e) { hovertimeout = setTimeout(showToolTip, 1000, $(this.parentElement).find('.helptooltip').first(), e); },
        function (e) { closeAllTooltips(); }
    );

    function showToolTip(tooltip, target) { tooltip.fadeIn("slow", function () { }); }

    function closeAllTooltips() {
        clearTimeout(hovertimeout);
        $(".helptooltip:visible").each(function (index, tt) { $(tt).fadeOut("slow", function () { }); });
    }
}

function wireupHelpTooltipEventsOnDemand() {
    var lasthoverid = "";
    var tooltipcache = new Map();
    var hovertimeout;

    $(".helplink").hover(
        function (e) { hovertimeout = setTimeout(showToolTip, 1000, $(this.parentElement).find('.helptooltip').first(), e); },
        function (e) { closeAllTooltips(); }
    );

    function showToolTip(tooltip, eventtarget) {
        var lookupid = eventtarget.target.attributes['data-lookupkey'].nodeValue;
        var apiroot = eventtarget.target.attributes['data-apiroot'].nodeValue;
        var cachedresult = tooltipcache.get(lookupid);
        if (cachedresult === undefined) {
            if (lookupid !== lasthoverid) {
                $.ajax({
                    url: apiroot + '/api/helpinstruction/lookup/' + lookupid,
                    type: 'get',
                    success: function (msg) {
                        tooltip[0].innerHTML = msg.tooltipText;
                        tooltip.fadeIn("slow", function () { });
                        lasthoverid = lookupid;
                        tooltipcache.set(lookupid, msg);
                    }
                });
            }
        }
        else {
            tooltip[0].innerHTML = tooltipcache.get(lookupid).tooltipText;
            tooltip.fadeIn("slow", function () { });
            lasthoverid = lookupid;
        }
    }

    function closeAllTooltips() {
        clearTimeout(hovertimeout);
        $(".helptooltip:visible").each(function (index, tt) {
            $(tt).fadeOut("slow", function () { });
            lasthoverid = "";
        });
    }
}