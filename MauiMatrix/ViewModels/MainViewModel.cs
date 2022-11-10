using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Android.Renderscripts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Networking;
using CommunityToolkit.Maui.Core.Views;

namespace MauiMatrix.ViewModels;

[ObservableObject]
public partial class MainViewModel
{
    int buttonSize = 30;

    [ObservableProperty]
    bool checkColor0 = true;

    [ObservableProperty]
    bool checkColor1;

    [ObservableProperty]
    bool checkColor2;

    [ObservableProperty]
    bool checkColor3;

    [ObservableProperty]
    bool checkColor4;

    [ObservableProperty]
    bool checkColor5;

    [ObservableProperty]
    bool checkColor6;

    [ObservableProperty]
    bool checkColor7;

    [ObservableProperty]
    string filename;


    [ObservableProperty]
    Color color0 = Color.Parse("Black");

    [ObservableProperty]
    Color color1 = Color.Parse("Red");

    [ObservableProperty]
    Color color2 = Color.Parse("Green");

    [ObservableProperty]
    Color color3 = Color.Parse("Blue");

    [ObservableProperty]
    Color color4 = Color.Parse("Yellow");

    [ObservableProperty]
    Color color5 = Color.Parse("Orange");

    [ObservableProperty]
    Color color6 = Color.Parse("Pink");

    [ObservableProperty]
    Color color7 = Color.Parse("Gold");

    [ObservableProperty]
    ObservableCollection<string> animationFiles;
    //ObservableCollection<AnimationFileViewModel> animationFiles;

    [ObservableProperty]
    ObservableCollection<MatrixRect> rectangles;

    [ObservableProperty]
    RowDefinitionCollection rowDefinitions;

    [ObservableProperty]
    ColumnDefinitionCollection columnDefinitions;

    [ObservableProperty]
    string text = "Click Me!";

    [ObservableProperty]
    int rows;

    [ObservableProperty]
    int columns;

    public ObservableCollection<IDrawingLine> Lines { get; } = new();

    public MainViewModel()
    {
        RowDefinitions = new RowDefinitionCollection();
        ColumnDefinitions = new ColumnDefinitionCollection();
        Rectangles = SetRowsAndColumns(10, 10);

        //AnimationFiles = new ObservableCollection<AnimationFileViewModel>();
        //AnimationFiles.Add(new AnimationFileViewModel { FileName = "File 1" });
        //AnimationFiles.Add(new AnimationFileViewModel { FileName = "File 2" });
        //AnimationFiles.Add(new AnimationFileViewModel { FileName = "File 3" });

        AnimationFiles = new ObservableCollection<string>();
        AnimationFiles.Add("File 1");
        AnimationFiles.Add("File 2");
        AnimationFiles.Add("File 3");

        Lines.Add(CreateRect(10, 10, 10, color1));
    }

    ObservableCollection<MatrixRect> SetRowsAndColumns(int columns, int rows)
    {
        ObservableCollection<MatrixRect> ret = new ObservableCollection<MatrixRect>();
        for (int r = 0; r < rows; r++)
        {
            RowDefinitions.Add(new RowDefinition(new GridLength(buttonSize, GridUnitType.Absolute)));
        }
        for (int c = 0; c < columns; c++)
        {
            ColumnDefinitions.Add(new ColumnDefinition(new GridLength(buttonSize, GridUnitType.Absolute)));
        }
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                ret.Add(new MatrixRect(c, r, Color.FromRgb(0, 0, 0)));
            }
        }
        return ret;
    }

    [RelayCommand]
    public void IncrementRows()
    {
        RowDefinitions.Add(new RowDefinition(new GridLength(buttonSize, GridUnitType.Absolute)));

        for (int y = 0; y < ColumnDefinitions.Count; y++)
        {
            Rectangles.Add(new MatrixRect(y, RowDefinitions.Count - 1, Color.FromRgb(0, 0, 0)));
        }
    }

    [RelayCommand]
    public void DecrementRows()
    {
        if (RowDefinitions.Count > 1)
        {
            RowDefinitions.RemoveAt(RowDefinitions.Count - 1);

            List<MatrixRect> rs = new List<MatrixRect>(Rectangles.Where(r => r.Y == RowDefinitions.Count));
            foreach (var r in rs)
            {
                Rectangles.Remove(r);
            }
        }
    }

    [RelayCommand]
    public void CurrentColorChanged(string colorIndex)
    {
        var color = int.Parse(colorIndex) switch
        {
            0 => Color0,
            1 => Color1,
            2 => Color2,
            3 => Color3,
            4 => Color4,
            5 => Color5,
            6 => Color6,
            7 => Color7,
            _ => Color0
        };

        Rectangles[0] = new MatrixRect(10, 10, color);
        Lines[0] = CreateRect(10, 10, 10, color);
        
    }

    DrawingLine CreateRect(int x, int y, int w, Color color)
    {
        return new DrawingLine
        {
            ShouldSmoothPathWhenDrawn = true,
            LineColor = color,
            LineWidth = 4,
            Points = new ObservableCollection<PointF>
        {
            new PointF(x, y),
            new PointF(x, y + w),
            new PointF(x+w, y + w),
            new PointF(x+w, y),
            new PointF(x, y)
        }
        };
    }


    [RelayCommand]
    public void IncrementColumns()
    {
        ColumnDefinitions.Add(new ColumnDefinition(new GridLength(buttonSize, GridUnitType.Absolute)));

        for (int x = 0; x < RowDefinitions.Count; x++)
        {
            Rectangles.Add(new MatrixRect(ColumnDefinitions.Count - 1, x, Color.FromRgb(0, 0, 0)));
        }
    }

    [RelayCommand]
    public void DecrementColumns()
    {
        if (ColumnDefinitions.Count > 1)
        {
            ColumnDefinitions.RemoveAt(ColumnDefinitions.Count - 1);
        }
        List<MatrixRect> rs = new List<MatrixRect>(Rectangles.Where(r => r.X == ColumnDefinitions.Count));
        foreach (var r in rs)
        {
            Rectangles.Remove(r);
        }
    }

    [RelayCommand]
    public void Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        //if (connectivity.NetworkAccess != NetworkAccess.Internet)
        //{
        //    await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
        //    return;
        //}
        //try
        //{
        //    //AnimationFiles.Add(new AnimationFileViewModel { FileName = Text });
        //    AnimationFiles.Add(Text);
        //}
        //catch (Exception e)
        //{

        //}

        // add our item
        Text = string.Empty;
    }

    [RelayCommand]
    public void SetColor(int index)
    {

        Rectangles.First(r => r.Index == index).Color = GetSelectedColor();
    }

    private Color GetSelectedColor()
    {
        if (checkColor0) return Color0;
        if (checkColor1) return Color1;
        if (checkColor2) return Color2;
        if (checkColor3) return Color3;
        if (checkColor4) return Color4;
        if (checkColor5) return Color5;
        if (checkColor6) return Color6;
        if (checkColor7) return Color7;
        return Color0;
    }

    [RelayCommand]
    public void Delete(string file)
    {
        //try
        //{
        //    if (AnimationFiles.Contains(file))
        //    {
        //        AnimationFiles.Remove(file);
        //    }
        //}
        //catch (Exception e)
        //{

        //}
    }

    //[RelayCommand]
    //async Task Tap(string s)
    //{
    //    await Shell.Current.GoToAsync($"{nameof(PropertyPage)}?Filename={s}");
    //}


    [RelayCommand]
    async Task ShowProperties()
    {
        await Shell.Current.GoToAsync($"{nameof(PropertyPage)}?Filename=ABCD");
    }
}