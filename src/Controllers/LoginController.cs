using Clinic_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Clinic_Management_System.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        LoginModel lm = new LoginModel();
        public ActionResult UserLogin()
        {
            return View("UserLogin");
        }
        public ActionResult StaffLogin(FormCollection fc, string action)
        {         
            DataTable dt = lm.Get();
            //DataTable dt1 = lm.GetAppt();
            string uname = fc["txt_uname"];
            string password = fc["txt_pwd"];
            if (action == "Submit")
            {
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    if (uname == dt.Rows[i]["UserName"].ToString() && password == dt.Rows[i]["StaffPassword"].ToString())
                    {
                        return RedirectToAction("Home");
                    }

                    else
                    {
                        return View("UserLogin");
                    }
                }
            }
            return View("UserLogin");
        }
          
            public ActionResult Home()
             {
            DataTable dt = lm.GetAppt();
            return View("Home",dt);
            }
        public ActionResult AddDoctor()
        {
            DataTable dt = lm.GetDoc();
            return View("AddDoctor",dt);
        }
        public ActionResult AddDocDetails(FormCollection fc, string action)
        {
           // DataTable dt = lm.GetDoc();
           // string docname = fc["txt_fname"];
          //  string spec = fc["dept"];
            if (action == "Submit")
            {
               // for (int i = 0; i <= dt.Rows.Count; i++)
               // {
                  //  if (docname != dt.Rows[i]["FirstName"].ToString() && spec !=dt.Rows[i]["Specialization"].ToString())
                   // {
                        string fname = fc["txt_fname"];
                        string lname = fc["txt_lname"];
                        string sex = fc["Choice"];
                        string spl = fc["dept"];
                        string vhrs = fc["txt_Vhrs"];
                        int ins = lm.InsertDocDetails(fname, lname, sex, spl, vhrs);
                        return RedirectToAction("AddDoctor");
                  //  }
                   // else
                   // {
                        return View("AddDoctor");
                   // }
               // }
            }  
                return View("AddDoctor"); 
        }
        public ActionResult AddPatient()
        {
            DataTable dt = lm.GetPat();
            return View("AddPatient",dt);
        }
        public ActionResult AddPatDetails(FormCollection fc, string action)
        {
           // DataTable dt = lm.GetPat();
            if (action == "Submit")
            {
                string fname = fc["txt_fname"];
                string lname = fc["txt_lname"];
                string sex = fc["Choice"];
                string age = fc["txt_age"];
                string dob = fc["txt_dob"];
                int ins = lm.InsertPatDetails(fname, lname, sex, age, Convert.ToDateTime(dob));
                return RedirectToAction("AddPatient");
            }
            else
            {
                return RedirectToAction("AddPatient");
            }
        }
        public ActionResult Appointment()
        {
            DataTable dt = lm.GetAppt();
            return View("Appointment", dt);
        }
        public ActionResult Schedule(LoginModel mod)
        {
            mod.DocList = mod.GetDocList();
            var selectedItem = mod.DocList.Find(p => p.Value == mod.FirstName);
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
                ViewBag.Message = "DoctorName: " + selectedItem.Text;
            }
            return View(mod);
        }
        public ActionResult ScheduleAppointment(FormCollection fc, string action)
        {
            //if (dept == "Select")
            //{
            //    string dep = fc["dept"];
            //    for (int i = 0; i <= dt.Rows.Count; i++)
            //    {
            //        if (dep == dt.Rows[i]["Specialization"].ToString())
            //        {
            //            fc["txtdname"] = dt.Rows[i]["FirstName"].ToString();
            //        }
            //    }
            //}
            DataTable dt = lm.GetAppt();
            if (action == "Submit")
            {               
                string pid = fc["Txt_Pid"];
                string spl = fc["dept"];
                string dname = fc["FirstName"];
                string vdate = fc["txt_Vdate"];
                string atime = fc["txt_AppTime"];
                int ins = lm.ScheduleApp(Convert.ToInt32(pid), spl, dname, vdate, atime);
                //TempData["Message"] = "Appointment for" + fc["Txt_Pid"] + " is scheduled with " + fc["txt_Dname"] + " successfully";

                // return RedirectToAction("Schedule");
                return RedirectToAction("Appointment");
         

            }
            else
            {
                return View("Appointment");
            }
        }
        public ActionResult Cancel(int aid)
        {
            DataTable dt = lm.CancelById(aid);
            return View("Cancel", dt);
        }
        public ActionResult CancelApp(FormCollection fc, string action)
        {
            if (action == "Cancel Appointment")
            {
                int aid = Convert.ToInt32(fc["txt_Aid"]);
                int res = lm.CancelAppt(aid);
                return RedirectToAction("Home");
            }
            return RedirectToAction("Home");
        }
        public ActionResult Logout()
        {
            return View("UserLogin");
        }
    }
}