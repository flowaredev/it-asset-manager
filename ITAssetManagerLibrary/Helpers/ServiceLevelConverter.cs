using ITAssetManagerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerLibrary.Helpers
{
    public class ServiceLevelConverter
    {
        public static string ToString(ServiceLevel level) => level switch
        {
            ServiceLevel.None => "없음",
            ServiceLevel.Poor => "불량",
            ServiceLevel.Insufficient => "미흡",
            ServiceLevel.Average => "보통",
            ServiceLevel.Good => "우수",
            ServiceLevel.Excellent => "탁월",
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
        };
    }
}
