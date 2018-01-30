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
using System.IO;
namespace MEDICINE_TEST
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //切换窗口使用
        private Form1 masterForm;
        public Form1 MasterForm
        {
            set
            {
                this.masterForm = value;
            }
        }
        //选择得到的章，节参数及显示标题名tion
        public int chapterid;
        public string titleName;
        public int subjectid;
        public int tem_id;

        public int index = 0;    //记录题目ID
        public int count = 0;    //
        public bool iftest;      //传入判断是否为章测验
        //测验数据存储
        string[] questions;
        int[] question = new int[20];
        bool[] Bquestion;
        //答案记录
        public string[] give_answer;    //测验答案
        public string[] Tanswer;          //正确答案

        public string frompath1 = @"XML\FinalXml1.xml";
        public string frompath2 = @"XML\FinalXml2.xml"; 
        public string frompath3 = @"XML\FinalXml3.xml";
        public string frompath4 = @"XML\FinalXml4.xml";
        public string frompath5 = @"XML\FinalXml5.xml";
        public string frompath6 = @"XML\FinalXml6.xml";
        public string frompath7 = @"XML\FinalXml7.xml";
        public string frompath8 = @"XML\FinalXml8.xml";
        public string frompath9 = @"XML\FinalXml9.xml";
        public string frompath10 = @"XML\FinalXml10.xml";
        public string frompath11 = @"XML\FinalXml11.xml";

        public string frompath0_1 = @"XML\FinalXml0_1.xml";
        public string frompath0_2 = @"XML\FinalXml0_2.xml";
        public string frompath0_3 = @"XML\FinalXml0_3.xml";
        public string frompath0_4 = @"XML\FinalXml0_4.xml";
        public string frompath0_5 = @"XML\FinalXml0_5.xml";

        public int judge_end = 0; //判断是否做完题 0代表没做完 1代表做完了

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //SqlConnection sqlConnection;
        //SqlDataAdapter sqlDataAdapter;
        //string connStr = "server=localhost;database=TEM;integrated security=SSPI";
        DataSet ds = new DataSet();
        //DataRow[] question12 = new DataRow[20];
        DataRow[] question12;    //单选题存储
        DataRow[] question13;    //多选题存储

        DataRow[] question_s;
        DataRow[] question_m;
        DataRow[] question_tem;
        DataTable dt;
        
        private void Form2_Load(object sender, EventArgs e)
        {
            subjectid = subjectid + 1;
            chapterid = chapterid + 1;
            
            //xml格式读取
            //StringReader stream = null;
            //XmlTextReader reader = null;
             
            //XmlDocument doc = new XmlDocument();
            //doc.Load(frompath);
            //StreamReader tem = System.IO.StreamReader(doc.InnerXml);
            //stream = new StreamReader();
            //reader = new XmlTextReader(stream);
            switch (chapterid)
            {
                case 1: ds.ReadXml(frompath1);
                    break;
                case 2: ds.ReadXml(frompath2);
                    break;
                case 3: ds.ReadXml(frompath3);
                    break;
                case 4: ds.ReadXml(frompath4);
                    break;
                case 5: ds.ReadXml(frompath5);
                    break;
                case 6: ds.ReadXml(frompath6);
                    break;
                case 7: ds.ReadXml(frompath7);
                    break;
                case 8: ds.ReadXml(frompath8);
                    break;
                case 9: ds.ReadXml(frompath9);
                    break;
                case 10: ds.ReadXml(frompath10);
                    break;
                case 11: ds.ReadXml(frompath11);
                    break;
                case 12:if(subjectid == 1) 
                           ds.ReadXml(frompath0_1);
                        else if(subjectid == 2)
                           ds.ReadXml(frompath0_2);
                        else if(subjectid == 3)
                           ds.ReadXml(frompath0_3);
                        else if (subjectid == 4)
                           ds.ReadXml(frompath0_4);
                        else if(subjectid == 5)
                           ds.ReadXml(frompath0_5);
                    break;
                default: break;
            }
            //ds.ReadXml(frompath);
            //DataTable question12 = new DataTable();
            //question12 = ds.Tables[0];
            dt = new DataTable();
            dt = ds.Tables[0].Clone();
            this.Text = "答题界面--" + titleName;
            
            //章测验
            if (iftest == true)
            {
                this.count = 20;
                give_answer = new string[this.count];
                Tanswer = new string[this.count];

                button2.Enabled = false; //使显示答案按钮无效
                button2.Visible = false;
                question12 = new DataRow[20];
                randomInsert();
                QuestionMethod();
                
            }
            //通关密卷
            else if(chapterid == 12)
            {
                int k = 0;
                this.count = 120;
                give_answer = new string[this.count];
                Tanswer = new string[this.count];
                iftest = true;

                button2.Enabled = false; //使显示答案按钮无效
                button2.Visible = false;

                string ds_table_sql_test_s = "subjectid =" + subjectid + " and chapterid = '0' and class = '单选题'";
                question12 = ds.Tables[0].Select(ds_table_sql_test_s);
                for ( k = 0; k < 110; k++)
                {
                    dt.ImportRow(question12[k]);
                    Tanswer[k] = question12[k]["answer"].ToString();
                }

                string ds_table_sql_test_m = "subjectid =" + subjectid + " and chapterid = '0' and class = '多选题'";
                question13 = ds.Tables[0].Select(ds_table_sql_test_m);
                for ( k = 110; k < this.count; k++)
                {
                    dt.ImportRow(question13[k-110]);
                    Tanswer[k] = question13[k-110]["answer"].ToString();
                }

                QuestionMethod();
            }
            //正常章节做题
            else    
            {
                int k = 0;
                //string sql = "select * from question1 where subjectid=" + subjectid + " and chapterid = " + chapterid + "";
                //sqlConnection = new SqlConnection(connStr);
                //sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                //sqlDataAdapter.Fill(ds, "question");
                //this.count = ds.Tables[0].Rows.Count;
                button3.Visible = false;
                //单选题
                string ds_table_sql_s = "subjectid =" + subjectid + " and chapterid =" + chapterid + "and class = '单选题'";
                question12 = ds.Tables[0].Select(ds_table_sql_s);
                
                this.count = question12.Length;
                for (k = 0; k < this.count; k++)
                {
                    dt.ImportRow(question12[k]);
                }
                //多选题
                string ds_table_sql_m = "subjectid =" + subjectid + " and chapterid =" + chapterid + "and class = '多选题'";
                question13 = ds.Tables[0].Select(ds_table_sql_m);
                for (k = this.count; k < this.count + question13.Length ; k++)
                {
                    dt.ImportRow(question13[k-this.count]);
                }
                this.count = this.count + question13.Length;
                QuestionMethod();
            }
            
        }
        private void QuestionMethod()//查找并将question表里的信息加载到界面上
        {
            //txtQuestion.Text = ds.Tables[0].Rows[index]["q_content"].ToString();
            //txtA.Text = ds.Tables[0].Rows[index]["a_choice"].ToString();
            //txtB.Text = ds.Tables[0].Rows[index]["b_choice"].ToString();
            //txtC.Text = ds.Tables[0].Rows[index]["c_choice"].ToString();
            //txtD.Text = ds.Tables[0].Rows[index]["d_choice"].ToString();
            //label1.Text = ds.Tables[0].Rows[index]["class"].ToString();
            //answer.Text = ds.Tables["question"].Rows[index]["answer"].ToString();
            //txtQuestion.Text = question12[index]["q_content"].ToString();
            
            txtQuestion.Text = dt.Rows[index]["q_content"].ToString();
            txtA.Text = dt.Rows[index]["a_choice"].ToString();
            txtB.Text = dt.Rows[index]["b_choice"].ToString();
            txtC.Text = dt.Rows[index]["c_choice"].ToString();
            txtD.Text = dt.Rows[index]["d_choice"].ToString();
            txtE.Text = dt.Rows[index]["e_choice"].ToString();
            label1.Text = (index+1).ToString() + '.' + dt.Rows[index]["class"].ToString();
            //label7.Text = dt.Rows[index]["answer"].ToString();
            if(judge_end == 1 )
            { 
                label7.Text = dt.Rows[index]["answer"].ToString();
                answer.Text = give_answer[index];
            }
              
            if (index == 0)
            {
                btnLast.Enabled = false;
                btnNext.Enabled = true;
            }
            else if (index == this.count - 1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = true;
            }
            else
            {
                btnNext.Enabled = btnLast.Enabled = true;
            }
            
        }
        //上一题按钮
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (iftest == true & judge_end == 0)
            {
                if (cbA.Checked == false & cbB.Checked == false & cbC.Checked == false & cbD.Checked == false & cbE.Checked == false)
                { }
                else
                {
                    give_answer[index] = answer_judge();
                }
            }

            index--;
            QuestionMethod();

            if (judge_end == 0 & iftest == true)
            {
                answer.Text = give_answer[index];
            }
            if (judge_end == 0)
            {
                cbA.Checked = false;
                cbB.Checked = false;
                cbC.Checked = false;
                cbD.Checked = false;
                cbE.Checked = false;
            }
        }
        //下一题按钮
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (iftest == true & judge_end == 0)
            {
                if (cbA.Checked == false & cbB.Checked == false & cbC.Checked == false & cbD.Checked == false & cbE.Checked == false)
                { }
                else
                {
                    give_answer[index] = answer_judge();
                }
            }

            index++;
            QuestionMethod();

            if (judge_end == 0 & iftest == true)
            {
                answer.Text = give_answer[index];
            }
            if (judge_end == 0)
            {
                cbA.Checked = false;
                cbB.Checked = false;
                cbC.Checked = false;
                cbD.Checked = false;
                cbE.Checked = false;
            }
        }
        //返回按钮
        private void button1_Click(object sender, EventArgs e)
        {
            

            this.masterForm.Show();
            this.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
             label7.Text = dt.Rows[index]["answer"].ToString();
            //answer.Text = ds.Tables["question"].Rows[index]["answer"].ToString();
        }
        //测验随机抽题 15道单选 5道多选
        private void randomInsert()
        {
            //DataTable ds_tem = ds.Tables[0].Clone();

            int i = 0;
            string ds_table_sql_s = "chapterid=" + chapterid + "and class = '单选题'";
            question_s = ds.Tables[0].Select(ds_table_sql_s);
            
            //string sql = "select * from question1 where chapterid=" + chapterid + "and class = '单选题'";
            //sqlConnection = new SqlConnection(connStr);
            //sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
            //sqlDataAdapter.Fill(ds, "selectQuestion");
            //int C = ds.Tables["selectQuestion"].Rows.Count;
            int C = question_s.Length;
            int ran = 0;
            //questions = new string[C];
            Bquestion = new bool[C];


            Random r_s = new Random();
            //装选择的15道单选题和5道多选题的表格

            for (i = 0; i < 15; )
            {
                ran = r_s.Next(C);
                if (Bquestion[ran] == false)//判断Bquestion[ran]的值是否为false，若不是则表示此次产生的随机数ran对应的questionid
                //已交给过question数组
                {
                    //question[i] = ds.Tables["selectQuestion"].Rows[ran]["id"].ToString();
                    question[i] = Convert.ToInt32(question_s[ran]["id"]);
                    Bquestion[ran] = true;//将Bool型数组Bquestion所对应的ran下标的值设为true
                    i++;
                }
            }
            
            //选择5道多选题
            //string sql1 = "select * from question1 where chapterid=" + chapterid + "and class = '多选题'";
            //sqlConnection = new SqlConnection(connStr);
            //sqlDataAdapter = new SqlDataAdapter(sql1, sqlConnection);
            //sqlDataAdapter.Fill(ds, "cselectQuestion");
            //C = ds.Tables["cselectQuestion"].Rows.Count;
            string ds_table_sql_m = "chapterid =" + chapterid + "and class = '多选题'";
            question_m = ds.Tables[0].Select(ds_table_sql_m);
            C = question_m.Length;
            Bquestion = new bool[C];
            Random r_m = new Random();
            
            for (i = 15; i < 20; )
            {
                ran = r_m.Next(C);
                if (Bquestion[ran] == false)//判断Bquestion[ran]的值是否为false，若不是则表示此次产生的随机数ran对应的questionid
                //已交给过question数组~
                {
                    //question[i] = ds.Tables["cselectQuestion"].Rows[ran]["id"].ToString();
                    question[i] = Convert.ToInt32(question_m[ran]["id"]);
                    Bquestion[ran] = true;//将Bool型数组Bquestion所对应的ran下标的值设为true
                    i++;
                }
            }

            
            //循环将查找到的20道题目存入ds数据集中，待用！
            for ( i = 0; i < 20; i++)
            {
                //string sql2 = "select * from question1 where id=" + Convert.ToInt32(question[i]) + "";
                //sqlConnection = new SqlConnection(connStr);
                //sqlDataAdapter = new SqlDataAdapter(sql2, sqlConnection);
                //sqlDataAdapter.Fill(ds, "question");
                string str = "id = " + "'" + question[i].ToString() + "'";
                question_tem = ds.Tables[0].Select(str);
                dt.ImportRow(question_tem[0]);
                str = "";
                Tanswer[i] = question_tem[0]["answer"].ToString(); //答案传递
                //ds_tem.Rows.Add(question_tem);
                //Tanswer[i] = question_tem[0]["answer"].ToString();
                //question12[i] = question_tem[0];

            }
           
            //for (int j = 0; j < 20; j++)
            //{
                //Tanswer[j] = ds.Tables["question"].Rows[j]["answer"].ToString();//传递正确答案，已备后面判分对照用
                //Tanswer[j] = question12[j]["answer"].ToString();
            //}
            btnLast.Enabled = false;
        }
        //每次记录选择答案
        private string answer_judge()
        {
            string a = "";
            if (cbA.Checked == true)
                a = a + "A";
            if (cbB.Checked == true)
                a = a + "B";
            if (cbC.Checked == true)
                a = a + "C";
            if (cbD.Checked == true)
                a = a + "D";
            if (cbE.Checked == true)
                a = a + "E";
            return a;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        //关闭按钮重写
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.masterForm.Show();
        }
        //完成按钮
        private void button3_Click(object sender, EventArgs e)
        {
            if (cbA.Checked == false & cbB.Checked == false & cbC.Checked == false & cbD.Checked == false & cbE.Checked == false)
                { }
            else
                {
                    give_answer[index] = answer_judge();
                }
            

            if (chapterid == 12)
            {
                give_answer[index] = answer_judge();//答题所得结果
                int score = 0;
                for (int i = 0; i < 120; i++)
                {
                    if (give_answer[i] == Tanswer[i])
                    {
                        score = score + 1;
                    }

                }
                label7.Text = score.ToString();
                DialogResult dr = MessageBox.Show(score.ToString(), "测验分数");
                judge_end = 1;    //代表为测验
                button3.Enabled = false;
                label7.Text = dt.Rows[index]["answer"].ToString();
                answer.Text = give_answer[index];
            }
            else
            {
                //give_answer[index] = answer_judge();//答题所得结果
                int score = 0;
                for (int i = 0; i < 20; i++)
                {
                    if (give_answer[i] == Tanswer[i])
                    {
                        score = score + 5;
                    }

                }
                label7.Text = score.ToString();
                DialogResult dr = MessageBox.Show(score.ToString(), "测验分数");
                judge_end = 1;    //代表测验
                button3.Enabled = false;
                label7.Text = dt.Rows[index]["answer"].ToString();
                answer.Text = give_answer[index];
            }
        }

        private void txtB_TextChanged(object sender, EventArgs e)
        {

        }
        
        
    }
}
