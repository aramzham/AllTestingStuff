﻿$(document).ready(function () {
    var rowTemplate = '<tr id="{id}"><td>{id}</td><td>{name}</td><td>{surname}</td><td>{salary}</td><td>{isBonus}</td><td>{univId}</td><td>{info}</td><td><img src="C:\Users\Aram\Source\Repos\AllTestingStuff\NarekFormTask\NFT.MvcWebPage\Content\Images\edit.jpg" class="editImg"></td><td class="chbox" style="display:none"><input type="checkbox" value="" /></td></tr>';

    var ids = [];

    $(".chbox").hide();
    $("#checkallTh").hide();
    $("#cancelButton").hide();
    $('button').addClass('btn btn-primary');
    $(".editImg").on("click", editRow);
    $("#addButton").on("click", addTextBoxes);
    $("#deleteButton").on("click", deleteToConfirm);
    //$("#cancelButton").on("click", function () { saveToAdd(); confirmToDelete(); });
    $('#checkall').click(function () {
        var checked = $(this).prop('checked');
        $("input[type='checkbox']").prop('checked', checked);
    });
    $(":checkbox").on("change", function () {
        if (this.checked) {
            ids.push($(this).closest("tr").attr("id"));
        } else {
            var index = ids.indexOf($(this).closest("tr").attr("id"));
            if (index != -1) ids.splice(index, 1);
        }
    });

    function saveEditedEmployee() {
        var currentTr = $(this).closest("tr");
        var trChildren = currentTr.children("td");
        var editId = trChildren.eq(0).text();
        var editName = trChildren.eq(1).children("input").val();
        var editSurname = trChildren.eq(2).children("input").val();
        var editSalary = trChildren.eq(3).children("input").val();
        var editIsBonus = trChildren.eq(4).children("input").val();
        var editUnivId = trChildren.eq(5).children("input").val();
        var editInfo = trChildren.eq(6).children("input").attr("value");
        var dataToSave = { id: editId, name: editName, surname: editSurname, salary: editSalary, isGettingBonus: editIsBonus, universityId: editUnivId, info: editInfo };
        $("#yesBtn").unbind("click");
        $("#noBtn").unbind("click");

        $("#myModal").modal('toggle');

        $("#yesBtn").on("click",
            function () {
                $.ajax({
                    url: '/home/EditEmployeeById',
                    type: "POST",
                    data: { editEmployee: dataToSave },
                    success: function (result) {
                        for (var i = 0; i < 7; i++) {
                            trChildren.eq(i).html(trChildren.eq(i).children("input").val());
                        }
                        trChildren.eq(7).children("img").attr('src', '../Content/Images/edit.png');
                        //trChildren.eq(7).children("img").removeAttr("alt");
                        $(".saveImg").unbind("click");
                        $(".saveImg").on("click", editRow);
                        $(".saveImg").attr("class", "editImg");
                        //$(this).closest("tr").replaceWith('<tr id="' + editId + '"><td>' + editId + '</td><td>' + editName + '</td><td>' + editSurname + '</td><td>' + editSalary + '</td><td>' + editIsBonus + '</td><td>' + editUnivId + '</td><td>' + editInfo + '</td></tr>');
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.statusText);
                        alert(thrownError);
                    }
                });
            });
        $("#noBtn").on("click", function () {
            console.log(rowTemplate.replace(/{id}/g, editId).replace("{name}", editName).replace("{surname}", editSurname).replace("{salary}", editSalary).replace("{isBonus}", editIsBonus).replace("{univId}", editUnivId).replace("{info}", editInfo));
            currentTr.replaceWith(rowTemplate.replace(/{id}/g, editId).replace("{name}", editName).replace("{surname}", editSurname).replace("{salary}", editSalary).replace("{isBonus}", editIsBonus).replace("{univId}", editUnivId).replace("{info}", editInfo));
        });
        
        $(".chbox").hide();
    }

    function editRow() {
        var currentTrId = $(this).closest("tr").attr("id");
        $("#" + currentTrId + " td:gt(0)").each(function () {
            //if ($(this).is($("#" + currentTrId).children(":first"))) return; else
            if ($(this).html().indexOf("img src") != -1) {
                var img = $(this).children().eq(0);
                img.attr('class', 'saveImg');
                img.attr('src', '../Content/Images/save.png');
                img.attr('alt', 'chka nkar');
                img.removeAttr("data-toggle");
                img.removeAttr("data-target");
                img.unbind("click");
                img.on("click", saveEditedEmployee);
            }
            else if ($(this).html().indexOf('checkbox') == -1) $(this).html('<input type="text" value="' + $(this).text() + '"/>');
        });
    }

    function addRow(result) {
        var addName = $("#addName").val() ? $("#addName").val() : "no name";
        var addSurname = $("#addSurname").val() ? $("#addSurname").val() : "no surname";
        var addSalary = $("#addSalary").val() ? $("#addSalary").val() : 0;
        var addIsBonus = $("#addIsBonus").is(":checked") ? true : false
        var addUnivId = $("#addUnivId").val() ? $("#addUnivId").val() : 1
        var addInfo = $("#addInfo").val() ? $("#addInfo").val() : "no info";
        $('#employees tr:last').after(rowTemplate.replace(/{id}/g, result).replace("{name}", addName).replace("{surname}", addSurname).replace("{salary}", addSalary).replace("{isBonus}", addIsBonus).replace("{univId}", addUnivId).replace("{info}", addInfo));
        $(".chbox").hide();
    }

    function saveEmployee() {
        $.ajax({
            url: '@Url.Action("AddEmployee")',
            //dataType: "json",
            type: "POST",
            data: { toAdd: [$("#addName").val(), $("#addSurname").val(), $("#addSalary").val(), $("#addIsBonus").is(":checked") ? true : false, $("#addUnivId").val(), $("#addInfo").val()] },
            success: function (result) {
                //$("#helpContent").empty().append(result);
                //$("#helpContent").show();
                //$("#addButton").removeClass("helpTrigger").addClass("closeTrigger");
                addRow(result);
                saveToAdd();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.statusText);
                alert(thrownError);
            }
        });
    }

    function addTextBoxes() {
        //$("#helpContent").empty();
        //$("#helpContent").hide();
        //$("#helpTrigger").removeClass("closeTrigger").addClass("helpTrigger");
        $("#employees tr:last").after("<tr id=\"addBlocks\"\ ><td></td><td><input type=\"text\" id=\"addName\"/></td><td><input type=\"text\" id=\"addSurname\"/></td><td><input type=\"text\" id=\"addSalary\"/></td><td><input type=\"checkbox\" id=\"addIsBonus\"/></td><td><input type=\"number\" id=\"addUnivId\"/></td><td><input type=\"text\" id=\"addInfo\"/></td></tr>");
        $("#addBlocks").animate({ height: "200px" });
        $("#addBlocks").animate({ height: "" });
        $("#addButton").text("Save");
        $("#cancelButton").show();
        $("#cancelButton").on("click", saveToAdd);
        $("#addButton").unbind("click");
        $("#addButton").on("click", saveEmployee);
        //$("#deleteButton").addClass("disabled");
        //$("#deleteButton").unbind("click");
        $("#deleteButton").prop("disabled", true);
    }

    function saveToAdd() {
        $("#addButton").unbind("click");
        $("#addButton").click(addTextBoxes);
        $("#addButton").text("Add");
        $("#cancelButton").hide();
        $("#cancelButton").unbind("click");
        $("#addBlocks").remove();
        //$("#deleteButton").removeClass("disabled");
        //$("#deleteButton").on("click", deleteToConfirm);
        $("#deleteButton").removeAttr("disabled");
    }

    function deleteToConfirm() {
        $(".chbox").show();
        $("#checkallTh").show();
        $("#cancelButton").show();
        $("#cancelButton").on("click",confirmToDelete);
        $("#deleteButton").text("Confirm");
        $("#deleteButton").unbind("click");
        $("#deleteButton").on("click", submitDelete);
        //$("#addButton").addClass("disabled");
        //$("#addButton").unbind("click");
        $("#addButton").prop("disabled",true);
    }

    function submitDelete() {
        if (ids != undefined && ids.length > 0) {
            $("#yesBtn").unbind("click");
            $("#noBtn").unbind("click");

            $("#myModal").modal('toggle');
            $("#yesBtn").on("click", function() {
                $.ajax({
                    url: '/home/RemoveEmployeesByIds',
                    data: { 'ids': ids },
                    type: "POST",
                    //dataType: "json",
                    success: function (result, status, jqXHR) {
                        for (var i = 0; i < ids.length; i++) {
                            $("#" + ids[i].toString()).remove();
                        }
                        uncheckChecked();
                        confirmToDelete();
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.statusText);
                        alert(thrownError);
                    }
                });
            });
            $("#noBtn").on("click", function() {
                uncheckChecked();
                confirmToDelete();
            });
        }
    }

    function uncheckChecked() {
        $("input[type='checkbox']").prop('checked', false);
        ids = [];
    }

    function confirmToDelete() {
        $(".chbox").hide("slow", "linear");
        $("#cancelButton").hide();
        $("#cancelButton").unbind("click");
        $("#checkallTh").hide("slow", "swing");
        $("#deleteButton").text("Delete");
        $("#deleteButton").unbind("click");
        $("#deleteButton").on("click", deleteToConfirm);
        //$("#addButton").removeClass("disabled");
        //$("#addButton").on("click",addTextBoxes);
        $("#addButton").removeAttr("disabled");
    }
});