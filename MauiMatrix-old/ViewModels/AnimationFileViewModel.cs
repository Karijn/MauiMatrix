using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiMatrix.ViewModels;

public partial class AnimationFileViewModel : BaseViewModel
{
    public AnimationFileViewModel()
    {
        Images = new ObservableCollection<AnimationImageViewModel>();
        Width = 0;
        Height = 0;
    }


    [ObservableProperty]
    string fileName;

    [ObservableProperty]
    int width;

    [ObservableProperty]
    int height;

    [ObservableProperty]
    ObservableCollection<AnimationImageViewModel> images;
}
