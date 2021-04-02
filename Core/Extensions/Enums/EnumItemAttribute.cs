using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions.Enums
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumItemAttribute : Attribute
    {
        public EnumItemAttribute(params string[] texts)
            : this(0, false, false, false, texts)
        {
        }

        public EnumItemAttribute(bool isDefault, params string[] texts)
            : this(0, isDefault, false, false, texts)
        {
        }

        public EnumItemAttribute(int sortOrder, params string[] texts)
            : this(sortOrder, false, false, false, texts)
        {
        }

        public EnumItemAttribute(int sortOrder, bool isDefault, params string[] texts)
            : this(sortOrder, isDefault, false, false, texts)
        {
        }

        public EnumItemAttribute(int sortOrder, bool isDefault, bool isDisabled, params string[] texts)
            : this(sortOrder, isDefault, isDisabled, false, texts)
        {
        }

        public EnumItemAttribute(int sortOrder, bool isDefault, bool isDisabled, bool isNonSelectable, params string[] texts)
        {
            IsDefault = isDefault;
            SortOrder = sortOrder;
            IsDisabled = isDisabled;
            IsNonSelectable = isNonSelectable;
            Texts = texts ?? new string[0];
        }

        public bool IsDefault { get; private set; }

        public bool IsDisabled { get; private set; }

        public bool IsNonSelectable { get; private set; }

        public int SortOrder { get; private set; }

        public string[] Texts { get; private set; }

        public object Data { get; set; }
    }
}
