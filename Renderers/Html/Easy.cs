using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp.Renderers.Html
{
    public static partial class HtmlRenderer
    {
        public static string BbToHtml(string BBCode)
        {
            return BBCodeDocument.Load(BBCode, false, new string[] { "hr" }).ToHtml(false);
        }
    }
}
