using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerComponents.Shared
{
    public class UpdateItemsEventResult<TItem>
    {
        public int TotalItems { get; set; }
        public required List<TItem> Items { get; set; }
    }
}
