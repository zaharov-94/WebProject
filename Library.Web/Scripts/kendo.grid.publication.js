$(document).ready(function () {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: window.location.origin + "/AllPublication/List/"
            },
            schema: {
                model: {
                    fields: {
                        Name: { type: "string" },
                        Type: { type: "string" }
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
        { field: "Type", title: "Type" }]
    });
});
