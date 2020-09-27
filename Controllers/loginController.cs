using Ebiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ebiz.Controllers
{
    public class loginController : Controller
    {
        EbizEntitiesEntities1 model = new EbizEntitiesEntities1();
        // GET: login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Reg()
        {
            return View();
        }
        public JsonResult ChkLogin(string EmailID, string Password)
        {
            try
            {
                var chkdata = model.UserMasters.Where(q => q.EmailId == EmailID && q.Password == Password).FirstOrDefault();
                if (chkdata == null)
                {
                    return Json("Incorrect Email/Password.", JsonRequestBehavior.AllowGet);
                }
                Session["UserID"] = chkdata.UserId;
                Session["RoleID"] = chkdata.RoleId;
                Session["Name"] = chkdata.Name;
                return Json(chkdata.RoleId.ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Register(string Name, string Address, string Mobile, string EmailID, string Password, string Pincode)
        {
            try
            {
                var chkdata = model.UserMasters.Where(q => q.EmailId == EmailID).FirstOrDefault();
                if (chkdata != null)
                {
                    return Json("EmailID Already Exist", JsonRequestBehavior.AllowGet);
                }
                chkdata = model.UserMasters.Where(q => q.PhoneNo == Mobile).FirstOrDefault();
                if (chkdata != null)
                {
                    return Json("Mobile Number Already Exist", JsonRequestBehavior.AllowGet);
                }

                UserMaster obj = new UserMaster();
                obj.Name = Name;
                obj.Address = Address;
                obj.PinCode = Pincode;
                obj.PhoneNo = Mobile;
                obj.EmailId = EmailID;
                obj.Password = Password;
                obj.RoleId = 3;
                model.UserMasters.Add(obj);
                model.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error3", JsonRequestBehavior.AllowGet);
            }
        }

        

    }
}