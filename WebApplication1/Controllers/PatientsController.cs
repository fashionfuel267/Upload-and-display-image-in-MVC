using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PatientsController : Controller
    {
        dbHospital db=new dbHospital();
        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.OrderBy(p=>p.Name).Where(p=>p.isActive).ToList());
        }

      public ActionResult Create() {

         return View();
      }
        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            string folderpath =Path.Combine( Server.MapPath("~/"),"PatientProfile");
            string fname = Path.GetFileName(patient.profile.FileName);
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            if(patient.profile !=null)
            {
                patient.profile.SaveAs(Path.Combine( folderpath, fname));
            }

            patient.profilePath = "~/PatientProfile/" + fname;

            //image upload
            if (patient.Image!=null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/"), "PatientImage");
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }
                string ext = Path.GetExtension(patient.Image.FileName);
                string imgFileName = Path.GetFileName(patient.Image.FileName).ToString();
                if(ext.ToLower()==".jpg"|| ext.ToLower() == ".jpeg" || ext.ToLower() == ".png")
                {
                    patient.Image.SaveAs(Path.Combine(imagePath, imgFileName));
                    patient.ImagePath = "~/PatientImage/" + imgFileName;
                }
                else
                {
                    ViewBag.mesaage = "please provide jpg/jpeg/png type picture";
                    return View();
                }
            }
            db.Patients.Add(patient);
           if( db.SaveChanges()>0)
            {
                ViewBag.mesaage = "Save success";
                return RedirectToAction("Index");
            }
           else
            {
                ViewBag.mesaage = "Save failed";
            }
            return View();
        }
    }
}