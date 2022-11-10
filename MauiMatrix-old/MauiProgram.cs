//using Android.Service.QuickSettings;

using CommunityToolkit.Maui;
using MauiMatrix.Services;
using MauiMatrix.ViewModels;
using MauiMatrix.Views;

namespace MauiMatrix;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddTransient<PropertyPage>();
        builder.Services.AddTransient<PropertyViewModel>();

        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<HomeViewModel>();

        builder.Services.AddTransient<ColorsPage>();
        builder.Services.AddTransient<ColorsViewModel>();

        builder.Services.AddTransient<NewAnimationPage>();
        builder.Services.AddTransient<NewAnimationViewModel>();

        builder.Services.AddTransient<AnimationPage>();
        builder.Services.AddTransient<AnimationViewModel>();
        builder.Services.AddTransient<AnimationFileInfo>();

        builder.Services.AddSingleton<FileIoService>();

        return builder.Build();
	}
}
