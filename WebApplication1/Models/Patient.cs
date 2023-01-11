using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
   public class Patient
   {
        public int Id { get; set; }
      public string Name
      {
         get; set;
      }

      public string Password { get; set; }
      public string About { get; set; }

      public string hobby { get; set; }

      public string gender { get; set; }

      public string country { get; set; }
      public string SCountry { get; set; }
        [DataType(DataType.Date)]
        public DateTime Bdate { get; set; }
        public bool isActive { get; set; }
      public string profilePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase profile { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
    }
}