using System;
using System.Collections.Generic;
using System.Linq;
using DSLSemanticModel;
using DSLSemanticModel.ComponentsModels;

namespace DSLBuilderExpression
{

    public class ComponentList
    {
        public ComponentList(params Row[] args)
        {
           Rows = args?.Select((e, i) => e.Init(i + 1)).ToList() ?? new List<Row>();
        }

        public List<Row> Rows { get; private set; }

        public string Generate()
        {
            var controller = new DSLSemanticController();
            var items = Rows?.SelectMany(e => e.Columns?.SelectMany(z => z.Items)).ToList();
            List<ComponentListItem> resultItems = new List<ComponentListItem>();

            foreach(var item in items)
            {
                resultItems.Add((item as ItemList).CreateComponentItem().componentListItem);
            }

            return controller.GenerateList("", resultItems);
        }
    }

    public class ComponentCard
    {
        public ComponentCard(params Row[] rows)
        {
            Rows = rows?.Select((e, i) => e.Init(i + 1)).ToList() ?? new List<Row>();
        }

        public List<Row> Rows { get; private set; }

        public string Generate()
        {
            var controller = new DSLSemanticController();
            var items = Rows?.SelectMany(e => e.Columns?.SelectMany(z => z.Items)).ToList();
            List<ComponentCardInfoTextItem> resultItems = new List<ComponentCardInfoTextItem>();

            foreach (var item in items)
            {
                resultItems.Add((item as ItemCardInfo).CreateComponentItem().componentCardInfoText);
            }

            return controller.GenerateCard("", resultItems);
        }
    }

    public class Row
    {
        public Row(params Column[] args)
        {
            Columns = args.ToList();
        }

        public int NumberRow { get;  set; }

        public List<Column> Columns { get; private set; }

        public Row Init(int numberRow)
        {
            NumberRow = numberRow;
            Columns = Columns.Select((e, i) => e.Init(this, i + 1)).ToList();

            return this;
        }
    }

    public class Column
    {
        public Column(params Item[] args)
        {
            Items = args.ToList();
        }

        public int NumberColumn { get; private set; }

        public Row Parent { get; private set; }

        public List<Item> Items { get; private set; }

        public Column Init(Row parent, int numberColumn)
        {
            Parent = parent;
            NumberColumn = numberColumn;
            Items = Items.Select((e, i) => e.Init(this)).ToList();

            return this;
        }

    }

    public class Item
    {
        public Column Parent { get; private set; }

        public Item Init(Column parent)
        {
            Parent = parent;

            return this;
        }
    }

    public class ItemList: Item
    {
        public ItemList(string title = null, string text = null, string smallText = null, string date = null, string link = null)
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

        public ComponentListItem componentListItem { get; private set; }

        public ItemList CreateComponentItem()
        {
            componentListItem = new ComponentListItem(Title, Text, SmallText, Date, Link, Parent.NumberColumn, Parent.Parent.NumberRow);

            return this;
        }
    }

    public class ItemCardInfo : Item
    {
        public ItemCardInfo(string text = null)
        {
            Text = text ?? "";
        }

        public string Text { get; private set; }

        public ComponentCardInfoTextItem componentCardInfoText { get; private set; }

        public ItemCardInfo CreateComponentItem()
        {
            componentCardInfoText = new ComponentCardInfoTextItem(Text, Parent.NumberColumn, Parent.Parent.NumberRow);

            return this;
        }
    }
}
