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
            IGS4 igs4 = new IGS4(eip);
            var result1 = from m in igs4.List
                          where m.ship_num_or_customer != ""
                          select m;

            List<IGS4.Model> has_ship_num_or_customer_List = result1.ToList();

            //var result2 = from m in result1List
            //         where m.ship_num_or_customer != ""
            //             select m;
            OYR1D1Report oyr1_d1report = new OYR1D1Report(eip);

            var combinedData = from i in has_ship_num_or_customer_List
                               join o in oyr1_d1report.List on i.label equals o.label into igs4oyr1_d1report
                               from io in igs4oyr1_d1report
                               select new LiveUpdateOY07
                               {
                                   bill_of_ladding = i.bill_of_ladding,
                                   order_num = i.order_num,
                                   state = i.state,
                                   ship_num_or_customer = i.ship_num_or_customer,
                                   warehouse = io.warehouse,
                                   area = io.area,
                                   coil_number = io.coil_number,
                                   label = io.label,
                                   coil_code = io.coil_code,
                                   area_code = io.area_code,
                                   position = io.position,
                                   length = io.length,
                                   width = io.width,
                                   thick = io.thick,
                                   weight = io.weight,
                                   inner_diameter = io.inner_diameter,
                                   outer_diameter = io.outer_diameter,
                                   red_tag_coil_layer = io.red_tag_coil_layer,
                                   area_available_layer = io.area_available_layer,
                                   move_count_oymv = io.move_count_oymv
                               };
            List<LiveUpdateOY07> has_ship_num_or_customer_with_position_List = combinedData.ToList();
            List<LiveUpdateOY07> has_ship_num_supressed_List = new List<LiveUpdateOY07>();
            foreach (LiveUpdateOY07 shipitem in has_ship_num_or_customer_with_position_List)
            {
                var checkUperCoil = from c in oyr1_d1report.List
                                    where c.area == shipitem.area && isSupressed(c.position, shipitem.position) && !(isShipItem(c.label, has_ship_num_or_customer_List))
                                    select c;
                if (checkUperCoil.Count() != 0)
                {
                    has_ship_num_supressed_List.Add(shipitem);
                    System.Diagnostics.Debug.WriteLine($"{shipitem.label} is Supressed by {checkUperCoil.ElementAt(0).label}");
                    System.Diagnostics.Debug.WriteLine($"{shipitem.label} at {shipitem.area} {shipitem.position}, " +
                        $"{checkUperCoil.ElementAt(0).label} at {checkUperCoil.ElementAt(0).area} {checkUperCoil.ElementAt(0).position}");
                }
            }


            List<string> bill_of_ladding_List = igs4.List
                .Select(x => x.bill_of_ladding)
                .Distinct()
                .ToList();
            SPB1 spb1 = new SPB1(eip, bill_of_ladding_List);

            var finalData = from h in has_ship_num_supressed_List
                            join s in spb1.List on h.bill_of_ladding equals s.bill_of_ladding into igs4oyr1_d1report
                               from hs in igs4oyr1_d1report
                               select new LiveUpdateOY07
                               {
                                   bill_of_ladding = h.bill_of_ladding,
                                   order_num = h.order_num,
                                   state = h.state,
                                   ship_num_or_customer = h.ship_num_or_customer,
                                   warehouse = h.warehouse,
                                   area = h.area,
                                   coil_number = h.coil_number,
                                   label = h.label,
                                   coil_code = h.coil_code,
                                   area_code = h.area_code,
                                   position = h.position,
                                   length = h.length,
                                   width = h.width,
                                   thick = h.thick,
                                   weight = h.weight,
                                   inner_diameter = h.inner_diameter,
                                   outer_diameter = h.outer_diameter,
                                   red_tag_coil_layer = h.red_tag_coil_layer,
                                   area_available_layer = h.area_available_layer,
                                   move_count_oymv = h.move_count_oymv,
                                   making_cargo_plan = hs.making_cargo_plan
                               };
            List<LiveUpdateOY07> dataForView = finalData.ToList();
            //return View(MakingCargoPlanList);
            return View(dataForView);
        }

        private bool isSupressed(string uper_coil_pos, string chk_coil_pos)
        {
            if (Int16.Parse(uper_coil_pos.Substring(0, 1)) - Int16.Parse(chk_coil_pos.Substring(0, 1)) != 1) return false;
            if (Math.Abs(Int16.Parse(uper_coil_pos.Substring(1, 2)) - Int16.Parse(chk_coil_pos.Substring(1, 2))) != 1) return false;
            return true;
        }
        private bool isShipItem(string label, List<IGS4.Model> has_ship_num_or_customer_List)
        {
            var result1 = from h in has_ship_num_or_customer_List
                          where h.label == label
                          select h;
            var yy = result1.Count();
            if (yy == 0)
                return false;
            return true;
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