using System;

namespace JCCoreLib3.Helper
{
  public class DateTimeHelper
  {
    private DateTimeHelper()
    {
    }

    public static long ConvertToUnixTime(DateTime datetime)
    {
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
      return (long) (datetime - dateTime).TotalSeconds;
    }

    public static long ConvertToUnixTimeMilliseconds(DateTime datetime) => (long) (datetime.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;
  }
}
