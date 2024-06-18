﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos244_GJYB.Models
{
    #region N：6.2.3.8 【2208】门诊结算撤销
    public class T2208
    {
        public class Data
        {
            /// <summary>
            /// 结算ID
            /// </summary>
            public string setl_id { get; set; }
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 字段扩展
            /// </summary>
            public string expContent { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
        }
    }

    public class RT2208
    {
        /// <summary>
        /// 输出-结算信息（节点标识：setlinfo）
        /// </summary>
        public class Setlinfo
        {
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 结算ID
            /// </summary>
            public string setl_id { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 人员姓名
            /// </summary>
            public string psn_name { get; set; }
            /// <summary>
            /// 人员证件类型
            /// </summary>
            public string psn_cert_type { get; set; }
            /// <summary>
            /// 证件号码
            /// </summary>
            public string certno { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public string gend { get; set; }
            /// <summary>
            /// 民族
            /// </summary>
            public string naty { get; set; }
            /// <summary>
            /// 出生日期
            /// </summary>
            public string brdy { get; set; }
            /// <summary>
            /// 年龄
            /// </summary>
            public string age { get; set; }
            /// <summary>
            /// 险种类型
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 人员类别
            /// </summary>
            public string psn_type { get; set; }
            /// <summary>
            /// 公务员标志
            /// </summary>
            public string cvlserv_flag { get; set; }
            /// <summary>
            /// 结算时间
            /// </summary>
            public string setl_time { get; set; }
            /// <summary>
            /// 就诊凭证类型
            /// </summary>
            public string mdtrt_cert_type { get; set; }
            /// <summary>
            /// 医疗类别
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 医疗费总额
            /// </summary>
            public decimal? medfee_sumamt { get; set; }
            /// <summary>
            /// 全自费金额
            /// </summary>
            public decimal? fulamt_ownpay_amt { get; set; }
            /// <summary>
            /// 超限价自费费用
            /// </summary>
            public decimal? overlmt_selfpay { get; set; }
            /// <summary>
            /// 先行自付金额
            /// </summary>
            public decimal? preselfpay_amt { get; set; }
            /// <summary>
            /// 符合政策范围金额
            /// </summary>
            public decimal? inscp_scp_amt { get; set; }
            /// <summary>
            /// 实际支付起付线
            /// </summary>
            public decimal? act_pay_dedc { get; set; }
            /// <summary>
            /// 基本医疗保险统筹基金支出
            /// </summary>
            public decimal? hifp_pay { get; set; }
            /// <summary>
            /// 基本医疗保险统筹基金支付比例
            /// </summary>
            public decimal? pool_prop_selfpay { get; set; }
            /// <summary>
            /// 公务员医疗补助资金支出
            /// </summary>
            public decimal? cvlserv_pay { get; set; }
            /// <summary>
            /// 企业补充医疗保险基金支出
            /// </summary>
            public decimal? hifes_pay { get; set; }
            /// <summary>
            /// 居民大病保险资金支出
            /// </summary>
            public decimal? hifmi_pay { get; set; }
            /// <summary>
            /// 职工大额医疗费用补助基金支出
            /// </summary>
            public decimal? hifob_pay { get; set; }
            /// <summary>
            /// 医疗救助基金支出
            /// </summary>
            public decimal? maf_pay { get; set; }
            /// <summary>
            /// 其他支出
            /// </summary>
            public decimal? oth_pay { get; set; }
            /// <summary>
            /// 基金支付总额
            /// </summary>
            public decimal? fund_pay_sumamt { get; set; }
            /// <summary>
            /// 个人负担总金额
            /// </summary>
            public decimal? psn_part_amt { get; set; }
            /// <summary>
            /// 个人账户支出
            /// </summary>
            public decimal? acct_pay { get; set; }
            /// <summary>
            /// 个人现金支出
            /// </summary>
            public decimal? psn_cash_pay { get; set; }
            /// <summary>
            /// 医院负担金额
            /// </summary>
            public decimal? hosp_part_amt { get; set; }
            /// <summary>
            /// 余额
            /// </summary>
            public decimal? balc { get; set; }
            /// <summary>
            /// 个人账户共济支付金额
            /// </summary>
            public decimal? acct_mulaid_pay { get; set; }
            /// <summary>
            /// 医药机构结算ID
            /// </summary>
            public string medins_setl_id { get; set; }
            /// <summary>
            /// 清算经办机构
            /// </summary>
            public string clr_optins { get; set; }
            /// <summary>
            /// 清算方式
            /// </summary>
            public string clr_way { get; set; }
            /// <summary>
            /// 清算类别
            /// </summary>
            public string clr_type { get; set; }

        }
        /// <summary>
        /// 输出-结算基金分项信息（节点标识：setldetail）
        /// </summary>
        public class Setldetail
        {
            public string fund_pay_type { get; set; }//	基金支付类型
            public decimal? inscp_scp_amt { get; set; }//	符合政策范围金额
            public decimal? crt_payb_lmt_amt { get; set; }//	本次可支付限额金额
            public decimal? fund_payamt { get; set; }//	基金支付金额
            public string fund_pay_type_name { get; set; }//	基金支付类型名称
            public string setl_proc_info { get; set; }//	结算过程信息

        }

        public class Root
        {
            public Setlinfo setlinfo { get; set; }

            public List<Setldetail> setldetail { get; set; }
 
        }
    }


    #endregion
}
