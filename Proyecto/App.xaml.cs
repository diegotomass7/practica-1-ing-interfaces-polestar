using System.ComponentModel;
using System.Windows;

namespace Polestar;

public partial class App : Application
{
    private readonly ResourceDictionary contrast = new();

    protected override void OnStartup(StartupEventArgs e)
    {
        ApplyContrast();
        SystemParameters.StaticPropertyChanged += OnSystemParametersChanged;
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        SystemParameters.StaticPropertyChanged -= OnSystemParametersChanged;
        base.OnExit(e);
    }

    private void OnSystemParametersChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SystemParameters.HighContrast))
            Dispatcher.InvokeAsync(ApplyContrast);
    }

    private void ApplyContrast()
    {
        Resources.MergedDictionaries.Remove(contrast);
        if (!SystemParameters.HighContrast)
            return;

        contrast["CanvasBrush"] = SystemColors.WindowBrush;
        contrast["SurfaceBrush"] = SystemColors.WindowBrush;
        contrast["ElevatedBrush"] = SystemColors.ControlBrush;
        contrast["StrokeBrush"] = SystemColors.WindowTextBrush;
        contrast["InputStrokeBrush"] = SystemColors.WindowTextBrush;
        contrast["TextBrush"] = SystemColors.WindowTextBrush;
        contrast["MutedBrush"] = SystemColors.WindowTextBrush;
        contrast["AccentBrush"] = SystemColors.HighlightBrush;
        contrast["AccentHoverBrush"] = SystemColors.HighlightBrush;
        contrast["AccentPressedBrush"] = SystemColors.HighlightBrush;
        contrast["OnAccentBrush"] = SystemColors.HighlightTextBrush;
        Resources.MergedDictionaries.Add(contrast);
    }
}
