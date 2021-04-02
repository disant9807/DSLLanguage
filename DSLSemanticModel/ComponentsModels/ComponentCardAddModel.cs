using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Models.Elements;

namespace DSLSemanticModel.ComponentsModels
{
    public class ComponentCardAddModel
    {
        public ComponentCardAddModel(string title, List<ComponentCardAddItem> items)
        {
            Title = title;
            Items = items ?? new List<ComponentCardAddItem>();
        }

        public string Title { get; private set; }

        public List<ComponentCardAddItem> Items { get; private set; }

        public List<List<IElement>> ToElementsCollection()
        {
            var result = new List<List<IElement>>();

            var maxRowElement = Items?.Select(e => e.Row).Max() ?? 0;
            var maxColumnElements = Items?.Select(e => e.Column).Max() ?? 0;

            var rows = 0;
            while (rows < maxRowElement)
            {
                rows++;
                var column = 0;
                List<IElement> resultRow = new List<IElement>();

                while (column < maxColumnElements)
                {
                    var element = Items?.FirstOrDefault(e => e.Row == rows && e.Column == column);

                    if (element != null)
                    {
                        resultRow.Add(new InputTextElement(element.Value, element.Name, element.Placeholder));
                    }
                    else
                    {
                        resultRow.Add(new Nulllement());
                    }
                }

                result.Add(resultRow);
            }

            return result;
        }
    }

    public class ComponentCardAddItem
    {
        public ComponentCardAddItem(string value, int column, int row)
        {
            Id = Guid.NewGuid();
            Value = value;
            Column = column;
            Row = row;
        }

        public Guid Id { get; private set; }

        public string Value { get; private set; }

        public string Name { get; private set; }

        public string Placeholder { get; private set; }

        public int Column { get; private set; }

        public int Row { get; private set; }

    }
}

