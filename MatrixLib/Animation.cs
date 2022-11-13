using Microsoft.Maui.Graphics;
using static System.Net.Mime.MediaTypeNames;


namespace MatrixLib;


public delegate void PixelChanged(object sender, PixelChangedEventArgs args);
public delegate void ImagePixelChanged(object sender, ImagePixelChangedEventArgs args);

public class Animation
{
    List<AnimationImage> _frames;

    public Animation(int width, int height)
    {
        _frames = new List<AnimationImage>();
        
        Width = width;
        Height = height;
    }

    public AnimationImage Add(AnimationImage image)
    {
        image.PixelChangedEvent += Image_RaiseImagePixelChanged;
        _frames.Add(image);
        return image;
    }

    // Declare the event using EventHandler<T>
    public event EventHandler<ImagePixelChangedEventArgs>? ImagePixelChangedEvent;

    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    protected virtual void OnImagePixelChanged(ImagePixelChangedEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately after the null check and before the event is raised.
        EventHandler<ImagePixelChangedEventArgs>? raiseEvent = ImagePixelChangedEvent;

        // Event will be null if there are no subscribers
        if (raiseEvent != null)
        {
            // Call to raise the event.
            raiseEvent(this, e);
        }
    }

    private void Image_RaiseImagePixelChanged(object? sender, PixelChangedEventArgs e)
    {
        OnImagePixelChanged(new ImagePixelChangedEventArgs(e.X, e.Y));
    }

    public void InsertAt(int currentEdit, AnimationImage image)
    {
        image.PixelChangedEvent += Image_RaiseImagePixelChanged;
        _frames.Insert(currentEdit, image);
    }

    public bool DeleteAt(int from)
    {
        if(_frames.Count == 1)
        {
            _frames[0].PixelChangedEvent -= Image_RaiseImagePixelChanged;
            _frames[0] = New(Colors.Black);
            _frames[0].PixelChangedEvent += Image_RaiseImagePixelChanged;
            return true;
        }
        if (from < 0 || from >= _frames.Count)
        {
            return false;
        }

        _frames[from].PixelChangedEvent -= Image_RaiseImagePixelChanged;
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

    public AnimationImage New(Color background)
    {
        AnimationImage bmp = new AnimationImage(Width, Height);
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

        AnimationImage bmp = new AnimationImage(Width, Height);
        AnimationImage org = _frames[currentEdit];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                bmp[x, y] = org[x, y]; // Color.FromRgba(0, 0, 0, 255);
            }
        }
        _frames.Insert(currentEdit, bmp);
        bmp.PixelChangedEvent += Image_RaiseImagePixelChanged;

        return true;
    }

    public AnimationImage this[int index] => _frames[index];

    public int Count => _frames.Count;

    public int Width { get; set; }
    public int Height { get; set; }
}