using System.Drawing;
using System.Windows.Forms;

namespace HomeworkTest
{
    public partial class preTimed : Form
    {
        private Label titleLabel;
        private System.ComponentModel.IContainer components = null;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button exitButton;
        private Button backButton;

        private void InitializeComponent()
        {
            titleLabel = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            exitButton = new Button();
            backButton = new Button();
            SuspendLayout();
            //
            //label
            //
            titleLabel.Text = "Select Difficulty Level";
            titleLabel.Font = new Font("Calibri", 30F, FontStyle.Bold);
            titleLabel.ForeColor = Color.Black;
            titleLabel.AutoSize = true; // Automatically size to fit text
            titleLabel.Location = new Point(150, 100); // Position on form
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button1.Location = new System.Drawing.Point(400, 300);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(200, 100);
            button1.TabIndex = 0;
            button1.Text = "Easy";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.White;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button2.Location = new System.Drawing.Point(400, 500);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(200, 100);
            button2.TabIndex = 1;
            button2.Text = "Medium";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.Color.White;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new System.Drawing.Font("Calibri", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button3.Location = new System.Drawing.Point(400, 700);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(200, 100);
            button3.TabIndex = 2;
            button3.Text = "Hard";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // exitButton
            // 
            exitButton.BackColor = System.Drawing.Color.Transparent;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            exitButton.ForeColor = System.Drawing.Color.Black;
            exitButton.Location = new System.Drawing.Point(950, 0);
            exitButton.Name = "exitButton";
            exitButton.Size = new System.Drawing.Size(50, 60);
            exitButton.TabIndex = 3;
            exitButton.TabStop = false;
            exitButton.Text = "✕";
            exitButton.UseVisualStyleBackColor = false;
            exitButton.Click += exitButton_Click;
            //
            //backButton
            //
            backButton.BackColor = System.Drawing.Color.Transparent;
            backButton.FlatAppearance.BorderSize = 0;
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            backButton.ForeColor = System.Drawing.Color.Black;
            backButton.Location = new System.Drawing.Point(0, 0);
            backButton.Name = "backButton";
            backButton.Size = new System.Drawing.Size(50, 60);
            backButton.TabIndex = 3;
            backButton.TabStop = false;
            backButton.Text = "<--";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += back_Click;
            // 
            // preTimed
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1000, 1000);
            Controls.Add(titleLabel);
            Controls.Add(backButton);
            Controls.Add(exitButton);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "preTimed";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Difficulty Selection";
            Load += preTimed_Load;
            ResumeLayout(false);
        }
    }
}