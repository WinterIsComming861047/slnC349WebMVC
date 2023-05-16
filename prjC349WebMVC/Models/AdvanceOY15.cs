using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjC349WebMVC.Models
{
    public class AdvanceOY15
    {
        //from igs4
        public string bill_of_ladding { get; set; }
        //public string area { get; set; }
        //public string label { get; set; }
        public string order_num { get; set; }
        //public string weight { get; set; }
        //public string width { get; set; }
        public string state { get; set; }
        public string ship_num_or_customer { get; set; }

        //from oyr1d1report
        public string warehouse { get; set; }
        public string area { get; set; }
        public string coil_number { get; set; }
        public string label { get; set; }
        public string coil_code { get; set; }
        public string area_code { get; set; }
        public string position { get; set; }
        public string length { get; set; }
        public string width { get; set; }
        public string thick { get; set; }
        public string weight { get; set; }
        public string inner_diameter { get; set; }
        public string outer_diameter { get; set; }
        public string red_tag_coil_layer { get; set; }
        public string area_available_layer { get; set; }
        public string move_count_oymv { get; set; }

        //for ranking
        public string catagory { get; set; }

        //from igs1
        public string is_order_finish { get; set; }
        public string order_delivery { get; set; }

        //for target warehouse
        public string target_warehouse { get; set; }

    }
}