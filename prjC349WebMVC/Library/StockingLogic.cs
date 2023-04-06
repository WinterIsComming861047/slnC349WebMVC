using NPOI.SS.Formula.Functions;
using prjC349WebMVC.Library.WebCrawler;
using prjC349WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjC349WebMVC.Library
{
    public class StockingLogic
    {
        public EIP eip;
        public StockingLogic(EIP eip)
        {
            this.eip = eip;
        }
        public List<LiveUpdateOY07> ToDoList()
        {
            IGS4 igs4 = new IGS4(eip);
            OYR1D1Report oyr1_d1report = new OYR1D1Report(eip);


            var result1 = from m in igs4.List
                          where m.ship_num_or_customer != ""
                          select m;

            List<IGS4.Model> has_ship_num_or_customer_List = result1.ToList();
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
                                    where c.area == shipitem.area && isSupressedBy(shipitem.position, c.position) && !(isShipItem(c.label, has_ship_num_or_customer_List))
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

            List<string> label_List = oyr1_d1report.List
                .Select(x => x.label)
                .Distinct()
                .ToList();
            
            //IGI1 igi1 = new IGI1(eip, label_List);
            //List<IGI1.Model> result_igi1 = igi1.List.Where(r => r.state == "23" || r.order_num.Substring(0, 1) == "T" || r.order_num == "").ToList();


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
                                making_cargo_plan = hs.making_cargo_plan,
                                catagory = "出貨被壓"
                            };
            List<LiveUpdateOY07> dataForView = finalData.ToList();

            IGS1 igs1 = new IGS1(eip);

            var combinedData2 = from i in igs1.List
                                join o in oyr1_d1report.List on i.label equals o.label into igs1oyr1_d1report
                                from io in igs1oyr1_d1report
                                select new LiveUpdateOY07
                                {
                                    is_order_finish = i.is_order_finish,
                                    bill_of_ladding = i.bill_of_ladding,
                                    order_num = i.order_num,
                                    state = i.state,
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
                                    move_count_oymv = io.move_count_oymv,
                                    catagory = "無主"
                                };
            List<LiveUpdateOY07> igs1oyr1_d1report_list = combinedData2.ToList().
                Where(r => r.state == "23" || r.state == "24" || r.state == "25" || r.state == "26" || r.order_num.Substring(0, 1) == "T" || r.is_order_finish == "已結案").ToList();
            List<LiveUpdateOY07> todoList = new List<LiveUpdateOY07>();

            foreach (LiveUpdateOY07 tmp_igs1 in igs1oyr1_d1report_list)
            {
                var checkUperCoil = from c in oyr1_d1report.List
                                    where c.area == tmp_igs1.area && isSupressedBy(c.position, tmp_igs1.position) && !(isWhiteItem(c.label, igs1oyr1_d1report_list))
                                    select c;
                if (checkUperCoil.Count() != 0)
                {
                    dataForView.Add(tmp_igs1);
                }
            }

            return dataForView;
        }

        private bool isWhiteItem(string label, List<LiveUpdateOY07> has_ship_num_or_customer_List)
        {
            var result1 = from h in has_ship_num_or_customer_List
                          where h.label == label
                          select h;
            var yy = result1.Count();
            if (yy == 0)
                return false;
            return true;
        }

        private bool isSupressedBy(string chk_coil_pos, string uper_coil_pos)
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
    }
}