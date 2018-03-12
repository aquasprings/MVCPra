using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    public class praveenController : Controller
    {
        DB01TEST01Entities db = new DB01TEST01Entities();
        // GET: praveen
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(loginpraveen p)
        {
            ObjectParameter op = new ObjectParameter("Loginid", 0);
            db.sp_loginpraveen(op, p.username, p.password);
            db.SaveChanges();
            int r = Convert.ToInt32(op.Value);
            if(r==1)
            {
                return RedirectToAction("Register");
            }
            else
            {
                Response.Write("<script>alert('Login failed')</script>");
            }
            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(registerpraveen r)
        {
            ObjectParameter op = new ObjectParameter("regId", 0);
            db.sp_registerpraveen(op, r.Name, r.sex, r.Age, r.city);
            db.SaveChanges();
            if(Convert.ToInt32(op.Value)>0)
            {
                Response.Write("<script>alert('  successfully registered with Registered : " + op.Value +"')</script>");
            }
            ModelState.Clear();
          
            return View();
        }
        public ActionResult view(registerpraveen a)
        {
            List<registerpraveen> lst = new List<registerpraveen>();
            lst = db.registerpraveens.ToList();
            return View(lst);
        }
        public ActionResult deleterow(int id)
        {
            db.sp_praveendelete(id);
                  Response.Write("deleted successfully");
            return RedirectToAction("view");
        }
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(int id,string city)
        {
            db.sp_praveenupdate(id,city);
           
            Response.Write("<script>alert('  successfully updated')</script>");
            return RedirectToAction("view");
        }

    }


}
