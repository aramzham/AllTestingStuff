(function () {
    window.onload = function () {
        document.getElementsByName("getAll")[0].onclick = GetAllEmployees;
        document.getElementsByName("getById")[0].onclick = GetEmployeeById;
    }
})();