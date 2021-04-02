using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions.Enums
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumItemParentAttribute : Attribute
    {
        public EnumItemParentAttribute(int parentValue)
        {
            ParentValue = parentValue;
        }

        public int ParentValue { get; private set; }
    }
}
