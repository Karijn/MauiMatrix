using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MatrixLib;
using MauiMatrix.Services;
using MauiMatrix.Views;

namespace MauiMatrix.ViewModels
{

    public partial class AnimationFileInfo : ObservableObject
    {
        [ObservableProperty]
        string fileName;

        [ObservableProperty]
        bool isNewFile = false;

        [ObservableProperty]
        int animationWidth;

        [ObservableProperty]
        int animationHeight;

        [ObservableProperty]
        int nrOfImages;

        [ObservableProperty]
        int nrOfColors;

        public string SizeString
        {
            get
            {
                return $"({animationWidth}, {animationHeight})";
            }
        }
    }

    public partial class HomeViewModel : BaseViewModel
    {
        string mainDir;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy = false;

        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        string title = "Home";

        List<Point> points = new List<Point>();

        //public AnimationViewModel AnimationVM { get; }

        [ObservableProperty]
        ObservableCollection<AnimationFileInfo> files = new();


        private readonly FileIoService fileIoService;

        public HomeViewModel(FileIoService fileIoService)
        {
            //CreateAnimation();

            mainDir = FileSystem.Current.AppDataDirectory;
            this.fileIoService = fileIoService;
            //AnimationVM = animationVM;

            //WriteFiles();

            CleanDirectory();
            ReadDirectory();
        }

        private void CreateAnimation()
        {
            points.Add(new Point(4, 1));
            points.Add(new Point(5, 1));
            points.Add(new Point(6, 2));
            points.Add(new Point(7, 3));
            points.Add(new Point(7, 4));
            points.Add(new Point(7, 5));
            points.Add(new Point(6, 6));
            points.Add(new Point(5, 7));
            points.Add(new Point(4, 7));
            points.Add(new Point(3, 7));
            points.Add(new Point(2, 6));
            points.Add(new Point(1, 5));
            points.Add(new Point(1, 4));
            points.Add(new Point(1, 3));
            points.Add(new Point(2, 2));
            points.Add(new Point(3, 1));
        }

        [RelayCommand]
        public async void NewAnimation()
        {
            await Shell.Current.GoToAsync(nameof(NewAnimationPage), true);
        }

        [RelayCommand]
        public void RefreshFiles()
        {
            ReadDirectory();
        }

        [RelayCommand]
        public async void GoToDetails(AnimationFileInfo animationFileInfo)
        {
            if (animationFileInfo == null)
                return;

            var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var file = Path.Combine(folder, animationFileInfo.FileName );

            await Shell.Current.GoToAsync(nameof(AnimationPage), true, new Dictionary<string, object>
            {
                {"AnimationFileInfo", animationFileInfo }
            });

        }

        private void ReadDirectory()
        {
            IsBusy = true;
            try
            {
                files.Clear();
                var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                foreach (var fileName in Directory.EnumerateFiles(folder, "*.animation"))
                {
                    var path = Path.GetFileName(fileName);
                    var data = AnimationSerializer.ReadHeader(fileName);
                    var animation = new AnimationFileInfo
                    {
                        FileName = path,
                        AnimationHeight = data.AnimationHeight,
                        AnimationWidth = data.AnimationWidth,
                        NrOfImages = data.NrOfImages,
                        NrOfColors = data.NrOfColors
                    };
                    files.Add(animation);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void CleanDirectory() 
        {
            RemoveFileAsync("File_1-(10-10).animation");
            RemoveFileAsync("File_2-(10-10).animation");
            RemoveFileAsync("File_3-(10-10).animation");
            RemoveFileAsync("File_4-(10-10).animation");
            RemoveFileAsync("File_5-(10-10).animation");
            RemoveFileAsync(".animation");
        }

        private void WriteFiles()
        {
            WriteDummyFileAsync("File_1-(10-10).animation");
            WriteDummyFileAsync("File_2-(10-10).animation");
            WriteDummyFileAsync("File_3-(10-10).animation");
        }

        private void RemoveFileAsync(string filename)
        {
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
            if (File.Exists(backingFile))
            File.Delete(backingFile);
        }

        private void DrawOn(AnimationImage img, int len)
        {
            for (int i = 0; i < len; i++)
            {
                img[(int)points[i].X, (int)points[i].Y] = Colors.White;
            }
        }

        private void WriteDummyFileAsync(string filename)
        {
            MatrixLib.Animation animation = new MatrixLib.Animation(10, 10);
            AnimationImage img;

            for (int i = 0; i <= points.Count; i++)
            {
                img = animation.Add(animation.New(Colors.DarkGoldenrod));
                DrawOn(img, i);
            }
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);

            if (backingFile == null /*|| File.Exists(backingFile)*/)
            {
                return;
            }
            MatrixLib.AnimationSerializer.Save(backingFile, animation);
        }
    }
}
