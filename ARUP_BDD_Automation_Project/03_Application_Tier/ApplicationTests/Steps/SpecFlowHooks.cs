using FreshCodedUIProject._02_Utility_Tier;
using FreshCodedUIProject.PageObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace CodedUITestProject7._03_Application_Tier.ApplicationTests.Steps
{
    [Binding]
    public sealed class SpecFlowHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        [BeforeTestRun]
        public static void BeforeAll()
        {
            Console.WriteLine("Test Initialize");
            CommonFunctions.setRelativePath();
            Reporter.GetEnvConfigDetails();
            Reporter.setTestResultFoders();
            Reporter.HTML_Execution_Summary_Initialize();
        }

        [BeforeScenario]
        public void BeforeScenario()
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
            LoginPage oLoginPage = new LoginPage();
            oLoginPage.launchApplication();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //Default Reporting function call for Test Method. Please do not REMOVE this functions calls
            Reporter.HTML_TC_Iteration_Footer();
            Reporter.HTML_TestCase_Footer();
            Reporter.HTML_Execution_Summary_TCAddLink();
        }

        [AfterTestRun]
        public static void AfterAll()
        {
            Reporter.HTML_Execution_Summary_Footer();

            Process.Start("IExplore.exe", Reporter.strExecSummaryHTMLFilePath);
        }
    }
}
