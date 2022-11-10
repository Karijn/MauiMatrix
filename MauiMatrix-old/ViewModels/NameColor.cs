using CommunityToolkit.Mvvm.ComponentModel;


namespace MauiMatrix.ViewModels;

public partial class NameColor : ObservableObject
{
    public NameColor(string name, Color color)
    {
        Name = name;
        Background = color;
    }

    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public Color background;

}

