using Microsoft.Maui.Graphics;


namespace MatrixLib;

public struct ColorStruct
{
    public byte A;
    public byte R;
    public byte G;
    public byte B;
}
public class Animation
{
    List<Color[,]> _frames;

    public Animation(int width, int height)
    {
        _frames = new List<Color[,]>();
        
        Width = width;
        Height = height;
    }

    public Color[,] Add(Color[,] image)
    {
        _frames.Add(image);
        return image;
    }

    public void InsertAt(int currentEdit, Color[,] colors)
    {
        _frames.Insert(currentEdit, colors);
    }

    public bool DeleteAt(int from)
    {
        if(_frames.Count == 1)
        {
            _frames[0] = New(Colors.Black);
            return true;
        }
        if (from < 0 || from >= _frames.Count)
        {
            return false;
        }

        _frames.RemoveAt(from);
        return true;
    }

    public bool MoveImage(int from, int to)
    {
        if (from < 0 || from >= _frames.Count ||
            to < 0 || to >= _frames.Count ||
            from == to)
        {
            return false;
        }
        var img = _frames[from];
        _frames.RemoveAt(from);
        if (to > from)
        {
            _frames.Insert(to - 1, img);
        }
        else
        {
            _frames.Insert(to, img);
        }
        return true;
    }

    public bool MoveToStart(int from)
    {
        return MoveImage(0, from);
    }

    public bool MoveBack(int from)
    {
        return MoveImage(from, from - 1);
    }

    public bool MoveForward(int from)
    {
        return MoveImage(from, from + 1);
    }

    public bool MoveToEnd(int from)
    {
        return MoveImage(_frames.Count - 1, from);
    }

    public Color[,] New(Color background)
    {
        Color[,] bmp = new Color[Width, Height];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                bmp[x, y] = background; // Color.FromRgba(0, 0, 0, 255);
            }
        }
        return bmp;
    }

    public bool Copy(int currentEdit)
    {
        if (currentEdit < 0 || currentEdit >= _frames.Count)
        {
            return false;
        }

        Color[,] bmp = new Color[Width, Height];
        Color[,] org = _frames[currentEdit];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                bmp[x, y] = org[x, y]; // Color.FromRgba(0, 0, 0, 255);
            }
        }
        _frames.Insert(currentEdit, bmp);

        return true;
    }

    public Color[,] this[int index] => _frames[index];

    public int Count => _frames.Count;

    public int Width { get; set; }
    public int Height { get; set; }
}