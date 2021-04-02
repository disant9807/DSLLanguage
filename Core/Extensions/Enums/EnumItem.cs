namespace Core.Extensions.Enums
{
    public class EnumItem<T> where T : struct
    {
        public T Value { get; set; }

        public string Text { get; set; }

        public int SortOrder { get; set; }

        public bool IsDefault { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsNonSelectable { get; set; }

        public object Data { get; set; }

        public override string ToString()
        {
            return $"{Value}, {Text}, SortOrder: {SortOrder}, IsDefault: {IsDefault}, IsDisabled: {IsDisabled}, IsNonSelectable: {IsNonSelectable}";
        }
    }
}
