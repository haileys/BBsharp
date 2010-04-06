using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
    public partial class BBCodeDocument : BBCodeNode
    {
        /*
        public BBCodeDocument(string TagName, string Attribute, string InnerText) : base(TagName, Attribute, InnerText) { }
        public BBCodeDocument(string TagName, string Attribute, bool IsSingular) : base(TagName, Attribute, IsSingular) { }
        public BBCodeDocument(string TagName, string Attribute) : base(TagName, Attribute) { }
        public BBCodeDocument(string TagName) : base(TagName) { }
        */
        private BBCodeDocument() : base() { }
    }
}
