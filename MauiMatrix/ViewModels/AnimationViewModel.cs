using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMatrix.Views;
using MauiMatrix.Services;
using Microsoft.Maui.Devices;
using MatrixLib;

namespace MauiMatrix.ViewModels;

public enum GraphicTool
{
    DrawDot,
    Fill
}



[QueryProperty(nameof(AnimationFileInfo), "AnimationFileInfo")]
public partial class AnimationViewModel : BaseViewModel
{
    public AnimationViewModel()
    {
        NameToColor = new ObservableCollection<NameColor>
        {
/* 0  *  */    new NameColor("Black", Colors.Black ),
/* 1  *  */    new NameColor("White", Colors.White),
/* 2  *  */    new NameColor("DarkGrey", Colors.DarkGrey),
/* 3  *  */    new NameColor("Grey", Colors.Grey),
/* 4  *  */    new NameColor("Silver", Colors.Silver),
/* 5  *  */    new NameColor("LightGrey", Colors.LightGrey),
/* 6  *  */    new NameColor("Brown", Colors.Brown),
/* 7  *  */    new NameColor("DarkRed", Colors.DarkRed),
/* 8  *  */    new NameColor("Red", Colors.Red),
/* 9  *  */    new NameColor("OrangeRed", Colors.OrangeRed),
/* 0  *  */    new NameColor("DarkOrange", Colors.DarkOrange),
/* 1  *  */    new NameColor("Orange", Colors.Orange),
/* 2  *  */    new NameColor("Purple", Colors.Purple),
/* 3  *  */    new NameColor("MediumPurple", Colors.MediumPurple),
/* 4  *  */    new NameColor("MediumVioletRed", Colors.MediumVioletRed),
/* 5  *  */    new NameColor("Gold", Colors.Gold),
/* 6  *  */    new NameColor("Yellow", Colors.Yellow),
/* 7  *  */    new NameColor("DarkGreen", Colors.DarkGreen),
/* 8  *  */    new NameColor("ForestGreen", Colors.ForestGreen),
/* 9  *  */    new NameColor("Green", Colors.Green),
/* 0  *  */    new NameColor("LawnGreen", Colors.LawnGreen),
/* 1  *  */    new NameColor("LightGreen", Colors.LightGreen),
/* 2  *  */    new NameColor("MediumSpringGreen", Colors.MediumSpringGreen),
/* 3  *  */    new NameColor("SpringGreen", Colors.SpringGreen),
/* 4  *  */    new NameColor("DarkOliveGreen", Colors.DarkOliveGreen),
/* 5     */    new NameColor("Chartreuse", Colors.Chartreuse),
/* 6     */    new NameColor("Lime", Colors.Lime),
/* 7     */    new NameColor("Aqua", Colors.Aqua),
/* 8     */    new NameColor("YellowGreen", Colors.YellowGreen),
/* 9     */    new NameColor("GreenYellow", Colors.GreenYellow),
/* 0     */    new NameColor("LimeGreen", Colors.LimeGreen),
/* 1  *  */    new NameColor("PaleGreen", Colors.PaleGreen),
/* 2  *  */    new NameColor("LightSeaGreen", Colors.LightSeaGreen),
/* 3  *  */    new NameColor("MediumSeaGreen", Colors.MediumSeaGreen),
/* 4  *  */    new NameColor("DarkSeaGreen", Colors.DarkSeaGreen),
/* 5  *  */    new NameColor("SeaGreen", Colors.SeaGreen),
/* 6  *  */    new NameColor("LightCyan", Colors.LightCyan),
/* 7  *  */    new NameColor("DarkCyan", Colors.DarkCyan),
/* 8  *  */    new NameColor("Cyan", Colors.Cyan),
/* 9  *  */    new NameColor("DeepPink", Colors.DeepPink),
/* 0  *  */    new NameColor("HotPink", Colors.HotPink),
/* 1  *  */    new NameColor("Pink", Colors.Pink),
/* 2  *  */    new NameColor("LightPink", Colors.LightPink),
/* 3  *  */    new NameColor("DarkBlue", Colors.DarkBlue),
/* 4  *  */    new NameColor("Blue", Colors.Blue),
/* 5  *  */    new NameColor("LightBlue", Colors.LightBlue),
/* 6  *  */    new NameColor("LightSkyBlue", Colors.LightSkyBlue),
/* 7  *  */    new NameColor("DeepSkyBlue", Colors.DeepSkyBlue),
/* 8  *  */    new NameColor("SkyBlue", Colors.SkyBlue),
/* 9  *  */    new NameColor("MidnightBlue", Colors.MidnightBlue),
/* 0  *  */    new NameColor("CadetBlue", Colors.CadetBlue),
/* 1  *  */    new NameColor("CornflowerBlue", Colors.CornflowerBlue),
/* 2  *  */    new NameColor("DodgerBlue", Colors.DodgerBlue),
/* 3  *  */    new NameColor("PowderBlue", Colors.PowderBlue),
/* 4  *  */    new NameColor("MediumBlue", Colors.MediumBlue),
/* 5  *  */    new NameColor("LightSteelBlue", Colors.LightSteelBlue),
/* 6  *  */    new NameColor("SteelBlue", Colors.SteelBlue),
/* 7  *  */    new NameColor("RoyalBlue", Colors.RoyalBlue),
/* 8  *  */    new NameColor("MediumSlateBlue", Colors.MediumSlateBlue),
/* 9  *  */    new NameColor("DarkSlateBlue", Colors.DarkSlateBlue),
/* 0  *  */    new NameColor("SlateBlue", Colors.SlateBlue),
/* 1  *  */    new NameColor("AliceBlue", Colors.AliceBlue),
/* 2  *  */    new NameColor("Navy", Colors.Navy),
/* 3  *  */    new NameColor("MistyRose", Colors.MistyRose),
/* 4  *  */    new NameColor("MediumTurquoise", Colors.MediumTurquoise),
/* 5  *  */    new NameColor("DarkTurquoise", Colors.DarkTurquoise),
/* 6  *  */    new NameColor("PaleTurquoise", Colors.PaleTurquoise),
/* 7  *  */    new NameColor("Turquoise", Colors.Turquoise),
/* 8  *  */    new NameColor("Ivory", Colors.Ivory),
/* 9  *  */    new NameColor("Snow", Colors.Snow),
/* 0  *  */    new NameColor("Honeydew", Colors.Honeydew),
/* 1  *  */    new NameColor("Peru", Colors.Peru),
/* 2  *  */    new NameColor("DarkViolet", Colors.DarkViolet),
/* 3  *  */    new NameColor("BlueViolet", Colors.BlueViolet),
/* 4  *  */    new NameColor("Violet", Colors.Violet),
/* 5  *  */    new NameColor("Wheat", Colors.Wheat),
/* 6  *  */    new NameColor("Lavender", Colors.Lavender),
/* 7  *  */    new NameColor("PapayaWhip", Colors.PapayaWhip),
/* 8  *  */    new NameColor("Tomato", Colors.Tomato),
/* 9  *  */    new NameColor("Gainsboro", Colors.Gainsboro),
/* 0  *  */    new NameColor("Indigo", Colors.Indigo),
/* 1  *  */    new NameColor("Crimson", Colors.Crimson),
/* 2  *  */    new NameColor("Maroon", Colors.Maroon),
/* 3  *  */    new NameColor("LemonChiffon", Colors.LemonChiffon),
/* 4  *  */    new NameColor("Moccasin", Colors.Moccasin),
/* 5  *  */    new NameColor("Linen", Colors.Linen),
/* 6  *  */    new NameColor("Tan", Colors.Tan),
/* 7  *  */    new NameColor("Plum", Colors.Plum),
/* 8  *  */    new NameColor("MintCream", Colors.MintCream),
/* 9  *  */    new NameColor("SeaShell", Colors.SeaShell),
/* 0  *  */    new NameColor("LightCoral", Colors.LightCoral),
/* 1  *  */    new NameColor("Coral", Colors.Coral),
/* 2  *  */    new NameColor("Teal", Colors.Teal),
/* 3  *  */    new NameColor("Cornsilk", Colors.Cornsilk),
/* 4  *  */    new NameColor("Firebrick", Colors.Firebrick),
/* 5  *  */    new NameColor("DarkKhaki", Colors.DarkKhaki),
/* 6  *  */    new NameColor("Khaki", Colors.Khaki),
/* 7  *  */    new NameColor("LavenderBlush", Colors.LavenderBlush),
/* 8  *  */    new NameColor("PeachPuff", Colors.PeachPuff),
/* 9  *  */    new NameColor("Olive", Colors.Olive),
/* 0  *  */    new NameColor("Bisque", Colors.Bisque),
/* 1  *  */    new NameColor("Chocolate", Colors.Chocolate),
/* 2  *  */    new NameColor("Azure", Colors.Azure),
/* 3  *  */    new NameColor("MediumAquamarine", Colors.MediumAquamarine),
/* 4  *  */    new NameColor("Aquamarine", Colors.Aquamarine),
/* 5  *  */    new NameColor("Thistle", Colors.Thistle),
/* 6  *  */    new NameColor("WhiteSmoke", Colors.WhiteSmoke),
/* 7  *  */    new NameColor("OldLace", Colors.OldLace),
/* 8  *  */    new NameColor("BurlyWood", Colors.BurlyWood),
/* 9  *  */    new NameColor("BlanchedAlmond", Colors.BlanchedAlmond),
/* 0  *  */    new NameColor("MediumOrchid", Colors.MediumOrchid),
/* 1  *  */    new NameColor("DarkOrchid", Colors.DarkOrchid),
/* 2  *  */    new NameColor("Orchid", Colors.Orchid),
/* 3  *  */    new NameColor("OliveDrab", Colors.OliveDrab),
/* 4  *  */    new NameColor("DarkMagenta", Colors.DarkMagenta),
/* 5  *  */    new NameColor("Magenta", Colors.Magenta),
/* 6  *  */    new NameColor("Sienna", Colors.Sienna),
/* 7  *  */    new NameColor("Fuchsia", Colors.Fuchsia),
/* 8  *  */    new NameColor("GhostWhite", Colors.GhostWhite),
/* 9  *  */    new NameColor("DimGrey", Colors.DimGrey),
/* 0  *  */    new NameColor("LightSlateGrey", Colors.LightSlateGrey),
/* 1  *  */    new NameColor("DarkSlateGrey", Colors.DarkSlateGrey),
/* 2  *  */    new NameColor("SlateGrey", Colors.SlateGrey),
/* 3  *  */    new NameColor("Beige", Colors.Beige),
/* 4  *  */    new NameColor("DarkGoldenrod", Colors.DarkGoldenrod),
/* 5  *  */    new NameColor("PaleGoldenrod", Colors.PaleGoldenrod),
/* 6  *  */    new NameColor("Goldenrod", Colors.Goldenrod),
/* 7  *  */    new NameColor("PaleVioletRed", Colors.PaleVioletRed),
/* 8  *  */    new NameColor("IndianRed", Colors.IndianRed),
/* 9  *  */    new NameColor("LightGoldenrodYellow", Colors.LightGoldenrodYellow),
/* 0  *  */    new NameColor("NavajoWhite", Colors.NavajoWhite),
/* 1  *  */    new NameColor("FloralWhite", Colors.FloralWhite),
/* 2  *  */    new NameColor("AntiqueWhite", Colors.AntiqueWhite),
/* 3  *  */    new NameColor("LightSalmon", Colors.LightSalmon),
/* 4  *  */    new NameColor("DarkSalmon", Colors.DarkSalmon),
/* 5  *  */    new NameColor("Salmon", Colors.Salmon),
/* 6  *  */    new NameColor("RosyBrown", Colors.RosyBrown),
/* 7  *  */    new NameColor("SandyBrown", Colors.SandyBrown),
/* 8  *  */    new NameColor("SaddleBrown", Colors.SaddleBrown),
/* 9  *  */    new NameColor("LightYellow", Colors.LightYellow),
//=========================
//              new NameColor("Transparent", Colors.Transparent),
        };

        selectedColor = NameToColor[0];
        SelectedTool = GraphicTool.DrawDot;
    }

