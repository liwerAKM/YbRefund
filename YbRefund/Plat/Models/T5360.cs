﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineBusHos244_GJYB.Models
{
    /// <summary>
    /// 5.5.3.5 【5360】人员审批定点信息获取
    /// </summary>
    public class T5360
    {
        public class data
        {
            //public string mdtrt_cert_type { get; set; }// mdtrt_cert_type	就诊凭证类型
            //public string mdtrt_cert_no { get; set; }//mdtrt_cert_no	就诊凭证编号
            //public string card_sn { get; set; }//card_sn	卡识别码
            //public string begntime { get; set; }//begntime	开始时间
            //public string psn_cert_type { get; set; }//psn_cert_type	人员证件类型
            //public string certno { get; set; } //certno	证件号码
            //public string psn_name { get; set; } //psn_name	人员姓名
            //public string fixmedins_code { get; set; } //fixmedins_code	定点医药机构编号

            public string begntime { get; set; }//begntime	开始时间
            public string psn_name { get; set; } //psn_name	人员姓名
            public string psn_no { get; set; } //psn_name	人员编号
            public string insutype { get; set; } //psn_name	险种

        }

        public class Root
        {
            public data data { get; set; }
        }
    }

    /// <summary>
    /// 输出（节点标识：insuinfo）
    /// </summary>
    public class RT5360
    {
        public class insuinfo
        {
            public string psn_no { get; set; }//psn_no	人员编号
            public string insutype { get; set; } //insutype	险种类型
            public string med_type { get; set; } //med_type	医疗类别
            public string dise_codg { get; set; }//dise_codg	病种编码
            public string dise_name { get; set; }//dise_name	病种名称
            public string begndate { get; set; }//begndate	开始日期
            public string enddate { get; set; }//enddate	结束日期
            public string exp_content { get; set; }//exp_content	字段扩展


        }

        public class Output
        {
            /// <summary>
            /// 
            /// </summary>
            public List<insuinfo> data { get; set; }
        }
        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public string infcode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string inf_refmsgid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string refmsg_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string respond_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string enctype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string signtype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string err_msg { get; set; }

            public Output output { get; set; }
           

        }
    }
}
