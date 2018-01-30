using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;


namespace MEDICINE_TEST
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        private Form3 masterForm;
        public Form3 MasterForm
        {
            set
            {
                this.masterForm = value;
            }
        }
        
        string[] chapter = { "第一章 执业药师与药品安全", "第二章 医药卫生体制改革与国家基本药物制度", "第三章 药品监督管理体制与法律体系", "第四章 药品研制与生产管理", "第五章 药品经营与使用管理", "第六章 中药管理", "第七章 特殊管理的药品管理", "第八章 药品标准与药品质量监督检验", "第九章 药品广告管理与消费者权益保护", "第十章 药品安全法律责任", "第十一章 医疗器械" ,"2016执业药师资格考试通关密卷"};
        string[][] subject = { new string[] { "第一节 执业药师管理", "第二节 执业药师职业道德与服务规范", "第三节 药品与药品安全管理", "章节测验" }, new string[] { "第一节 深化医药卫生体制改革", "第二节 国家基本药物制度", "章节测验" }, new string[] { "第一节 药品监督管理机构", "第二节 药品监督管理技术支撑机构","第三节 药品管理法", "第四节 药品监督管理行政法律制度", "章节测验" }, new string[] { "第一节  药品研制与注册管理", "第二节 药品生产管理", "章节测验" }, new string[] { "第一节 药品经营管理", "第二节  药品使用管理", "第三节 药品分类管理", "第四节 医疗保障用药管理", "第五节 药品不良反应报告与监测管理", "章节测验" }, new string[] { "第一节 中药与中药创新发展", "第二节 中药材管理", "第三节 中药饮片管理", "第四节 中成药管理", "章节测验" }, new string[] { "第一节 麻醉药品，精神药品的管理", "第二节 医疗用毒性药品的管理", "第三节 类易制毒化学品的管理", "第四节 含特殊药品复方制剂的管理", "第五节 兴奋剂的管理", "第六节 疫苗的管理", "章节测验" }, new string[] { "第一节 药品标准管理", "第二节 药品说明书和标签管理", "章节测验" }, new string[] { "第一节 药品广告管理", "第二节 反不正当竞争法", "第三节 消费者权益保护", "章节测验" }, new string[] { "第一节 药品安全法律责任与特征", "第二节 生产销售假药，劣药的法律责任", "第三节 违反药品监督管理规定的法律责任", "第四节 违反特殊管理的药品管理规定的法律责任", "章节测验" }, new string[] { "第一节	医疗器械管理", "第二节 保健食品管理", "第三节	化妆品管理","章节测验" } ,new string[] {"模拟试卷(一)","模拟试卷(二)","模拟试卷(三)","模拟试卷(四)","模拟试卷(五)"}};
        //SqlConnection sqlConnection;
        //SqlDataAdapter sqlDataAdapter;
        //string connStr = @"server=localhost;database=TEM;integrated security=SSPI";
        DataSet ds = new DataSet();
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int chapternumber = comboBox1.SelectedIndex;

            switch (chapternumber)
            {
                case 0: comboBox2.DataSource = subject[0];
                    break;
                case 1: comboBox2.DataSource = subject[1];
                    break;
                case 2: comboBox2.DataSource = subject[2];
                    break;
                case 3: comboBox2.DataSource = subject[3];
                    break;
                case 4: comboBox2.DataSource = subject[4];
                    break;
                case 5: comboBox2.DataSource = subject[5];
                    break;
                case 6: comboBox2.DataSource = subject[6];
                    break;
                case 7: comboBox2.DataSource = subject[7];
                    break;
                case 8: comboBox2.DataSource = subject[8];
                    break;
                case 9: comboBox2.DataSource = subject[9];
                    break;
                case 10: comboBox2.DataSource = subject[10];
                    break;
                case 11: comboBox2.DataSource = subject[11];
                    break;
                default: break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = "select ChapterId,ChapterName from Chapter";
            //sqlConnection = new SqlConnection(connStr);
            //sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
            //sqlDataAdapter.Fill(ds, "Chapter");   
            

            comboBox1.DataSource = chapter;
            //comboBox1.DataSource = ds.Tables["Chapter"];
            //comboBox1.DisplayMember = "ChapterName";
            //comboBox1.ValueMember = "ChapterId";

            comboBox2.DataSource = subject[0]; //初始化combox2
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 mm = new Form2();
            mm.titleName = comboBox2.Text;
            mm.subjectid = Convert.ToInt32(comboBox2.SelectedIndex);
            mm.chapterid = Convert.ToInt32(comboBox1.SelectedIndex);
            if(comboBox2.Text == "章节测验")
            {
                mm.iftest = true;
            }
            mm.MasterForm = this;
            this.Visible = false;
            mm.ShowDialog();
            //if (mm.DialogResult== DialogResult.OK)
            //{
                //this.Visible = true;
            //} 
        }
        //关闭按钮重写
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.masterForm.Show();
        }
        
    }
}
