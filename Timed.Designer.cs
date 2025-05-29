using System.Drawing;
using System.Windows.Forms;

namespace HomeworkTest
{
    partial class Timed : Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Timer timer1;
        private Button exitButton;
        private Label titleLabel1;
        private Label titleLabel2;
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            richTextBox2 = new System.Windows.Forms.RichTextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            exitButton = new Button();
            titleLabel1= new Label();
            titleLabel2= new Label();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(10, 100);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(Image1.Width, Image1.Height);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new System.Drawing.Point(Image1.Width + 20, 100);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(Image2.Width, Image2.Height);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox_Click;
            //
            //label 1
            //
            titleLabel1.Text = "Timer";
            titleLabel1.Font = new Font("Calibri", 10F, FontStyle.Bold);
            titleLabel1.ForeColor = Color.Black;
            titleLabel1.AutoSize = true; // Automatically size to fit text
            titleLabel1.Location = new Point(25, Image1.Height + 150); // Position on form
            //
            // richTextBox1
            // 
            richTextBox1.Location = new System.Drawing.Point(150, Image2.Height + 150);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(100, 25);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            //
            //label 2
            //
            titleLabel2.Text = "No of differences";
            titleLabel2.Font = new Font("Calibri", 10F, FontStyle.Bold);
            titleLabel2.ForeColor = Color.Black;
            titleLabel2.AutoSize = true; // Automatically size to fit text
            titleLabel2.Location = new Point(Image1.Width + 50, Image2.Height + 150); // Position on form
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new System.Drawing.Point(Image1.Width + 175, Image2.Height + 150);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new System.Drawing.Size(150, 25);
            richTextBox2.TabIndex = 3;
            richTextBox2.Text = "";
            // 
            // exitButton
            // 
            exitButton.BackColor = Color.Transparent;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.Font = new Font("Calibri", 10F, FontStyle.Bold, GraphicsUnit.Point);
            exitButton.ForeColor = Color.Black;
            exitButton.Location = new Point(Image1.Width + Image2.Width, 0);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(50, 60);
            exitButton.TabIndex = 2;
            exitButton.TabStop = false;
            exitButton.Text = "✕";
            exitButton.UseVisualStyleBackColor = false;
            exitButton.TextAlign = ContentAlignment.MiddleCenter;
            exitButton.Click += exitButton_Click;
            // 
            // Timed
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(Image1.Width + Image2.Width + 50, Image1.Height + 300);
            Controls.Add(titleLabel2);
            Controls.Add(richTextBox2);
            Controls.Add(titleLabel1);
            Controls.Add(richTextBox1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(exitButton);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "Timed";
            Text = "Timed";
            Load += Timed_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }
    }
}