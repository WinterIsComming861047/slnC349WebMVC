﻿using NPOI.SS.Formula.Functions;
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

namespace prjC349WebMVC.Controllers
{
    public class ToolBoxController : Controller
    {
        c349dbEntities db = new c349dbEntities();
        // GET: ToolBox
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LiveUpdateOY07()
        {
            //var MakingCargoPlanList = db.tMakingCargoPlan.OrderByDescending(m => m.id).ToList();
            //if (Session["isLogin"] == null) return View(MakingCargoPlanList);
            if (Session["isLogin"] == null) return View();
            User tmpUser = new User(Session["userId"].ToString(), Session["userPassword"].ToString(), Session["isLogin"].ToString());
            //if (tmpUser.isLogin == false) return View(MakingCargoPlanList);
            if (tmpUser.isLogin == false) return View();


            EIP eip = tryLoginEIP(tmpUser.userId, tmpUser.userPassword);
            StockingLogic stockingLogic = new StockingLogic(eip);

            //var result2 = from m in result1List
            //         where m.ship_num_or_customer != ""
            //             select m;           

            List<LiveUpdateOY07> dataForView = stockingLogic.ToDoList();
            //return View(MakingCargoPlanList);
            return View(dataForView);
        }



        [HttpPost]
        public ActionResult LogIn(string userId, string userPassword)
        {
            if (CheckLoginInfo(userId, userPassword) == false) return RedirectToAction("LiveUpdateOY07");
            tryLoginEIP(userId, userPassword);
            return RedirectToAction("LiveUpdateOY07");
        }
        [HttpPost]
        public ActionResult LogOut()
        {
            Session["userId"] = null;
            Session["userPassword"] = null;
            Session["isLogin"] = null;
            return RedirectToAction("LiveUpdateOY07");
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