using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjC349WebMVC.Models;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Web.Helpers;
using prjC349WebMVC.Library.WebCrawler;
using prjC349WebMVC.Library;
using System.Security.Policy;
using static System.Net.WebRequestMethods;
using System.Web.Services.Description;

namespace prjC349WebMVC.Controllers
{
    public class ToolBoxController : Controller
    {
        c349dbEntities_AdvanceOY15_ClearMixArea db = new c349dbEntities_AdvanceOY15_ClearMixArea();
        // GET: ToolBox
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Receieve()
        {
            return View();
        }
        public ActionResult MailAndPhonebook()
        {
            return View();
        }
        public ActionResult Attendance()
        {
            return View();

        }

        public ActionResult LiveUpdateOY07_APIView()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LiveUpdateOY07_API(string userId, string userPassword, string warehouse)
        {
            EIP eip = tryLoginEIP(userId, userPassword);
            StockingLogic stockingLogic = new StockingLogic(eip, warehouse);

            //var result2 = from m in result1List
            //         where m.ship_num_or_customer != ""
            //             select m;           

            List<LiveUpdateOY07> dataForView = stockingLogic.LiveOY07_ToDoList();
            //return View(MakingCargoPlanList);

            return Json(dataForView);
        }

        public ActionResult AdvanceOY15(string warehouse)
        {
            if (Session["isLogin"] == null) return View();
            User tmpUser = new User(Session["userId"].ToString(), Session["userPassword"].ToString(), Session["isLogin"].ToString());
            if (tmpUser.isLogin == false) return View();
            if (warehouse != null) Session["warehouse"] = warehouse;
            EIP eip = tryLoginEIP(tmpUser.userId, tmpUser.userPassword);
            StockingLogic stockingLogic = new StockingLogic(eip, Session["warehouse"].ToString());
            List<AdvanceOY15> dataForView = stockingLogic.AdvanceOY15_ToDoList();
            return View(dataForView);
        }

        public ActionResult AdvanceOY15ClearMixAreaAdmin(string src_warehouse,string dst_warehouse,string dst_section)
        {

            if (Session["src_warehouse"] != null) { src_warehouse = Session["src_warehouse"].ToString(); }
            else if (src_warehouse == null) { src_warehouse = "86"; }
            if (Session["dst_warehouse"] != null) { dst_warehouse = Session["dst_warehouse"].ToString(); }
            else if (dst_warehouse == null) { dst_warehouse = "82"; }
            if (Session["dst_section"] != null) { dst_section = Session["dst_section"].ToString(); }
            else if (dst_section == null) { dst_section = "2"; }

            var todos = db.advanceoy15_clearmixarea.Where(m => m.src_warehouse == src_warehouse && m.dst_warehouse == dst_warehouse && m.dst_section == dst_section).ToList();

            //沒有登入Session就直接回傳資料庫舊資料
            if (Session["isLogin"] == null) return View(todos);

            User tmpUser = new User(Session["userId"].ToString(), Session["userPassword"].ToString(), Session["isLogin"].ToString());
            //帳號密碼錯誤就直接回傳資料庫舊資料，其中會在Session中設定警示訊息
            if (tmpUser.isLogin == false) return View(todos);
            EIP eip = tryLoginEIP(tmpUser.userId, tmpUser.userPassword);
            AdvanceOY15_ClearMixArea_Logic ACL = new AdvanceOY15_ClearMixArea_Logic(eip);
            ACL.UpdateToDoList();
            todos = db.advanceoy15_clearmixarea.Where(m => m.src_warehouse == src_warehouse && m.dst_warehouse == dst_warehouse && m.dst_section == dst_section).ToList();
            return View(todos);
        }
        public ActionResult AdvanceOY15ClearMixAreaCraneOperator(string src_warehouse, string dst_warehouse, string dst_section)
        {
            if (Session["src_warehouse"] != null) { src_warehouse = Session["src_warehouse"].ToString(); }
            else if (src_warehouse == null) { src_warehouse = "86"; }
            if (Session["dst_warehouse"] != null) { dst_warehouse = Session["dst_warehouse"].ToString(); }
            else if (dst_warehouse == null) { dst_warehouse = "82"; }
            if (Session["dst_section"] != null) { dst_section = Session["dst_section"].ToString(); }
            else if (dst_section == null) { dst_section = "2"; }

            var todos = db.advanceoy15_clearmixarea.Where(m => m.src_warehouse == src_warehouse && m.dst_warehouse == dst_warehouse && m.dst_section == dst_section).ToList();
            return View(todos);
        }
        [HttpPost]
        public JsonResult AdvanceOY15ClearMixAreaAPI(string src_warehouse, string dst_warehouse, string dst_section)
        {
            if (Session["src_warehouse"] != null) { src_warehouse = Session["src_warehouse"].ToString(); }
            else if (src_warehouse == null) { src_warehouse = "86"; }
            if (Session["dst_warehouse"] != null) { dst_warehouse = Session["dst_warehouse"].ToString(); }
            else if (dst_warehouse == null) { dst_warehouse = "82"; }
            if (Session["dst_section"] != null) { dst_section = Session["dst_section"].ToString(); }
            else if (dst_section == null) { dst_section = "2"; }
            var todos = db.advanceoy15_clearmixarea.Where(m => m.src_warehouse == src_warehouse && m.dst_warehouse == dst_warehouse && m.dst_section == dst_section).ToList();
            return Json(todos);
        }
        public ActionResult LiveUpdateOY07(string warehouse)
        {
            //var MakingCargoPlanList = db.tMakingCargoPlan.OrderByDescending(m => m.id).ToList();
            //if (Session["isLogin"] == null) return View(MakingCargoPlanList);
            if (Session["isLogin"] == null) return View();
            User tmpUser = new User(Session["userId"].ToString(), Session["userPassword"].ToString(), Session["isLogin"].ToString());
            //if (tmpUser.isLogin == false) return View(MakingCargoPlanList);
            if (tmpUser.isLogin == false) return View();

            if (warehouse != null) Session["warehouse"] = warehouse;
            EIP eip = tryLoginEIP(tmpUser.userId, tmpUser.userPassword);
            StockingLogic stockingLogic = new StockingLogic(eip, Session["warehouse"].ToString());

            //var result2 = from m in result1List
            //         where m.ship_num_or_customer != ""
            //             select m;           

            List<LiveUpdateOY07> dataForView = stockingLogic.LiveOY07_ToDoList();
            //return View(MakingCargoPlanList);
            return View(dataForView);
        }


