using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Polestar;

public partial class MainWindow : Window
{
    private UserControl? current;

    public MainWindow()
    {
        InitializeComponent();
        HomeNav.Checked += Navigate;
        GalleryNav.Checked += Navigate;
        LoginNav.Checked += Navigate;
        HomeNav.IsChecked = true;
    }

    private void OpenGallery(object? sender, EventArgs e)
    {
        GalleryNav.IsChecked = true;
        GalleryNav.Focus();
    }

    private void Navigate(object sender, RoutedEventArgs e)
    {
        var next = sender == HomeNav ? (UserControl)Home : sender == GalleryNav ? (UserControl)Gallery : Login;
        if (current != null)
        {
            current.BeginAnimation(OpacityProperty, null);
            current.Visibility = Visibility.Collapsed;
        }
        if (current == Login)
            Login.Reset();

        current = next;
        next.Visibility = Visibility.Visible;
        if (ReduceMotion.IsChecked != true && SystemParameters.ClientAreaAnimation && !SystemParameters.HighContrast)
        {
            next.BeginAnimation(OpacityProperty, new DoubleAnimation
            {
                From = 0.78,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(160),
                FillBehavior = FillBehavior.Stop
            });
        }
    }
}

