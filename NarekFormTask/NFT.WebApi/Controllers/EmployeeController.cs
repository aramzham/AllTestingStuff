﻿using NFT.WebApi.Infrastructure;
using NFT.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace NFT.WebApi.Controllers
{
    //[RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private DB_Manager db_manager = new DB_Manager();

        [HttpGet, Route("api/employee")]
        public async Task<IHttpActionResult> GetAllEmployees()
        {
            var employees = await db_manager.GetAllEmployees();
            if (employees == null) return BadRequest();
            return Ok(employees);
        }

        // GET: api/Contacts?Guid=guid
        [HttpGet, Route("api/employee"), ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> GetEmployeeById([FromUri]int id)
        {
            var employee = await db_manager.GetEmployeeById(id);
            if (employee == null) return NotFound();

            return Ok(employee);
        }

        // POST: api/Contacts
        [HttpPost, Route("api/employee")]
        public async Task<IHttpActionResult> PostEmployee([FromBody]Employee contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //if ((await appManager.GetAllEmails()).Contains(contact.Email)) return BadRequest("A contact with such email already exists");

            db_manager.AddEmployee(contact);

            return CreatedAtRoute("DefaultApi", new { }, contact); //return chexav ado-ov shows up in location header
        }
    }
}
