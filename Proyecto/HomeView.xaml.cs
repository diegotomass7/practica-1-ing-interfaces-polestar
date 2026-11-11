using System;
using System.Windows;
using System.Windows.Controls;

namespace Polestar;

public partial class HomeView : UserControl
{
    public event EventHandler? GalleryRequested;
    public HomeView() => InitializeComponent();

    private void Explore(object sender, RoutedEventArgs e)
        => GalleryRequested?.Invoke(this, EventArgs.Empty);
}
