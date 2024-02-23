using ITAssetManagerLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerComponents.Shared
{
    public class SaveItemEventArgs<TItem>
    {
        public required TItem Item { get; set; }
    }
}
