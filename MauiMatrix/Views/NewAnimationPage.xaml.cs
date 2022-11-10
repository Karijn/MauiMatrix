using Android.Content;
using Android.Views.InputMethods;
using Android.Widget;
using MauiMatrix.ViewModels;

namespace MauiMatrix.Views
{
    public partial class NewAnimationPage : ContentPage
    {
        NewAnimationViewModel vm;
        public NewAnimationPage(NewAnimationViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            BindingContext = this.vm;
        }
    }
}
