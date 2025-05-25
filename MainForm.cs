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
        private Bitmap originalImage;
        private Bitmap modifiedImage;
        private Bitmap diffImage;
        private List<Rectangle> diffRegions;
        private List<Rectangle> foundRegions;
        private const int TOLERANCE = 60;
        private PictureBox pictureBox;

        public MainForm()
        {
            this.Width = 1200;
            this.Height = 700;
            this.Text = "Find the Difference Game";

            PictureBox originalBox = new PictureBox
            {
                Width = 350,
                Height = 500,
                Location = new Point(20, 50),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox modifiedBox = new PictureBox
            {
                Width = 350,
                Height = 500,
                Location = new Point(390, 50),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };

            originalBox.MouseClick += (s, e) => PictureBox_MouseClick(s, e, originalBox);
            modifiedBox.MouseClick += (s, e) => PictureBox_MouseClick(s, e, modifiedBox);

            this.Controls.Add(originalBox);
            this.Controls.Add(modifiedBox);

            LoadImages();
            DetectDifferences();
            originalBox.Image = originalImage;
            modifiedBox.Image = modifiedImage;
        }

        private void LoadImages()
        {
            string originalPath = Path.Combine(Application.StartupPath, "assets", "original.png");
            string editedPath = Path.Combine(Application.StartupPath, "assets", "edited.png");

            if (!File.Exists(originalPath) || !File.Exists(editedPath))
            {
                MessageBox.Show(originalPath);

                //MessageBox.Show("One or both image files not found!");
                return;
            }

            originalImage = new Bitmap(originalPath);
            modifiedImage = new Bitmap(editedPath);
            diffImage = new Bitmap(modifiedImage.Width, modifiedImage.Height);
        }

        private void DetectDifferences()
        {
            diffRegions = new List<Rectangle>();
            foundRegions = new List<Rectangle>();

            Rectangle rect = new Rectangle(0, 0, originalImage.Width, originalImage.Height);
            BitmapData data1 = originalImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData data2 = modifiedImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dataDiff = diffImage.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int stride = data1.Stride;

            try
            {
                unsafe
                {
                    byte* ptr1 = (byte*)data1.Scan0;
                    byte* ptr2 = (byte*)data2.Scan0;
                    byte* ptrDiff = (byte*)dataDiff.Scan0;

                    for (int y = 0; y < originalImage.Height; y++)
                    {
                        for (int x = 0; x < originalImage.Width; x++)
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

            originalImage.UnlockBits(data1);
            modifiedImage.UnlockBits(data2);
            diffImage.UnlockBits(dataDiff);

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

        private void PictureBox_MouseClick(object sender, MouseEventArgs e, PictureBox box)
        {
            if (box.Image == null) return;

            float scaleX = (float)originalImage.Width / box.Width;
            float scaleY = (float)originalImage.Height / box.Height;

            int imgX = (int)(e.X * scaleX);
            int imgY = (int)(e.Y * scaleY);

            foreach (var region in diffRegions)
            {
                if (region.Contains(imgX, imgY) && !foundRegions.Contains(region))
                {
                    foundRegions.Add(region);
                    DrawBoundingBox(box, region);
                    break;
                }
            }
        }

        private void DrawBoundingBox(PictureBox box, Rectangle region)
        {
            using (Graphics g = Graphics.FromImage(diffImage))
            {
                using (Pen pen = new Pen(Color.Green, 3))
                {
                    g.DrawRectangle(pen, region);
                }
            }

            box.Image = null;
            box.Image = diffImage;
        }
    }
}
