using System;

namespace JCCoreLib3
{
  public class URL
  {
    private URL()
    {
    }

    public static string HrefPathCombine(string url, string relativeOrAbsoluteUri) => relativeOrAbsoluteUri.StartsWith("/") ? URL.CombineUriToString(url, relativeOrAbsoluteUri) : url;

    private static Uri CombineUri(string baseUri, string relativeOrAbsoluteUri) => new Uri(new Uri(baseUri), relativeOrAbsoluteUri);

    private static string CombineUriToString(string baseUri, string relativeOrAbsoluteUri) => new Uri(new Uri(baseUri), relativeOrAbsoluteUri).ToString();
  }
}
