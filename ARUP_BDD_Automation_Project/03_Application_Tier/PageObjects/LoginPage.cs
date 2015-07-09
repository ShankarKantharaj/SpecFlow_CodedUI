using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using FreshCodedUIProject;
using FreshCodedUIProject._02_Utility_Tier;

namespace FreshCodedUIProject.PageObjects
{
    class LoginPage //: AbstractPage
    {
       /*public LoginPage(BrowserWindow oBrowser)
        {
            this.oBrowser = base.oBrowser;
        }
        */

        public LoginPage launchApplication()
        {
            //BrowserWindow.CurrentBrowser = "Firefox";
            BrowserWindow oBrowser = BrowserWindow.Launch("http://demo.guru99.com/V4");

            oBrowser.CloseOnPlaybackCleanup = false;
            /*
            if (!oBrowser.Maximized)
            {
                oBrowser.Maximized = true;
            }
             */ 
            return new LoginPage();
        }
        public HomePage loginToApplication()
        {
            //Testdata sheet login credentials overwrites Environment config Login credentials
            if (Reporter.strCurrentUserID == "") Reporter.strCurrentUserID = ExcelUtil.GetData("LoginUserName");
            if (Reporter.strCurrentPassword == "") Reporter.strCurrentPassword = ExcelUtil.GetData("LoginPassword");

            Reporter.ReportEvent("Login", "Loging in with User: " + Reporter.strCurrentUserID, "Done");

            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "uid", Reporter.strCurrentUserID);
            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "password", Reporter.strCurrentPassword);
            WebUtility.ClickItem<HtmlInputButton>(WebUtility.PropertyType.Name, "btnLogin");

            //Validate Login
            if (WebUtility.WaitForItemExist<HtmlHyperlink>(WebUtility.PropertyType.InnerText, "New Customer"))
                Reporter.ReportEvent("Login successful.", "Logged in successfully with User: " + Reporter.strCurrentUserID, "PASS");
            else
                Reporter.ReportEvent("Login Fail.", "Login is un successfully with User: " + Reporter.strCurrentUserID, "FAIL");

            return new HomePage();
        }
    }
}
