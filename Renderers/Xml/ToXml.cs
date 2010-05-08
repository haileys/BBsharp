using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace bbsharp.Renderers.Xml
{
    public static class XmlRenderer
    {
        public static XmlDocument ToXml(this BBCodeDocument obj)
        {
            var xdoc = new XmlDocument();

            var root = xdoc.CreateElement("body");

            foreach (var c in obj.Children)
                root.AppendChild(c.ToXml(xdoc));

            return xdoc;
        }

        public static XmlNode ToXml(this BBCodeNode obj, XmlDocument xdoc)
        {
            var el = xdoc.CreateElement(obj.TagName);
            el.SetAttribute("attribute", obj.Attribute);

            if (!obj.Singular)
                foreach (var c in obj.Children)
                    el.AppendChild(c.ToXml(xdoc));

            return el;
        }
    }
}
