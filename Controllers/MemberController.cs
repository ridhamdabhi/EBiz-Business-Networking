using Ebiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ebiz.Controllers
{

    public class MemberController : Controller
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

        public ActionResult BringGuest(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Cat = model.BusinessCatMsts.ToList();
            Session["MeetId"] = id;
            return View();
        }

        public JsonResult AddGuest(string Name, string PhoneNo, string Email, string CatId)
        {
            try
            {
                GuestMaster db = new GuestMaster();
                db.Name = Name;
                db.PhoneNo = PhoneNo;
                db.MemberId = Convert.ToInt32(Session["UserID"]);
                db.EmailId = Email;
                db.CatId = Convert.ToInt32(CatId);
                db.MeetingId = Convert.ToInt32(Session["MeetId"]);
                model.GuestMasters.Add(db);
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
        }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }


        public ActionResult ProvideFeed()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult AddComplaint()
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
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }

        public ActionResult AddReferral(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Session["ReferToId"] = id;
            return View();
        }

        public JsonResult AddRefer(string Name, string PhoneNo, string Email)
        {
            try
            {
                ReferralMst db = new ReferralMst();
                db.Name = Name;
                db.PhoneNo = PhoneNo;
                var MemId = Convert.ToInt32(Session["UserID"]);
                db.MemberId = MemId;
                db.Email = Email;
                db.ReferToId = Convert.ToInt32(Session["ReferToId"]);
                var GrpId = model.GrpMemberMsts.Where(q => q.MemberId == MemId).Select(q=>q.GroupId).FirstOrDefault();
                db.GrpId = GrpId;
                model.ReferralMsts.Add(db);
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }

        public ActionResult ViewGrpMember()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var UserId = Convert.ToInt32(Session["UserID"]);
            var id = model.GrpMemberMsts.Where(q=>q.MemberId == UserId).Select(q=>q.GroupId).FirstOrDefault();
            Session["GrpId"] = id;
            var GrpId = Convert.ToInt32(id);
            ViewBag.User = model.UserMasters.ToList();
            ViewBag.Cat = model.BusinessCatMsts.ToList();
            ViewBag.GrpMem = model.GrpMemberMsts.Where(q => q.GroupId == GrpId && q.MemberId!=UserId).ToList();
            return View();
        }


        public ActionResult ViewReferrals()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var UserId = Convert.ToInt32(Session["UserID"]);
            ViewBag.Referrals = model.ReferralMsts.Where(q => q.ReferToId == UserId).ToList();
            ViewBag.User = model.UserMasters.ToList();
            ViewBag.Cat = model.BusinessCatMsts.ToList();
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

        public ActionResult ChangePass()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
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

        public ActionResult ViewMeeting()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var CustomerId = Convert.ToInt32(Session["UserID"]);
            var Grps = model.GrpMemberMsts.Where(q => q.MemberId == CustomerId).ToList();
            var GrpIds = Grps.Select(q => q.GroupId).ToList();
            var meetings = model.MeetingMsts.Where(q => GrpIds.Contains(q.GrpId)).ToList();
            ViewBag.Grps = Grps;
            ViewBag.Guest = model.GuestMasters.Where(q => q.MemberId == CustomerId).ToList();
            var MeetMem = model.MeetingMemberMsts.ToList();
            ViewBag.MeetMem = MeetMem;


            return View(meetings);
        }

        public ActionResult JoinMeet(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var CustomerId = Convert.ToInt32(Session["UserID"]);
            MeetingMemberMst obj = new MeetingMemberMst();
            obj.MemberId = CustomerId;
            obj.MeetingId = id;
            return View();
        }

        public ActionResult JoinMeeting(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var CustomerId = Convert.ToInt32(Session["UserID"]);
            var chkMeeting = model.MeetingMemberMsts.Where(q => q.MemberId == CustomerId && q.MeetingId == id).FirstOrDefault();
            if(chkMeeting == null)
            {
                MeetingMemberMst obj = new MeetingMemberMst();
                obj.MemberId = CustomerId;
                obj.MeetingId = id;
                model.MeetingMemberMsts.Add(obj);
                model.SaveChanges();
            }
            return RedirectToAction("ViewMyMeetings");
        }

        public ActionResult ViewMyMeetings()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var CustomerId = Convert.ToInt32(Session["UserID"]);
            var Grps = model.GrpMemberMsts.Where(q => q.MemberId == CustomerId).ToList();
            ViewBag.Grps = Grps;
            var Meetings = model.MeetingMemberMsts.Where(q => q.MemberId == CustomerId).ToList();
            var MeetingIds = Meetings.Select(q => q.MeetingId).ToList();
            var meetings = model.MeetingMsts.Where(q => MeetingIds.Contains(q.GrpId)).ToList();
            return View();
        }

        //public ActionResult AddGuest(int id)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }
        //    var grpid = model.MeetingMsts.Where(q => q.Id == id).FirstOrDefault();
        //    var Grps = model.GrpMemberMsts.Where(q => q.GroupId == grpid.GrpId).Select(q=>q.MemberId).ToList();
        //    var usermst = model.UserMasters.Where(q => !Grps.Contains(q.UserId)).ToList();
        //    ViewBag.MeetingId = id;
        //    return View();
        //}

        //public ActionResult RemoveGuest(int id)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }
        //    var refers = model.GuestMasters.Where(q => q.MeetingId == id).Select(q => q.ReferId).ToList();
        //    var usermst = model.UserMasters.Where(q=> refers.Contains(q.UserId)).ToList();
        //    return View(usermst);
        //}

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult ViewMsg()
        {
            var CustomerId = Convert.ToInt32(Session["UserID"]);
            var grps = model.GrpMemberMsts.Where(q=>q.MemberId == CustomerId).FirstOrDefault();
            var grpComm = model.CommunicationMsts.Where(q => q.GrpId == grps.GroupId).OrderBy(q=>q.Date).ToList();
            ViewBag.users = model.UserMasters.ToList();
            return View();
        }

        public ActionResult ViewVotingDetails()
        {
            var votes = model.VoteMsts.Where(q => q.StartDate <= DateTime.Now && q.EndDate >= DateTime.Now.AddDays(-1)).ToList();
            return View(votes);
        }

        public ActionResult ViewCandidate(int id)
        {
            Session["VoteId"] = id;
            var userid = model.CandidateMsts.Where(q => q.VoteId == id).Select(q => q.UserId).ToList();
            var users = model.UserMasters.Where(q => userid.Contains(q.UserId)).ToList();
            return View(users);

        }

        public ActionResult VoteNow(int CandidateId,int VoteId)
        {
            var CustomerId = Convert.ToInt32(Session["UserID"]);
            var chkVote = model.VotingMsts.Where(q => q.VoteId == VoteId && q.MemberId == CustomerId).FirstOrDefault();
            if(chkVote != null)
            {

            }
            else
            {
                VotingMst obj = new VotingMst();
                obj.Date = DateTime.Now;
                obj.GrpMemberId = CandidateId;
                obj.MemberId = CustomerId;

                model.VotingMsts.Add(obj);
                model.SaveChanges();

                var candidate = model.CandidateMsts.Where(q => q.UserId == CandidateId).FirstOrDefault();
                candidate.TotalVotes = candidate.TotalVotes + 1;
                model.Entry(candidate).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();

            }
            return View();
        }



    }
}