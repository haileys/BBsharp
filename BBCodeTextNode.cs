using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
    class BBCodeTextNode : BBCodeNode
    {
        public string InnerText { get; set; }

        public override string ToString()
        {
            return InnerText;
        }
    }
}
