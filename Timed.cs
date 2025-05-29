using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeworkTest
{
    public partial class Timed : Form
    {
        private int difficulty;
        private Bitmap Image1;
        private Bitmap Image2;
        private Bitmap diff1;
        private Bitmap diff2;
        PictureBox Box1, Box2, diffBox;
        private SoundPlayer audio1, audio2;
        private List<Rectangle> diffRegions;
        private List<Rectangle> foundRegions;
        private const int TOLERANCE = 70;
        private const int diffCount = 0;
        private Timer countdownTimer;
        private int countdownValue;
        private int differences;

        public Timed(int x,int d)
        {
            difficulty = d;
            LoadImages();
            LoadAudios();
            InitializeComponent();
            countdownValue = x;
            Box1 = pictureBox1;
            Box2 = pictureBox2;
            pictureBox1.Image = Image1;
            pictureBox2.Image = Image2;
            DetectDifferences();
            richTextBox1.Text = countdownValue.ToString(); // Show initial value
            richTextBox2.Text = differences.ToString();
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // 1 second
            countdownTimer.Tick += timer1_Tick;
            countdownTimer.Start();

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

        private void Timed_Load(object sender, EventArgs e)
        {
            
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
            // Stop the timer
            countdownTimer.Stop();

            // Create a label with the winning message
            Label winLabel = new Label();
            winLabel.Text = "YOU LOSE!";
            winLabel.Font = new Font("Calibri", 48, FontStyle.Bold);
            winLabel.ForeColor = Color.Red;
            winLabel.AutoSize = false; // Disable AutoSize to manually control dimensions
            winLabel.TextAlign = ContentAlignment.MiddleCenter; // Center-align text inside the label

            // Calculate the required size for the text
            using (Graphics g = this.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(winLabel.Text, winLabel.Font);
                winLabel.Width = (int)textSize.Width + 20; // Add padding
                winLabel.Height = (int)textSize.Height + 20;
            }

            // Position the label in the exact center of the form
            winLabel.Location = new Point(
                (this.ClientSize.Width - winLabel.Width) / 2,
                (this.ClientSize.Height - winLabel.Height) / 2
            );

            // Ensure the label stays centered even if the form is resized
            winLabel.Anchor = AnchorStyles.None;

            // Add the label to the form and bring it to the front
            this.Controls.Add(winLabel);
            winLabel.BringToFront();

            // Optional: Disable further clicks on the picture boxes
            Box1.Enabled = false;
            Box2.Enabled = false;

            // Optional: Close the form after a delay (e.g., 3 seconds)
            Timer closeTimer = new Timer();
            closeTimer.Interval = 3000;
            closeTimer.Tick += (s, e) =>
            {
                closeTimer.Stop();
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            };
            closeTimer.Start();

        }
        private void timeWon()
        {
            // Stop the timer
            countdownTimer.Stop();

            // Create a label with the winning message
            Label winLabel = new Label();
            winLabel.Text = "YOU WON!";
            winLabel.Font = new Font("Calibri", 48, FontStyle.Bold);
            winLabel.ForeColor = Color.Green;
            winLabel.AutoSize = false; // Disable AutoSize to manually control dimensions
            winLabel.TextAlign = ContentAlignment.MiddleCenter; // Center-align text inside the label

            // Calculate the required size for the text
            using (Graphics g = this.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(winLabel.Text, winLabel.Font);
                winLabel.Width = (int)textSize.Width + 20; // Add padding
                winLabel.Height = (int)textSize.Height + 20;
            }

            // Position the label in the exact center of the form
            winLabel.Location = new Point(
                (this.ClientSize.Width - winLabel.Width) / 2,
                (this.ClientSize.Height - winLabel.Height) / 2
            );

            // Ensure the label stays centered even if the form is resized
            winLabel.Anchor = AnchorStyles.None;

            // Add the label to the form and bring it to the front
            this.Controls.Add(winLabel);
            winLabel.BringToFront();

            // Optional: Disable further clicks on the picture boxes
            Box1.Enabled = false;
            Box2.Enabled = false;

            // Optional: Close the form after a delay (e.g., 3 seconds)
            Timer closeTimer = new Timer();
            closeTimer.Interval = 3000;
            closeTimer.Tick += (s, e) =>
            {
                closeTimer.Stop();
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            };
            closeTimer.Start();
        }
        private void LoadImages()
        {
            string Path1 = "";
            string Path2 = "";
            if (difficulty == 1)
            {
                Path1 = Path.Combine(Application.StartupPath, "assets", "sample1.jpg");
                Path2 = Path.Combine(Application.StartupPath, "assets", "sample2.jpg");
            }
            else if (difficulty == 2)
            {
                Path1 = Path.Combine(Application.StartupPath, "assets", "sample3.jpg");
                Path2 = Path.Combine(Application.StartupPath, "assets", "sample4.jpg");
            }
            else
            {
                Path1 = Path.Combine(Application.StartupPath, "assets", "sample5.jpg");
                Path2 = Path.Combine(Application.StartupPath, "assets", "sample6.jpg");
            }

            if (!File.Exists(Path1) || !File.Exists(Path2))
            {
                MessageBox.Show("One or both image files not found!");
                return;
            }

            Image1 = new Bitmap(Path1);
            Image2 = new Bitmap(Path2);

            if (Image1.Width != Image2.Width || Image1.Height != Image2.Height)
            {
                Console.WriteLine("Images are not the same size.");
                return;
            }

            diff1 = new Bitmap(Image1.Width, Image1.Height);
            diff2 = new Bitmap(Image2.Width, Image2.Height);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            if (me == null) return;
            PictureBox clickedBox = sender as PictureBox;

            if (clickedBox.Image == null) return;

            float scaleX = (float)Image1.Width / clickedBox.Width;
            float scaleY = (float)Image1.Height / clickedBox.Height;

            int imgX = (int)(me.X * scaleX);
            int imgY = (int)(me.Y * scaleY);

            bool found = false;

            foreach (var region in diffRegions)
            {
                if (region.Contains(imgX, imgY) && !foundRegions.Contains(region))
                {
                    found = true;
                    differences -= 1;
                    richTextBox2.Text = differences.ToString();
                    foundRegions.Add(region);
                    ShowClickedDifference(Box1);
                    ShowClickedDifference(Box2);
                    if (differences <= 0) {
                        timeWon();
                    }
                    break;
                }
            }
            PlaySound(found);
            _ = !found;
        }

        private void DetectDifferences()
        {
            const int BLOCK_SIZE = 10; // Size of each comparison block
            diffRegions = new List<Rectangle>();
            
            foundRegions = new List<Rectangle>();

            Rectangle rect = new Rectangle(0, 0, Image1.Width, Image1.Height);
            BitmapData data1 = Image1.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData data2 = Image2.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dataDiff1 = diff1.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            BitmapData dataDiff2 = diff2.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int stride = data1.Stride;

            try
            {
                unsafe
                {
                    byte* ptr1 = (byte*)data1.Scan0;
                    byte* ptr2 = (byte*)data2.Scan0;
                    byte* ptrDiff = (byte*)dataDiff1.Scan0;
                    byte* ptrDiff2 = (byte*)dataDiff2.Scan0;

                    for (int y = 0; y < Image1.Height; y += BLOCK_SIZE)
                    {
                        for (int x = 0; x < Image1.Width; x += BLOCK_SIZE)
                        {
                            int totalDiff = 0;
                            int pixelsCompared = 0;

                            for (int dy = 0; dy < BLOCK_SIZE && (y + dy) < Image1.Height; dy++)
                            {
                                for (int dx = 0; dx < BLOCK_SIZE && (x + dx) < Image1.Width; dx++)
                                {
                                    int px = x + dx;
                                    int py = y + dy;
                                    int index = py * stride + px * 3;

                                    byte b1 = ptr1[index];
                                    byte g1 = ptr1[index + 1];
                                    byte r1 = ptr1[index + 2];

                                    byte b2 = ptr2[index];
                                    byte g2 = ptr2[index + 1];
                                    byte r2 = ptr2[index + 2];

                                    int diff = Math.Abs(r1 - r2) + Math.Abs(g1 - g2) + Math.Abs(b1 - b2);
                                    totalDiff += diff;
                                    pixelsCompared++;
                                }
                            }

                            int avgDiff = totalDiff / pixelsCompared;

                            for (int dy = 0; dy < BLOCK_SIZE && (y + dy) < Image1.Height; dy++)
                            {
                                for (int dx = 0; dx < BLOCK_SIZE && (x + dx) < Image1.Width; dx++)
                                {
                                    int px = x + dx;
                                    int py = y + dy;
                                    int index = py * stride + px * 3;

                                    if (avgDiff > TOLERANCE)
                                    {
                                        ptrDiff[index] = 0;
                                        ptrDiff[index + 1] = 0;
                                        ptrDiff[index + 2] = 255;
                                    }
                                    else
                                    {
                                        ptrDiff[index] = ptr2[index];
                                        ptrDiff[index + 1] = ptr2[index + 1];
                                        ptrDiff[index + 2] = ptr2[index + 2];
                                    }
                                }
                            }

                            if (avgDiff > TOLERANCE)
                            {
                                diffRegions.Add(new Rectangle(x, y, BLOCK_SIZE, BLOCK_SIZE));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetectDifferences error: " + ex.Message);
            }

            Image1.UnlockBits(data1);
            Image2.UnlockBits(data2);
            diff1.UnlockBits(dataDiff1);
            diff2.UnlockBits(dataDiff2);

            
            MergeDiffRegions();
        }

        private void MergeDiffRegions()
        {
            List<Rectangle> merged = new List<Rectangle>();
            bool[] visited = new bool[diffRegions.Count];

            for (int i = 0; i < diffRegions.Count; i++)
            {
                if (visited[i]) continue;

                Rectangle current = diffRegions[i];
                visited[i] = true;

                bool changed;

                do
                {
                    changed = false;

                    for (int j = 0; j < diffRegions.Count; j++)
                    {
                        if (visited[j]) continue;

                        if (AreAdjacentOrOverlapping(current, diffRegions[j]))
                        {
                            current = Rectangle.Union(current, diffRegions[j]);
                            visited[j] = true;
                            changed = true;
                        }
                    }
                } while (changed);

                merged.Add(current);
            }

            diffRegions = merged;
            differences = diffRegions.Count;
        }

        private bool AreAdjacentOrOverlapping(Rectangle a, Rectangle b)
        {
            // Expand A slightly to check for touching sides/corners
            Rectangle expanded = new Rectangle(
                a.X - 1, a.Y - 1,
                a.Width + 2, a.Height + 2
            );
            return expanded.IntersectsWith(b);
        }
        private void LoadAudios()
        {
            string Path1 = Path.Combine(Application.StartupPath, "assets", "found.wav");
            string Path2 = Path.Combine(Application.StartupPath, "assets", "notfound.wav");

            if (!File.Exists(Path1) || !File.Exists(Path2))
            {
                MessageBox.Show("One or both audios files not found!");
                return;
            }

            audio1 = new SoundPlayer(Path1);
            audio2 = new SoundPlayer(Path2);
        }

        

        private void ShowClickedDifference(PictureBox box)
        {
            Bitmap tempImage = new Bitmap(box.Image);

            using (Graphics g = Graphics.FromImage(tempImage))
            using (Pen pen = new Pen(Color.Green, 3))
            {
                foreach (var region in foundRegions)
                {
                    g.DrawRectangle(pen, region);
                }
            }

            box.Image = tempImage;
        }

        private void PlaySound(bool found)
        {
            if (found)
                audio1?.Play();   // plays asynchronously
            else
                audio2?.Play();   // plays asynchronously
        }
    }

}
