namespace MatrixLib;

public class PixelChangedEventArgs : EventArgs
{
    public PixelChangedEventArgs(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }
}