    AnimationFileInfo _animationFileInfo;

    public AnimationFileInfo AnimationFileInfo
    {
        get => _animationFileInfo;
        set
        {
            _animationFileInfo = value;
            FileName = _animationFileInfo.FileName;
            if (_animationFileInfo.IsNewFile)
            {
                Animation = new MatrixLib.Animation(_animationFileInfo.AnimationWidth, _animationFileInfo.AnimationHeight);
                Animation.Add(Animation.New(Colors.Black));
            }
            else if (_animationFileInfo.FileName.Length > 0)
            {
                var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var file = Path.Combine(folder, _animationFileInfo.FileName);
                Animation = AnimationSerializer.Load(file);
            }
            OnPropertyChanged(nameof(HasImages));
            OnPropertyChanged(nameof(LastIndex));
            OnPropertyChanged(nameof(CurrentEdit));
            OnPropertyChanged(nameof(AnimationHeight));
            OnPropertyChanged(nameof(AnimationWidth));
            OnPropertyChanged(nameof(EditHeight));
            OnPropertyChanged(nameof(EditWidth));
        }
    }
    public MatrixLib.Animation Animation { get; set; } = new MatrixLib.Animation(1, 1);

    [ObservableProperty]
    GraphicTool selectedTool;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    private string fileName;

