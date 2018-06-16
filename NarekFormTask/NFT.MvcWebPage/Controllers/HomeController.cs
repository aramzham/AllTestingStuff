using NFT.MvcWebPage.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFT.MvcWebPage.Controllers
{
    public class HomeController : Controller
    {
        private DB_Manager db_manager = new DB_Manager();

        public ActionResult Index()
        {
            return View();
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

        //[HttpGet, Route("api/employee")]
        //public async Task<IHttpActionResult> GetAllEmployees()
        //{
        //    var employees = await db_manager.GetAllEmployees();
        //    if (employees == null) return BadRequest();
        //    return Ok(employees);
        //}

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