using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiMatrix.ViewModels
{
    public partial class AnimationFileInfo : BaseViewModel
    {
        [ObservableProperty]
        string fileName;

        [ObservableProperty]
        bool isNewFile = false;

        [ObservableProperty]
        int animationWidth;

        [ObservableProperty]
        int animationHeight;
    }
}