        [HttpPost]
        public ActionResult LogIn(string userId, string userPassword, string warehouse, string operation)
        {


            Session["warehouse"] = warehouse;

            if (operation == "LiveUpdateOY07")
            {
                if (CheckLoginInfo(userId, userPassword) == false) return RedirectToAction("LiveUpdateOY07");
                tryLoginEIP(userId, userPassword);
                return RedirectToAction("LiveUpdateOY07");
            }
            else if (operation == "AdvanceOY15") 
            {
                if (CheckLoginInfo(userId, userPassword) == false) return RedirectToAction("AdvanceOY15");
                tryLoginEIP(userId, userPassword);
                return RedirectToAction("AdvanceOY15");
            }
            else if (operation == "AdvanceOY15ClearMixAreaAdmin")
            {
                if (string.IsNullOrEmpty(userId)&& string.IsNullOrEmpty(userPassword)) return RedirectToAction("AdvanceOY15ClearMixAreaAdmin");
                if (CheckLoginInfo(userId, userPassword) == false) return RedirectToAction("AdvanceOY15ClearMixAreaAdmin");
                tryLoginEIP(userId, userPassword);
                return RedirectToAction("AdvanceOY15ClearMixAreaAdmin");
            }
            else
            {
                return RedirectToAction("LiveUpdateOY07");
            }
        }
        [HttpPost]
        public ActionResult LogOut(string operation)
        {
            Session["userId"] = null;
            Session["userPassword"] = null;
            Session["isLogin"] = null;
            if (operation == "LiveUpdateOY07")
            {
                return RedirectToAction("LiveUpdateOY07");
            }
            else if (operation == "AdvanceOY15")
            {
                return RedirectToAction("AdvanceOY15");
            }
            else if (operation == "AdvanceOY15ClearMixAreaAdmin")
            {
                return RedirectToAction("AdvanceOY15ClearMixAreaAdmin");
            }
            else
            {
                return RedirectToAction("LiveUpdateOY07");
            }
        }


        private class User
        {
            public string userId { get; set; }
            public string userPassword { get; set; }
            public bool isLogin { get; set; }
            public User(string userId, string userPassword, string isLogin)
            {
                this.userId = userId;
                this.userPassword = userPassword;
                this.isLogin = isLogin == null ? false : Convert.ToBoolean(isLogin);
            }
        }
        private bool CheckSessionExist()
        {
            System.Diagnostics.Debug.WriteLine($"Session[\"userId\"] = {Session["userId"]}");
            System.Diagnostics.Debug.WriteLine($"Session[\"userPassword\"] = {Session["userPassword"]}");
            var yy = Session["userId"] == null || Session["userPassword"] == null ? false : true;
            return (Session["userId"] == null || Session["userPassword"] == null) ? false : true;
        }
        public bool CheckLoginInfo(string userId, string userPassword)
        {
            bool isOldUser = CheckSessionExist();
            if (isOldUser) return true;

            bool isNewUser = (userId == "" || userPassword == "") ? false : true;
            if (isNewUser == false) return false;

            #region If you reach here that means you are NewUser
            if (isNewUser == true)
            {
                Session["userId"] = userId;
                Session["userPassword"] = userPassword;
                return true;
            }
            else
            {
                return false;
            }
            #endregion
        }
        private EIP tryLoginEIP(string userId, string userPassword)
        {
            EIP tmpEIPinstance = new EIP(userId, userPassword);

            if (tmpEIPinstance.Login() == false)
            {
                Session["isLogin"] = null;
                Session["userId"] = null;
                Session["userPassword"] = null;
                Session["LoginFail"] = true;
            }
            else
            {
                Session["LoginFail"] = null;
                Session["isLogin"] = true;
            }
            return tmpEIPinstance;
        }
    }
}