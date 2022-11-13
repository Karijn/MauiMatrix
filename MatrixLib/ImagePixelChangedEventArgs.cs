namespace MatrixLib;

public class ImagePixelChangedEventArgs : EventArgs
{
    public ImagePixelChangedEventArgs(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }
}
