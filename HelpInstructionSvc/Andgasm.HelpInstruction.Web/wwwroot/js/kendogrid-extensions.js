function mapTransportToJSON(e, t) {
    if (t === 'read') {
        if (e.filter !== undefined && e.filter !== null) {
            e.filter = e.filter.filters;
        }
    }
    return JSON.stringify(e);
}

function appendIdToTransportUrl(dataItem) {
    var i = this.url.lastIndexOf('/');
    var s = this.url.substring(0, i + 1);
    this.url = s + dataItem.internalId;
}

function handleGridError(e) {
    var grid = getGridForSender(this);
    if (grid.editable === undefined) {
        var err = e.errors[Object.keys(e.errors)[0]].errors[0];
        alert(err);
        grid.cancelChanges();
    }
    else {
        var gridEditElement = grid.editable.element;
        $.each(e.xhr.responseJSON, function (key, value) {
            var applicableColumn = getApplicableColumn(key, grid);
            var columnfield = Array.isArray(applicableColumn) ? applicableColumn[0].field.replace('.', '\\.') : applicableColumn.field.replace('.', '\\.');
            createValidationElementForField(gridEditElement, columnfield, value);
        });
    }
}

function getGridForSender(e) {
    var grid;
    $(".k-grid").each(function () {
        grid = $(this).data("kendoGrid");
        if (grid !== null && grid.dataSource === e.sender) {
            return grid;
        }
    });
    return grid;
}

function createValidationElementForField(gridEditElement, columnfield, value) {
    var validationelement = gridEditElement.find("[data-valmsg-for^=" + columnfield + "],[data-val-msg-for^=" + columnfield + "], [data-for^= " + columnfield + "]");
    if (validationelement.length === 0) {
        var src = gridEditElement.find("[data-container-for=" + columnfield + "]");
        validationelement = $("<span data-for='" + columnfield + "' class='k-invalid-msg' style='display: none;'></span>");
        validationelement.appendTo(src);
    }

    var validationMessageTemplate = createValidationTemplate();
    validationelement.replaceWith(validationMessageTemplate({ field: columnfield, message: value }));
    gridEditElement.find("input[name=" + columnfield + "]").focus();
}

function createValidationTemplate() {
    return kendo.template(
        "<div id='#=field#_validationMessage' " +
        "class='k-widget k-tooltip k-tooltip-validation " +
        "k-invalid-msg field-validation-error' " +
        "style='margin: 0.5em!important' " +
        "data-for='#=field#' " +
        "data-val-msg-for='#=field#' role='alert'>" +
        "<span class='k-icon k-warning'></span>" +
        "#=message#" +
        "<div class='k-callout k-callout-n'></div>" +
        "</div>");
}

function getApplicableColumn(col, grid) {
    var columns = grid.columns;
    var visibleColumns = [];
    jQuery.each(columns, function (index) {
        if (!this.hidden && this.field !== undefined) {
            visibleColumns.push({
                field: this.field,
                title: this.title
            });
        }
    });
    var applicablecolumn = visibleColumns.filter(function (column) { return column.field.toLowerCase() === col.toLowerCase(); });
    if (applicablecolumn.length === 0) applicablecolumn = visibleColumns.filter(function (column) { return column.field.toLowerCase() === col.toLowerCase(); });
    if (applicablecolumn.length <= 0) applicablecolumn = visibleColumns[0];
    return applicablecolumn;
}

function validatableTextBox(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    var val = $("<span data-for='" + options.field + "' class='k-invalid-msg' style='display: none;'></span>");
    input.attr(options.field, options.field);
    input.appendTo(container);
    val.appendTo(container);
    input.kendoMaskedTextBox();
}