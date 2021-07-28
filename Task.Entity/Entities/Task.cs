using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Task.Entity.Entities
{
    public partial class Task
    {
        [Key]
        public int IdTask { get; set; }

        public int? IdEmployee { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateEnd { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ListEmployees { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
