﻿$.getJSON('../../Book/GetAllPublish/', function (json) {
    var val = $("#PublicationHouses");
        $("#PublicationHouses").kendoMultiSelect({
            dataTextField: "Name",
            dataValueField: "Id",
            dataSource: {
                schema: {
                    model: {
                        id: "Id",
                        fields: {
                            Id: { type: "number" },
                            Name: { type: "string" },
                            Address: { type: "string" },
                            Books: { type: "object" }
                        }
                    }
                },
                pageSize: 500
            }
        });
        var viewModel = kendo.observable({
            isPrimitive: false,
            PublicationHouses: json
        });  

        var multiselect = $("#PublicationHouses").data("kendoMultiSelect");
        kendo.bind($("#PublicationHouses"), viewModel);
});

$(document).ready(function () {
    $('form').submit(function () {
        var multiselect = $("#PublicationHouses").data("kendoMultiSelect");

        var $data = {};
        $('form').find('input').each(function () {
           $data[this.name] = $(this).val();
        });
        var otherFields = multiselect.dataItems();
        $data["PublicationHouses"] = otherFields;
        var obj = $.toJSON($data);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../../Book/Create/',
            data: obj
        });
        window.location.pathname = '../../Book/Index';
        return false;
    });
});