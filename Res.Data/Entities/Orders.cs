using Exam.Models.Baseclass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.Entities
{
   public class Orders :BaseClass
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int? Qun { get; set; }
        public decimal? OrderTotal { get; set; }
        public string? Description { get; set; }
         
        public DateTime? OrderDate { get; set; }
         
        public int? OrdersStatusId { get; set; }
        [ForeignKey("OrdersStatusId")]
        public OrdersStatus OrdersStatus { get; set; }


         


        public string AddressName { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }

        //public string FullName = { FirstName + LastName };
        public string PhoneNo { get; set; }


    }
}
