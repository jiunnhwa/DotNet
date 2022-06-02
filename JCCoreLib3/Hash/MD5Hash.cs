// Decompiled with JetBrains decompiler
using System;
using System.Security.Cryptography;
using System.Text;

namespace JCCoreLib3.Hash
{
  public class MD5Hash
  {
    private MD5Hash()
    {
    }

    public static string GetMD5Hash(string TextToHash)
    {
      switch (TextToHash)
      {
        case "":
        case null:
          return string.Empty;
        default:
          return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.Default.GetBytes(TextToHash)));
      }
    }

    public static string CreateMD5(string input)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
          stringBuilder.Append(hash[index].ToString("x2"));
        return stringBuilder.ToString();
      }
    }

    private string StringToHash(string data, string salt, HashAlgorithm algorithm)
    {
      byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data + salt);
      return BitConverter.ToString(algorithm.ComputeHash(bytes));
    }
  }
}
