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


}