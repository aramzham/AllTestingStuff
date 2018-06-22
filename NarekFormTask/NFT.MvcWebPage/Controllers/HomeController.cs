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

        public void AddEmployee(string[] toAdd)
        {
            if (toAdd is null || toAdd.Length != 6) return;
            var emp = new Employee() { Info = toAdd[5], IsGettingBonus = bool.Parse(toAdd[3]), Name = toAdd[0], Salary = decimal.Parse(toAdd[2]), Surname = toAdd[1], UniversityId = int.Parse(toAdd[4]) };
            _dbManager.AddEmployee(emp);
        }

        public void RemoveEmployeesByIds(List<int> ids)
        {
            if (ids is null || ids.Count == 0) return;
            _dbManager.DeleteEmployeeByIds(ids);
        }
        //// GET: api/Contacts?Guid=guid
        //[HttpGet, Route("api/employee"), ResponseType(typeof(Employee))]
        //public async Task<IHttpActionResult> GetEmployeeById([FromUri]int id)
        //{
        //    var employee = await db_manager.GetEmployeeById(id);
        //    if (employee == null) return NotFound();

        //    return Ok(employee);
        //}

        //// POST: api/Contacts
        //[HttpPost, Route("api/employee")]
        //public async Task<IHttpActionResult> PostEmployee([FromBody]Employee contact)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    //if ((await appManager.GetAllEmails()).Contains(contact.Email)) return BadRequest("A contact with such email already exists");

        //    db_manager.AddEmployee(contact);

        //    return CreatedAtRoute("DefaultApi", new { }, contact); //return chexav ado-ov shows up in location header
        //}
    }
}