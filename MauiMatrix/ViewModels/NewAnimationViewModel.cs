using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MatrixLib;
using MauiMatrix.Views;

namespace MauiMatrix.ViewModels
{
    public partial class NewAnimationViewModel : BaseViewModel
    {
        public NewAnimationViewModel() 
        {
        }

        [ObservableProperty]
        int animationWidth = 5;

        [ObservableProperty]
        int animationHeight = 7;

        [ObservableProperty]
        string fileName = "Test";

        [RelayCommand]
        public async void CreateAnimation()
        {
            //var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //var file = Path.Combine(folder, FileName + ".animation");

            var info = new AnimationFileInfo
            {
                FileName = FileName,
                AnimationWidth = AnimationWidth,
                AnimationHeight = AnimationHeight,
                IsNewFile = true
            };
            
            await Shell.Current.GoToAsync("../" + nameof(AnimationPage), true, new Dictionary<string, object>
            {
                {"AnimationFileInfo", info}
            });

        }
    }
}
