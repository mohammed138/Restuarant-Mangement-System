using Exam.Models.Baseclass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.Entities
{
  public  class Booking:BaseClass
    {

        [Display(Name = "Notes")]
        public string? Notes { get; set; }


        [Display(Name= "Booking Type")]
        public int? BookingTypeId { get; set; }
        [ForeignKey("BookingTypeId")]
        public BookingType BookingType { get; set; }



        //[Display(Name = "Booking Description")]
        //public string? BookingDescription { get; set; }
  



        [Display(Name = "Table")]
        public int? TablesId { get; set; }
        [ForeignKey("TablesId")]
        public Tables Tables { get; set; }


        [Display(Name = "Special Event Name")]
        public int? SpecialEventId { get; set; }
        [ForeignKey("SpecialEventId")]
        public SpecialEvent SpecialEvent { get; set; }


        [Display(Name = "Products")]
        public int? ProductsId { get; set; }
        [ForeignKey("ProductsId")]
        public Product Product { get; set; }


        [Display(Name = "Number Of People")]
        public int? NumberOfPeopleId { get; set; }  
        [ForeignKey("NumberOfPeopleId")]
        public NumberOfPeople NumberOfPeople { get; set; }

        public int? BookingStatusId { get; set; }
        [ForeignKey("BookingStatusId")]
        public BookingStatus BookingStatus { get; set; }


        [Display(Name = "Booking Date")]
        public DateTime? BookingDate { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Display(Name = "Phone No")]
        public int? PhoneNo { get; set; }

      
    }
}
