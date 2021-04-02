using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Core.Models.Elements;

namespace DSLSemanticModel.ComponentsModels
{
    public class ComponentListModel
    {
        public ComponentListModel(string title, List<ComponentListItem> items)
        {
            Title = title;
            Items = items ?? new List<ComponentListItem>();
        }

        public string Title { get; private set; }

        public List<ComponentListItem> Items { get; private set; }

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
                        resultRow.Add(new CardListElement(element.Text, element.Text, element.SmallText, element.Date, element.Link));
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

    public class ComponentListItem
    {
        public ComponentListItem(string title, string text, string smallText, string date, string link, int column, int row)
        {
            Id = Guid.NewGuid();
            Column = column;
            Row = row;
            Text = text;
            SmallText = smallText;
            Date = date;
            Link = link;
            Title = title;
        }

        public Guid Id { get; private set; }

        public string Text { get; private set; }

        public string Title { get; private set; }

        public string SmallText { get; private set; }

        public string Date { get; private set; }

        public string Link { get; private set; }

        public int Column { get; private set; }

        public int Row { get; private set; }
    }
}
