using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions.Enums
{
    public class EnumItemTreeNode<T> : EnumItem<T> where T : struct
    {
        public T? ParentValue { get; set; }

        public List<EnumItemTreeNode<T>> Items { get; set; }

        public override string ToString()
        {
            string parentValueStr = ParentValue?.ToString() ?? "Null";
            return base.ToString() + $", ParentValue: {parentValueStr}";
        }
    }
}
