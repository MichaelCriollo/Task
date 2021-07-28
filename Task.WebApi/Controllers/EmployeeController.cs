using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task.BusinessLogic.Class;
using Task.Entity.Entities;

namespace Task.WebApi.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        private EmployeeBL employeeBL;
        public EmployeeController()
        {
            employeeBL = new EmployeeBL();
        }

        [HttpGet]
        [Route("AllEmployee")]
        public HttpResponseMessage FindAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                IEnumerable<Employee> EmployeeFound = employeeBL.FindAll();
                if (EmployeeFound != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, EmployeeFound);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
    }
}
