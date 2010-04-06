using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
    public class BBCodeNode
    {
        public BBCodeNode[] Children { get { return children.ToArray(); } }
        List<BBCodeNode> children;

        public BBCodeNode Parent { get; private set; }

        public bool Singular { get; private set; }

        public string TagName { get; private set; }
        public string Attribute { get; set; }

        public BBCodeNode(string TagName, string Attribute, bool IsSingular)
        {
            if (TagName == null)
                throw new ArgumentNullException("TagName cannot be null");

            TagName = TagName.Trim();
            if (TagName == "")
                throw new ArgumentException("TagName cannot be empty");

            this.TagName = TagName;
            this.Attribute = Attribute;
            this.Singular = IsSingular;
            children = new List<BBCodeNode>();
        }
        public BBCodeNode(string TagName, string Attribute)
        {
            if (TagName == null)
                throw new ArgumentNullException("TagName cannot be null");

            TagName = TagName.Trim();
            if (TagName == "")
                throw new ArgumentException("TagName cannot be empty");

            this.TagName = TagName;
            this.Attribute = Attribute;
            children = new List<BBCodeNode>();
        }
        public BBCodeNode(string TagName) : this(TagName, null) { }
        protected BBCodeNode()
        {
            children = new List<BBCodeNode>();
        }

        public virtual BBCodeNode AppendChild(BBCodeNode Node)
        {
            if (Singular)
                throw new InvalidOperationException("Cannot add children to a singular node");

            if(Node == null)
                throw new ArgumentNullException("Node may not be null");

            if (Node.Parent != null)
                throw new ArgumentException("The BBCodeNode provided is already a child of another node");

            children.Add(Node);
            Node.Parent = this;

            return Node;
        }
        public virtual BBCodeNode AppendChild(string TagName, string Attribute)
        {
            var node = new BBCodeNode(TagName, Attribute)
            {
                Parent = this,
            };

            return AppendChild(node);
        }
        public virtual BBCodeNode AppendChild(string TagName)
        {
            return AppendChild(TagName, "");
        }

        public virtual object Clone()
        {
            BBCodeNode node = new BBCodeNode(TagName, Attribute);
            foreach (var Child in Children)
                node.AppendChild((BBCodeNode)Child.Clone());
            return node;
        }

        public virtual BBCodeNode InsertAfter(BBCodeNode Node, BBCodeNode After)
        {
            if (Singular)
                throw new InvalidOperationException("Cannot add children to a singular node");

            if(Node == null)
                throw new ArgumentNullException("Node may not be null");

            if(After == null)
                throw new ArgumentNullException("After may not be null");

            if (Node.Parent != null)
                throw new ArgumentException("The Node provided is already a child of another node");

            if (After.Parent == null || After.Parent != this)
                throw new ArgumentException("The After node provided is not a child of this node");

            children.Insert(children.IndexOf(After) + 1, Node);

            return Node;
        }

        public virtual BBCodeNode InsertBefore(BBCodeNode Node, BBCodeNode Before)
        {
            if (Singular)
                throw new InvalidOperationException("Cannot add children to a singular node");

            if(Node == null)
                throw new ArgumentNullException("Node may not be null");

            if(Before == null)
                throw new ArgumentNullException("After may not be null");

            if (Node.Parent != null)
                throw new ArgumentException("The Node provided is already a child of another node");

            if (Before.Parent == null || Before.Parent != this)
                throw new ArgumentException("The Before node provided is not a child of this node");

            children.Insert(children.IndexOf(Before), Node);

            return Node;
        }

        public virtual BBCodeNode PrependChild(BBCodeNode Node)
        {
            if (Singular)
                throw new InvalidOperationException("Cannot add children to a singular node");

            if (Node == null)
                throw new ArgumentNullException("Node may not be null");

            if (Node.Parent != null)
                throw new ArgumentException("The BBCodeNode provided is already a child of another node");

            children.Insert(0, Node);

            return Node;
        }

        public virtual void RemoveAll()
        {
            children.Clear();
        }

        public virtual BBCodeNode RemoveChild(BBCodeNode Node)
        {
            if (Node == null)
                throw new ArgumentNullException("Node may not be null");

            if (Node.Parent != null)
                throw new ArgumentException("The BBCodeNode provided is not a child of this node");

            children.Remove(Node);

            return Node;
        }

        public virtual BBCodeNode ReplaceChild(BBCodeNode Old, BBCodeNode New)
        {
            if (Old == null || New == null)
                throw new ArgumentNullException("Arguments may not be null");

            if (Old.Parent != this)
                throw new ArgumentException("The Old node provided is not a child of this node");

            if (New.Parent != null)
                throw new ArgumentException("The New node provided is a child of another node");

            int index = children.IndexOf(Old);
            children.Remove(Old);
            children.Insert(index, New);

            return Old;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("[" + this.TagName);

            if ((Attribute ?? "").Trim() != "")
                str.Append("=" + Attribute);

            str.Append("]");

            if(Singular)
                return str.ToString();

            foreach (var child in children)
                str.Append(child.ToString());

            str.Append("[/" + this.TagName + "]");

            return str.ToString();
        }
    }
}
