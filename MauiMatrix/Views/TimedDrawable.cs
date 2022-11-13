using MatrixLib;
using MauiMatrix.ViewModels;

namespace MauiMatrix.Views;

public class TimedDrawable : IDrawable
{
    private readonly IGraphicsView graphicsView;
    private readonly AnimationViewModel vm;
    Timer timer;
    int current = 0;

    public TimedDrawable(IGraphicsView graphicsView, AnimationViewModel vm)
    {
        this.graphicsView = graphicsView;
        this.vm = vm;
        timer = new Timer(OnTimer);
        timer.Change(10, 10);
    }

    int animationCount = 0;
    private void OnTimer(object state)
    {
        animationCount++;
        animationCount %= 10;
        if (animationCount == 0)
        {
            graphicsView.Invalidate();
        }
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Colors.Black;
        canvas.FillRectangle(0, 0,
            vm.AnimationPixelGap + vm.Animation.Width * (vm.AnimationPixelSize + vm.AnimationPixelGap),
            vm.AnimationPixelGap + vm.Animation.Height * (vm.AnimationPixelSize + vm.AnimationPixelGap));

        if (vm.Animation.Count == 0) return;

        current = current % vm.Animation.Count;
        if (current < 0 || current >= vm.Animation.Count)
        {
            return;
        }

        AnimationImage img = vm.Animation[current];
        current++;

        for (int x = 0; x < vm.Animation.Width; x++)
        {
            for (int y = 0; y < vm.Animation.Height; y++)
            {
                canvas.StrokeColor = img[x, y];
                canvas.FillColor = img[x, y];
                canvas.FillRectangle(vm.AnimationPixelGap + x * (vm.AnimationPixelSize + vm.AnimationPixelGap),
                                     vm.AnimationPixelGap + y * (vm.AnimationPixelSize + vm.AnimationPixelGap),
                                     vm.AnimationPixelSize, vm.AnimationPixelSize);
            }
        }
    }
}
