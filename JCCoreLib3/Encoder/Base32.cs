using System;
using System.Text;

namespace JCCoreLib3.Encoder
{
  public class Base32
  {
    private const int InByteSize = 8;
    private const int OutByteSize = 5;
    private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

    public static string ToBase32String(byte[] bytes)
    {
      if (bytes == null)
        return (string) null;
      if (bytes.Length == 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder(bytes.Length * 8 / 5);
      int index1 = 0;
      int num1 = 0;
      byte index2 = 0;
      int num2 = 0;
      while (index1 < bytes.Length)
      {
        int num3 = Math.Min(8 - num1, 5 - num2);
        index2 = (byte) ((uint) (byte) ((uint) index2 << num3) | (uint) (byte) ((uint) bytes[index1] >> 8 - (num1 + num3)));
        num1 += num3;
        if (num1 >= 8)
        {
          ++index1;
          num1 = 0;
        }
        num2 += num3;
        if (num2 >= 5)
        {
          index2 &= (byte) 31;
          stringBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"[(int) index2]);
          num2 = 0;
        }
      }
      if (num2 > 0)
      {
        byte index3 = (byte) ((uint) (byte) ((uint) index2 << 5 - num2) & 31U);
        stringBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"[(int) index3]);
      }
      return stringBuilder.ToString();
    }

    public static byte[] FromBase32String(string base32String)
    {
      if (base32String == null)
        return (byte[]) null;
      if (base32String == string.Empty)
        return new byte[0];
      string upperInvariant = base32String.ToUpperInvariant();
      byte[] numArray = new byte[upperInvariant.Length * 5 / 8];
      if (numArray.Length == 0)
        throw new ArgumentException("Specified string is not valid Base32 format because it doesn't have enough data to construct a complete byte array");
      int index1 = 0;
      int num1 = 0;
      int index2 = 0;
      int num2 = 0;
      while (index2 < numArray.Length)
      {
        int num3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".IndexOf(upperInvariant[index1]);
        if (num3 < 0)
          throw new ArgumentException(string.Format("Specified string is not valid Base32 format because character \"{0}\" does not exist in Base32 alphabet", (object) base32String[index1]));
        int num4 = Math.Min(5 - num1, 8 - num2);
        numArray[index2] <<= num4;
        numArray[index2] |= (byte) (num3 >> 5 - (num1 + num4));
        num2 += num4;
        if (num2 >= 8)
        {
          ++index2;
          num2 = 0;
        }
        num1 += num4;
        if (num1 >= 5)
        {
          ++index1;
          num1 = 0;
        }
      }
      return numArray;
    }
  }
}
