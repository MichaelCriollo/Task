using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Task.Entity.Entities;

namespace Task.WebSite.ServiceConsumer
{
    public class EmployeeSC
    {
        private string UrlWebApi;

        public IEnumerable<Employee> FindAll()
        {
            string responseMessage = string.Empty;
            IEnumerable<Employee> ListEmployees = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    UrlWebApi = string.Format("{0}Employee/AllEmployees", ConfigurationManager.AppSettings["UrlWebApi"]);
                    client.BaseAddress = new Uri(UrlWebApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage GetResponse = client.GetAsync(UrlWebApi).Result;
                    switch (GetResponse.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            responseMessage = GetResponse.Content.ReadAsStringAsync().Result;
                            ListEmployees = JsonConvert.DeserializeObject<IEnumerable<Employee>>(responseMessage);
                            break;
                    }
                }
                return ListEmployees;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}