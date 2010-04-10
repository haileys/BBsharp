using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bbsharp.Renderers.Html;

namespace bbsharp.Easy
{
    public static class EasyHtmlRenderer
    {
        public static string BbToHtml(string BBCode)
        {
            return BBCodeDocument.Load(BBCode, false, new string[] { "hr" }).ToHtml(false);
        }
    }
}
