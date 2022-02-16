using Exam.Models.Baseclass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.Entities
{
  public  class SpecialEvent : BaseClass
    {
         
        public string EventName { get; set; }
        public string EventImg { get; set; }
        public string? Description { get; set; } 
        public int? EventPrice { get; set; }
         
    }
}
