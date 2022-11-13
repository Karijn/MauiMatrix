using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiMatrix.ViewModels;

[ObservableObject]
public partial class MatrixRect
{
    //[ObservableProperty]
    //int index;

    [ObservableProperty]
    int x;

    [ObservableProperty]
    int y;

    [ObservableProperty]
    Color color;

    public MatrixRect(int x, int y, Color color)
    {
        //Index = y * 1000 + x;
        X = x;
        Y = y;
        Color = color;
    }

    public override string ToString()
    {
        return $"{X} {Y} {Color}";
    }
}
