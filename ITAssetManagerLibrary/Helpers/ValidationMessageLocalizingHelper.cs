using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerLibrary.Helpers
{
    public class ValidationMessageLocalizingHelper
    {
        private static readonly Dictionary<string, string> _applicationLocalizerDictionary = new()
        {
            { "The {0} field is required.", "내용을 입력하세요." }
        };

        public static string GetValidationMessage(string message, IEnumerable<string> arguments)
        {
            return string.Format(_applicationLocalizerDictionary[message], [.. arguments]);
        }
    }
}
