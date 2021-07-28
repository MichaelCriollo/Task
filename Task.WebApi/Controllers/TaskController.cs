using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task.BusinessLogic.Class;

namespace Task.WebApi.Controllers
{
    [RoutePrefix("api/Task")]
    public class TaskController : ApiController
    {
        private TaskBL taskBL;
        public TaskController()
        {
            taskBL = new TaskBL();
        }

        [HttpPost]
        [Route("FindByIdTask")]
        public HttpResponseMessage FindById([FromBody] int idTask)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                Entity.Entities.Task TaskFound = taskBL.FindById(idTask);
                if (TaskFound != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, TaskFound);
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

        [HttpGet]
        [Route("AllTasks")]
        public HttpResponseMessage FindAll()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                IEnumerable<Entity.Entities.Task> TaskFound = taskBL.FindAll();
                if (TaskFound != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, TaskFound);
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

        [HttpPost]
        [Route("AddTask")]
        public HttpResponseMessage Create(Entity.Entities.Task task)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                Entity.Entities.Task taskCreate = taskBL.Create(task);
                response = Request.CreateResponse(HttpStatusCode.OK, taskCreate);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        [HttpPut]
        [Route("UpdateTask")]
        public HttpResponseMessage Update(Entity.Entities.Task task)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                bool taskUpdate = taskBL.Update(task);
                response = Request.CreateResponse(HttpStatusCode.OK, taskUpdate);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        [HttpPut]
        [Route("DeleteTask")]
        public HttpResponseMessage Delete(Entity.Entities.Task task)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                bool taskDelete = taskBL.Delete(task);
                response = Request.CreateResponse(HttpStatusCode.OK, taskDelete);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
    }
}
