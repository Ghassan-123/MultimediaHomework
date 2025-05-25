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
    public partial class Timed : Form
    {
        private Timer countdownTimer;
        private int countdownValue;

        public Timed(int x)
        {
            InitializeComponent();
            countdownValue = x;
            
        }

        private void Timed_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = countdownValue.ToString(); // Show initial value

            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // 1 second
            countdownTimer.Tick += timer1_Tick;
            countdownTimer.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            countdownValue--;

            if (countdownValue > 0)
            {
                richTextBox1.Text = countdownValue.ToString();
            }
            else
            {
                countdownTimer.Stop();
                richTextBox1.Text = "Time's up!";
                timeEnded();
            }
        }
        private void timeEnded()
        {
            //logic for ending
            this.Close();
        }
    }

}
