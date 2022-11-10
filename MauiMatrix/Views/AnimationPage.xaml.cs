using MauiMatrix.ViewModels;

namespace MauiMatrix.Views;

public partial class AnimationPage : ContentPage
{
    AnimationViewModel vm;
    MyDrawable2 drawable1;

    public AnimationPage(AnimationViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = this.vm;

        m_GraphicsView1.Drawable = drawable1 = new MyDrawable2(m_GraphicsView1, vm);
        m_GraphicsView2.Drawable = new TimedDrawable(m_GraphicsView2, vm);
        vm.PropertyChanged += Vm_PropertyChanged;
    }

    private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("CurrentEdit"))
        {
            drawable1.Image = vm.CurrentEdit;
            m_GraphicsView1.Invalidate();
        }
    }
}

internal class MyDrawable2 : IDrawable
{
    private readonly IGraphicsView graphicsView;
    private readonly AnimationViewModel vm;
    private int image = 0;

    public MyDrawable2(IGraphicsView graphicsView, AnimationViewModel vm)
    {
        this.graphicsView = graphicsView;
        this.vm = vm;
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

        Color[,] img = vm.Animation[image];

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



internal class TimedDrawable : IDrawable
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
        timer.Change(100, 100);
    }

    private void OnTimer(object state)
    {
        graphicsView.Invalidate();
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

        Color[,] img = vm.Animation[current];
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
