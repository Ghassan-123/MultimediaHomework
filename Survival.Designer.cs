using System.Drawing;
using System.Windows.Forms;

namespace HomeworkTest
{
    public partial class Survival : Form
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
            titleLabel1 = new Label();
            titleLabel2 = new Label();
            SuspendLayout();
            // 
            // pictureBox1
            // 

            //
            pictureBox1.Location = new System.Drawing.Point(75, 50);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(Image1.Width, Image1.Height);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new System.Drawing.Point(800, 50);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(Image1.Width, Image1.Height);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox_Click;
            //
            //label 1
            //
            titleLabel1.Text = "attempts";
            titleLabel1.Font = new Font("Calibri", 10F, FontStyle.Bold);
            titleLabel1.ForeColor = Color.Black;
            titleLabel1.AutoSize = true; // Automatically size to fit text
            titleLabel1.Location = new Point(75, 700); // Position on form
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new System.Drawing.Point(200, 700);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(200, 50);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            //
            //label 2
            //
            titleLabel2.Text = "No of differences";
            titleLabel2.Font = new Font("Calibri", 10F, FontStyle.Bold);
            titleLabel2.ForeColor = Color.Black;
            titleLabel2.AutoSize = true; // Automatically size to fit text
            titleLabel2.Location = new Point(825, 700); // Position on form
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new System.Drawing.Point(1050, 700);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new System.Drawing.Size(200, 50);
            richTextBox2.TabIndex = 6;
            richTextBox2.Text = "";
            // 
            // exitButton
            // 
            exitButton.BackColor = Color.Transparent;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.Font = new Font("Calibri", 10F, FontStyle.Bold, GraphicsUnit.Point);
            exitButton.ForeColor = Color.Black;
            exitButton.Location = new Point(1350, 0);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(100, 50);
            exitButton.TabIndex = 2;
            exitButton.TabStop = false;
            exitButton.Text = "✕";
            exitButton.UseVisualStyleBackColor = false;
            exitButton.Click += exitButton_Click;
            // 
            // Survival
            // 
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1500, 1500);
            Controls.Add(exitButton);
            Controls.Add(titleLabel2);
            Controls.Add(richTextBox2);
            Controls.Add(titleLabel1);
            Controls.Add(richTextBox1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5, 5, 5, 5);
            Name = "Survival";
            Text = "Survival";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
    }
}