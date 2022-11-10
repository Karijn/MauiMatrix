using MauiMatrix.ViewModels;

namespace MauiMatrix;

public partial class PropertyPage : ContentPage
{
	public PropertyPage(PropertyViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}