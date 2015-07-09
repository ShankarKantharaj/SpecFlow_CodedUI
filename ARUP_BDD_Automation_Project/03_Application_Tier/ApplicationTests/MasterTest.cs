using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;

using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using FreshCodedUIProject.PageObjects;
using System.Data;
using FreshCodedUIProject._02_Utility_Tier;
using FreshCodedUIProject._01_Manager_Tier.EnvironmentFiles;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UITest.Common.UIMap;

namespace FreshCodedUIProject._03_Application_Tier.ApplicationTests
{
    /// <summary>
    /// This is the MaterTest class that contains the initialize and cleanup methods
    /// </summary>
    [CodedUITest]
    public class MasterTest
    {
        public MasterTest()
        {
        }

       [AssemblyInitialize]
        public static void InittializeBaseState(TestContext hi){
           /*
            Console.WriteLine("Test Initialize");
            CommonFunctions.setRelativePath();
            Reporter.GetEnvConfigDetails();
            Reporter.setTestResultFoders();
            Reporter.HTML_Execution_Summary_Initialize();
            */
       }

        

        [TestInitialize]
        public void SetAppLaunch(){
            LoginPage oLoginPage = new LoginPage();
            oLoginPage.launchApplication();
        }

        [TestMethod]
        public void TC1_CreateNewCustomer()
        {
            //Following Variable needs to be filled accordingly for each Test Method. Do not have spaces within Module & Test name
            //******************************************************************************************************
            Reporter.strCurrentModule = "Manager_Account";
            Reporter.strCurrentScenarioID = "SC1";
            Reporter.strCurrentTestID = "TC1";
            Reporter.strCurrentTestDesc = "Create New Customer";
            //*******************************************************************************************************
            //Default Reporting function call for Test Method. Please do not REMOVE this functions calls
            Reporter.HTML_TestCase_Initialize();

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
                LoginPage oLoginPage = new LoginPage();
                oLoginPage.loginToApplication()
                    .navigateToNewCustPage()
                    .fillNewCustDetails()
                    .SubmitNewCustDetails()

                    .navigateToHomePage()
                    .applicationLogout();

                //this.UIMap.GDrive();



                //*****************************************************************************************************
                //End Point for Coding the Test case. 
            }

            //Default Reporting function call for Test Method. Please do not REMOVE this functions calls
            Reporter.HTML_Execution_Summary_TCAddLink();
            Reporter.HTML_TC_Iteration_Footer();
        }

        [TestCleanup]
        public void TestClosure()
        {
            Reporter.HTML_TestCase_Footer();

        }

        [AssemblyCleanup]
        public static void ReturnBaseState()
        {
            /*
            Reporter.HTML_Execution_Summary_Footer();

            Process.Start("IExplore.exe", Reporter.strExecSummaryHTMLFilePath);
        
             */
             
          }

        #region Additional test attributes

        
        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
