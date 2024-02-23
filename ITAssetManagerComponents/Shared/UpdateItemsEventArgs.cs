using ITAssetManagerLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerComponents.Shared
{
    public class UpdateItemsEventArgs
    {
        public int SkipCount { get; set; }
        public int TakeCount { get; set; }
    }
}
