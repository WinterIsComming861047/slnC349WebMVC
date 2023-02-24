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
using System.Web.Http.Cors;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

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
        [EnableCors(origins: "*", headers: "*", methods: "*")]
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
                    ip = dt.Rows[0]["ip"].ToString(),
                    location =dt.Rows[0]["location"].ToString()
                };
            }
            catch
            {

            }

            return emp;
        }


        // GET: api/RVI_API
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string Create(string tdate, string carId, string coil1, string coil2, string coil3, string coil4, string coil5, string coil6, string coil7, string coil8, string creator, string location)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO remote_visual_inspection_epslog(tdate,carId,coil1,coil2,coil3,coil4,coil5,coil6,coil7,coil8,creator,updateTime,ip, location)" +
                    "VALUES(@tdate,@carId,@coil1,@coil2,@coil3,@coil4,@coil5,@coil6,@coil7,@coil8,@creator,@updateTime,@ip,@location)";
                //cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = employee.id;
                cmd.Parameters.Add(new MySqlParameter("@tdate", MySqlDbType.DateTime)).Value = tdate;
                cmd.Parameters.Add(new MySqlParameter("@carId", MySqlDbType.VarChar)).Value = carId;
                cmd.Parameters.Add(new MySqlParameter("@coil1", MySqlDbType.VarChar)).Value = coil1;
                cmd.Parameters.Add(new MySqlParameter("@coil2", MySqlDbType.VarChar)).Value = coil2;
                cmd.Parameters.Add(new MySqlParameter("@coil3", MySqlDbType.VarChar)).Value = coil3;
                cmd.Parameters.Add(new MySqlParameter("@coil4", MySqlDbType.VarChar)).Value = coil4;
                cmd.Parameters.Add(new MySqlParameter("@coil5", MySqlDbType.VarChar)).Value = coil5;
                cmd.Parameters.Add(new MySqlParameter("@coil6", MySqlDbType.VarChar)).Value = coil6;
                cmd.Parameters.Add(new MySqlParameter("@coil7", MySqlDbType.VarChar)).Value = coil7;
                cmd.Parameters.Add(new MySqlParameter("@coil8", MySqlDbType.VarChar)).Value = coil8;
                cmd.Parameters.Add(new MySqlParameter("@creator", MySqlDbType.VarChar)).Value = creator;
                cmd.Parameters.Add(new MySqlParameter("@location", MySqlDbType.VarChar)).Value = location;
                cmd.Parameters.Add(new MySqlParameter("@updateTime", MySqlDbType.DateTime)).Value = DateTime.Parse(tdate.ToString())
                    .AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                cmd.Parameters.Add(new MySqlParameter("@ip", MySqlDbType.VarChar)).Value = IPAddress.Get();
                ExecuteCmd(cmd);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.ToString();                
            }

        }

        // POST: api/RVI_API
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string Post(string tdate, string carId, string coil1, string coil2, string coil3, string coil4, string coil5, string coil6, string coil7, string coil8, string location)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO remote_visual_inspection_epslog(tdate,carId,coil1,coil2,coil3,coil4,coil5,coil6,coil7,coil8,creator,updateTime,ip, location)" +
                        "VALUES(@tdate,@carId,@coil1,@coil2,@coil3,@coil4,@coil5,@coil6,@coil7,@coil8,@creator,@updateTime,@ip,@location)";
                    //cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = employee.id;
                    cmd.Parameters.Add(new MySqlParameter("@tdate", MySqlDbType.DateTime)).Value = tdate;
                    cmd.Parameters.Add(new MySqlParameter("@carId", MySqlDbType.VarChar)).Value = carId;
                    cmd.Parameters.Add(new MySqlParameter("@coil1", MySqlDbType.VarChar)).Value = coil1;
                    cmd.Parameters.Add(new MySqlParameter("@coil2", MySqlDbType.VarChar)).Value = coil2;
                    cmd.Parameters.Add(new MySqlParameter("@coil3", MySqlDbType.VarChar)).Value = coil3;
                    cmd.Parameters.Add(new MySqlParameter("@coil4", MySqlDbType.VarChar)).Value = coil4;
                    cmd.Parameters.Add(new MySqlParameter("@coil5", MySqlDbType.VarChar)).Value = coil5;
                    cmd.Parameters.Add(new MySqlParameter("@coil6", MySqlDbType.VarChar)).Value = coil6;
                    cmd.Parameters.Add(new MySqlParameter("@coil7", MySqlDbType.VarChar)).Value = coil7;
                    cmd.Parameters.Add(new MySqlParameter("@coil8", MySqlDbType.VarChar)).Value = coil8;
                    cmd.Parameters.Add(new MySqlParameter("@creator", MySqlDbType.VarChar)).Value = "epslog";
                    cmd.Parameters.Add(new MySqlParameter("@location", MySqlDbType.VarChar)).Value = location;
                    cmd.Parameters.Add(new MySqlParameter("@updateTime", MySqlDbType.DateTime)).Value = DateTime.Parse(tdate.ToString())
                        .AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                    cmd.Parameters.Add(new MySqlParameter("@ip", MySqlDbType.VarChar)).Value = IPAddress.Get();
                    ExecuteCmd(cmd);
                    return "Success";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return "Model State is not valid";
        }

        private void ExecuteCmd(MySqlCommand cmd)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            cmd.Connection = conn;
            var yy = cmd.ExecuteNonQuery();
            conn.Close();
        }
        // PUT: api/RVI_API/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/RVI_API/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Delete(int id)
        {
        }
    }
}
