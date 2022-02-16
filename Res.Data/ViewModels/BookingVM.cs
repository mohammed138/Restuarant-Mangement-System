using Microsoft.AspNetCore.Mvc.Rendering;
using Res.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.ViewModels
{
   public class BookingVM
    {
        public Booking Booking { get; set; }

        public IEnumerable<Booking> BookingList { get; set; }
        public IEnumerable<SelectListItem> BookingStatusDrop { get; set; }
        public IEnumerable<SelectListItem> BookingTypeDrop { get; set; }
        public IEnumerable<SelectListItem> NumberOfPeopleDrop { get; set; } 
        public IEnumerable<SelectListItem> ProductDrop { get; set; }
        public IEnumerable<SelectListItem> SpecialEventDrop { get; set; } 
        public IEnumerable<SelectListItem> TablesDrop { get; set; }

    }
}
