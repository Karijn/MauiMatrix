using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiMatrix.ViewModels;

[QueryProperty("Filename", "Filename")]
public partial class PropertyViewModel : ObservableObject
{
    [ObservableProperty]
    string filename;

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}