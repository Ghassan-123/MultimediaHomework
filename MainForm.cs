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
        PictureBox Box1, Box2;
        private List<Rectangle> diffRegions;
        private List<Rectangle> foundRegions;
        private const int TOLERANCE = 60;

        public MainForm()
        {
            this.Width = 640;
            this.Height = 480;
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

            Box1.MouseClick += (s, e) => PictureBox_MouseClick(s, e);
            Box2.MouseClick += (s, e) => PictureBox_MouseClick(s, e);

            this.Controls.Add(Box1);
            this.Controls.Add(Box2);

            LoadImages();
            Box1.Image = Image1;
            Box2.Image = Image2;


            DetectDifferences();
        }

        private void LoadImages()
        {
            string Path1 = Path.Combine(Application.StartupPath, "assets", "original.png");
            string Path2 = Path.Combine(Application.StartupPath, "assets", "edited.png");

            if (!File.Exists(Path1) || !File.Exists(Path2))
            {
                MessageBox.Show("One or both image files not found!");
                return;
            }

            Image1 = new Bitmap(Path1);
            Image2 = new Bitmap(Path2);
            diff1 = new Bitmap(Image1.Width, Image1.Height);
            diff1 = new Bitmap(Image2.Width, Image2.Height);
        }

        private void DetectDifferences()
        {
            diffRegions = new List<Rectangle>();
            foundRegions = new List<Rectangle>();

            Rectangle rect = new Rectangle(0, 0, Image1.Width, Image1.Height);
            BitmapData data1 = Image1.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData data2 = Image2.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dataDiff = diff1.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int stride = data1.Stride;

            try
            {
                unsafe
                {
                    byte* ptr1 = (byte*)data1.Scan0;
                    byte* ptr2 = (byte*)data2.Scan0;
                    byte* ptrDiff = (byte*)dataDiff.Scan0;

                    for (int y = 0; y < Image1.Height; y++)
                    {
                        for (int x = 0; x < Image1.Width; x++)
                        {
                            int index = y * stride + x * 3;

                            byte b1 = ptr1[index];
                            byte g1 = ptr1[index + 1];
                            byte r1 = ptr1[index + 2];

                            byte b2 = ptr2[index];
                            byte g2 = ptr2[index + 1];
                            byte r2 = ptr2[index + 2];

                            int diff = Math.Abs(r1 - r2) + Math.Abs(g1 - g2) + Math.Abs(b1 - b2);

                            if (diff > TOLERANCE)
                            {
                                ptrDiff[index] = 0;
                                ptrDiff[index + 1] = 0;
                                ptrDiff[index + 2] = 255;
                                diffRegions.Add(new Rectangle(x - 10, y - 10, 20, 20));
                            }
                            else
                            {
                                ptrDiff[index] = b2;
                                ptrDiff[index + 1] = g2;
                                ptrDiff[index + 2] = r2;
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
            diff1.UnlockBits(dataDiff);

            MergeDiffRegions();
        }

        private void MergeDiffRegions()
        {
            List<Rectangle> merged = new List<Rectangle>();

            foreach (var r in diffRegions)
            {
                bool added = false;
                for (int i = 0; i < merged.Count; i++)
                {
                    if (merged[i].IntersectsWith(r))
                    {
                        merged[i] = Rectangle.Union(merged[i], r);
                        added = true;
                        break;
                    }
                }

                if (!added)
                    merged.Add(r);
            }

            diffRegions = merged;
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
                if (region.Contains(imgX, imgY) && !foundRegions.Contains(region))
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
