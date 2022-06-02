using System.Text.RegularExpressions;

namespace JCCoreLib3
{
  public class HTML
  {
    private HTML()
    {
    }

    public static string ExtractHtmlInnerText(string htmlText) => new Regex("(<.*?>\\s*)+", RegexOptions.Singleline).Replace(htmlText, " ").Trim().Replace("&#039;", "");
  }
}
