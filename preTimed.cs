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
            showTimed(60); 
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showTimed(45);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showTimed(30);
        }

        private void showTimed(int time)
        {
            Timed = new Timed(time);
            Timed.Show();
            this.Close();
        }
    }
}
