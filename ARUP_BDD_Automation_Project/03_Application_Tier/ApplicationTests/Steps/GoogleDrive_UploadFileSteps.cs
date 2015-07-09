using System;
using TechTalk.SpecFlow;

namespace CodedUITestProject7._03_Application_Tier.Features
{
    [Binding]
    public class GoogleDrive_UploadFileSteps
    {
        [Given(@"Logged into Google Drive")]
        public void GivenLoggedIntoGoogleDrive()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Given File is uploaded")]
        public void WhenGivenFileIsUploaded(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"File Uploaded successfully")]
        public void ThenFileUploadedSuccessfully()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
