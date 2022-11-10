using MauiMatrix.ViewModels;

namespace MauiMatrix.Views
{
    public partial class ColorsPage : ContentPage
    {
        public ColorsPage(ColorsViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}
