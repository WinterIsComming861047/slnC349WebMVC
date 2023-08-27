using NPOI.POIFS.Crypt.Dsig;
using Org.BouncyCastle.Bcpg.OpenPgp;
using prjC349WebMVC.Library.WebCrawler;
using prjC349WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

namespace prjC349WebMVC.Library
{
    public class AdvanceOY15_ClearMixArea_Logic
    {
        public EIP eip;
        c349dbEntities_AdvanceOY15_ClearMixArea db = new c349dbEntities_AdvanceOY15_ClearMixArea();
        c349dbEntities_Dev db_Dev = new c349dbEntities_Dev();
        public AdvanceOY15_ClearMixArea_Logic(EIP eip)
        {
            this.eip = eip;
        }

        public void UpdateToDoList()
        {
            MSSQL_clear_table();
            List<string> srcWarehouseList = new List<string>() { "86", "87" };
            List<string> dstWarehouseList = new List<string>() { "82", "83", "84", "85", "89", "91" };
            //List<string> dstSegmentList = new List<string>() { "1", "2", "5" };
            Dictionary<string, List<string>> WarehouseSectionDic = new Dictionary<string, List<string>>();
            WarehouseSectionDic.Add("81", new List<string> { "2", "5" });
            WarehouseSectionDic.Add("82", new List<string> { "2", "5" });
            WarehouseSectionDic.Add("83", new List<string> { "2", "5" });
            WarehouseSectionDic.Add("84", new List<string> { "2", "5" });
            WarehouseSectionDic.Add("85", new List<string> { "2", "5" });
            WarehouseSectionDic.Add("89", new List<string> { "1" });
            WarehouseSectionDic.Add("91", new List<string> { "1" });
            //List<string> dstWarehouseList = new List<string>() { "82_2", "82_5", "83_2", "83_5", "84_2", "84_5", "85_2", "85_5", "89_1", "91_1" };
            Dictionary<string, List<OYR1_OY01Report.Model>> dst_warehouse_Area_List = new Dictionary<string, List<OYR1_OY01Report.Model>>();
            foreach (string dstWarehouse in dstWarehouseList)
            {
                OYR1_OY01Report dst_oyr1_oy01report = new OYR1_OY01Report(eip, dstWarehouse);//目標庫儲區資訊
                List<OYR1_OY01Report.Model> dst_Area_List = dst_oyr1_oy01report.List.Where(m => int.Parse(m.remain) > 0).ToList();//只挑選有剩餘儲位的儲區                
                //foreach (OYR1_OY01Report.Model item in dst_Area_List)
                //{
                //    item.warehouse += "_" + determine_dst_warehouse_section(item.warehouse, item.area);
                //}
                dst_warehouse_Area_List.Add(dstWarehouse, dst_Area_List);//目標庫儲區資訊
            }
            foreach (string warehouse in srcWarehouseList)
            {
                OYR1_OY01Report src_oyr1_oy01report = new OYR1_OY01Report(eip, warehouse);//來源庫儲區資訊

                //將儲區資訊中雜區的資料挑選並蒐集到 src_mixArea_List
                List<OYR1_OY01Report.Model> src_mixArea_List = src_oyr1_oy01report.List.Where(m => m.category == "雜區").ToList();

                OYR1D1Report src_oyr1_d1report = new OYR1D1Report(eip, warehouse);//來源庫鋼捲資訊

                //移除src_oyr1_d1report.List中weight、width無法解析成整數的元素
                src_oyr1_d1report.List.RemoveAll(item =>
                {
                    int weight;
                    int width;

                    return !int.TryParse(item.weight, out weight) || !int.TryParse(item.width, out width);
                });
                src_oyr1_d1report.List.RemoveAll(item => item == null);

                List<OYR1D1Report.Model> src_mixArea_Coil_List = new List<OYR1D1Report.Model>();



                foreach (OYR1_OY01Report.Model areaItem in src_mixArea_List)
                {
                    //重量小於5000、寬度小於1700的鋼捲儲存至 checkCoilInMixArea
                    var checkCoilInMixArea = from c in src_oyr1_d1report.List
                                             where isInArea(c.area, areaItem) && int.Parse(c.weight) < 5000 && int.Parse(c.width) < 1700
                                             && (c.state == "39" || c.state == "23") && c.bill_of_ladding == ""
                                             select c;

                    if (checkCoilInMixArea.Count() != 0)
                    {
                        src_mixArea_Coil_List.AddRange(checkCoilInMixArea);
                    }
                }
                #region LinQ寫法
                //var selectedCoils = from coil in src_mixArea_Coil_List
                //                    join dst in tmp_dst_Area_List on (coil.coil_code+coil.red_tag_coil_layer) equals (dst.area_code+dst.layer) into result
                //                    from r in result
                //                    select new advanceoy15_clearmixarea
                //                    {
                //                        area= coil.area,
                //                        position=coil.position,
                //                        coil_code=coil.coil_code,
                //                        red_tag_coil_layer=coil.red_tag_coil_layer,
                //                        label=coil.label,
                //                        warehouse=coil.warehouse,
                //                        weight=coil.weight,
                //                    };
                #endregion

                List<advanceoy15_clearmixarea> updateList = new List<advanceoy15_clearmixarea>();

                //走訪所有的來源鋼捲
                foreach (var srcItem in src_mixArea_Coil_List)
                {
                    //走訪所有的目標庫
                    foreach (string warehouseItem in dstWarehouseList)
                    {

                        Dictionary<string, List<OYR1_OY01Report.Model>> tmp_dst_section_Area_list = new Dictionary<string, List<OYR1_OY01Report.Model>>();
                        //走訪目標庫的所有庫段，依照儲區建立字典
                        //例如:{庫段1,List<OYR1_OY01Report.Model>},{庫段22,List<OYR1_OY01Report.Model>},{庫段5,List<OYR1_OY01Report.Model>}
                        foreach (string sectionItem in WarehouseSectionDic[warehouseItem])
                        {
                            List<OYR1_OY01Report.Model> tmp_dst_Area_List = new List<OYR1_OY01Report.Model>();
                            tmp_dst_section_Area_list.Add(sectionItem, tmp_dst_Area_List);
                        }

                        //走訪目標庫的所有庫段，將儲區依照庫段分類
                        foreach (string sectionItem in WarehouseSectionDic[warehouseItem])
                        {
                            List<OYR1_OY01Report.Model> tmp_dst_Area_List = dst_warehouse_Area_List[warehouseItem];

                            //將 儲區 分類儲存至至 tmp_dst_section_Area_list，現在我們有一個依照庫段分類的字典，裏面包含了多個儲區
                            foreach (var dst_Area in tmp_dst_Area_List)
                            {
                                string dst_warehouse_section = determine_dst_warehouse_section(dst_Area.warehouse, dst_Area.area);
                                if(dst_warehouse_section== sectionItem)tmp_dst_section_Area_list[sectionItem].Add(dst_Area);
                            }

                        }

                        //走訪目標庫的所有庫段
                        foreach (string sectionItem in WarehouseSectionDic[warehouseItem])
                        {

                            Boolean updateItem_isCreate = false;//updateItem_isCreate 用來控制是否在同一庫段下已創建過該鋼捲
                            advanceoy15_clearmixarea tmp_updateItem = new advanceoy15_clearmixarea();

                            List<OYR1_OY01Report.Model> tmp_dst_Area_List = dst_warehouse_Area_List[warehouseItem];
                            //走訪目標庫 特定庫段的所有儲區
                            foreach (var dst_Area in tmp_dst_section_Area_list[sectionItem])
                            {
                                //將目標庫-特定庫段-所有儲區與來源鋼捲比對，如果兩者代碼相同，且層數相同，則將其納入雜區清理鋼捲清單
                                if (srcItem.coil_code == dst_Area.area_code && srcItem.red_tag_coil_layer == dst_Area.layer)
                                {
                                    string dst_warehouse_section = determine_dst_warehouse_section(dst_Area.warehouse, dst_Area.area);
                                    if (!updateItem_isCreate || tmp_updateItem.dst_section != dst_warehouse_section)
                                    {
                                        tmp_updateItem = new advanceoy15_clearmixarea
                                        {
                                            area = srcItem.area,
                                            position = srcItem.position,
                                            coil_code = srcItem.coil_code,
                                            red_tag_coil_layer = srcItem.red_tag_coil_layer,
                                            src_warehouse = srcItem.warehouse,
                                            dst_warehouse = dst_Area.warehouse,
                                            dst_section = dst_warehouse_section,
                                            label = srcItem.label,
                                            weight = Math.Round((double.Parse(srcItem.weight)/1000),1).ToString(),
                                        };
                                        updateItem_isCreate = true;
                                    }

                                    //將同庫段目標儲區記錄在備註
                                    if (string.IsNullOrEmpty(tmp_updateItem.comment))
                                    {
                                        tmp_updateItem.comment = $"{(dst_Area.area + dst_Area.line + dst_Area.preserve)} 餘 {dst_Area.remain}";
                                    }
                                    else
                                    {
                                        tmp_updateItem.comment += $", {(dst_Area.area + dst_Area.line + dst_Area.preserve)} 餘 {dst_Area.remain}";
                                    }


                                }
                            }
                            updateList.Add(tmp_updateItem);
                        }
                    }
                }
                int x = 1;
                string filePath = $@"D:\C349WebMVC\output_{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}.csv";
                CsvExporter.ExportToCsv(updateList, filePath);

                MSSQL_update_table(updateList);
            }

        }

