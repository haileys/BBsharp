using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
    public class BBCodeNode
    {
        /// <summary>
        /// Gets an array of this node's child nodes
        /// </summary>
        public BBCodeNode[] Children { get { return children.ToArray(); } }
        List<BBCodeNode> children;

        /// <summary>
        /// Gets the parent node of this node.
        /// </summary>
        public BBCodeNode Parent { get; protected set; }

        /// <summary>
        /// Gets whether this node is singular. Singular nodes are self closing and can have no children.
        /// </summary>
        public bool Singular { get; protected set; }

        /// <summary>
        /// Gets the tag name of this node. The tag name is the main part of the tag, and is mandatory.
        /// </summary>
        public string TagName { get; protected set; }
        /// <summary>
        /// Gets or sets this node's attribute. The Attribute is the part of the tag that comes after the equals sign. It is optional, and this property may return either null or an empty string.
        /// </summary>
        public string Attribute { get; set; }

        /// <summary>
        /// Gets an array of children BBCodeNodes with the specified TagName
        /// </summary>
        /// <param name="TagName">The TagName of BBCodeNodes to return</param>
        /// <returns>Array of matching BBCodeNodes</returns>
        public BBCodeNode[] this[string TagName]
        {
            get
            {
                return children.Where(x => x.TagName == TagName).ToArray();
            }
        }
        /// <summary>
        /// Gets the nth child BBCodeNode
        /// </summary>
        /// <param name="Index">The index of the BBCodeNode to access</param>
        /// <returns>BBCodeNode at the specified index</returns>
        public BBCodeNode this[int Index]
        {
            get
            {
                return children[Index];
            }
        }

        /// <summary>
        /// Creates a new BBCodeNode.
        /// </summary>
        /// <param name="TagName">The node's tag name. Mandatory.</param>
        /// <param name="Attribute">The node's optional attribute. This may be an empty string or null.</param>
        /// <param name="IsSingular">Singular nodes are self closing and may not have children</param>
        public BBCodeNode(string TagName, string Attribute, bool IsSingular)
        {
            if (TagName == null)
                throw new ArgumentNullException("TagName cannot be null");

            TagName = TagName.Trim();
            if (TagName == "")
                throw new ArgumentException("TagName cannot be empty");

            this.TagName = TagName.ToLower();
            this.Attribute = Attribute;
            this.Singular = IsSingular;
            children = new List<BBCodeNode>();
        }
        /// <summary>
        /// Creates a new BBCodeNode.
        /// </summary>
        /// <param name="TagName">The node's tag name. Mandatory.</param>
        /// <param name="Attribute">The node's optional attribute. This may be an empty string or null.</param>
        public BBCodeNode(string TagName, string Attribute) : this(TagName, Attribute, false) { }
        /// <summary>
        /// Creates a new BBCodeNode.
        /// </summary>
        /// <param name="TagName">The node's tag name. Mandatory.</param>
        public BBCodeNode(string TagName) : this(TagName, null) { }
        protected BBCodeNode()
        {
            children = new List<BBCodeNode>();
        }

        /// <summary>
        /// Adds a new child node at the end of this node's descendants
        /// </summary>
        /// <param name="Node">The existing BBCodeNode to add. This may not already be childed to another node.</param>
        /// <returns>The node passed</returns>
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
        /// <summary>
        /// Adds a new child node at the end of this node's descendants
        /// </summary>
        /// <param name="TagName">The node's tag name. Mandatory.</param>
        /// <param name="Attribute">The node's optional attribute. This may be an empty string or null.</param>
        /// <returns>The newly created child node</returns>
        public virtual BBCodeNode AppendChild(string TagName, string Attribute)
        {
            var node = new BBCodeNode(TagName, Attribute)
            {
                Parent = this,
            };

            return AppendChild(node);
        }
        /// <summary>
        /// Adds a new child node at the end of this node's descendants
        /// </summary>
        /// <param name="TagName">The node's tag name. Mandatory.</param>
        /// <returns>The newly created child node</returns>
        public virtual BBCodeNode AppendChild(string TagName)
        {
            return AppendChild(TagName, "");
        }

        /// <summary>
        /// Creates a recursive copy of the current nodes and its children
        /// </summary>
        /// <returns>A deep clone of the current node</returns>
        public virtual object Clone()
        {
            BBCodeNode node = new BBCodeNode(TagName, Attribute);
            foreach (var Child in Children)
                node.AppendChild((BBCodeNode)Child.Clone());
            return node;
        }

        /// <summary>
        /// Inserts a new child node after the reference node passed.
        /// </summary>
        /// <param name="Node">The new child node to add. This may not be already childed to another node</param>
        /// <param name="After">The reference node. This must be a child of the current node</param>
        /// <returns>The added node</returns>
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

        /// <summary>
        /// Inserts a new child node before the reference node passed.
        /// </summary>
        /// <param name="Node">The new child node to add. This may not be already childed to another node</param>
        /// <param name="After">The reference node. This must be a child of the current node</param>
        /// <returns>The added node</returns>
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

        /// <summary>
        /// Adds a new child node at the beginning of this node's descendants
        /// </summary>
        /// <param name="Node">The existing BBCodeNode to add. This may not already be childed to another node.</param>
        /// <returns>The node passed</returns>
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

        /// <summary>
        /// Removes all child nodes
        /// </summary>
        public virtual void RemoveAll()
        {
            children.Clear();
        }

        /// <summary>
        /// Removes a specific child node
        /// </summary>
        /// <param name="Node">The child node to remove. This must be a child of the current node.</param>
        /// <returns>The removed node</returns>
        public virtual BBCodeNode RemoveChild(BBCodeNode Node)
        {
            if (Node == null)
                throw new ArgumentNullException("Node may not be null");

            if (Node.Parent != null)
                throw new ArgumentException("The BBCodeNode provided is not a child of this node");

            children.Remove(Node);

            return Node;
        }

        /// <summary>
        /// Replaces a specific child node with another
        /// </summary>
        /// <param name="Old">The node to remove. This must be a child of this node</param>
        /// <param name="New">The replacement node. This may not already be childed to another node</param>
        /// <returns>The removed node</returns>
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

        /// <summary>
        /// Recursively generates the BBCode representation of the current node and its children
        /// </summary>
        /// <returns>A BBCode string</returns>
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
