using NFT.MvcWebPage.Infrastructure;
using NFT.MvcWebPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NFT.MvcWebPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly DB_Manager _dbManager = new DB_Manager();

        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            var employees = await _dbManager.GetAllEmployees();
            if (employees == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "naughty");
            return View(employees);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult AddEmployee(string[] toAdd)
        {
            if (toAdd is null || toAdd.Length != 6) return null;
            var emp = new Employee()
            {
                Info = string.IsNullOrEmpty(toAdd[5]) ? "no info" : toAdd[5],
                IsGettingBonus = bool.Parse(toAdd[3]),
                Name = string.IsNullOrEmpty(toAdd[0]) ? "no name" : toAdd[0],
                Salary = decimal.TryParse(toAdd[2], out var s) ? s : 0,
                Surname = string.IsNullOrEmpty(toAdd[1]) ? "no surname" : toAdd[1],
                UniversityId = int.TryParse(toAdd[4], out var uid) ? uid : 1
            };
            var id = _dbManager.AddEmployee(emp);
            return Json(id);
        }

        public JsonResult Test()
        {
            return Json(20, JsonRequestBehavior.AllowGet);
        }

        public void RemoveEmployeesByIds(List<int> ids)
        {
            if (ids is null || ids.Count == 0) return;
            _dbManager.DeleteEmployeeByIds(ids);
        }

        public void EditEmployeeById(Employee editEmployee)
        {
            if(editEmployee is null || editEmployee.Id == default(int)) return;
            //var emp = new Employee()
            //{
            //    Name = string.IsNullOrEmpty(editEmployee.Item2[0]) ? "no name" : editEmployee.Item2[0],
            //    Surname = string.IsNullOrEmpty(editEmployee.Item2[1]) ? "no surname" : editEmployee.Item2[1],
            //    Salary = decimal.TryParse(editEmployee.Item2[2], out var s) ? s : 0,
            //    IsGettingBonus = bool.Parse(editEmployee.Item2[3]),
            //    UniversityId = int.TryParse(editEmployee.Item2[4], out var uid) ? uid : 1,
            //    Info = string.IsNullOrEmpty(editEmployee.Item2[5]) ? "no info" : editEmployee.Item2[5],
            //};
            _dbManager.EditEmployeeById(editEmployee.Id, editEmployee);
        }
    }
}