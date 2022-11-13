using Microsoft.Maui.Graphics;

namespace MatrixLib;

public class AnimationImage
{
    private readonly Color[,] m_image;

    public event EventHandler<PixelChangedEventArgs>? PixelChangedEvent;

    public AnimationImage(int columns, int rows)
    {
        m_image = new Color[columns, rows];
    }

    public Color this[int x, int y]
    {
        get => m_image[x, y];
        
        set
        {
            if (m_image[x, y] != value)
            {
                m_image[x, y] = value;
                OnRaisePixelChanged(new PixelChangedEventArgs(x, y));
            }
        }
    }

    protected virtual void OnRaisePixelChanged(PixelChangedEventArgs e)
    {
        EventHandler<PixelChangedEventArgs>? raiseEvent = PixelChangedEvent;

        if (raiseEvent != null)
        {
            raiseEvent(this, e);
        }
    }
}
