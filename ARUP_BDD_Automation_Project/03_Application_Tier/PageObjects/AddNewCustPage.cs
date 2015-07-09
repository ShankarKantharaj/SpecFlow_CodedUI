using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreshCodedUIProject._02_Utility_Tier;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace FreshCodedUIProject.PageObjects
{
    class AddNewCustPage
    {
        public AddNewCustPage()
        {

        }

        public AddNewCustPage fillNewCustDetails()
        {
            
            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "name", ExcelUtil.GetData("CustName"));
            WebUtility.ClickRadioOrCheckbox<HtmlRadioButton>(WebUtility.PropertyType.Name, "rad1", ExcelUtil.GetData("Gender"));
            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "dob", ExcelUtil.GetData("DOB"));
            WebUtility.EnterText<HtmlTextArea>(WebUtility.PropertyType.Name, "addr", ExcelUtil.GetData("Address"));
            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "city", ExcelUtil.GetData("City"));
            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "state", ExcelUtil.GetData("State"));
            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "pinno", ExcelUtil.GetData("PIN"));
            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "telephoneno", ExcelUtil.GetData("MobileNumber"));
            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "emailid", ExcelUtil.GetData("E-Mail"));
            WebUtility.EnterText<HtmlEdit>(WebUtility.PropertyType.Name, "password", ExcelUtil.GetData("CustPassword"));

            Reporter.ReportEvent("Enter New Customer Details", "New Customer details entered successfully", "Done");
            return new AddNewCustPage();
        }

        public CustRegistPage SubmitNewCustDetails()
        {
            WebUtility.ClickItem<HtmlInputButton>(WebUtility.PropertyType.Name, "sub");

            if (WebUtility.clickWebPopupOKButton().WaitForControlExist(5000))
            {
                Mouse.Click(WebUtility.clickWebPopupOKButton());
                Reporter.ReportEvent("Unable to Submit Customer Details", "The email ID used already exists", "FAIL");
            }
            else{
                Reporter.ReportEvent("Submit New Customer Details", "New Customer details Submited successfully", "PASS");
            }

            return new CustRegistPage();
        }
    }
}
