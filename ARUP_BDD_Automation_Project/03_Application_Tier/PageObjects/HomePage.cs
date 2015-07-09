using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using FreshCodedUIProject._02_Utility_Tier;

namespace FreshCodedUIProject.PageObjects
{
    class HomePage //: AbstractPage
    {
        /*public HomePage(BrowserWindow oBrowser)
        {
            this.oBrowser = base.oBrowser;
        }
        */
        public HomePage validateLogin()
        {

            Console.WriteLine("Login Validated");
            return new HomePage();
        }

        public AddNewCustPage navigateToNewCustPage()
        {
            WebUtility.ClickItem<HtmlHyperlink>(WebUtility.PropertyType.InnerText, "New Customer");
           
            
            //Validate Login
            if (WebUtility.WaitForItemExist<HtmlEdit>(WebUtility.PropertyType.Name, "name"))
                Reporter.ReportEvent("New Customer Page navigate", "Navigated successfully to New Customer Page", "PASS");
            else
                Reporter.ReportEvent("New Customer Page navigate", "Failed to navigate to New Customer Page", "FAIL");

            return new AddNewCustPage();
        }

        public LoginPage applicationLogout()
        {
            WebUtility.ClickItem<HtmlHyperlink>(WebUtility.PropertyType.InnerText, "Log out");
            Mouse.Click(WebUtility.clickWebPopupOKButton());
            return new LoginPage();
        }
    }
}
