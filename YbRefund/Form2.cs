using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos244_GJYB.Models;
using Soft.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YBRefund
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            JObject jin1101 = JObject.Parse(input);
            string psn_name = jin1101["input"]["data"]["psn_name"].ToString();

            string json = JsonConvert.SerializeObject(jin1101);
            string injson = jin1101["input"]["data"].ToString();

            T1101.Data data = JSONSerializer.Deserialize<T1101.Data>(injson);

            string injson2 = jin1101["input"].ToString();
            T1101.Root t1101 = JSONSerializer.Deserialize<T1101.Root>(injson2);

            string aac = jin1101["input"]["Root"].ToString();
            T1101.Root root = JSONSerializer.Deserialize<T1101.Root>(aac);
            //反序列化看的是属性，不是内部类
            input t11011 = JSONSerializer.Deserialize<input>(jin1101["input"].ToString());
            input1 input1 = JSONSerializer.Deserialize<input1>(jin1101.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
