using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.WebSite.Models
{
    public class SweetAlertModel
    {
        public class SweetAlert
        {
            public const string TempDataKey = "TempDataSweetAlerts";
            public string title { get; set; }
            public string message { get; set; }
            public string type { get; set; }
        }

        public class SweetAlertStyle
        {
            public const string success = "success";
            public const string error = "error";
            public const string warning = "warning";
            public const string info = "info";
            public const string question = "question";
        }

        public static SweetAlert GetSweetAlert(string title, string message, string type)
        {
            SweetAlert sweetAlert = new SweetAlert
            {
                title = title,
                message = message,
                type = type
            };

            return sweetAlert;
        }
    }
}