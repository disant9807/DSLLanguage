using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions.Enums
{
    public class EnumItemFlattenNode<T> : EnumItem<T> where T : struct
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int Level { get; set; }

        public T? ParentValue { get; set; }

        public override string ToString()
        {
            string parentIdStr = ParentId?.ToString() ?? "Null";
            string parentValueStr = ParentValue?.ToString() ?? "Null";
            return $"Id: {Id}, ParentId: {parentIdStr}, Level: {Level}, " + base.ToString() + $", ParentValue: {parentValueStr}";
        }
    }
}
