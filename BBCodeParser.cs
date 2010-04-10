using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
    public partial class BBCodeDocument
    {
        /// <summary>
        /// Loads a string of BBCode as a BBCodeDocument object
        /// </summary>
        /// <param name="BBCode">A string of BBCode text</param>
        /// <returns>The DOM representation of the text</returns>
        public static BBCodeDocument Load(string BBCode)
        {
            return Load(BBCode, true);
        }
        /// <summary>
        /// Loads a string of BBCode as a BBCodeDocument object
        /// </summary>
        /// <param name="BBCode">A string of BBCode text</param>
        /// <param name="ThrowOnError">Whether to throw an exception on parse error. If false, the error is ignored</param>
        /// <returns>The DOM representation of the text</returns>
        public static BBCodeDocument Load(string BBCode, bool ThrowOnError)
        {
            return Load(BBCode, ThrowOnError, new string[0]);
        }
        /// <summary>
        /// Loads a string of BBCode as a BBCodeDocument object
        /// </summary>
        /// <param name="BBCode">A string of BBCode text</param>
        /// <param name="ThrowOnError">Whether to throw an exception on parse error. If false, the error is ignored</param>
        /// <param name="SingularTags">A list of tags which should be considered singular by the parser. Singular tags are self closing and may not have children.</param>
        /// <returns>The DOM representation of the text</returns>
        public static BBCodeDocument Load(string BBCode, bool ThrowOnError, IEnumerable<string> SingularTags)
        {
            BBCodeDocument document = new BBCodeDocument();
            Stack<BBCodeNode> nodestack = new Stack<BBCodeNode>();
            nodestack.Push(document);

            // iterate through all characters in text
            for (int i = 0; i < BBCode.Length; i++)
            {
                // the character is not a tag
                if (BBCode[i] != '[')
                    AddPlainText(document, nodestack, BBCode[i].ToString());
                // beginning of a tag
                else
                {
                    // if we have an open text node, close it.
                    if ((nodestack.Peek() as BBCodeTextNode) != null)
                        nodestack.Pop();

                    StringBuilder TagName = new StringBuilder();
                    i++;

                    // edge case where there is an [ as the last character
                    if (i == BBCode.Length)
                    {
                        if (ThrowOnError)
                            throw new BBCodeParseException("Reached end of document while reading tag", i);
                        AddPlainText(document, nodestack, "[" + TagName.ToString());
                        continue;
                    }


                    bool IsClosing = BBCode[i] == '/';
                    if (IsClosing)
                        i++;

                    // read in the entire tagname
                    while (i < BBCode.Length && char.IsLetter(BBCode[i]))
                        TagName.Append(BBCode[i++]);

                    if (i == BBCode.Length)
                        break;

                    // reached the end of tagname, handle accordingly
                    if (!IsClosing && (BBCode[i] == '=' || BBCode[i] == ']'))
                    {
                        var el = new BBCodeNode(TagName.ToString(), "", SingularTags.Contains(TagName.ToString()));
                        nodestack.Peek().AppendChild(el);
                        if (!el.Singular)
                            nodestack.Push(el);
                        if (BBCode[i] == ']')
                            continue;

                        // skip over the equals sign so we don't accidentally read it into the attribute
                        if (BBCode[i] == '=')
                            i++;

                        StringBuilder Attribute = new StringBuilder();
                        while (i < BBCode.Length && BBCode[i] != ']')
                            Attribute.Append(BBCode[i++]);
                        el.Attribute = Attribute.ToString();
                    }
                    else if (IsClosing && BBCode[i] == ']')
                    {
                        if (nodestack.Count == 0 || nodestack.Peek().TagName != TagName.ToString())
                        {
                            if (ThrowOnError)
                                throw new BBCodeParseException("Unmatched closing tag", i);
                            AddPlainText(document, nodestack, "[/" + TagName.ToString() + "]");
                            continue;
                        }

                        nodestack.Pop();
                    }
                    else
                    {
                        // edge case where encountering something like: [[i] would cause the [i] tag not to be read properly
                        if (BBCode[i] == '[')
                            i--;

                        // illegal character in tag name
                        if (ThrowOnError)
                            throw new BBCodeParseException("Illegal character in tag name", i);
                        // if ThrowOnError is false, we'll just add it as plain text.
                        AddPlainText(document, nodestack, "[" + TagName.ToString()); // prepend the [ char which started the tag
                    }
                }
            }
            // close up a final text node if it exists:
            if ((nodestack.Peek() as BBCodeTextNode) != null)
                nodestack.Pop();

            // close the body tag if it's the next one
            if ((nodestack.Peek() as BBCodeDocument) != null)
                nodestack.Pop();

            if (nodestack.Count > 0 && ThrowOnError)
                throw new BBCodeParseException("Reached end of document with " + (nodestack.Count-1).ToString() + " unclosed tags", BBCode.Length);

            return document;
        }
        static void AddPlainText(BBCodeDocument doc, Stack<BBCodeNode> stack, string text)
        {
            if ((stack.Peek() as BBCodeTextNode) == null)
            {
                var el = new BBCodeTextNode(text);
                stack.Peek().AppendChild(el);
                stack.Push(el);
            }
            else
                ((BBCodeTextNode)stack.Peek()).AppendText(text);
        }
    }
}
