using System;
using System.Security.Cryptography;
using System.Text;
public class Encrypt
{
    public string encriptarSHA512(string contrasena)
    {
        using(var sha512 = SHA512.Create())
        {
            var bytesContrasena = Encoding.UTF8.GetBytes(contrasena);
            var hashContrasena = sha512.ComputeHash(bytesContrasena);
            return Convert.ToBase64String(hashContrasena);
        }
    }

    public bool verificarContrasena(string contrasenaIngresada, string hashContrasenaAlmacenada)
    {
        using( var sha512 = SHA512.Create())
        {
            var bytesContrasenaIngresada = Encoding.UTF8.GetBytes(contrasenaIngresada);
            var hashContrasenaIngresada = sha512.ComputeHash(bytesContrasenaIngresada);
            return Convert.ToBase64String(hashContrasenaIngresada) == hashContrasenaAlmacenada;
        }
    }


}