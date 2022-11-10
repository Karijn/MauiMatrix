using MatrixLib;
using Microsoft.Maui.Graphics;

namespace MatrixMaker
{
    public partial class Form1 : Form
    {
        string path = "C:\\Users\\Karij\\Documents\\MicroPython\\MauiMatrix";


        Bitmap bmp;
        Graphics graphics;
        Animation? animation;
        string _FileName = "";
        int currentImage = 0;
        bool isMouseDown = false;
        int _imageIndex = -1;

        public Form1()
        {
            InitializeComponent();
        }

        bool ImageChanged { get; set; } = false;

        public int ImageIndex
        {
            get { return _imageIndex; }
            set
            {
                if (_imageIndex != -1 && ImageChanged)
                {
                    UpdateImage();
                }
                _imageIndex = value;

                ZoomImage();
            }
        }

        private void P_Click(object? sender, EventArgs e)
        {
            if (animation == null) return;
            PictureBox pbSource = (PictureBox)sender;
            ImageIndex = ((int)pbSource.Tag);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (animation == null) return;
            pictureBox1.Image = animation.Bitmap(currentImage);
            currentImage = (currentImage + 1) % animation.Count;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            SetPixel(e);
            isMouseDown = true;
            ImageChanged = true;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = $"Mouse Pos({e.X / (int)udZoom.Value}, {e.Y / (int)udZoom.Value})";
            if (animation == null) return;
            if (isMouseDown)
            {
                SetPixel(e);
            }
        }

        void SetPixel(MouseEventArgs e)
        {
            if (animation == null) return;

            Microsoft.Maui.Graphics.Color c;
            if (e.Button == MouseButtons.Left)
            {
                c = Microsoft.Maui.Graphics.Color.FromRgba( btnForeColor.BackColor.R, btnForeColor.BackColor.G, btnForeColor.BackColor.B, btnForeColor.BackColor.A);
            }
            else if (e.Button == MouseButtons.Right)
            {
                c = Microsoft.Maui.Graphics.Color.FromRgba(btnBackColor.BackColor.R, btnBackColor.BackColor.G, btnBackColor.BackColor.B, btnBackColor.BackColor.A);
            }
            else return;

            int x = (e.X / (int)udZoom.Value);
            int y = (e.Y / (int)udZoom.Value);

            if (x < 0 || y < 0 || x >= animation.Width || y > animation.Height) return;

                graphics.FillRectangle(new SolidBrush(Color.FromArgb((byte)c.Alpha, (byte)c.Red, (byte)c.Green, (byte)c.Blue)),
                2 + x * (int)udZoom.Value,
                2 + y * (int)udZoom.Value,
                (int)udZoom.Value - 3,
                (int)udZoom.Value - 3);
            pictureBox2.Image = bmp;

            Microsoft.Maui.Graphics.Color[,] source = animation[ImageIndex];
            source[x, y] = c;

            ((PictureBox)(flowLayoutPanel1.Controls[ImageIndex])).Image = animation.Bitmap(ImageIndex);

        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newAnimationForm = new NewAnimationForm();
            if (newAnimationForm.ShowDialog() == DialogResult.OK)
            {
                _FileName = String.Empty;
                animation = new Animation(newAnimationForm.ImageWidth, newAnimationForm.ImageHeight);
                animation.Add(animation.New(Colors.Black));

                saveToolStripMenuItem.Enabled = false;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = path;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _FileName = openFileDialog1.FileName;
                animation = AnimationSerializer.Load(_FileName);
                for (int frameNo = 0; frameNo < animation.Count; frameNo++)
                {
                    PictureBox p = new PictureBox();
                    p.Tag = frameNo;
                    p.SizeMode = PictureBoxSizeMode.Zoom;
                    p.Click += P_Click;
                    p.Size = new Size(150, 60);
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Image = animation.Bitmap(frameNo);
                    flowLayoutPanel1.Controls.Add(p);
                }
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (animation == null) return;

            AnimationSerializer.Save(_FileName, animation);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (animation == null) return;

            saveFileDialog1.InitialDirectory = path;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _FileName = saveFileDialog1.FileName;
                AnimationSerializer.Save(_FileName, animation);

                saveToolStripMenuItem.Enabled = false;
            }
        }

