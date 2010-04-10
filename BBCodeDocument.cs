using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
    /// <summary>
    /// Represents an entire BBCode document. Similar to the HTML tag &lt;body&gt;
    /// </summary>
    public partial class BBCodeDocument : BBCodeNode
    {
        /// <summary>
        /// Creates a new Document Node.
        /// </summary>
        public BBCodeDocument() : base()
        {
            TagName = "body";
        }
    }
}
