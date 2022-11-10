using MauiMatrix.ViewModels;
using MauiMatrix.Views;

namespace MauiMatrix;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(PropertyPage), typeof(PropertyPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(AnimationPage), typeof(AnimationPage));
        Routing.RegisterRoute(nameof(NewAnimationPage), typeof(NewAnimationPage));
        Routing.RegisterRoute(nameof(ColorsPage), typeof(ColorsPage));
    }
}
