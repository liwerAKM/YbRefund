using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos244_GJYB;
using OnlineBusHos244_GJYB.SqlSugarModel;

using System.Reflection;

namespace YBRefund
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string psn_no = textBox1.Text;

            string mdtrt_id = textBox2.Text;
            string setl_id = textBox3.Text;
            string HOS_ID = textBox5.Text;
            string tran_id = textBox7.Text;
            if (string.IsNullOrEmpty(HOS_ID)) { HOS_ID = "244"; }

            string opter_no = textBox4.Text;

            if (string.IsNullOrEmpty(opter_no)) { opter_no = "001"; }
            string msg = "";
            string infno = "";
            bool flag = false;
            //门诊结算撤销【2208】
            infno = "2208";
            OnlineBusHos244_GJYB.Models.InputRoot inputRoot2208 = new OnlineBusHos244_GJYB.Models.InputRoot();
            JObject jin2208 = new JObject();
            JObject jin2208data = new JObject();
            var db = new DbContext().Client;
            OnlineBusHos244_GJYB.SqlSugarModel.ChsTran chsTran=new();

            if (string.IsNullOrEmpty(tran_id))
            {
                chsTran = db.Queryable<OnlineBusHos244_GJYB.SqlSugarModel.ChsTran>().Where(t => t.mdtrt_id == mdtrt_id && t.setl_id == setl_id).Single();
                if (chsTran == null)
                {
                    textBox6.Text = "未能查询到mdtrtid对应流水号的交易记录";
                    return;
                }


            }
            else {

                chsTran = db.Queryable<OnlineBusHos244_GJYB.SqlSugarModel.ChsTran>().Where(t => t.TRAN_ID == tran_id).Single();
                

                if (chsTran == null)
                {
                    textBox6.Text = "未能查询到tranid对应流水号的交易记录";
                    return;
                }
                psn_no = chsTran.psn_no;
                mdtrt_id = chsTran.mdtrt_id;
                setl_id = chsTran.setl_id;

            }
            string insuplc_admdvs = chsTran.insuplc_admdvs;

            psn_no = chsTran.psn_no;

   
            jin2208data["psn_no"] = psn_no;
            jin2208data["mdtrt_id"] = mdtrt_id;
            jin2208data["setl_id"] = setl_id;
            jin2208["data"] = jin2208data;

            flag = CSBHelper.CreateInputRoot(HOS_ID, infno, "", opter_no, insuplc_admdvs, jin2208, ref inputRoot2208, ref msg);
            if (!flag)
            {
                //textBox6.Text = "生成医保入参异常";
            }
            //调用医保
            OnlineBusHos244_GJYB.Models.OutputRoot outputRoot2208 = GlobalVar.YBTrans(HOS_ID, inputRoot2208);
            if (outputRoot2208.infcode != "0")
            {
                textBox6.Text = outputRoot2208.err_msg;
                return;
            }

            chsTran.chsInput2208 = JsonConvert.SerializeObject(inputRoot2208);
            chsTran.chsOutput2208 = JsonConvert.SerializeObject(outputRoot2208);
            db.Updateable(chsTran).ExecuteCommand();

            textBox6.Text = "退费成功";










        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}