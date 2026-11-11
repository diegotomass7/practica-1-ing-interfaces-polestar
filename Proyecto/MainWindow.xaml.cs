using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Automation;
using System.Windows.Input;
using System.Windows.Media;
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
        HomeNav.Click += SelectMenuItem;
        GalleryNav.Click += SelectMenuItem;
        LoginNav.Click += SelectMenuItem;
    }

    private void OpenGallery(object? sender, EventArgs e)
    {
        GalleryNav.IsChecked = true;
        MenuButton.Focus();
    }

    private void ToggleMenu(object sender, RoutedEventArgs e) =>
        SetMenu(MenuOverlay.Visibility != Visibility.Visible);

    private void DismissMenu(object sender, MouseButtonEventArgs e) => SetMenu(false);
    private void SelectMenuItem(object sender, RoutedEventArgs e) => SetMenu(false);

    private void SetMenu(bool open)
    {
        if (open == (MenuOverlay.Visibility == Visibility.Visible))
            return;

        MenuOverlay.Visibility = open ? Visibility.Visible : Visibility.Collapsed;
        ContentHost.IsEnabled = !open;
        AutomationProperties.SetName(MenuButton, open ? "Cerrar menú" : "Abrir menú");

        if (open)
        {
            var selected = HomeNav.IsChecked == true ? HomeNav :
                GalleryNav.IsChecked == true ? GalleryNav : LoginNav;
            Dispatcher.InvokeAsync(() =>
            {
                if (MenuOverlay.IsVisible)
                    selected.Focus();
            }, System.Windows.Threading.DispatcherPriority.Loaded);
        }
        else
        {
            MenuButton.Focus();
        }
    }

    private void HandleMenuKeys(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape && MenuOverlay.IsVisible)
        {
            SetMenu(false);
            e.Handled = true;
            return;
        }

        if (e.Key != Key.System || Keyboard.Modifiers != ModifierKeys.Alt)
            return;

        if (e.SystemKey == Key.M)
        {
            SetMenu(!MenuOverlay.IsVisible);
            e.Handled = true;
            return;
        }

        var item = e.SystemKey switch
        {
            Key.I => HomeNav,
            Key.G => GalleryNav,
            Key.D => LoginNav,
            _ => null
        };
        if (item == null)
            return;

        item.IsChecked = true;
        SetMenu(false);
        e.Handled = true;
    }

    private void Navigate(object sender, RoutedEventArgs e)
    {
        var next = sender == HomeNav ? (UserControl)Home : sender == GalleryNav ? (UserControl)Gallery : Login;
        if (current != null)
        {
            current.BeginAnimation(OpacityProperty, null);
            current.RenderTransform = Transform.Identity;
            current.Visibility = Visibility.Collapsed;
        }
        if (current == Login)
            Login.Reset();

        current = next;
        next.Visibility = Visibility.Visible;
        if (ReduceMotion.IsChecked != true && SystemParameters.ClientAreaAnimation && !SystemParameters.HighContrast)
        {
            var offset = new TranslateTransform();
            next.RenderTransform = offset;
            offset.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(8, 0, TimeSpan.FromMilliseconds(200))
            {
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut },
                FillBehavior = FillBehavior.Stop
            });
            next.BeginAnimation(OpacityProperty, new DoubleAnimation
            {
                From = 0.82,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(200),
                FillBehavior = FillBehavior.Stop
            });
        }
    }
}



