using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Polestar;

public partial class GalleryView : UserControl
{
    private sealed record Vehicle(string Name, string Body, string Version, string Price, string Range, string Power);

    private static readonly Dictionary<string, Vehicle> Vehicles = new()
    {
        ["polestar-2"] = new("Polestar 2", "Fastback eléctrico", "Standard range Single motor", "42.000 €", "554 km", "200 kW"),
        ["polestar-3"] = new("Polestar 3", "SUV eléctrico", "Rear motor", "82.000 €", "603 km", "245 kW"),
        ["polestar-4-coupe"] = new("Polestar 4 coupé", "SUV coupé eléctrico", "Rear motor", "66.000 €", "620 km", "200 kW"),
        ["polestar-4-suv"] = new("Polestar 4 SUV", "SUV eléctrico de silueta familiar", "Rear motor", "56.000 €", "630 km", "200 kW"),
        ["polestar-5"] = new("Polestar 5", "Gran turismo eléctrico", "Dual motor", "122.000 €", "678 km", "550 kW"),
    };

    private Button? lastCard;
    private double catalogOffset;

    public GalleryView() => InitializeComponent();

    private void OpenDetail(object sender, RoutedEventArgs e)
    {
        var card = (Button)sender;
        var model = (string)card.Tag;
        var vehicle = Vehicles[model];

        lastCard = card;
        catalogOffset = CatalogScroll.VerticalOffset;
        DetailName.Text = vehicle.Name;
        DetailDescription.Text = $"{vehicle.Body} · {vehicle.Version}";
        DetailPrice.Text = vehicle.Price;
        DetailRange.Text = vehicle.Range;
        DetailPower.Text = vehicle.Power;
        DetailNote.Text = model == "polestar-5"
            ? "Autonomía WLTP objetivo de la versión indicada."
            : "Datos técnicos de la versión indicada.";
        DetailPhoto.Source = new BitmapImage(new Uri($"pack://application:,,,/Polestar;component/Assets/{model}.jpg"));
        AutomationProperties.SetName(DetailPhoto, $"Imagen del {vehicle.Name}");

        CatalogScroll.Visibility = Visibility.Collapsed;
        DetailScroll.Visibility = Visibility.Visible;
        DetailScroll.ScrollToTop();
        Dispatcher.InvokeAsync(() =>
        {
            if (DetailScroll.IsVisible)
                BackButton.Focus();
        }, DispatcherPriority.Loaded);
        if (UIElementAutomationPeer.CreatePeerForElement(DetailName) is AutomationPeer peer)
            peer.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);
    }

    private void CloseDetail(object sender, RoutedEventArgs e) => RestoreCatalog();

    private void HandleNavigationKey(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Escape || DetailScroll.Visibility != Visibility.Visible)
            return;

        RestoreCatalog();
        e.Handled = true;
    }

    private void RestoreCatalog()
    {
        DetailScroll.Visibility = Visibility.Collapsed;
        CatalogScroll.Visibility = Visibility.Visible;
        Dispatcher.InvokeAsync(() =>
        {
            if (!CatalogScroll.IsVisible)
                return;

            lastCard?.Focus();
            CatalogScroll.ScrollToVerticalOffset(catalogOffset);
        }, DispatcherPriority.Loaded);
    }
}


