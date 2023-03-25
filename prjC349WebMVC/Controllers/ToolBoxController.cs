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

        public ActionResult MakingCargoPlan(string userId, string userPassword)
        {
            var MakingCargoPlanList = db.tMakingCargoPlan.OrderByDescending(m => m.id).ToList();
            if (userId == null || userPassword == null)
            {
                return View(MakingCargoPlanList);
            }

            EIP tmpEIPinstance = new EIP(userId, userPassword);
            if (tmpEIPinstance.Login())
            {
                ViewBag.isLogin = tmpEIPinstance.isLogin;
            };
            ViewBag.userId = userId;
            ViewBag.userPassword = userPassword;

            IGS4 igs4 = new IGS4(tmpEIPinstance.cookieContainer);
            
            var result = from m in igs4.List
                         where m.ship_num_or_customer != ""
                         select m;
            ViewBag.test = result;
            return View(MakingCargoPlanList);
        }
    }
}