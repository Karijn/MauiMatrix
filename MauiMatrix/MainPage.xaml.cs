using MauiMatrix.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiMatrix;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
    }
}

