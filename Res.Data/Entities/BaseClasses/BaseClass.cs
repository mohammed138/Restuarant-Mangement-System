using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Models.Baseclass 
{
   public class BaseClass
    {
        [Key]
        public int Id { get; set; }
         
        public bool? IsDelete { get; set; }
         public string? UserAdd { get; set; }
        public string? UserEdit { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }


    }
}