        private string determine_dst_warehouse_section(string dst_warehouse, string dst_area)
        {
            string section = "";
            switch (dst_warehouse)
            {
                case "82":
                    section = int.Parse(dst_area) <= 15 ? "2" : "5";
                    break;
                case "83":
                    section = int.Parse(dst_area) <= 15 ? "2" : "5";
                    break;
                case "84":
                    section = int.Parse(dst_area) <= 13 ? "2" : "5";
                    break;
                case "85":
                    section = int.Parse(dst_area) <= 12 ? "2" : "5";
                    break;
                case "81":
                    section = int.Parse(dst_area) <= 15 ? "2" : "5";
                    break;
                default:
                    section = "1";
                    break;
            }
            return section;
        }
        private void MSSQL_clear_table()
        {
            //IList<advanceoy15_clearmixarea> toDelete = db.advanceoy15_clearmixarea.Where(m => m.warehouse == warehouse).ToList();
            //IList<advanceoy15_clearmixarea> toDelete = db.advanceoy15_clearmixarea.ToList();
            //foreach (var item in toDelete)
            //{
            //    db.advanceoy15_clearmixarea.Remove(item);
            //    db.SaveChanges();
            //}
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE advanceoy15_clearmixarea");
        }
        private void MSSQL_update_table(List<advanceoy15_clearmixarea> updateItems)
        {
            foreach (var item in updateItems)
            {
                advanceoy15_clearmixarea model_item = new advanceoy15_clearmixarea();
                model_item.area = item.area;
                model_item.position = item.position;
                model_item.coil_code = item.coil_code;
                model_item.red_tag_coil_layer = item.red_tag_coil_layer;
                model_item.label = item.label;
                model_item.weight = item.weight;
                model_item.src_warehouse = item.src_warehouse;
                model_item.dst_warehouse = item.dst_warehouse;
                model_item.dst_section = item.dst_section;
                model_item.comment = item.comment;
                db.advanceoy15_clearmixarea.Add(model_item);

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
        private bool isInArea(string area, OYR1_OY01Report.Model coilitem)
        {
            return area == (coilitem.area + coilitem.line + coilitem.preserve);
        }

    }

    public class CsvExporter
    {
        public static void ExportToCsv(List<advanceoy15_clearmixarea> data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the CSV header
                writer.WriteLine("id,area,position,coil_code,red_tag_coil_layer,label,weight,src_warehouse,dst_warehouse,dst_section,comment");

                // Write each data row
                foreach (var item in data)
                {
                    writer.WriteLine($"{item.id},{item.area},{item.position},{item.coil_code},{item.red_tag_coil_layer},{item.label},{item.weight},{item.src_warehouse},{item.dst_warehouse},{item.dst_section},{item.comment}");
                }
            }

            Console.WriteLine($"CSV exported successfully to: {filePath}");
        }
    }
}