using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
    class BBCodeTextNode : BBCodeNode
    {
        public string InnerText { get { return text.ToString(); } }

        StringBuilder text = new StringBuilder();

        public BBCodeTextNode(string InnerText)
        {
            TagName = "span";
            Singular = true;
            text.Append(InnerText);
        }

        public void AppendText(string Text)
        {
            text.Append(Text);
        }

        public void AppendText(char Text)
        {
            text.Append(Text);
        }

        public override string ToString()
        {
            return InnerText;
        }
    }
}
