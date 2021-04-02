using DSLSemanticModel.Components;
using DSLSemanticModel.ComponentsModels;
using System.Collections.Generic;

namespace DSLSemanticModel
{
    public class DSLSemanticController
    {
        public string GenerateList(string title, List<ComponentListItem> items)
        {
            var model = new ComponentListModel(title, items);

            var component = new ComponentList(model);

            return component.Generate();
        }

        public string GenerateCard(string title, List<ComponentCardInfoTextItem> items)
        {
            var model = new ComponentCardInfoModel(title, items);

            var component = new ComponentCardInfo(model);

            return component.Generate();
        }

        public string GenerateForm(string title, List<ComponentCardAddItem> items)
        {
            var model = new ComponentCardAddModel(title, items);

            var component = new ComponentCardAdd(model);

            return component.Generate();
        }
    }
}
