function GetAllEmployees() {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "http://localhost:59406/api/employee", true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            document.getElementById("mainTable").innerHTML = xhr.responseText;
        }
    }
    xhr.send();
}

function GetEmployeeById() {
    var id = document.getElementById("idBox").value;

    var xhr = new XMLHttpRequest();
    xhr.open("GET", "http://localhost:59406/api/employee?id={id}".replace("{id}",id), true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            document.getElementById("mainTable").innerHTML = xhr.responseText;
        }
    }
    xhr.send();
}