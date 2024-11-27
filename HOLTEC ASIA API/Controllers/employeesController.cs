using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using HOLTEC_ASIA_API.Models;

namespace HOLTEC_ASIA_API.Controllers
{
   // [RoutePrefix("api/employees")]
    public class employeesController : ApiController
    {
        iemployeebl bl; 

        public employeesController(iemployeebl _bl) {
        
        bl = _bl;
        }
        //  private HOLTEC_ASIADBCONTEXT db = new HOLTEC_ASIADBCONTEXT();


         [Route("api/employees")]
        [ResponseType(typeof(IEnumerable<employee>))]
        public IHttpActionResult GetAllEmployees()
        {
            var allEmployees = bl.getall().Select(e => new
            {
                e.firstname,
                e.lastname,
                e.age,
                e.dob,
                e.address,
                e.Email,
               e.gender,
            });
            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("api/employees/byemail")]
        [ResponseType(typeof(employee))]  
        public IHttpActionResult GetEmployeeByEmail(string email)
        {
            employee employee = bl.getbyemail(email); 
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(new { employee.firstname,
        employee.lastname,
        employee.age,
        employee.dob,
        employee.address,
        employee.Email,
       employee.gender,
           }); // Serialize as string (e.g., "Male"));
        }

        [HttpPut]
        [Route("api/employees")]
     //   [ResponseType(typeof(void))]
        public IHttpActionResult Putemployee([FromUri]string  email, [FromBody]employee employee)
        {

            try
            {
                if (email != null && employee != null)
                {
                    if (email == employee.Email)
                    {
                        bl.update(employee);
                        return Ok();

                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("api/employees")]
        [ResponseType(typeof(employee))]
        public IHttpActionResult Postemployee([FromBody]employee employee)
        {
            try
            {
                if (employee != null && employee.gender>0)
                {

                    bl.create(employee);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        [HttpDelete]
        [Route("api/employees")]
        [ResponseType(typeof(employee))]
        public IHttpActionResult Deleteemployee(string email)
        {
            try
            {
                if (email != null)
                {
                    employee employee = bl.getbyemail(email);
                    if (employee != null)
                    {
                        bl.delete(email);
                        return Ok(employee);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {

                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

       
    }
  

}