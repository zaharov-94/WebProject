$(document).ready(function () {
    var grid = $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: window.location.origin + "/Book/List/"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        Name: { type: "string" },
                        Author: { type: "string" },
                        YearOfPublishing: { type: "number" }
                    }
                }
            },
            autoSync: true,
            pageSize: 20
        },
        height: 500,
        sortable: true,
        pageable: true,
        columns: [
        { field: "Name", title: "Name" },
        { field: "Author", title: "Author" },
        { field: "YearOfPublishing", title: "Year of publishing" },
        { command: [{ text: "Edit", click: Edit }, { text: "Delete", click: Delete }], title: "&nbsp;", width: "250px" }]
    });
    if (document.getElementById('Create')==null) {
        grid.data("kendoGrid").setOptions({
            columns: [
                { field: "Name", title: "Name" },
                { field: "Author", title: "Author" },
                { field: "YearOfPublishing", title: "Year of publishing" }]
        });
    }    
});
function Edit(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    window.location.pathname = "Book/Edit/" + dataItem.Id;
}
function Delete(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

    $.ajax({
        url: window.location.origin + "/Book/Delete/" + dataItem.Id,
        type: "Get",
        success: function () {
            $('#grid').data('kendoGrid').dataSource.read();
        }
    });
}