using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BugTracker.Models;

namespace Asp.NETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Ticket/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (Entities db = new Entities())
            {
                List<Ticket> empList = db.Tickets.ToList<Ticket>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Ticket());
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.Tickets.Where(x => x.TicketID==id).FirstOrDefault<Ticket>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Ticket emp)
        {
            using (Entities db = new Entities())
            {
                if (emp.TicketID == 0)
                {
                    db.Tickets.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Entities db = new Entities())
            {
                Ticket emp = db.Tickets.Where(x => x.TicketID == id).FirstOrDefault<Ticket>();
                db.Tickets.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}