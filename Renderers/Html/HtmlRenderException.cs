using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp.Renderers.Html
{
    class HtmlRenderException : Exception
    {
        public HtmlRenderException(string Message)
            : base(Message)
        { }
    }
}
