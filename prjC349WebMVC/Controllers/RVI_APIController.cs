using MySql.Data.MySqlClient;
using prjC349WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace prjC349WebMVC.Controllers
{
    public class RVI_APIController : ApiController
    {
        string connStr = WebConfigurationManager.ConnectionStrings["c349ConnectionString"].ConnectionString;

        // GET: api/RVI_API
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RVI_API/5
        public remote_visual_inspection Get(string tdate, string carId)
        {
            return GetRecrod(tdate, carId);
        }

        private remote_visual_inspection GetRecrod(string tdate, string carId)
        {
            remote_visual_inspection emp = new remote_visual_inspection();
            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.ConnectionString = connStr;
                //MySqlCommand cmd = new MySqlCommand("SELECT * FROM remote_visua_inspection_epslog WHERE id=@id", conn);
                MySqlCommand cmd = new MySqlCommand($"select * from `c349`.`remote_visual_inspection_epslog` where `tdate` = @tdate and `carId` = @carId", conn);
                //MySqlCommand cmd = new MySqlCommand($"select * from `c349`.`remote_visual_inspection_epslog` where `tdate` = \"2022-07-08 17:40:15\" and `carId` = \"KLC6767F\" ", conn);

                //cmd.Parameters.Add(new MySqlParameter("@tdate", MySqlDbType.Date)).Value = tdate;
                cmd.Parameters.Add(new MySqlParameter("@tdate", MySqlDbType.VarChar)).Value = tdate;
                cmd.Parameters.Add(new MySqlParameter("@carId", MySqlDbType.VarChar)).Value = carId;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                emp = new remote_visual_inspection
                {
                    id = dt.Rows[0]["id"].ToString(),
                    tdate = DateTime.Parse(dt.Rows[0]["tdate"].ToString()),
                    carId = dt.Rows[0]["carId"].ToString(),
                    comment1 = dt.Rows[0]["comment1"].ToString(),
                    comment2 = dt.Rows[0]["comment2"].ToString(),
                    coil1 = dt.Rows[0]["coil1"].ToString(),
                    coil2 = dt.Rows[0]["coil2"].ToString(),
                    coil3 = dt.Rows[0]["coil3"].ToString(),
                    coil4 = dt.Rows[0]["coil4"].ToString(),
                    coil5 = dt.Rows[0]["coil5"].ToString(),
                    coil6 = dt.Rows[0]["coil6"].ToString(),
                    coil7 = dt.Rows[0]["coil7"].ToString(),
                    coil8 = dt.Rows[0]["coil8"].ToString(),
                    creator = dt.Rows[0]["creator"].ToString(),
                    updateTime = DateTime.Parse(dt.Rows[0]["updateTime"].ToString()),
                    ip = dt.Rows[0]["ip"].ToString()
                };
            }
            catch
            {

            }

            return emp;
        }


        // POST: api/RVI_API
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RVI_API/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RVI_API/5
        public void Delete(int id)
        {
        }
    }
}
