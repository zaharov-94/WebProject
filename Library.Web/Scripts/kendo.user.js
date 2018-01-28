$.getJSON('../../Home/GetAllPublish/', function (json) {
    $("#multiselect").kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "Id"
    });
    var bookId = 1;
    $.getJSON('../../Home/GetSelectedPublish/'+bookId, function (jsonsel) {
        var dataSource = new kendo.data.DataSource({
            data: json,
            pageSize: 500
        });

        var multiselect = $("#multiselect").data("kendoMultiSelect");
        multiselect.setDataSource(dataSource);
        multiselect.value(jsonsel);
    });
});

