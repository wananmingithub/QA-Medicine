using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEDICINE_TEST
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "dldx13" & textBox2.Text == "1121")
            {
                Form1 F_1 = new Form1();
                F_1.MasterForm = this;
                this.Visible = false;
                F_1.ShowDialog();
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }
        }
    }
}
