using Exam.Models.Baseclass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res.Data.Entities
{
  public  class Tables:BaseClass
    {
        public string TableName { get; set; }
        public string? TableDescription { get; set; }
        public string? TableLocation { get; set; }

    }
}
