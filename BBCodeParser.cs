using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
    public partial class BBCodeDocument
    {
        public static BBCodeDocument Load(string BBCode)
        {
            BBCodeDocument document = new BBCodeDocument();
            Stack<BBCodeNode> nodestack = new Stack<BBCodeNode>();
            nodestack.Push(document);

            for (int i = 0; i < BBCode.Length; i++)
            {
                if (BBCode[i] != '[')
                {
                    if((nodestack.Peek() as BBCodeTextNode) == null)
                }
            }

            return document;
        }
    }
}
