using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
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
