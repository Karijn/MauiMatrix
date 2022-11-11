using Microsoft.Maui.Graphics;

namespace MatrixLib
{
    public class AnimationSerializer
    {
        public static AnimationFileData ReadHeader(string filename)
        {
            AnimationFileData header = new AnimationFileData();
            header.FileName = filename;

            using (FileStream fs = File.OpenRead(filename))
            {
                var v = fs.getWord();
                int version = v switch
                {
                    0x01AA => 1,
                    0x02AA => 2,
                    0x03AA => 3,
                    _ => 0
                };

                if (version > 0)
                {
                    header.AnimationWidth = fs.getWord();
                    header.AnimationHeight = fs.getWord();
                }
                else
                {
                    header.AnimationWidth = v;
                    header.AnimationHeight = fs.getWord();
                }

                header.NrOfImages = (version > 2) ? fs.getWord() : fs.getByte();
                header.NrOfColors = 0;

            }
            return header;
        }

        public static Animation Load(string filename)
        {
            using (FileStream fs = File.OpenRead(filename))
            {
                var v = fs.getWord();
                int version = v switch
                {
                    0x01AA => 1,
                    0x02AA => 2,
                    0x03AA => 3,
                    _ => 0
                };

                Animation animation = (version > 0) ? new Animation(fs.getWord(), fs.getWord()) : new Animation(v, fs.getWord());

                int nrOfImages = (version > 2) ? fs.getWord() : fs.getByte();

                for (int imageNr = 0; imageNr < nrOfImages; imageNr++)
                {
                    Color[,] bitmap = animation.New(Colors.Black);
                    int numColors = fs.getWord();

                    for (int colorindex = 0; colorindex < numColors; colorindex++)
                    {
                        Color c = (version > 0) ? fs.getColor() : fs.getColor3();
                        int numLeds = fs.getWord();

                        for (int ledindex = 0; ledindex < numLeds; ledindex++)
                        {
                            int ledNo = fs.getWord();
                            int x = ledNo % animation.Width;
                            int y = ledNo / animation.Width;
                            bitmap[x, y] = c;
                        }
                    }
                    animation.Add(bitmap);
                }
                fs.Close();
                return animation;
            }
        }

        public static void Save(string filename, Animation animation)
        {
            using (var fs = File.OpenWrite(filename))
            {
                fs.putWord(0x03AA); // version number AA 03
                int w = animation.Width;
                int h = animation.Height;
                fs.putWord(w);
                fs.putWord(h);

                fs.putWord(animation.Count);

                for (int imageIndex = 0; imageIndex < animation.Count; imageIndex++)
                {
                    Color[,] bitmap = animation[imageIndex];
                    Dictionary<Color, List<int>> colors = new();
                    for (int x = 0; x < w; x++)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            int ledNo = x + y * animation.Width;
                            Color c = bitmap[x, y];
                            c.ToRgba(out byte r, out byte g, out byte b, out byte a);

                            if (!colors.Keys.Contains(c))
                            {
                                if (!(r == 0 && g == 0 && b == 0 && a == 255))
                                {
                                    colors.Add(c, new List<int> { ledNo });
                                }
                            }
                            else
                            {
                                colors[c].Add(ledNo);
                            }
                        }
                    }
                    fs.putWord(colors.Count);
                    foreach (var color in colors.Keys)
                    {
                        fs.putColor(color);
                        fs.putWord(colors[color].Count);
                        foreach (var pixel in colors[color])
                        {
                            fs.putWord(pixel);
                        }
                    }
                }
            }
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
            byte b1 = (byte)fs.getByte();
            byte b2 = (byte)fs.getByte();
            return (b1 & 0xff) + ((b2 & 0xff) << 8);
        }

        public static int getInt(this FileStream fs)
        {
            byte b1 = (byte)fs.getByte();
            byte b2 = (byte)fs.getByte();
            byte b3 = (byte)fs.getByte();
            byte b4 = (byte)fs.getByte();
            return (b1 & 0xff) + ((b2 & 0xff) << 8) + ((b3 & 0xff) << 16) + ((b4 & 0xff) << 24);

        }

        public static Color getColor(this FileStream fs)
        {
            byte a = (byte)fs.getByte();
            byte r = (byte)fs.getByte();
            byte g = (byte)fs.getByte();
            byte b = (byte)fs.getByte();
            return Color.FromRgba(r, g, b, a);
        }

        public static Color getColor3(this FileStream fs)
        {
            byte r = (byte)fs.getByte();
            byte g = (byte)fs.getByte();
            byte b = (byte)fs.getByte();
            return Color.FromRgb(r, g, b);
        }

        public static void putByte(this FileStream fs, int b)
        {
            fs.WriteByte((byte)b);
        }


        public static void putWord(this FileStream fs, int w)
        {
            fs.WriteByte((byte)(w & 0xff));
            fs.WriteByte((byte)((w >> 8) & 0xff));
        }

        public static void putInt(this FileStream fs, int i)
        {
            fs.WriteByte((byte)(i & 0xff));
            fs.WriteByte((byte)((i >> 8) & 0xff));
            fs.WriteByte((byte)((i >> 16) & 0xff));
            fs.WriteByte((byte)((i >> 24) & 0xff));
        }

        public static void putColor(this FileStream fs, Color c)
        {
            c.ToRgba(out byte r, out byte g, out byte b, out byte a);

            fs.putByte(a);
            fs.putByte(r);
            fs.putByte(g);
            fs.putByte(b);
        }

    }

}


