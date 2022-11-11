using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixLib;
using MauiMatrix.ViewModels;

namespace MauiMatrix.Services
{
    public class FileIoService
    {
        public List<AnimationFileInfo> ReadFolder()
        {
            var list = new List<AnimationFileInfo>();
            var t = Task.Run(() =>
            {
                var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                foreach (var fileName in Directory.EnumerateFiles(folder))
                {
                    var path = Path.GetFileName(fileName);
                    var data = AnimationSerializer.ReadHeader(path);
                    var animation = new AnimationFileInfo 
                    {
                        FileName = data.FileName,
                        AnimationHeight = data.AnimationHeight,
                        AnimationWidth = data.AnimationWidth,
                        NrOfImages = data.NrOfImages,
                        NrOfColors = data.NrOfColors
                    };
                    list.Add(animation);
                }
            });

            return list;
        }
    }
}