        private void udZoom_ValueChanged(object sender, EventArgs e)
        {
            ZoomImage();
        }

        void ZoomImage()
        {
            if (animation == null) return;
            if (ImageIndex < 0 || ImageIndex >= animation.Count) return;

            pictureBox2.Width = 1 + (int)udZoom.Value * animation.Width;
            pictureBox2.Height = 1 + (int)udZoom.Value * animation.Height;

            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
            }
            bmp = new Bitmap((int)udZoom.Value * animation.Width + 1, (int)udZoom.Value * animation.Height + 1);
            graphics = Graphics.FromImage(bmp);
            graphics.Clear(Color.Black);

            Bitmap source = animation.Bitmap(ImageIndex); 
            for (int x = 0; x < animation.Width + 1; x++)
            {
                graphics.DrawLine(Pens.White, x * (int)udZoom.Value, 0, x * (int)udZoom.Value, pictureBox2.Width);
            }
            for (int y = 0; y < animation.Height + 1; y++)
            {
                graphics.DrawLine(Pens.White, 0, y * (int)udZoom.Value, pictureBox2.Width, y * (int)udZoom.Value);
            }
            for (int x = 0; x < animation.Width; x++)
            {
                for (int y = 0; y < animation.Height; y++)
                {
                    Color c = source.GetPixel(x, y);
                    graphics.FillRectangle(new SolidBrush(c), x * (int)udZoom.Value + 2, y * (int)udZoom.Value + 2, (int)udZoom.Value - 3, (int)udZoom.Value - 3);
                }
            }
            pictureBox2.Image = bmp;
        }

        void UpdateImage()
        {
            if (animation == null) return;

            ((PictureBox)(flowLayoutPanel1.Controls[ImageIndex])).Image = animation.Bitmap(ImageIndex);
        }

        private void chkAnimate_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = chkAnimate.Checked;
        }

        private void udZoomAnimation_ValueChanged(object sender, EventArgs e)
        {
            if (animation == null) return;

            pictureBox1.Size = new Size(animation.Width * (int)udZoomAnimation.Value,
                                        animation.Height * (int)udZoomAnimation.Value);
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = btnForeColor.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnForeColor.BackColor = colorDialog1.Color;
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = btnBackColor.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnBackColor.BackColor = colorDialog1.Color;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int frameNo = flowLayoutPanel1.Controls.Count;
            PictureBox p = new PictureBox();
            p.Tag = frameNo;
            p.SizeMode = PictureBoxSizeMode.Zoom;
            p.Click += P_Click;
            p.Size = new Size(150, 60);
            p.SizeMode = PictureBoxSizeMode.StretchImage;
            var img = animation.New(Colors.Black);
            animation.Add(img);
            p.Image = animation.Bitmap(frameNo);
            flowLayoutPanel1.Controls.Add(p);
        }
    }

    public static class FileStreamExtensions
    {
        public static int getByte(this FileStream fs)
        {
            return fs.ReadByte() & 0xff;
        }

        public static int getWord(this FileStream fs)
        {
            return (fs.ReadByte() & 0xff) + ((fs.ReadByte() & 0xff) << 8);
        }
    }

    public static class animationHelpers
    {
        public static void FromBitmap(this Animation animation, int index, Bitmap bitmap)
        {

            //animation[index];
        }

        public static Bitmap Bitmap(this Animation animation, int index)
        {
            System.maui.graphics.Color[,] raw = animation[index];
            Bitmap bmp = new Bitmap(animation.Width, animation.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < animation.Width; x++)
            {
                for (int y = 0; y < animation.Height; y++)
                {
                    bmp.SetPixel(x, y, raw[x, y]);
                }
            }
            return bmp;
        }
    }
}