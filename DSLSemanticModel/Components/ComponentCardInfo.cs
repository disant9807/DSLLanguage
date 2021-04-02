using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Core.Models.Elements;
using DSLSemanticModel.ComponentsModels;

namespace DSLSemanticModel.Components
{
    public class ComponentCardInfo : BaseComponent
    {
        public ComponentCardInfo(ComponentCardInfoModel model)
        {
            Model = model;
        }

        public ComponentCardInfoModel Model { get; private set; }

        public List<List<IElement>> ElementsUI => Model.ToElementsCollection();

        public override string Generate()
        {
            var htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<div class=\"card\">");
            htmlBuilder.Append("<div class=\"card-body\">");
            htmlBuilder.Append($"<h5 class=\"card-title\">{ Model.Title }</h5>");

            foreach (var rowElement in ElementsUI)
            {
                htmlBuilder.Append("<div class=\"row\">");
                foreach (var element in rowElement)
                {
                    htmlBuilder.Append("<div class=\"col-1\">");
                    htmlBuilder.Append(element.Generate());
                }
                htmlBuilder.Append("</div>");
            }

            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</div>");

            return htmlBuilder.ToString();
        }
    }
}
