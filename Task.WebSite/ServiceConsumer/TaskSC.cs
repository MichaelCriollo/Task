using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Task.WebSite.ServiceConsumer
{
    public class TaskSC
    {
        private string UrlWebApi;

        public TaskSC() { }

        public Entity.Entities.Task Create(Entity.Entities.Task task)
        {
            string responseMessage = string.Empty;
            Entity.Entities.Task taskCreate = new Entity.Entities.Task();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    UrlWebApi = string.Format("{0}Task/AddTask", ConfigurationManager.AppSettings["UrlWebApi"]);
                    client.BaseAddress = new Uri(UrlWebApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage GetResponse = client.PostAsync(UrlWebApi, task, new JsonMediaTypeFormatter { }).Result;
                    switch (GetResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            responseMessage = GetResponse.Content.ReadAsStringAsync().Result;
                            taskCreate = JsonConvert.DeserializeObject<Entity.Entities.Task>(responseMessage);
                            break;
                    }
                }
                return taskCreate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(Entity.Entities.Task task)
        {
            string responseMessage = string.Empty;
            Entity.Entities.Task taskUpdate = new Entity.Entities.Task();
            bool bUpdate = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    UrlWebApi = string.Format("{0}Task/UpdateTask", ConfigurationManager.AppSettings["UrlWebApi"]);
                    client.BaseAddress = new Uri(UrlWebApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage GetResponse = client.PutAsync(UrlWebApi, task, new JsonMediaTypeFormatter { }).Result;
                    switch (GetResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            responseMessage = GetResponse.Content.ReadAsStringAsync().Result;
                            bUpdate = Convert.ToBoolean(JsonConvert.DeserializeObject(responseMessage));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return bUpdate;
        }

        public Entity.Entities.Task FindById(int idTask)
        {
            string responseMessage = string.Empty;
            Entity.Entities.Task Task = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    UrlWebApi = string.Format("{0}Task/FindByIdTask", ConfigurationManager.AppSettings["UrlWebApi"]);
                    client.BaseAddress = new Uri(UrlWebApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage GetResponse = client.PostAsync(UrlWebApi, idTask, new JsonMediaTypeFormatter { }).Result;
                    switch (GetResponse.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            responseMessage = GetResponse.Content.ReadAsStringAsync().Result;
                            Task = JsonConvert.DeserializeObject<Entity.Entities.Task>(responseMessage);
                            break;
                    }
                }
                return Task;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Entity.Entities.Task> FindAll()
        {
            string responseMessage = string.Empty;
            try
            {
                IEnumerable<Entity.Entities.Task> TaskFound = null;
                using (HttpClient client = new HttpClient())
                {
                    UrlWebApi = string.Format("{0}Task/AllTasks", ConfigurationManager.AppSettings["UrlWebApi"]);
                    client.BaseAddress = new Uri(UrlWebApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage GetResponse = client.GetAsync(UrlWebApi).Result;
                    switch (GetResponse.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            responseMessage = GetResponse.Content.ReadAsStringAsync().Result;
                            TaskFound = JsonConvert.DeserializeObject<IEnumerable<Entity.Entities.Task>>(responseMessage);
                            break;
                        case HttpStatusCode.NotFound:
                            return TaskFound;
                        case HttpStatusCode.InternalServerError:
                            responseMessage = GetResponse.Content.ReadAsStringAsync().Result;
                            break;
                    }
                }
                return TaskFound;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Entity.Entities.Task task)
        {
            string responseMessage = string.Empty;
            bool bDeleteTask = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    UrlWebApi = string.Format("{0}Task/DeleteTask", ConfigurationManager.AppSettings["UrlWebApi"]);
                    client.BaseAddress = new Uri(UrlWebApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage GetResponse = client.PutAsync(UrlWebApi, task, new JsonMediaTypeFormatter { }).Result;
                    switch (GetResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            responseMessage = GetResponse.Content.ReadAsStringAsync().Result;
                            bDeleteTask = Convert.ToBoolean(JsonConvert.DeserializeObject(responseMessage));
                            break;
                    }
                }
                return bDeleteTask;
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
    }
}