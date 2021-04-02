using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLLanguage.ViewModels.Staff
{
    public class StaffListViewModel
    {
        public string HtmlList { get; private set; }

        public StaffListViewModel Init(string htmlList)
        {
            HtmlList = htmlList;

            return this;
        }
    }
}
