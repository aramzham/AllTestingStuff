﻿(function () {
    window.onload = function () {
        document.getElementById("getAll").onclick = getEmployees;
        document.getElementById("getById").onclick = getEmployeeById;
        document.getElementById("add").onclick = addButtonAction;
        document.getElementById('submit').onclick = submitNewEmployee;
    }
})();