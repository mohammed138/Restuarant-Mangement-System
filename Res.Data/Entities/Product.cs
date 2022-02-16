using Exam.Models.Baseclass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.Entities
{
   public class Product:BaseClass
    {
        public string Name { get; set; } 
        public string ShortDes { get; set; }
        public string LongDes { get; set; } 
        public string ProductImg { get; set; } 
        public double Price { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}
