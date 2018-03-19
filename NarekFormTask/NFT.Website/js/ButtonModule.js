function getEmployees() {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "http://localhost:59406/api/employee", true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            fillTable(xhr.responseText);
        }
    }
    xhr.send();
}

function getEmployeeById() {
    var id = document.getElementById("idBox").value;

    var xhr = new XMLHttpRequest();
    xhr.open("GET", "http://localhost:59406/api/employee?id={id}".replace("{id}", id), true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            fillTable(xhr.responseText);
        }
    }
    xhr.send();
}

function fillTable(list) {
    var tbody = document.getElementById("mainTable").children[1];
    //var tbody = table.getElementsByTagName("tbody")[0];
    tbody.innerHTML = "";
    var json = JSON.parse(list);
    if (json instanceof Array) {
        for (var i = 0; i < json.length; i++) {
            tbody.innerHTML += "<tr>" + "<td>" + json[i]["name"] + "</td>" + "<td>" + json[i].surname + "</td>" + "</tr>"
        }
    }
    else {
        tbody.innerHTML = "<tr>" + "<td>" + json["name"] + "</td>" + "<td>" + json.surname + "</td>" + "</tr>";
    }
}