using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeworkTest
{
    public partial class preTimed : Form
    {
        private Form Timed;
        public preTimed()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showTimed(60,1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            showTimed(45,2);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            showTimed(30,3);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
            // Hover effects
            exitButton.MouseEnter += (s, e) =>
            {
                exitButton.ForeColor = Color.White;
                exitButton.BackColor = Color.Red;
            };
            exitButton.MouseLeave += (s, e) =>
            {
                exitButton.ForeColor = Color.Gray;
                exitButton.BackColor = Color.Transparent;
            };
        }

        private void back_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void showTimed(int time,int d)
        {
            Timed = new Timed(time,d);
            Timed.Show();
            this.Hide();
        }

        private void preTimed_Load(object sender, EventArgs e)
        {

        }
    }
}
