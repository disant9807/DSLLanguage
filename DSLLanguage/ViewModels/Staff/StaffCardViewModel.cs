using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLLanguage.ViewModels.Staff
{
    public class StaffCardViewModel
    {
        public string HtmlCard { get; private set; }

        public StaffCardViewModel Init(string htmlList)
        {
            HtmlCard = htmlList;

            return this;
        }
    }
}
