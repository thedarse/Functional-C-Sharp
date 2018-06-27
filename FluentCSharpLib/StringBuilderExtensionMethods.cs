using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCSharpLib
{
    public static class StringBuilderExtensionMethods
    {
        public static StringBuilder AppendSequence(this StringBuilder @this, IEnumerable<string> itemsToAppend, string separationstring = ", ") =>
            itemsToAppend
                .Aggregate((current, next) => current + separationstring + next)
                .Transform(@this.Append);
    }
}
