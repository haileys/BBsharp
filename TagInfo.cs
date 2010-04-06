using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBSharp
{
    public class TagInfo
    {
        public string bbtag { get; set; }
        public string etag { get; set; }

        public TagInfo(string bbtag, string etag)
        {
            this.bbtag = bbtag;
            this.etag = etag;
        }
    }
}
