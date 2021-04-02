using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions.Enums
{
    public static class EnumExtensions
    {
        public static List<EnumItem<T>> GetItems<T>(this T @enum, int textIndex = 0)
            where T : struct
        {
            return GetItems<T>(textIndex);
        }

        public static List<EnumItem<T>> GetItems<T>(int textIndex = 0)
            where T : struct
        {
            return typeof(T).GetFields()
                .Where(f => f.CustomAttributes.Any(a => a.AttributeType == typeof(EnumItemAttribute)))
                .Select(f =>
                {
                    var attr = (EnumItemAttribute)Attribute.GetCustomAttribute(f, typeof(EnumItemAttribute));
                    return new EnumItem<T>
                    {
                        Value = (T)f.GetValue(null),
                        Text = textIndex < attr.Texts.Length ? attr.Texts[textIndex] : attr.Texts[0],
                        SortOrder = attr.SortOrder,
                        IsDefault = attr.IsDefault,
                        IsDisabled = attr.IsDisabled,
                        IsNonSelectable = attr.IsNonSelectable,
                        Data = attr.Data
                    };
                })
                .OrderBy(i => i.SortOrder)
                .ToList();
        }

        /// <summary>
        /// Получить дефолтный элемент.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="enum">Перечисление.</param>
        /// <param name="textIndex">Индекс в массиве возможных текстовых значений.</param>
        /// <returns>Элемент помеченый как элемент по умолчанию.</returns>
        public static EnumItem<T> GetItemsDefault<T>(this T @enum, int textIndex = 0)
            where T : struct
        {
            return GetItemsDefault<T>(textIndex);
        }

        /// <summary>
        /// Получить дефолтный элемент.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="textIndex">Индекс в массиве возможных текстовых значений.</param>
        /// <returns>Элемент помеченый как элемент по умолчанию.</returns>
        public static EnumItem<T> GetItemsDefault<T>(int textIndex = 0)
            where T : struct
        {
            var items = GetItems<T>(textIndex)
                .OrderByDescending(i => i.IsDefault)
                .ThenBy(i => i.SortOrder)
                .ThenBy(i => i.Text);

            return items.First();
        }

        /// <summary>
        /// Получить первый элемент перечисления содержащий в свойствах text.
        /// Если такой не найден, то вернуть элемент помеченный дефолтным.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="enum">Перечисление.</param>
        /// <param name="text">Текст поиска.</param>
        /// <param name="matchCase">Учитывать регистр или нет.</param>
        /// <returns>Элемент перечисления.</returns>
        public static T GetFirstOrDefaultByText<T>(this T @enum, string text, bool matchCase = false)
            where T : struct
        {
            return GetFirstOrDefaultByText<T>(text, matchCase);
        }

        /// <summary>
        /// Получить первый элемент перечисления содержащий в свойствах text.
        /// Если такой не найден, то вернуть элемент помеченный дефолтным.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="text">Текст поиска.</param>
        /// <param name="matchCase">Учитывать регистр или нет.</param>
        /// <returns>Элемент перечисления.</returns>
        public static T GetFirstOrDefaultByText<T>(string text, bool matchCase = false)
            where T : struct
        {
            var fis = typeof(T).GetFields()
                .Where(f => f.CustomAttributes.Any(a => a.AttributeType == typeof(EnumItemAttribute)))
                .Select(f => new { f, a = (EnumItemAttribute)Attribute.GetCustomAttribute(f, typeof(EnumItemAttribute)) });

            var fi = matchCase
                ? (fis.FirstOrDefault(f => f.a.Texts.Contains(text)) ?? fis.FirstOrDefault(f => f.a.IsDefault) ?? fis.First())?.f
                : (fis.FirstOrDefault(f => f.a.Texts.Any(t => t.Equals(text, StringComparison.InvariantCultureIgnoreCase))) ?? fis.FirstOrDefault(f => f.a.IsDefault) ?? fis.First())?.f;

            return (T)fi.GetValue(0);
        }

        public static List<EnumItemTreeNode<T>> GetItemsTree<T>(this T @enum, int textIndex = 0, T? parentValue = null)
            where T : struct
        {
            return GetItemsTree<T>(textIndex, parentValue);
        }

        public static List<EnumItemTreeNode<T>> GetItemsTree<T>(int textIndex = 0, T? parentValue = null)
            where T : struct
        {
            return typeof(T).GetFields()

                // выбираем только тех у кого есть атрибут Item
                .Where(f => f.CustomAttributes.Any(a => a.AttributeType == typeof(EnumItemAttribute)))

                // если parentValue != null, то выбираем тех у кого есть атрибут ItemParent и ParentValue == parentValue.Value
                // иначе выбираем тех у кого нет атрибута ItemParent
                .Where(f => parentValue.HasValue
                    ? Attribute.GetCustomAttributes(f, typeof(EnumItemParentAttribute)).Any(a => ((EnumItemParentAttribute)a).ParentValue == Convert.ToInt32(parentValue.Value))
                    : f.CustomAttributes.All(a => a.AttributeType != typeof(EnumItemParentAttribute)))

                .Select(f =>
                {
                    var attr = (EnumItemAttribute)Attribute.GetCustomAttribute(f, typeof(EnumItemAttribute));
                    var value = (T)f.GetValue(null);

                    return new EnumItemTreeNode<T>
                    {
                        Value = value,
                        ParentValue = parentValue,
                        Text = textIndex < attr.Texts.Length ? attr.Texts[textIndex] : attr.Texts[0],
                        SortOrder = attr.SortOrder,
                        IsDefault = attr.IsDefault,
                        IsDisabled = attr.IsDisabled,
                        IsNonSelectable = attr.IsNonSelectable,
                        Data = attr.Data,
                        Items = GetItemsTree<T>(textIndex, value)
                    };
                })
                .OrderBy(i => i.SortOrder)
                .ToList();
        }

        public static List<EnumItemFlattenNode<T>> GetItemsFlatten<T>(this T @enum, int textIndex = 0, T? parentValue = null)
            where T : struct
        {
            return GetItemsFlatten<T>(textIndex, parentValue);
        }

        public static List<EnumItemFlattenNode<T>> GetItemsFlatten<T>(int textIndex = 0, T? parentValue = null)
            where T : struct
        {
            var tree = GetItemsTree<T>(textIndex, parentValue);
            var result = new List<EnumItemFlattenNode<T>>();
            MakeFlatten<T>(result, tree, 0, null, 0);
            return result;
        }

        private static int MakeFlatten<T>(List<EnumItemFlattenNode<T>> result, List<EnumItemTreeNode<T>> tree, int id, int? parentId, int level)
            where T : struct
        {
            foreach(var node in tree)
            {
                result.Add(new EnumItemFlattenNode<T>
                {
                    Id = ++id,
                    ParentId = parentId,
                    Level = level,
                    IsDefault = node.IsDefault,
                    IsDisabled = node.IsDisabled,
                    IsNonSelectable = node.IsNonSelectable,
                    SortOrder = node.SortOrder,
                    Text = node.Text,
                    Data = node.Data,
                    Value = node.Value,
                    ParentValue = node.ParentValue
                });

                if (node.Items != null && 0 < node.Items.Count)
                {
                    id = MakeFlatten(result, node.Items, id, id, level + 1);
                }
            }

            return id;
        }

        public static string GetText<T>(this T value, int textIndex = 0) where T : struct
        {
            return GetItems(value, textIndex).First(i => i.Value.Equals(value)).Text;
        }

        /// <summary>
        /// Получить все текстовые значения для элемента перечисления.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="value">Значение элемента.</param>
        /// <returns>Массив строк текстовых значений.</returns>
        public static string[] GetTexts<T>(this T value) where T : struct
        {
            var fi = typeof(T).GetField(value.ToString());

            if (fi != null)
            {
                var attr = (EnumItemAttribute)Attribute.GetCustomAttribute(fi, typeof(EnumItemAttribute));
                return attr?.Texts;
            }

            return null;
        }

        public static string GetText(this DayOfWeek value)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("ru-RU");
            string result = culture.DateTimeFormat.GetDayName(value);
            return culture.TextInfo.ToTitleCase(result);
        }
    }
}
