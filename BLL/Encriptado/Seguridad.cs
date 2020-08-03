
using System;
using System.Security.Cryptography;
using System.Text;
/// Esta clase contiene funciones para encriptar/desencriptar
/// El ser estática no es necesario instanciar un objeto para 
/// usar las funciones Encriptar y DesEncriptar
public static class Seguridad
{
    /*public static string GetEncriptado(string str)
    {
        SHA256 sha256 = SHA256Managed.Create();
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] stream = null;
        StringBuilder sb = new StringBuilder();
        stream = sha256.ComputeHash(encoding.GetBytes(str));
        for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
        return sb.ToString();
    }*/

    /// Encripta una cadena
    public static string Encriptar(this string encriptarCadena)
    {
        string result = string.Empty;
        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(encriptarCadena);
        result = Convert.ToBase64String(encryted);
        return result;
    }

    /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
    public static string DesEncriptar(this string desencriptarCadena)
    {
        string result = string.Empty;
        byte[] decryted = Convert.FromBase64String(desencriptarCadena);
        //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
        result = System.Text.Encoding.Unicode.GetString(decryted);
        return result;
    }
}