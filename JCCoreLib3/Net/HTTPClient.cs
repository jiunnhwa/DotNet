using System.IO;
using System.Net;

namespace JCCoreLib3.Net
{
  public class HTTPClient
  {
    private HTTPClient()
    {
    }

    public static string httpPost(string url, string postData = "")
    {
      byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postData);
      int length = bytes.Length;
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
      httpWebRequest.Method = "POST";
      httpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";
      httpWebRequest.ContentLength = (long) length;
      using (Stream requestStream = httpWebRequest.GetRequestStream())
      {
        requestStream.Write(bytes, 0, length);
        requestStream.Close();
        try
        {
          using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
          {
            using (Stream responseStream = response.GetResponseStream())
            {
              using (StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                return streamReader.ReadToEnd();
            }
          }
        }
        catch (WebException ex)
        {
          using (Stream responseStream = ex.Response.GetResponseStream())
          {
            using (StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
              return streamReader.ReadToEnd();
          }
        }
      }
    }
  }
}
