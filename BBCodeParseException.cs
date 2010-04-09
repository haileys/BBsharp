using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bbsharp
{
    public class BBCodeParseException : Exception
    {
        /// <summary>
        /// The position of the character which caused the parse error
        /// </summary>
        public int Position { get; internal set; }

        internal BBCodeParseException(string Message, int Position) : base(Message + " at position " + Position.ToString())
        {
            this.Position = Position;
        }
    }
}
