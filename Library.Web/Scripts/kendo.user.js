$.getJSON('../../Book/GetAllPublish/', function (json) {
    var location = window.location.pathname.split("/");
    var bookId = location[location.length - 1];

    $("#PublicationHouses").kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "Id",
    });

    $.getJSON('../../Book/GetSelectedPublish/' + bookId, function (jsonsel) {
        var dataSource = new kendo.data.DataSource({
            data: json,
            pageSize: 500
        });

        var multiselect = $("#PublicationHouses").data("kendoMultiSelect");
        multiselect.setDataSource(dataSource);
        multiselect.value(jsonsel);
    });
});
