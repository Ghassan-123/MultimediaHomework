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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            preTimed preTimed = new preTimed();
            preTimed.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PreSurvival preSurvival = new PreSurvival();
            preSurvival.Show();
            this.Hide();
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

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
