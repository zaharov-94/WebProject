$(document).ready(function () {
    var grid = $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: window.location.origin + "/PublicationHouse/List/"
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        Name: { type: "string" },
                        Address: { type: "string" }
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
        { field: "Address", title: "Address" },
        { command: [{ text: "Edit", click: Edit }, { text: "Delete", click: Delete }], title: "&nbsp;", width: "250px" }]
    });
    if (document.getElementById('Create') == null) {
        grid.data("kendoGrid").setOptions({
            columns: [
                { field: "Name", title: "Name" },
                { field: "Address", title: "Address" }]
        });
    } 
});
function Edit(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    window.location.pathname = "PublicationHouse/Edit/" + dataItem.Id;
}
function Delete(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

    $.ajax({
        url: window.location.origin + "/PublicationHouse/Delete/" + dataItem.Id,
        type: "Get",
        success: function () {
            $('#grid').data('kendoGrid').dataSource.read();
        }
    });
}