param([string]$Project = (Join-Path $PSScriptRoot '../Proyecto'))
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName PresentationFramework
$app = [System.Windows.Application]::new()
$app.Resources = [System.Windows.Markup.XamlReader]::Parse((Get-Content "$Project/Theme.xaml" -Raw))
foreach ($name in 'HomeView','GalleryView','LoginView','MainWindow') {
    $raw = (Get-Content "$Project/$name.xaml" -Raw) -replace ' x:Class="[^"]+"| Click="[^"]+"','' -replace '<local:[^>]*/>',''
    $element = [System.Windows.Markup.XamlReader]::Parse($raw)
    $element.Measure([System.Windows.Size]::new(600,460))
    $element.Arrange([System.Windows.Rect]::new(0,0,600,460))
    "$name : XAML cargado y dispuesto"
}
$source = (Get-Content "$Project/DemoAccess.cs" -Raw) + @"
 
public static class Checks
{
    public static string[] Run()
    {
        var cases = new (string Email, string Password, AccessField Field, string Prefix)[]
        {
            ("", "", AccessField.Email, "Introduce el correo"),
            ("invalido", "", AccessField.Email, "Introduce un correo"),
            ("cliente@example.com", "", AccessField.Password, "Introduce la contraseña"),
            ("cliente@example.com", "incorrecta", AccessField.Password, "Correo o contraseña"),
            ("otro@example.com", "PolestarDemo1", AccessField.Password, "Correo o contraseña"),
            (" CLIENTE@example.com ", "PolestarDemo1", AccessField.None, "Identificación correcta")
        };
        var report = new string[cases.Length];
        for (int index = 0; index < cases.Length; index++)
        {
            var test = cases[index];
            var result = DemoAccess.Check(test.Email, test.Password);
            if (result.Field != test.Field || !result.Message.StartsWith(test.Prefix, StringComparison.Ordinal))
                throw new InvalidOperationException("Fallo en el caso " + (index + 1) + ": " + result);
            report[index] = "Caso " + (index + 1) + ": " + result.Message;
        }
        return report;
    }
}
"@
Add-Type -TypeDefinition $source
[Polestar.Checks]::Run()
