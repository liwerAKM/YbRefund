﻿using Log.Core.Model;
using Newtonsoft.Json.Linq;
using Soft.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace OnlineBusHos244_GJYB
{
    class GlobalVar
    {

        public static string DoBussiness = ""; //GetConfig("DoBussiness");

        public static string callmode = ""; // GetConfig("DoBussiness");

        public static string posturl = ""; // GetConfig("url");

        public static string parameter = ""; // GetConfig("parameters");

        public static string use_encryption = ""; // GetConfig("use_encryption");

        public static string MethodName = ""; //GetConfig("MethodName");
        public static string BusinessClass = ""; // GetConfig("BusinessClass");

        public static Type type = null;//Assembly.GetExecutingAssembly().GetType(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace+".BUS." + BusinessClass);
        public static object obj = null;//Activator.CreateInstance(type);
        //public static IBusiness business = null;//(IBusiness)obj;

        //public  IBusiness business = RegisterType(type).As<IBusiness>().SingleInstance();
        public static string GetConfig(string configname)
        {
            string key = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "_" + configname;
            Config config = null;
            try
            {
                config = DictionaryCacheHelper.GetCache(key, () => GetConfigClass(configname));
            }
            catch
            {
                config = DictionaryCacheHelper.UpdateCache(key, () => GetConfigClass(configname));
            }
            if (config == null)
            {
                return "";
            }
            else
            {
                TimeSpan ts = new TimeSpan();
                ts = DateTime.Now - config.Time;
                if (ts.Minutes >= 5)
                {
                    config = DictionaryCacheHelper.UpdateCache(key, () => GetConfigClass(configname));
                }
            }
            return config.Value;
        }

        public static Config GetConfigClass(string configname)
        {
            XmlDocument docini = new XmlDocument();
            docini.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + ".dll.config"));
            DataSet ds = XMLHelper.X_GetXmlData(docini, "configuration/appSettings");//请求的数据包
            DataRow[] dr = ds.Tables[0].Select("key='" + configname + "'");
            if (dr.Length > 0)
            {
                Config C = new Config();
                C.Key = configname;
                C.Value = dr[0]["value"].ToString();
                C.Time = DateTime.Now;
                return C;
            }
            else
            {
                return null;
            }
        }

        public class Config
        {
            public string Key { get; set; }

            public string Value { get; set; }

            public DateTime Time { get; set; }


        }

        //public static void Init ()
        //{

        //    BusinessClass = GetConfig("BusinessClass");
        //    Type type = Assembly.GetExecutingAssembly().GetType(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + ".BUS." + BusinessClass);
        //    obj = Activator.CreateInstance(type);
        //    business = (IBusiness)obj;

        //    DoBussiness = GetConfig("DoBussiness");
        //    callmode = GetConfig("callmode");
        //    posturl = GetConfig("url");
        //    parameter = GetConfig("parameters");
        //    use_encryption = GetConfig("use_encryption");
        //    MethodName = GetConfig("MethodName");
        //}

        /// <summary>
        /// 医保接口入口
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="inputRoot"></param>
        /// <returns></returns>
        public static Models.OutputRoot YBTrans(string HOS_ID, Models.InputRoot inputRoot)
        {
            Models.OutputRoot outputRoot = new Models.OutputRoot();
            //调用医保
            bool flag = CSBHelper.CallCSBService(inputRoot, out outputRoot);
            return outputRoot;
        }
 
        public static JObject GetVaildCard(string pat_card_out)
        {
            try
            {
                JObject jb = JObject.Parse(pat_card_out);
                //paus_insu_date 是null或大于今天 筛选条件
                //取列表第一个
                JArray insuinfo = JArray.Parse(jb["insuinfo"].ToString());
                insuinfo = new JArray(insuinfo.OrderBy(x => x["insutype"]));
                string insuplc_admdvs = "";
                foreach (var j in insuinfo)
                {
                    if (insuplc_admdvs == "" && (FormatHelper.GetStr(j["paus_insu_date"]) == "" || FormatHelper.GetStr(j["paus_insu_date"]).CompareTo(DateTime.Now.ToString("yyyy-MM-dd")) > 0))
                    {
                        return JObject.Parse(j.ToString());
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }


        public static bool CALLSERVICE(string HOS_ID, string inxml, ref string his_rtnxml)
        {
            DateTime intime = DateTime.Now;
            try
            {
                if (callmode == "0")//webservice
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable = GlobalVar.GetHashTable(inxml, HOS_ID, parameter, use_encryption);
                    XmlDocument doc_sec=new XmlDocument();
                    try
                    {
                         doc_sec = WebServiceHelper.QuerySoapWebService(posturl, MethodName, hashtable);
                    }
                    catch (Exception ex) {



                    }
                    his_rtnxml = doc_sec.InnerText;
                }
                else if (callmode == "1")//api
                {
                    string secretkey = EncryptionKeyCore.KeyData.AESKEY(HOS_ID);
                    string encryxml = AESExample.AESEncrypt(inxml, secretkey);
                    string signature = EncryptionKeyCore.MD5Helper.Md5(encryxml + secretkey);
                    indata apiin = new indata();
                    apiin.user_id = HOS_ID;
                    apiin.xmlstr = encryxml;
                    apiin.signature = signature;
                    var http = new Soft.Core.HttpClient(posturl);
                    string out_data = "";
                    int status = http.SendJson(encryxml, Encoding.UTF8, out out_data);
                    if (status == 200)
                    {
                        outdata outdata = JSONSerializer.Deserialize<outdata>(out_data);
                        his_rtnxml = outdata.outxml;
                    }
                    else
                    {
                        ModLogHosError modLogHos = new ModLogHosError();
                        modLogHos.inTime = intime;
                        modLogHos.inXml = inxml;
                        modLogHos.outTime = DateTime.Now;
                        modLogHos.outXml = out_data;
                        new Log.Core.MySQLDAL.DalLogHosError().Add(modLogHos);
                        his_rtnxml = out_data;
                        return false;
                    }
                }
                try
                {
                    if (use_encryption == "1")
                    {
                        string secretkey = EncryptionKeyCore.KeyData.AESKEY(HOS_ID);
                        his_rtnxml = AESExample.AESDecrypt(his_rtnxml, secretkey);
                    }
                }
             catch(Exception ex) {



                }

                if (DoBussiness == "1")
                {
                    ModLogHos modLogHos = new ModLogHos();
                    modLogHos.inTime = intime;
                    modLogHos.inXml = inxml;
                    modLogHos.outTime = DateTime.Now;
                    modLogHos.outXml = his_rtnxml;
                    new Log.Core.MySQLDAL.DalLogHos().Add(modLogHos);
                }
            }
            catch (Exception ex)
            {
                ModLogHosError modLogHos = new ModLogHosError();
                modLogHos.inTime = intime;
                modLogHos.inXml = inxml;
                modLogHos.outTime = DateTime.Now;
                modLogHos.outXml = his_rtnxml;
                new Log.Core.MySQLDAL.DalLogHosError().Add(modLogHos);
                his_rtnxml = ex.ToString();
                return false;
            }
            return true;
        }

        /// <summary>
        /// POST入参存入hashtable
        /// </summary>
        /// <param name="inxml">入参</param>
        /// <param name="hos_id">医院ID</param>
        /// <param name="para">POST参数</param>
        /// <returns></returns>
        public static Hashtable GetHashTable(string inxml, string hos_id, string para, string use_encryption)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                if (use_encryption == "1")
                {
                    string secretkey = "";
                    secretkey = EncryptionKeyCore.KeyData.AESKEY(hos_id);
                    string encryxml = AESExample.AESEncrypt(inxml, secretkey);
                    string signature = EncryptionKeyCore.MD5Helper.Md5(encryxml + secretkey);
                    string[] items = para.Split('^');
                    string[] _showids = items[0].Split('|');
                    string[] _shownames = items[1].Split('|');

                    if (_showids[0] == "1")
                    {
                        hashtable.Add(_shownames[0], encryxml);
                    }
                    if (_showids[1] == "1")
                    {
                        hashtable.Add(_shownames[1], hos_id);
                    }
                    if (_showids[2] == "1")
                    {
                        hashtable.Add(_shownames[2], signature);
                    }
                }
                else
                {
                    string[] items = para.Split('^');
                    string[] _showids = items[0].Split('|');
                    string[] _shownames = items[1].Split('|');
                    if (_showids[0] == "1")
                    {
                        hashtable.Add(_shownames[0], inxml);
                    }
                    if (_showids[1] == "1")
                    {
                        hashtable.Add(_shownames[1], hos_id);
                    }
                    if (_showids[2] == "1")
                    {
                        hashtable.Add(_shownames[2], "");
                    }
                }
                return hashtable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public class indata
        {
            public string xmlstr { get; set; }
            public string user_id { get; set; }
            public string signature { get; set; }
        }
        public class outdata
        {
            public string outxml { get; set; }
        }


        public static void WriteLog(string type, string className, string content)
        {
            string path = "";
            try
            {
                // path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\MySpringlog";
                path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PasSLog", "ZzjLog");
            }
            catch (Exception ex)
            {
                //   path = HttpContent.Current.Server.MapPath("MySpringlog");
            }

            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
                string filename = path + "/" + DateTime.Now.ToString("yyyyMMdd") + type.Replace('|', ':') + ".log";//用日期对日志文件命名
                //创建或打开日志文件，向日志文件末尾追加记录
                StreamWriter mySw = File.AppendText(filename);

                //向日志文件写入内容
                string write_content = className + ": " + content;
                mySw.WriteLine(time + " " + type);
                mySw.WriteLine(write_content);
                mySw.WriteLine("");
                //关闭日志文件
                mySw.Close();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
