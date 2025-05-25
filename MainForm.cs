using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace HomeworkTest
{
    public class MainForm : Form
    {
        private Bitmap Image1;
        private Bitmap Image2;
        private Bitmap diff1;
        private Bitmap diff2;
        PictureBox Box1, Box2, diffBox1, diffBox2;
        private List<Rectangle> diffRegions;
        private List<Rectangle> foundRegions;
        private const int TOLERANCE = 70;

        public MainForm()
        {
            this.Width = 1200;
            this.Height = 600;
            this.Text = "Find the Difference Game";

            Box1 = new PictureBox
            {
                Width = 300,
                Height = 400,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };

            Box2 = new PictureBox
            {
                Width = 300,
                Height = 400,
                Location = new Point(315, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            diffBox1 = new PictureBox
            {
                Width = 300,
                Height = 400,
                Location = new Point(600, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            diffBox2 = new PictureBox
            {
                Width = 300,
                Height = 400,
                Location = new Point(900, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };

            Box1.MouseClick += (s, e) => PictureBox_MouseClick(s, e);
            Box2.MouseClick += (s, e) => PictureBox_MouseClick(s, e);

            this.Controls.Add(Box1);
            this.Controls.Add(Box2);
            this.Controls.Add(diffBox1);

            LoadImages();
            Box1.Image = Image1;
            Box1.Width = Image1.Width;
            Box1.Height = Image1.Height;
            Box2.Image = Image2;
            Box2.Width = Image2.Width;
            Box2.Height = Image2.Height;
            diffBox1.Image = diff1;
            diffBox1.Width = diff1.Width;
            diffBox1.Height = diff1.Height;


            DetectDifferences();
        }

        private void LoadImages()
        {
            string Path1 = Path.Combine(Application.StartupPath, "assets", "sample5.jpg");
            string Path2 = Path.Combine(Application.StartupPath, "assets", "sample6.jpg");

            if (!File.Exists(Path1) || !File.Exists(Path2))
            {
                MessageBox.Show("One or both image files not found!");
                return;
            }

            Image1 = new Bitmap(Path1);
            Image2 = new Bitmap(Path2);
            diff1 = new Bitmap(Image1.Width, Image1.Height);
            diff2 = new Bitmap(Image1.Width, Image1.Height);
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


        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox clickedBox = sender as PictureBox;

            if (clickedBox.Image == null) return;

            float scaleX = (float)Image1.Width / clickedBox.Width;
            float scaleY = (float)Image1.Height / clickedBox.Height;

            int imgX = (int)(e.X * scaleX);
            int imgY = (int)(e.Y * scaleY);

            foreach (var region in diffRegions)
            {
                Rectangle expandedRegion = Rectangle.Inflate(region, 5, 5);
                if (expandedRegion.Contains(imgX, imgY) && !foundRegions.Contains(region))
                {
                    foundRegions.Add(region);
                    ShowClickedDifference(Box1);
                    ShowClickedDifference(Box2);
                    break;

                    //if (clickedBox == Box1)
                    //{
                    //    MessageBox.Show("Box1");
                    //}
                    //else if (clickedBox == Box2)
                    //{
                    //    MessageBox.Show("Box2");
                    //}
                }
            }
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


        //private void DrawBoundingBox(PictureBox box, Rectangle region)
        //{
        //    using (Graphics g = Graphics.FromImage(diff1))
        //    {
        //        using (Pen pen = new Pen(Color.Green, 3))
        //        {
        //            g.DrawRectangle(pen, region);
        //        }
        //    }

        //    box.Image = null;
        //    box.Image = diff1;
        //}


        //private void RedrawFoundDifferences(PictureBox box)
        //{
        //    Bitmap tempImage = new Bitmap(Image2);

        //    using (Graphics g = Graphics.FromImage(tempImage))
        //    using (Pen pen = new Pen(Color.Green, 3))
        //    {
        //        foreach (var region in foundRegions)
        //        {
        //            g.DrawRectangle(pen, region);
        //        }
        //    }

        //    box.Image = tempImage;
        //}
    }
}
