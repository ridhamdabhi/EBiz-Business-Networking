using Ebiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ebiz.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        EbizEntitiesEntities1 model = new EbizEntitiesEntities1();

        public ActionResult Home()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult AddCat()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult OrganizeMeeting()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Grps= model.GrpMsts.ToList();
            return View();
        }

        public JsonResult OrganizeMeet(string Date, string Location, string Desc, string Grps, string Amount)
        {
            try
            {
                MeetingMst obj = new MeetingMst();
                obj.Date = Convert.ToDateTime(Date);
                obj.Location = Location;
                obj.Desc = Desc;
                obj.GrpId = Convert.ToInt32(Grps);
                obj.Amount = Convert.ToInt32(Amount);
                model.MeetingMsts.Add(obj);
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddGrp()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult ViewCat()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var cat = model.BusinessCatMsts.ToList();
            return View(cat);
        }

        public ActionResult ViewComplain()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var comp = model.ComplainMasters.ToList();
            return View(comp);
        }

        public ActionResult SolveComp(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            
                var obj = model.ComplainMasters.Where(q => q.Id == id).FirstOrDefault();
          
                obj.Status = 1;
                model.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();
          
            return RedirectToAction("ViewComplain");
        }


        public ActionResult ViewMeeting()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var meet = model.MeetingMsts.ToList();
            return View(meet);

        }

        public ActionResult EditMeet(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var meet = model.MeetingMsts.Where(q => q.Id == id).FirstOrDefault();
                ViewBag.Grps = model.MeetingMsts.ToList();
                return View(meet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult DeleteMeet(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var meet = model.MeetingMsts.Where(q => q.Id == id).FirstOrDefault();
                if (meet != null)
                {
                    model.MeetingMsts.Remove(meet);
                    model.SaveChanges();
                }
                return RedirectToAction("ViewMeeting");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home");
            }
        }

        public JsonResult EditMeeting(string Date, string Location, string Desc, string Grps, string Amount,int id)
        {
            try
            {
                var obj = model.MeetingMsts.Where(q => q.Id == id).FirstOrDefault();
                obj.Date = Convert.ToDateTime(Date);
                obj.Location = Location;
                obj.Desc = Desc;
                obj.GrpId = Convert.ToInt32(Grps);
                obj.Amount = Convert.ToInt32(Amount);
                model.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteCat(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var cat = model.BusinessCatMsts.Where(q => q.CategoryId == id).FirstOrDefault();
                if (cat != null)
                {
                    model.BusinessCatMsts.Remove(cat);
                    model.SaveChanges();
                }
                return RedirectToAction("ViewCat");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult EdtCat(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var cat = model.BusinessCatMsts.Where(q => q.CategoryId == id).FirstOrDefault();

                return View(cat);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult ChangePass()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult ViewFeed()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var feed = model.FeedbackMasters.ToList();
            ViewBag.Leader = model.UserMasters.Where(q => q.RoleId == 2).ToList();
            ViewBag.Member = model.UserMasters.Where(q => q.RoleId == 3).ToList();
            return View(feed);
        }

        public ActionResult RemoveMember(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var grpmem = model.GrpMemberMsts.Where(q => q.Id == id).FirstOrDefault();
                var grpid = grpmem.GroupId;
                if (grpmem != null)
                {
                    model.GrpMemberMsts.Remove(grpmem);
                    model.SaveChanges();
                }
                return RedirectToAction("ViewGrpMember",new { id= grpid });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home");
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

        public ActionResult Deletegrp(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var grp = model.GrpMsts.Where(q => q.Id == id).FirstOrDefault();
                if (grp != null)
                {
                    model.GrpMsts.Remove(grp);
                    model.SaveChanges();
                }
                return RedirectToAction("ViewGrp");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult EdtGrp(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var grp = model.GrpMsts.Where(q => q.Id == id).FirstOrDefault();

                return View(grp);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult ViewCust()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var cust = model.UserMasters.Where(q => q.RoleId != 1).ToList();
            return View(cust);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }

        //public ActionResult Feedback(string Feedback)
        //{
        //    FeedbackMaster db = new FeedbackMaster();
        //    db.Date = DateTime.Now;
        //    db.CustomerId = Convert.ToInt32(Session["UserID"]);
        //    db.Feedback = Feedback;
        //    model.FeedbackMasters.Add(db);
        //    model.SaveChanges();
        //}

        public JsonResult AddCategory(string Name)
        {
            try
            {
                var addcat = model.BusinessCatMsts.Where(q => q.CategoryName == Name).FirstOrDefault();
                if (addcat != null)
                {
                    return Json("Category Already Exists.", JsonRequestBehavior.AllowGet);
                }

                BusinessCatMst obj = new BusinessCatMst();
                obj.CategoryName = Name;
                model.BusinessCatMsts.Add(obj);
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditCat(string Name, int id)
        {
            try
            {


                var addcat = model.BusinessCatMsts.Where(q => q.CategoryName == Name && q.CategoryId != id).FirstOrDefault();
                if (addcat != null)
                {
                    return Json("Category Name Already Exists.", JsonRequestBehavior.AllowGet);
                }

                var cat = model.BusinessCatMsts.Where(q => q.CategoryId == id).FirstOrDefault();
                cat.CategoryName = Name;
                model.Entry(cat).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditGroup(string Name, int id, string Desc)
        {
            try
            {


                var addgrp = model.GrpMsts.Where(q => q.Name == Name && q.Id != id).FirstOrDefault();
                if (addgrp != null)
                {
                    return Json("Group Name Already Exists.", JsonRequestBehavior.AllowGet);
                }

                var grp = model.GrpMsts.Where(q => q.Id == id).FirstOrDefault();
                grp.Name = Name;
                grp.Desc = Desc;
                model.Entry(grp).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ChangePassword(string cpass, string npass, string cnpass)
        {
            try
            {
                UserMaster obj = new UserMaster();
                var user = Convert.ToInt32(Session["UserID"]);
                obj = model.UserMasters.Where(q => q.UserId == user && q.Password == cpass).FirstOrDefault();

                if (obj != null)
                {

                    obj.Password = npass;
                    model.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Please enter details correctly", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddGroup(string Name, string Desc)
        {
            try
            {
                var addgrp = model.GrpMsts.Where(q => q.Name == Name).FirstOrDefault();
                if (addgrp != null)
                {
                    return Json("Group Already Exists.", JsonRequestBehavior.AllowGet);
                }

                GrpMst obj = new GrpMst();
                obj.Name = Name;
                obj.Desc = Desc;
                obj.Image = null;
                model.GrpMsts.Add(obj);
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ViewGrpMember(int id)
        {
            Session["GrpId"] = id;
            ViewBag.User = model.UserMasters.ToList();
            ViewBag.Cat = model.BusinessCatMsts.ToList();
            ViewBag.GrpMem = model.GrpMemberMsts.Where(q => q.GroupId == id).ToList();
            return View();
        }

        public ActionResult AddGrpMember()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Grps = model.GrpMsts.ToList();
            var usrs = model.GrpMemberMsts.Select(q => q.MemberId).ToList();
            ViewBag.Users = model.UserMasters.Where(q => q.RoleId != 1  && !usrs.Contains(q.UserId)).ToList();
            return View();
        }

        //Only for understanding purpose and nerver display this on UI
        public ActionResult EditGrpMember(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Grps = model.GrpMsts.ToList();
            ViewBag.Users = model.UserMasters.Where(q => q.RoleId != 1).ToList();
            var GrpDetails = model.GrpMemberMsts.Where(q => q.Id == id).FirstOrDefault();
            return View(GrpDetails);
        }

        public JsonResult AddGroupMember(int UserID)
        {
            var GrpId = Convert.ToInt32(Session["GrpId"]);
            var cat = model.GrpMemberMsts.Where(q => q.MemberId == UserID).FirstOrDefault();
            if (cat != null)
            {
                return Json("Error");
            }
            GrpMemberMst obj = new GrpMemberMst();
            obj.MemberId = UserID;
            obj.GroupId = GrpId;
            model.GrpMemberMsts.Add(obj);
            model.SaveChanges();
            return Json("success");

        }

        public ActionResult ViewCandidate(int id)
        {
            Session["VoteId"] = id;
            var userid = model.CandidateMsts.Where(q => q.VoteId == id).Select(q=>q.UserId).ToList();
            var users = model.UserMasters.Where(q => userid.Contains(q.UserId)).ToList();
            return View(users);

        }

        public ActionResult AddCandidate()
        {
            var voteId = Convert.ToInt32(Session["VoteId"]);
            var votes = model.VoteMsts.Where(q=>q.Id == voteId).FirstOrDefault();
            var Candidates = model.CandidateMsts.Where(q => q.VoteId == voteId).Select(q => q.UserId).ToList();
            var grpMembers = model.GrpMemberMsts.Where(q => q.GroupId == votes.GroupId).Select(q=>q.MemberId).ToList();
            var users = model.UserMasters.Where(q => grpMembers.Contains(q.UserId) && !Candidates.Contains(q.UserId)).ToList();
            return View(users);
        }
    }
}