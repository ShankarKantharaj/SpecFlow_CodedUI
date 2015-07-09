using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreshCodedUIProject._02_Utility_Tier;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace FreshCodedUIProject.PageObjects
{
    class CustRegistPage
    {
        public CustRegistPage()
        {

        }

        public CustRegistPage getNewCustRegDetails()
        {

            return new CustRegistPage();
        }

        public HomePage navigateToHomePage()
        {
            WebUtility.ClickItem<HtmlHyperlink>(WebUtility.PropertyType.InnerText, "Manager");
            return new HomePage();
        }
    }
}
