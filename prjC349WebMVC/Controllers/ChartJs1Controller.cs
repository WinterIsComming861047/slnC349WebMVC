using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjC349WebMVC.Models;
namespace prjC349WebMVC.Controllers
{
    public class ChartJs1Controller : Controller
    {
        // GET: ChartJs1
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChartJs1(string queryMonth)
        {

            if (queryMonth == null)
            {
                queryMonth = DateTime.Now.ToShortDateString();
            }

            getChartJsData getCJData = new getChartJsData(queryMonth);

            List<ChartJsDataSource> finalList = getCJData.ByCategory();

            string[] Labels = { "'鋼板'", "'線材'", "'熱軋'", "'冷軋'" };
            string[] LineColors = { "rgb(255,235, 205)", "rgb(118, 238, 198)", "rgb(255, 106, 106)", "rgb(99, 184, 255)" };

            //ChartJS_Common_ViewControl(finalList, "ByCategory", Labels, LineColors);

            //string[] Days = { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" };
            //        string[] Days = {
            //        "2022/ 5/ 1","2022/ 5/ 2","2022/ 5/ 3","2022/ 5/ 4","2022/ 5/ 5","2022/ 5/ 6","2022/ 5/ 7","2022/ 5/ 8","2022/ 5/ 9","2022/ 5/ 10",
            //"2022/ 5/ 11","2022/ 5/ 12","2022/ 5/ 13","2022/ 5/ 14","2022/ 5/ 15","2022/ 5/ 16","2022/ 5/ 17","2022/ 5/ 18","2022/ 5/ 19","2022/ 5/ 20",
            //"2022/ 5/ 21", "2022/ 5/ 22", "2022/ 5/ 23", "2022/ 5/ 24", "2022/ 5/ 25", "2022/ 5/ 26", "2022/ 5/ 27", "2022/ 5/ 28", "2022/ 5/ 29", "2022/ 5/ 30", "2022/ 5/ 31"
            //    };

            string[] Days = { };
            for (int i = 0; i < finalList.Count-1; i++)
            {
                Days=Days.Concat(new string[] { finalList[0].ChartJsDate }).ToArray();
            }

            ViewBag.MonthLabel = Days;


            List<ChartJs1> ChJ1 = new List<ChartJs1>();

            for (int i = 0; i < Labels.Length; i++)
            {
                ChartJs1 temp_chartJs = new ChartJs1();
                temp_chartJs.errCount = new string[] { };
                for (int j = 0; j < finalList.Count-1; j++)
                {
                    temp_chartJs.category = Labels[i];
                    temp_chartJs.errCount.Append(finalList[j].mList[i+1]);
                    temp_chartJs.errCount= temp_chartJs.errCount.Concat(new string[] { finalList[j].mList[i + 1] }).ToArray();
                }
                ChJ1.Add(temp_chartJs);
            }


            //List<ChartJs1> ChJ12 = new List<ChartJs1>();
            //{
            //    new ChartJs1
            //    {
            //        category="鋼板",
            //        errCount=new int[]
            //        {
            //            10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 99, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //        }
            //    },
            //    new ChartJs1
            //    {
            //        category="線材",
            //        errCount=new int[]
            //        {
            //            0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 88, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //        }
            //    },
            //    new ChartJs1
            //    {
            //        category="熱軋",
            //        errCount=new int[]
            //        {
            //            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 77, 0, 0, 0, 0,
            //        }
            //    },
            //    new ChartJs1
            //    {
            //        category="冷軋",
            //        errCount=new int[]
            //        {
            //            0, 0, 0, 0, 0, 0, 0, 0, 0, 55, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        }
            //    }
            //};

            //List<ChartJs1> ChJ2 = new List<ChartJs1>
            //{
            //    new ChartJs1
            //    {
            //        category="大奶",
            //        errCount=new int[]
            //        {
            //            100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 66, 0, 0, 0, 0, 0
            //        }
            //    },
            //    new ChartJs1
            //    {
            //        category="中奶",
            //        errCount=new int[]
            //        {
            //            0, 0, 0, 5, 0, 0, 33, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //        }
            //    },
            //    new ChartJs1
            //    {
            //        category="小奶",
            //        errCount=new int[]
            //        {
            //            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 33, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        }
            //    },
            //    new ChartJs1
            //    {
            //        category="微奶",
            //        errCount=new int[]
            //        {
            //            11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        }
            //    }
            //};



            List<List<ChartJs1>> chJJJ = new List<List<ChartJs1>>();
            chJJJ.Add(ChJ1);
            chJJJ.Add(ChJ1);
            return View(chJJJ);
        }
    }
}