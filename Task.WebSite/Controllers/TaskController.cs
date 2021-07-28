using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.WebSite.Models;
using Task.WebSite.ServiceConsumer;
using static Task.WebSite.Models.SweetAlertModel;

namespace Task.WebSite.Controllers
{
    public class TaskController : Controller
    {
        private TaskSC taskSC;
        private EmployeeSC employeeSC;

        public TaskController()
        {
            taskSC = new TaskSC();
            employeeSC = new EmployeeSC();
        }

        /// <summary>
        /// List All Task
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            IEnumerable<Entity.Entities.Task> ListTasks = null;
            try
            {
                ListTasks = taskSC.FindAll();
                if (ListTasks == null)
                {
                    TempData[SweetAlertModel.SweetAlert.TempDataKey] = SweetAlertModel.GetSweetAlert("Información",
                                                                   "No se encontraron tareas registrados",
                                                                   SweetAlertStyle.info);
                }
                else
                {
                    TempData["Tasks"] = ListTasks;
                    return View(ListTasks.ToList());
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData[SweetAlertModel.SweetAlert.TempDataKey] = SweetAlertModel.GetSweetAlert("Error",
                                                                 "Se presentó un error al consultar el listado de tareas.",
                                                                 SweetAlertStyle.error);
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Create Task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public ActionResult Create(Entity.Entities.Task task)
        {
            List<Entity.Entities.Task> ListTasks = taskSC.FindAll().ToList();
            task.IdEmployee = 1;
            task.Name = "Tarea " + (ListTasks.Count + 1);
            task.Status = false;
            task.DateStart = DateTime.Now;
            task.DateEnd = task.DateStart.AddDays(7);
            task = taskSC.Create(task);
            if (task.IdTask > 0)
            {
                TempData[SweetAlertModel.SweetAlert.TempDataKey] = SweetAlertModel.GetSweetAlert("Información",
                                                                   "La tarea se creo correctamente",
                                                                   SweetAlertStyle.error);
                return RedirectToAction("List", "Task");
            }
            else
            {
                TempData[SweetAlertModel.SweetAlert.TempDataKey] = SweetAlertModel.GetSweetAlert("Error",
                                                                   "Se presento un error al crear la tarea",
                                                                   SweetAlertStyle.error);
                return RedirectToAction("List", "Task");
            }

        }

        [HttpGet]
        public ActionResult Update(int IdTask)
        {
            Entity.Entities.Task task = taskSC.FindById(IdTask);
            return Update(task);
        }

        [HttpPost]
        public ActionResult Update(Entity.Entities.Task task)
        {
            task.Name = "Tarea " + task.IdTask + " Actualizada";
            task.Status = true;
            task.DateEnd = DateTime.Now;
            bool bUpdate = taskSC.Update(task);
            if (bUpdate)
            {
                TempData[SweetAlertModel.SweetAlert.TempDataKey] = SweetAlertModel.GetSweetAlert("Información",
                                                                   "La tarea se actualizo correctamente",
                                                                   SweetAlertStyle.error);
                return RedirectToAction("List", "Task");
            }
            else
            {
                TempData[SweetAlertModel.SweetAlert.TempDataKey] = SweetAlertModel.GetSweetAlert("Error",
                                                                   "Se presento un error al actualizar la tarea",
                                                                   SweetAlertStyle.error);
                return RedirectToAction("List", "Task");
            }
        }

        [HttpPost]
        public ActionResult Delete(int idTask)
        {
            Entity.Entities.Task task = taskSC.FindById(idTask);
            if(task.IdTask == null)
            {
                return RedirectToAction("List", "Task");
            }
            bool bDeleteTask = taskSC.Delete(task);
            if (bDeleteTask)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("List", "Task");
        }

        
    }
}