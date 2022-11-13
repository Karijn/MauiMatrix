using MatrixLib;
using MauiMatrix.ViewModels;

namespace MauiMatrix.Views;

public class EditableDrawable : IDrawable
{
    private readonly IGraphicsView graphicsView;
    private readonly AnimationViewModel vm;
    private int image = 0;

    public EditableDrawable(IGraphicsView graphicsView, AnimationViewModel vm)
    {
        this.graphicsView = graphicsView;
        this.vm = vm;
        vm.Animation.ImagePixelChangedEvent += Animation_RaiseCustomEvent;
        vm.PropertyChanged += Vm_PropertyChanged;
    }

    private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("CurrentEdit"))
        {
            Image = vm.CurrentEdit;
            graphicsView.Invalidate();
        }
    }

    private void Animation_RaiseCustomEvent(object sender, ImagePixelChangedEventArgs e)
    {
        graphicsView.Invalidate();
    }

    public int Image
    {
        get => image;
        set
        {
            if (image != value)
            {
                image = value;
                if (image < 0 || image >= vm.Animation.Count)
                {
                    return;
                }
                graphicsView.Invalidate();
            }
        }
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Colors.Black;
        canvas.FillRectangle(0,
                             0,
                             vm.EditWidth + vm.BorderWidth * 2,
                             vm.EditHeight + vm.BorderWidth * 2);

        if (image < 0 || image >= vm.Animation.Count)
        {
            return;
        }

        AnimationImage img = vm.Animation[image];

        for (int x = 0; x < vm.Animation.Width; x++)
        {
            for (int y = 0; y < vm.Animation.Height; y++)
            {
                canvas.StrokeColor = img[x, y];
                canvas.FillColor = img[x, y];
                canvas.FillRectangle(vm.BorderWidth + x * (vm.PixelSize + vm.PixelGap),
                                     vm.BorderWidth + y * (vm.PixelSize + vm.PixelGap),
                                     vm.PixelSize, vm.PixelSize);

                canvas.StrokeColor = Colors.White;
                canvas.DrawRectangle(vm.BorderWidth + x * (vm.PixelSize + vm.PixelGap),
                                     vm.BorderWidth + y * (vm.PixelSize + vm.PixelGap),
                                     vm.PixelSize, vm.PixelSize);
            }
        }
    }
}
