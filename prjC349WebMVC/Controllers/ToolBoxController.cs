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

            IGS4(tmpEIPinstance.cookieContainer);

            return View(MakingCargoPlanList);
        }
        public void IGS4(CookieContainer cookieContainer)
        {
            string url = "http://eas.csc.com.tw/ig/stock/igs4?_action=exportExcel&_format=json";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Method = "POST";
            request.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var jsonObj = new
            {
                stock = "89"
            };
            string PostData = JsonConvert.SerializeObject(jsonObj);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(PostData);
            request.ContentLength = bytes.Length;

            //意義不明
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(bytes, 0, bytes.Length);
                dataStream.Close();
            }

            //看到.GetResponse()才代表真正把 request 送到 伺服器
            using (WebResponse response = request.GetResponse())
            {
                using (Stream sr =response.GetResponseStream())
                {
                    // sr 就是伺服器回覆的資料
                    FileStream fileStream = new FileStream($"{DateTime.Now}_IGS4.xlsx", FileMode.Create);
                    sr.CopyTo(fileStream);
                    ExcelManager.readExcel(fileStream);
                }
            }
        }
    }
}