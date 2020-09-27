using Ebiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ebiz.Controllers
{
    public class LeaderController : Controller
    {
        // GET: Leader
        EbizEntitiesEntities1 model = new EbizEntitiesEntities1();
        public ActionResult Home()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }


        public ActionResult ProvideFeed()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }



        public ActionResult ChangePass()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public JsonResult ProvideFeedback(string feedback)
        {
            try
            {
                FeedbackMaster db = new FeedbackMaster();
                db.Date = DateTime.Now;
                db.CustomerId = Convert.ToInt32(Session["UserID"]);
                db.Feedback = feedback;
                model.FeedbackMasters.Add(db);
                model.SaveChanges();
                return Json("success",JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json("Error");
            }
        }

        public ActionResult AddComplaint()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public JsonResult AddComp(string Desc)
        {
            try
            {
                ComplainMaster db = new ComplainMaster();
                db.Date = DateTime.Now;
                db.CustId = Convert.ToInt32(Session["UserID"]);
                db.Status = 0;
                int gid = Convert.ToInt32(model.GrpMemberMsts.Where(q => q.MemberId == db.CustId).Select(q => q.GroupId).FirstOrDefault());
                db.GrpId = gid;
                db.Description = Desc;
                model.ComplainMasters.Add(db);
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }

        public JsonResult ChangePassword(string cpass, string npass, string cnpass)
        {
            try
            {
                UserMaster obj = new UserMaster();
                var user = Convert.ToInt32(Session["UserID"]);
                obj = model.UserMasters.Where(q => q.UserId == user && q.Password== cpass).FirstOrDefault();

                if (obj !=null)
                {
                    
                    obj.Password = npass;
                    model.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);                             
                }
                else
                { 
                return Json("Please enter password correctly", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult ViewGrp()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var grp = model.GrpMsts.ToList();
            return View(grp);
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }
    }
}