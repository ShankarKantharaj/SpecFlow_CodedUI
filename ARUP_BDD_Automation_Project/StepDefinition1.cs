namespace SpecflowAndCodedUI.StepDefinitions
{
    using SpecflowAndCodedUI.UI.UIMapLoader;
    using System;
    using TechTalk.SpecFlow;

    [Binding]
    public class StepDefinitions
    {
        /*
        [BeforeTestRun]
        public static void BeforeAll()
        {

            Console.WriteLine("Before ALL");
        }
        [BeforeScenario()]
        public static void PrepareForTest()
        {
            Calculator.General.LaunchCalculator();
            Console.WriteLine("Before Scenario");
        }

        [AfterScenario]
        public void CleanUpTest()
        {
            Calculator.General.CloseCalculator();
            Console.WriteLine("After Scenario");
        }
        [AfterTestRun]
        public static void AfterAll()
        {

            Console.WriteLine("After ALL");
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(string input)
        {
            Calculator.Standard.TypeAValue(input);
        }

        [Given(@"I press add")]
        public void GivenIPressAdd()
        {
            Calculator.Standard.ClickAddButton();
        }

        [When(@"I press equals")]
        public void WhenIPressEquals()
        {
            Calculator.Standard.ClickEqualsButton();
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBe120OnTheScreen(string expectedResult)
        {
            //Is is the alternative to modifying the Coded UI Test Method to accept a parameter as we did for the TypeAValue method
            //Instead you can use the Coded UI Tests Params properties to pass the value.
            Calculator.Standard.AssertTheResultExpectedValues.InputLableValueDisplayText = expectedResult;
            Calculator.Standard.AssertTheResult();
        }
         */ 
    }
}