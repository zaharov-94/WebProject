$.getJSON('../../Home/GetAllPublish/', function (json) {
    var location = window.location.pathname.split("/");
    var bookId = location[location.length - 1];

    $("#SelectedPublicationHouses").kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "Id"
      });

    $.getJSON('../../Home/GetSelectedPublish/'+bookId, function (jsonsel) {
        var dataSource = new kendo.data.DataSource({
            data: json,
            pageSize: 500
        });

        var multiselect = $("#SelectedPublicationHouses").data("kendoMultiSelect");
        multiselect.setDataSource(dataSource);
        multiselect.value(jsonsel);
    });
});
