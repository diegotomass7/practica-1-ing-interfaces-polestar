using System;
using System.Net.Mail;

namespace Polestar;

internal enum AccessField { None, Email, Password }

internal static class DemoAccess
{
    internal static (string Message, AccessField Field) Check(string email, string password)
    {
        email = email.Trim();
        if (email.Length == 0)
            return ("Introduce el correo electrónico.", AccessField.Email);
        if (!MailAddress.TryCreate(email, out var address) || address.Address != email ||
            !address.Host.Contains(".", StringComparison.Ordinal))
            return ("Introduce un correo con formato nombre@dominio.es.", AccessField.Email);
        if (string.IsNullOrWhiteSpace(password))
            return ("Introduce la contraseña.", AccessField.Password);
        if (!email.Equals("cliente@example.com", StringComparison.OrdinalIgnoreCase) || password != "PolestarDemo1")
            return ("Correo o contraseña incorrectos. Comprueba los datos de demostración.", AccessField.Password);

        return ("Identificación correcta. Ya puedes continuar explorando la galería.", AccessField.None);
    }
}
