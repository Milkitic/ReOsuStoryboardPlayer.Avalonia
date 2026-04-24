using System.Text.Json.Serialization.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using ReOsuStoryboardPlayer.Avalonia.Utils;
using SkiaSharp;

namespace ReOsuStoryboardPlayer.Avalonia.Models;

public partial class StoryboardPlayerSetting : ObservableObject
{
    [ObservableProperty]
    public partial bool AntiAliasing { get; set; } = false;

    public enum WASAPIPeriod
    {
        Minimal,
        Default,
        Maximal,
    }

    [ObservableProperty]
    public partial WASAPIPeriod WindowsAudioPeriod { get; set; } = WASAPIPeriod.Minimal;

    [ObservableProperty]
    public partial int AudioSampleRate { get; set; } = 48000;

    /// <summary>
    ///     if beatmap's AudioLeadIn is not set, use DefaultAudioLeadInSeconds
    /// </summary>
    [ObservableProperty]
    public partial float DefaultAudioLeadInSeconds { get; set; } = 1.8f;

    [ObservableProperty]
    public partial StoryboardFilterQuality FilterQuality { get; set; } = StoryboardFilterQuality.Low;

    /// <summary>
    ///     if beatmap's AudioLeadIn is zero, set DefaultAudioLeadInSeconds as well.
    /// </summary>
    [ObservableProperty]
    public partial bool ForceApplyDefaultAudioLeadInIfValueIsZero { get; set; }

    [ObservableProperty]
    public partial WideScreenOption WideScreenOption { get; set; } = WideScreenOption.Auto;

    public static JsonTypeInfo<StoryboardPlayerSetting> JsonTypeInfo =>
        JsonSourceGenerationContext.Default.StoryboardPlayerSetting;

    public bool AllowLog
    {
        get => Setting.AllowLog;
        set
        {
            Setting.AllowLog = value;
            OnPropertyChanged();
        }
    }

    public bool DebugMode
    {
        get => Setting.DebugMode;
        set
        {
            Setting.DebugMode = value;
            OnPropertyChanged();
        }
    }

    public bool EnableSplitMoveScaleCommand
    {
        get => Setting.EnableSplitMoveScaleCommand;
        set
        {
            Setting.EnableSplitMoveScaleCommand = value;
            OnPropertyChanged();
        }
    }

    public bool EnableLoopCommandUnrolling
    {
        get => Setting.EnableLoopCommandUnrolling;
        set
        {
            Setting.EnableLoopCommandUnrolling = value;
            OnPropertyChanged();
        }
    }

    public int UpdateThreadCount
    {
        get => Setting.UpdateThreadCount;
        set
        {
            Setting.UpdateThreadCount = value;
            OnPropertyChanged();
        }
    }

    public int ParallelParseCommandLimitCount
    {
        get => Setting.ParallelParseCommandLimitCount;
        set
        {
            Setting.ParallelParseCommandLimitCount = value;
            OnPropertyChanged();
        }
    }

    public int ParallelUpdateObjectsLimitCount
    {
        get => Setting.ParallelUpdateObjectsLimitCount;
        set
        {
            Setting.ParallelUpdateObjectsLimitCount = value;
            OnPropertyChanged();
        }
    }

    public bool FunReverseEasing
    {
        get => Setting.FunReverseEasing;
        set
        {
            Setting.FunReverseEasing = value;
            OnPropertyChanged();
        }
    }

    public bool ShowProfileSuggest
    {
        get => Setting.ShowProfileSuggest;
        set
        {
            Setting.ShowProfileSuggest = value;
            OnPropertyChanged();
        }
    }
}

public enum StoryboardFilterQuality
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3,
}

public static class StoryboardFilterQualityExtensions
{
    public static SKSamplingOptions ToSamplingOptions(this StoryboardFilterQuality quality)
    {
        return quality switch
        {
            StoryboardFilterQuality.None => new SKSamplingOptions(SKFilterMode.Nearest),
            StoryboardFilterQuality.Low => new SKSamplingOptions(SKFilterMode.Linear),
            StoryboardFilterQuality.Medium => new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear),
            StoryboardFilterQuality.High => new SKSamplingOptions(SKCubicResampler.Mitchell),
            _ => new SKSamplingOptions(SKFilterMode.Linear),
        };
    }
}
