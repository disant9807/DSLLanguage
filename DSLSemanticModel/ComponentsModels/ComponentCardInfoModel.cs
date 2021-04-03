using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Core.Models.Elements;

namespace DSLSemanticModel.ComponentsModels
{   
    public class ComponentCardInfoModel
    {
        public ComponentCardInfoModel(string title, List<ComponentCardInfoTextItem> items)
        {
            Title = title;
            Items = items ?? new List<ComponentCardInfoTextItem>();
        }

        public string Title { get; private set; }

        public List<ComponentCardInfoTextItem> Items { get; private set; }

        public List<List<IElement>> ToElementsCollection()
        {
            var result = new List<List<IElement>>();

            var maxRowElement = Items?.Select(e => e.Row).Max() ?? 0;
            var maxColumnElements = Items?.Select(e => e.Column).Max() ?? 0;

            var rows = 0;
            while(rows < maxRowElement)
            {
                rows++;
                var column = 0;
                List<IElement> resultRow = new List<IElement>();

                while(column < maxColumnElements)
                {
                    column++;

                    var element = Items?.FirstOrDefault(e => e.Row == rows && e.Column == column);

                    if (element != null)
                    {
                        resultRow.Add(new TextElement(element.Value));
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

    public class ComponentCardInfoTextItem
    {
        public ComponentCardInfoTextItem(string value, int column, int row)
        {
            Id = Guid.NewGuid();
            Value = value;
            Column = column;
            Row = row;
        }

        public Guid Id { get; private set; }

        public string Value { get; private set; }

        public int Column { get; private set; }

        public int Row { get; private set; }

    }

}
