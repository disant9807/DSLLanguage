using System;
using System.Text;

namespace Core.Models.Elements
{
    public interface IElement
    {
        Guid Id { get; }

        string Generate();
    }

    public class BaseElement : IElement
    {
        public virtual string Generate()
        {
            return $"This is UI element {Id}";
        }

        public BaseElement()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

    }

    public class CardListElement: BaseElement
    {
        public CardListElement(string title, string text, string smallText, string date, string href)
        {
            Title = title;
            Text = text;
            SmallText = smallText;
            Date = date;
            Href = href;
        }

        public string Title { get; private set; }

        public string Text { get; private set; }

        public string SmallText { get; private set; }

        public string Date { get; private set; }

        public string Href { get; private set; }

        override public string Generate()
        {
            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append($"<a href=\"{Href}\" class=\"list-group-item list-group-item-action\">");
            htmlBuilder.Append($"<div class=\"d-flex w-100 justify-content-between\">");
            htmlBuilder.Append($"<h5 class=\"mb-1\">{Title}</h5>");
            htmlBuilder.Append($"<small class=\"text-muted\">{Date}</small>");
            htmlBuilder.Append($"</div>");
            htmlBuilder.Append($"<p class=\"mb-1\">{Text}</p>");
            htmlBuilder.Append($"<small class=\"text-muted\">{ SmallText }</small>");
            htmlBuilder.Append($"</a>");

            return htmlBuilder.ToString();
        }
    }

    public class InputTextElement : BaseElement
    {
        public InputTextElement(string value, string name, string placeholder = null)
        {
            Value = value;
            Name = name;
            Placeholder = placeholder ?? "";
        }

        public string Name { get; private set; }

        public string Value { get; private set; }

        public string Placeholder { get; private set; }

        override public string Generate()
        {
            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append($"<div id=\"{Id}\" class=\"input-group\">");
            htmlBuilder.Append($"<input type=\"text\" name=\"{Name}\" class=\"form-control\" placeholder=\"{Placeholder}\" value=\"{Value}\" ");
            htmlBuilder.Append("</div>");

            return htmlBuilder.ToString();
        }
    }

    public class TitleH1Element : BaseElement
    {
        public TitleH1Element(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        override public string Generate()
        {
            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append($"<h1 id=\"{Id}\" class=\"my-0\"> {Value} </h1>");

            return htmlBuilder.ToString();
        }
    }

    public class TextElement : BaseElement
    { 
        public TextElement(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        override public string Generate()
        {
            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append($"<div id=\"{Id}\" class=\"text-dark\"> {Value} </div>");

            return htmlBuilder.ToString();
        }
    }

    public class ButtonYesElement : BaseElement
    {
        public ButtonYesElement(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        override public string Generate()
        {
            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append($"<button id=\"{Id}\" class=\"btn btn-success\"> {Value} </button>");

            return htmlBuilder.ToString();
        }
    }

    public class Nulllement : BaseElement
    {

        override public string Generate()
        {
            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append($"<div></div>");

            return htmlBuilder.ToString();
        }
    }
}
