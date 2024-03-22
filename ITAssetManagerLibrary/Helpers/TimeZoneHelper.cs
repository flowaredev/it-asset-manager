using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerLibrary.Helpers
{
    public class TimeZoneHelper
    {
        private const string KOREA_ZONE_ID = "Korea Standard Time";
        private static TimeZoneInfo _koreaZone = TimeZoneInfo.FindSystemTimeZoneById(KOREA_ZONE_ID);

        public static DateTime GetKoreaTimeNow()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _koreaZone);
        }
    }
}
