using FreshCodedUIProject._01_Manager_Tier.EnvironmentFiles;
using FreshCodedUIProject._02_Utility_Tier;
using FreshCodedUIProject.PageObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace CodedUITestProject7._03_Application_Tier.ApplicationTests.Steps
{
    [Binding]
    public sealed class Steps_Guru99
    {
        private LoginPage oLoginPage;
        private HomePage oHomePage;
        private CustRegistPage oCustRegPage;

        [Given(@"I have Logged into Guru(.*) Manager Homepage")]
        public void GivenIHaveLoggedIntoGuruManagerHomepage(int p0)
        {
            Console.WriteLine(GlbVar.strRelativePath);
             DataTable oTable = ExcelUtil.ExcelToTable(GlbVar.strRelativePath + "03_Application_Tier" + GlbVar.sysFileSeperator + "TestData" + GlbVar.sysFileSeperator + "TestData.xlsx", "Data");

            DataRow[] oDataRows = oTable.Select("TestCaseID = '" + Reporter.strCurrentTestID + "' and ExecutionFlag = 'Y'");
            foreach (DataRow oDataRow in oDataRows)
            {
                //Default Test data Fetching code. Please do not remove it
                ExcelUtil.oCurrentDataRow = oDataRow;
                string CustName = oDataRow["CustName"].ToString();
                int.TryParse(oDataRow["Iteration"].ToString(), out  Reporter.intCurrentIteration);
                //Default Reporting function call for Test Method. Please do not REMOVE this functions calls
                Reporter.HTML_TC_Iteration_Initialize();

                //Start Point for Coding the Test case. 
                //*****************************************************************************************************
                Console.WriteLine(CustName);
              
                    
            }
            oLoginPage = new LoginPage();
            oHomePage = oLoginPage.loginToApplication();
        }

        [When(@"I create a New Customer")]
        public void WhenICreateANewCustomer()
        {
            oCustRegPage =oHomePage.navigateToNewCustPage()
                                    .fillNewCustDetails()
                                    .SubmitNewCustDetails();
        }

        [Then(@"The Customer is successfully created")]
        public void ThenTheCustomerIsSuccessfullyCreated()
        {
            oCustRegPage.navigateToHomePage()
                        .applicationLogout();
        }

        [Given(@"I have Logged into Guru(.*) Manager HomepageRaj and  (.*)")]
        public void GivenIHaveLoggedIntoGuruManagerHomepageRajAnd(int p0, Decimal p1)
        {
            ScenarioContext.Current.Pending();
        }
    

    }
}
