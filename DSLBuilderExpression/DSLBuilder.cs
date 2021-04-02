using System;
using System.Collections.Generic;
using System.Linq;
using DSLSemanticModel;
using DSLSemanticModel.ComponentsModels;

namespace DSLBuilderExpression
{
    public class DSLBuilder
    {

    }


    public class ComponentList
    {
        public ComponentList(List<Row> rows)
        {
            Rows = rows?.Select((e, i) => e.Init(i + 1)).ToList() ?? new List<Row>();
        }

        public List<Row> Rows { get; private set; }

        public string Generate()
        {
            var controller = new DSLSemanticController();
            return controller.GenerateList("", Rows?.SelectMany(e => e.Columns?.SelectMany(z => z.Items.Select(t => t.componentListItem))).ToList());
        }
    }

    public class Row
    {
        public Row(List<Column> columns)
        {
            Columns = columns;
        }

        public int NumberRow { get;  set; }

        public List<Column> Columns { get; private set; }

        public Row Init(int numberRow)
        {
            NumberRow = numberRow;
            Columns.Select((e, i) => e.Init(this, i + 1));

            return this;
        }
    }

    public class Column
    {
        public Column(List<Item> items)
        {
            Items = items;
        }

        public int NumberColumn { get; private set; }

        public Row Parent { get; private set; }

        public List<Item> Items { get; private set; }

        public Column Init(Row parent, int numberColumn)
        {
            Parent = parent;
            NumberColumn = numberColumn;
            Items.Select((e, i) => e.Init(this));

            return this;
        }

    }

    public class Item
    {
        public Item(string title = null, string text = null, string smallText = null, string date = null, string link = null)
        {
            Title = title ?? "";
            Text = text ?? "";
            SmallText = smallText ?? "";
            Date = date ?? "";
            Link = link ?? "";
        }

        public string Title { get; private set; }

        public string Text { get; private set; }

        public string SmallText { get; private set; }

        public string Date { get; private set; }

        public string Link { get; private set; }

        public Column Parent { get; private set; }

        public ComponentListItem componentListItem { get; private set; }

        public Item Init(Column parent)
        {
            Parent = parent;
            componentListItem = new ComponentListItem(Title, Text, SmallText, Date, Link, parent.NumberColumn, parent.Parent.NumberRow);

            return this;
        }
    }
}
