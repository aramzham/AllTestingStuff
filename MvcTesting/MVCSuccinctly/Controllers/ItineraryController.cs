using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSuccinctly.Models;

namespace MVCSuccinctly.Controllers
{
    public class ItineraryController : Controller
    {
        // GET: Itinerary
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new ItineraryItem();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ItineraryItem data)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {
                ModelState.AddModelError("", "Not valid data");
            }
            return View();
        }

        [HttpGet]
        public JsonResult VerifyAvailability(DateTime when, string description)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}