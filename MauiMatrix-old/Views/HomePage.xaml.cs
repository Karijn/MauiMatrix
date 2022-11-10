using MauiMatrix.ViewModels;

namespace MauiMatrix.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage(HomeViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}
