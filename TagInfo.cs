using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBsharp
{
    class TagInfo
    {
        public string bbtag { get; private set; }
        public string etag { get; private set; }

        public TagInfo(string bbtag, string etag)
        {
            this.bbtag = bbtag;
            this.etag = etag;
        }
    }
}
