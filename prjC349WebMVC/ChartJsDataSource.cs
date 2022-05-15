using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjC349WebMVC
{
    public class ChartJsDataSource :IComparable<ChartJsDataSource>
    {
        public DateTime mDate;
        public string ChartJsDate;
        public List<string> mList = new List<string>();
        public ChartJsDataSource(List<string> list)
        {
            DateTime dt = DateTime.Parse(list.ElementAt(0).ToString());
            int myYear = dt.Year;
            int myMonth = dt.Month;
            int myDay = dt.Day;
            string ChartJSDate = $"{myYear}/ {myMonth}/ {myDay}";//ChartJs用的日期格式

            this.mDate = dt;
            this.ChartJsDate = ChartJSDate;

            mList.Add(this.ChartJsDate);
            for (int i = 1; i < list.Count; i++)
            {
                mList.Add(list.ElementAt(i).ToString());
            }
        }

        public int CompareTo(ChartJsDataSource other)
        {
            
            if (this.mDate > other.mDate)
                return 1;
            else if (this.mDate == other.mDate)
                return 0;
            else return -1;
        }

        //public int CompareTo(object obj)
        //{
        //    ChartDataSource tobeCompared = (ChartDataSource)obj;
        //    if (this.mDate > tobeCompared.mDate)
        //        return 1;
        //    else if (this.mDate == tobeCompared.mDate)
        //        return 0;
        //    else return -1;
        //}

        //public bool Equals(ChartArray other)
        //{
        //    if (other == null) return false;
        //    ChartArray objAsPart = other as ChartArray;
        //    if (objAsPart == null) return false;
        //    else return Equals(objAsPart);
        //}
    }
}