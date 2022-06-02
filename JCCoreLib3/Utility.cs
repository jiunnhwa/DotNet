using JCCoreLib3.Encoder;
using System;

namespace JCCoreLib3
{
  public class Utility
  {
    private Utility()
    {
    }

    public class Formatting
    {
      private Formatting()
      {
      }

      public static string ExceptionFormat(Exception ex) => string.Format("Exception:\t{1}{0}Message:\t{2}{0}Source:\t\t{3}{0}TargetSite:\t{4}{0}{0}StackTrace{0}--------------{0}{0}{5}{0}", (object) Environment.NewLine, (object) ex.GetType(), (object) ex.Message.Replace("\r", "").Replace('\n', '\t'), (object) ex.Source, (object) ex.TargetSite.ToString(), (object) ex.StackTrace);
    }

    public class Encoding
    {
      private Encoding()
      {
      }

      public static string Encode32(string txt) => Base32.ToBase32String(System.Text.Encoding.ASCII.GetBytes(txt));

      public static string Decode32(string enctxt) => System.Text.Encoding.ASCII.GetString(Base32.FromBase32String(enctxt));
    }
  }
}
