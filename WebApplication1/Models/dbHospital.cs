using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class dbHospital:DbContext
    {
        public dbHospital():base("dbHospital")
        {

        }
        public DbSet<Patient> Patients { get; set; }
    }
}