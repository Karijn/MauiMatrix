using CommunityToolkit.Mvvm.ComponentModel;

namespace MatrixLib;

public class AnimationFileData
{
    public string FileName { get; set; }
    public bool IsNewFile { get; set; }
    public int AnimationWidth { get; set; }
    public int AnimationHeight { get; set; }
    public int NrOfImages { get; set; }
    public int NrOfColors { get; set; }
}