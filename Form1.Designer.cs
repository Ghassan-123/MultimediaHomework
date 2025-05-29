using System;
using System.Drawing;
using System.Windows.Forms;

namespace HomeworkTest
{
    public partial class Form1 : Form
    {
        private Label titleLabel;
        private Button button1;
        private Button button2;
        private Button exitButton;

        private void InitializeComponent()
        {
            titleLabel = new Label();
            button1 = new Button();
            button2 = new Button();
            exitButton = new Button();
            SuspendLayout();
            //
            //label 
            //
            titleLabel.Text = "Difference Game";
            titleLabel.Font = new Font("Calibri", 30F, FontStyle.Bold);
            titleLabel.ForeColor = Color.Black;
            titleLabel.AutoSize = true; // Automatically size to fit text
            titleLabel.Location = new Point(100, 100); // Position on form
            // 
            // button1
            // 
            button1.BackgroundImageLayout = ImageLayout.Center;
            button1.Font = new Font("Calibri", 10F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(300, 250);
            button1.Name = "button1";
            button1.Size = new Size(200, 100);
            button1.TabIndex = 0;
            button1.Text = "Timed";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Calibri", 10F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(300, 400);
            button2.Name = "button2";
            button2.Size = new Size(200, 100);
            button2.TabIndex = 1;
            button2.Text = "Survival";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // exitButton
            // 
            exitButton.BackColor = Color.Transparent;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.Font = new Font("Calibri", 10F, FontStyle.Bold, GraphicsUnit.Point);
            exitButton.ForeColor = Color.Black;
            exitButton.Location = new Point(750, 0);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(50, 60);
            exitButton.TabIndex = 2;
            exitButton.TabStop = false;
            exitButton.Text = "✕";
            exitButton.UseVisualStyleBackColor = false;
            exitButton.Click += exitButton_Click;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 600);
            Controls.Add(titleLabel);
            Controls.Add(exitButton);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Homework Test";
            Load += Form1_Load_1;
            ResumeLayout(false);
        }
    }
}