    private int currentEdit;

    public int CurrentEdit
    {
        get => currentEdit;
        set
        {
            if (value > Animation.Count - 1)
            {
                SetProperty(ref currentEdit, Animation.Count - 1);
            }
            else if (value >= 0)
            {
                SetProperty(ref currentEdit, value);
            }
        }
    }

    [ObservableProperty]
    NameColor selectedColor;

    public bool HasImages => Animation.Count > 0;
    public int LastIndex => Animation.Count > 1 ? Animation.Count - 1 : 1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EditHeight))]
    [NotifyPropertyChangedFor(nameof(EditWidth))]
    public int pixelSize = 10;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EditHeight))]
    [NotifyPropertyChangedFor(nameof(EditWidth))]
    public int pixelGap = 2;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EditHeight))]
    [NotifyPropertyChangedFor(nameof(EditWidth))]
    public int borderWidth = 2;

    [ObservableProperty]
    string hoverPos;


    public int EditWidth => Animation.Width * (pixelSize + pixelGap) + pixelGap;
    public int EditHeight => Animation.Height * (pixelSize + pixelGap) + pixelGap;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AnimationHeight))]
    [NotifyPropertyChangedFor(nameof(AnimationWidth))]
    int animationPixelSize = 5;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AnimationHeight))]
    [NotifyPropertyChangedFor(nameof(AnimationWidth))]
    int animationPixelGap = 2;
    public int AnimationHeight => Animation.Height * (animationPixelSize + animationPixelGap) + animationPixelGap;
    public int AnimationWidth => Animation.Width * (animationPixelSize + animationPixelGap) + animationPixelGap;

    public string Title
    {
        get
        {
            return $"Animation: {FileName ?? "Unknown"}";
        }
        set
        {

        }
    }



    [RelayCommand]
    public void SaveImage()
    {
        var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        var file = Path.Combine(folder, fileName + ".animation");

        MatrixLib.AnimationSerializer.Save(file, Animation);

    }

    [RelayCommand]
    public async void GoBack()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    [RelayCommand]
    public void InsertImage()
    {
        Animation.InsertAt(CurrentEdit, Animation.New(Colors.Black));
        OnPropertyChanged(nameof(LastIndex));
        OnPropertyChanged(nameof(CurrentEdit));
    }

    [RelayCommand]
    public void AddImage()
    {
        Animation.InsertAt(CurrentEdit + 1, Animation.New(Colors.Black));
        CurrentEdit++;
        OnPropertyChanged(nameof(LastIndex));
    }

    [RelayCommand]
    public void CopyImage()
    {
        if (Animation.Copy(CurrentEdit))
        {
            CurrentEdit++;
            OnPropertyChanged(nameof(LastIndex));
        }
    }

    [RelayCommand]
    void MoveImageToStart()
    {
        if (Animation.MoveToStart(CurrentEdit))
        {
            CurrentEdit = 0;
        }
    }

    [RelayCommand]
    void MoveImageBack()
    {
        if (Animation.MoveBack(CurrentEdit))
        {
            CurrentEdit--;
        }
    }
    [RelayCommand]
    void MoveImageForward()
    {
        if (Animation.MoveForward(CurrentEdit))
        {
            CurrentEdit++;
        }
    }

    [RelayCommand]
    void MoveImageToEnd()
    {
        if (Animation.MoveToEnd(CurrentEdit))
        {
            CurrentEdit = Animation.Count - 1;
        }
    }

    [RelayCommand]
    void DeleteImage()
    {
        if (Animation.DeleteAt(CurrentEdit))
        {
            if (CurrentEdit > 0)
            {
                CurrentEdit--;
            }
            if (LastIndex > 0) OnPropertyChanged(nameof(LastIndex));
            OnPropertyChanged(nameof(CurrentEdit));
        }
    }

    [RelayCommand]
    public void ItemTapped(object o)
    {

    }


    [RelayCommand]
    public void StartHoverInteraction(InteractionParams point)
    {
        point = ToPixelPosition(point);
        HoverPos = $"at: ({point.X}, {point.Y})";
    }

    [RelayCommand]
    public void MoveHoverInteraction(InteractionParams point)
    {
        point = ToPixelPosition(point);
        HoverPos = $"at: ({point.X}, {point.Y})";
    }

    [RelayCommand]
    public void EndHoverInteraction()
    {
        HoverPos = "";
    }

    [RelayCommand]
    public void StartInteraction(InteractionParams point)
    {
        switch (selectedTool)
        {
            case GraphicTool.DrawDot:
                SetPixel(point.X, point.Y);
                break;
            case GraphicTool.Fill:
                FillColor(point.X, point.Y);
                break;
        }
    }

    private void FillColor(int x, int y)
    {
        if (SelectedColor == null) return;
        if (CurrentEdit < 0 || CurrentEdit >= Animation.Count) return;

        x = ToPixelPosition(x);
        y = ToPixelPosition(y);

        if (x < 0 || x >= Animation.Width || y < 0 || y >= Animation.Height)
        {
            return;
        }

        var oldColor = Animation[CurrentEdit][x, y];
        if (oldColor == selectedColor.background) return;
        FillColor(x, y, oldColor, selectedColor.background);
        OnPropertyChanged(nameof(CurrentEdit));
    }

    private void FillColor(int x, int y, Color oldColor, Color newColor)
    {
        if (x < 0 || x >= Animation.Width || y < 0 || y >= Animation.Height)
        {
            return;
        }
        if (oldColor == Animation[CurrentEdit][x, y])
        {
            Animation[CurrentEdit][x, y] = newColor;
            FillColor(x+1, y, oldColor, newColor);
            FillColor(x-1, y, oldColor, newColor);
            FillColor(x, y+1, oldColor, newColor);
            FillColor(x, y-1, oldColor, newColor);
        }
    }

    public ObservableCollection<NameColor> NameToColor { get; }

    private void SetPixel(int x, int y)
    {
        if (SelectedColor == null) return;
        if (CurrentEdit < 0 || CurrentEdit >= Animation.Count) return;

        x = ToPixelPosition(x);
        y = ToPixelPosition(y);
        HoverPos = $"at: ({x}, {y})";

        if (x >= 0 && x < Animation.Width && y >= 0 && y < Animation.Height)
        {
            Animation[CurrentEdit][x, y] = SelectedColor.Background;
            OnPropertyChanged(nameof(CurrentEdit));
        }
    }

    private int ToPixelPosition(int x)
    {
        x /= (PixelSize + PixelGap);
        return x;
    }

    InteractionParams ToPixelPosition(InteractionParams point)
    {
        return new InteractionParams
        {
            X = point.X / (PixelSize + PixelGap),
            Y = point.Y / (PixelSize + PixelGap),
        };
    }

    [RelayCommand]
    public void Fill(NameColor color)
    {
        if (CurrentEdit < 0 || CurrentEdit >= Animation.Count - 1) return;
        for (int x = 0; x < Animation.Width; x++)
        {
            for (int y = 0; y < Animation.Height; y++)
            {
                Animation[CurrentEdit][x, y] = color.Background;
            }
        }
        OnPropertyChanged(nameof(CurrentEdit));
    }

}

//public class MyBehavior : Behavior<GraphicsView>
//{
//    protected override void OnAttachedTo(GraphicsView bindable)
//    {
//        base.OnAttachedTo(bindable);
//        // Perform setup
//        bindable.StartInteraction += StartInteraction;
//    }

//    protected override void OnDetachingFrom(GraphicsView bindable)
//    {
//        base.OnDetachingFrom(bindable);
//        // Perform clean up
//        bindable.StartInteraction -= StartInteraction;
//    }

//    // Behavior implementation
//    void StartInteraction(object sender, TouchEventArgs e)
//    {
//    }
//}