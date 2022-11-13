using MatrixLib;
using MauiMatrix.ViewModels;

namespace MauiMatrix.Views;

public partial class AnimationPage : ContentPage
{
    AnimationViewModel vm;

    public AnimationPage(AnimationViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = this.vm;

        m_GraphicsView1.Drawable = new EditableDrawable(m_GraphicsView1, vm);
        m_GraphicsView2.Drawable = new TimedDrawable(m_GraphicsView2, vm);
    }
}
