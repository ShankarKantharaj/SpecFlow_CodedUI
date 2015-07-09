using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using FreshCodedUIProject._01_Manager_Tier.EnvironmentFiles;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace FreshCodedUIProject._02_Utility_Tier
{
    public class Reporter {

	public static string strExecSummaryHTMLFilePath;
    public static string strTestResHTMLFilePath;
    public static string strCurrentApplication;
    public static string strCurrentEnvironment;
    public static string strOnError;


    public static string strCurrentUserID;
    public static string strCurrentPassword;
    public static string strCurrentURL;
    public static string strCurrentBrowser;

    public static string strCurrentModule;
    public static string strCurrentScenarioID;
    public static string strCurrentScenarioDesc;
	
	public static string strCurrentTestID;
	public static string strCurrentTestIterationList;
	public static string strCurrentTestDesc;
	public static string strTCStatus;
	
	public static int intScreenshotCount = 0;
	public static int intCurrentIteration = 1;
	public static string strCurrentBusFlowKeyword = "";
	public static int intStepNumber = 1;
	public static int intPassStepCount = 0;
	public static int intFailStepCount = 0;
	public static int intPassTCCount = 0;
	public static int intFailTCCount = 0;
	
	private static double intTCStartTime = 0;
	private static double intTCEndTime = 0;
	private static string strTCDuration = "";
	private static double intExecStartTime = 0;
	private static double intExecEndTime = 0;
	
	//Constructor
	public Reporter(){

		//setRelativePath();
		setTestResultFoders();
	}
	
	

		public static void initialSettings(){

			strCurrentScenarioID = "SC1";
			strCurrentTestID = "TC1";
            strCurrentTestDesc = "hahahahahaah";
            
            HTML_Execution_Summary_Initialize();
			HTML_TestCase_Initialize();
			
			HTML_TC_Iteration_Initialize();
			
			HTML_TC_BusFlowKeyword_Initialize("Logeen");
			
			ReportEvent("Step1", "StepSampleDesc", "PASS");
			ReportEvent("Step1", "StepSampleDesc", "PASS");
			ReportEvent("Step1", "StepSampleDesc", "PASS");
			ReportEvent("Step1", "StepSampleDesc", "PASS");
			ReportEvent("Step1", "StepSampleDesc", "PASS");
			
			HTML_TC_Iteration_Footer();
			
			//
			
			HTML_TC_Iteration_Initialize();			
			HTML_TC_BusFlowKeyword_Initialize("Logout");			
			ReportEvent("Step10", "StepSampleDesc22222222222222222", "FAIL");			
			HTML_TC_Iteration_Footer();
			
			
			HTML_TestCase_Footer();			
			HTML_Execution_Summary_TCAddLink();
			
			HTML_Execution_Summary_Footer();
             
			
			/*
				
			
			GlbVar.strCurrentScenarioID = "SC2";
			GlbVar.strCurrentTestID = "TC2";
			HTMLReporting_Initialize();
			Reporting_TestCase_Initialize();
			Log_Error("Thiss is a error3");
			Log_Error("Thiss is a error4");
			Log_Message("Error Message");
			Reporting_TestCase_Footer();		
			HTML_Execution_Summary_AddLink();
			
			HTML_Execution_Summary_Close();
			
			*/
			
			
		}
		
	

	
	
		
	public static void setTestResultFoders() {

        DirectoryInfo oResDirInfo = new DirectoryInfo(GlbVar.strRelativePath + "04_Results_Tier");
        DirectoryInfo oResBackUpDirInfo = new DirectoryInfo(GlbVar.strRelativePath + "04_Results_Tier" + GlbVar.sysFileSeperator + "Backup");

        if (!oResBackUpDirInfo.Exists)
            oResBackUpDirInfo.Create();

//get all the files from a directory
        DirectoryInfo[] oResSubDirList = oResDirInfo.GetDirectories();
        foreach (DirectoryInfo oSubDir in oResSubDirList){
            if (oSubDir.Name.ToUpper().IndexOf("BACKUP") < 0){
                DirectoryInfo oBackUpTargetDir = new DirectoryInfo(oResDirInfo.FullName + 
							GlbVar.sysFileSeperator + "BackUp" + GlbVar.sysFileSeperator + oSubDir.Name);

                MoveDirectory(oSubDir.FullName, oBackUpTargetDir.FullName);
            }
        }
		string strRunFolderName = "Run_" + getTimeStamp(true);
		
        GlbVar.strTestRunResultPath = GlbVar.strRelativePath + "04_Results_Tier" + GlbVar.sysFileSeperator +
				strRunFolderName + GlbVar.sysFileSeperator;
		Directory.CreateDirectory(GlbVar.strTestRunResultPath);
		Directory.CreateDirectory(GlbVar.strTestRunResultPath + "HTML" + GlbVar.sysFileSeperator);
		Directory.CreateDirectory(GlbVar.strTestRunResultPath + "TEXT" + GlbVar.sysFileSeperator);
		Directory.CreateDirectory(GlbVar.strTestRunResultPath + "SCREENSHOTS" + GlbVar.sysFileSeperator);
	
	}
        public static void MoveDirectory(string strSourceDirectory, string strDestinationDirectory){
            try
            {
                Directory.Move(strSourceDirectory, strDestinationDirectory);  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
	public static string getTimeStamp(bool boolForFolderCreation){

	   if (boolForFolderCreation){
           return DateTime.Now.ToString("dd-MMM-yyyy_hh-mm-ss_tt");
	   }else{
           return DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
	   }
	}
	
         
        
        
	public static void HTML_Execution_Summary_Initialize(){

		intPassTCCount = 0;
		intFailTCCount = 0;
		strExecSummaryHTMLFilePath = GlbVar.strTestRunResultPath + strCurrentApplication + "_" + strCurrentEnvironment + "_Execution_Summary.HTML";
        try {        	
        	string strCSSFilePath = GlbVar.strRelativePath + GlbVar.sysFileSeperator +
                                    "01_Manager_Tier" + GlbVar.sysFileSeperator + "EnvironmentFiles" + GlbVar.sysFileSeperator + "Style.CSS";
        	StreamReader oFileReader = new StreamReader(strCSSFilePath); 

        	
            StreamWriter oFileWriter = new StreamWriter(strExecSummaryHTMLFilePath); 
 
            
            oFileWriter.WriteLine("<!DOCTYPE html>");
            oFileWriter.WriteLine("<html>");
    		oFileWriter.WriteLine("<head>");
    		oFileWriter.WriteLine("		 <meta charset='UTF-8'>"); 
    		oFileWriter.WriteLine("		 <title>CodedUI - Automation Execution Results Summary</title>"); 
            
            string strData;
			while ((strData = oFileReader.ReadLine()) != null) {
				oFileWriter.WriteLine(strData);
			}
        	oFileReader.Close();
        	
        	oFileWriter.WriteLine("</head>"); 
       	 
        	oFileWriter.WriteLine("<body>");
        	oFileWriter.WriteLine("<table id='header'>"); 
			oFileWriter.WriteLine("<colgroup>");
			oFileWriter.WriteLine("<col style='width: 25%' />"); 
			oFileWriter.WriteLine("<col style='width: 25%' />"); 
			oFileWriter.WriteLine("<col style='width: 25%' />"); 
			oFileWriter.WriteLine("<col style='width: 25%' />"); 
			oFileWriter.WriteLine("</colgroup>"); 
     
			oFileWriter.WriteLine("<thead>"); 
			oFileWriter.WriteLine("<tr class='heading'>"); 
			oFileWriter.WriteLine("<th colspan='4' style='font-family:Copperplate Gothic Bold; font-size:1.4em;'>"); 
			oFileWriter.WriteLine("CodedUI -  Automation Execution Result Summary"); 
			oFileWriter.WriteLine("</th>"); 
			oFileWriter.WriteLine("</tr>"); 
			oFileWriter.WriteLine("<tr class='subheading'>"); 
			oFileWriter.WriteLine("<th>&nbsp;Date&nbsp;&&nbsp;Time</th>"); 
			//oFileWriter.WriteLine("<th>&nbsp;:&nbsp;25-Jul-2014&nbsp;05:02:20&nbsp;PM</th>");
			oFileWriter.WriteLine("<th>&nbsp;:&nbsp;" + getTimeStamp(false) + "</th>"); 
			intExecStartTime = DateTime.Now.TimeOfDay.TotalSeconds;
			oFileWriter.WriteLine("<th>&nbsp;Browser</th>");
            oFileWriter.WriteLine("<th>&nbsp;:&nbsp;" + strCurrentBrowser + "</th>"); 
			oFileWriter.WriteLine("</tr>"); 
			oFileWriter.WriteLine("<tr class='subheading'>"); 
			oFileWriter.WriteLine("<th>&nbsp;Application</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;:&nbsp;" + strCurrentApplication + "</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;Environment</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;:&nbsp;" + strCurrentEnvironment + "</th>"); 
			oFileWriter.WriteLine("</tr>"); 
			oFileWriter.WriteLine("</thead>"); 
			oFileWriter.WriteLine("</table>"); 
			 
			oFileWriter.WriteLine("<table id='main'>"); 
			oFileWriter.WriteLine("<colgroup>"); 
			oFileWriter.WriteLine("<col style='width: 10%' />"); 
			oFileWriter.WriteLine("<col style='width: 10%' />"); 
			oFileWriter.WriteLine("<col style='width: 10%' />"); 
			oFileWriter.WriteLine("<col style='width: 44%' />"); 
			oFileWriter.WriteLine("<col style='width: 16%' />"); 
			oFileWriter.WriteLine("<col style='width: 10%' />"); 
			oFileWriter.WriteLine("</colgroup>"); 
			 
			oFileWriter.WriteLine("<thead>"); 
			oFileWriter.WriteLine("<tr class='heading'>"); 
			oFileWriter.WriteLine("<th>Module</th>"); 
			oFileWriter.WriteLine("<th>Test_Scenario</th>"); 
			oFileWriter.WriteLine("<th>Test_Case</th>"); 
			oFileWriter.WriteLine("<th>Test_Description</th>"); 
			oFileWriter.WriteLine("<th>Execution_Time</th>"); 
			oFileWriter.WriteLine("<th>Test_Status</th>"); 
			oFileWriter.WriteLine("</tr>"); 
			oFileWriter.WriteLine("</thead>");
     
            // Always close files.
            oFileWriter.Close(); 
        }
        catch(IOException ex) {
            Console.WriteLine(
                "Error writing to file '"
                + strExecSummaryHTMLFilePath + "'");
            Console.WriteLine(ex.StackTrace);
        }
	}
         

        
		
	public static void HTML_TestCase_Initialize(){
		
		string strCSSFilePath = GlbVar.strRelativePath + GlbVar.sysFileSeperator +
                "01_Manager_Tier" + GlbVar.sysFileSeperator + "EnvironmentFiles" + GlbVar.sysFileSeperator + "Style.CSS";
		
		strTestResHTMLFilePath = GlbVar.strTestRunResultPath + "HTML" + GlbVar.sysFileSeperator + 
				strCurrentApplication + "_" + strCurrentEnvironment + "-" + strCurrentModule + "_" + strCurrentTestID + ".HTML";

		intPassStepCount = 0;
		intFailStepCount = 0;
		strTCStatus = "PASSED";
		
        try {
            StreamReader oFileReader = new StreamReader(strCSSFilePath);
            StreamWriter oFileWriter = new StreamWriter(strTestResHTMLFilePath);
            
            oFileWriter.WriteLine("<!DOCTYPE html>");
            oFileWriter.WriteLine("<html>");
    		oFileWriter.WriteLine("<head>");
    		oFileWriter.WriteLine("		 <meta charset='UTF-8'>"); 
    		oFileWriter.WriteLine("		 <title>" + strCurrentApplication + " Application - "+
    								strCurrentTestID + " Automation Execution Results</title>"); 
            
            string strData;
            while ((strData = oFileReader.ReadLine()) != null)
            {
				oFileWriter.WriteLine(strData);
			}
        	oFileReader.Close();
        	
        	oFileWriter.WriteLine("</head>"); 
            
        	oFileWriter.WriteLine("<body>"); 
			oFileWriter.WriteLine("<table id='header'>"); 
			oFileWriter.WriteLine("<colgroup>"); 
			oFileWriter.WriteLine("<col style='width: 25%' />"); 
			oFileWriter.WriteLine("<col style='width: 25%' />"); 
			oFileWriter.WriteLine("<col style='width: 25%' />"); 
			oFileWriter.WriteLine("<col style='width: 25%' />"); 
			oFileWriter.WriteLine("</colgroup>"); 
			
			oFileWriter.WriteLine("<thead>"); 
			oFileWriter.WriteLine("<tr class='heading'>"); 
			oFileWriter.WriteLine("<th colspan='4' style='font-family:Copperplate Gothic Bold; font-size:1.4em;'>"); 
			oFileWriter.WriteLine(strCurrentApplication + " Application - "+
								strCurrentTestID + " Automation Execution Results");
			oFileWriter.WriteLine("</th>"); 
			oFileWriter.WriteLine("</tr>"); 
			oFileWriter.WriteLine("<tr class='subheading'>"); 
			oFileWriter.WriteLine("<th>&nbsp;Date&nbsp;&&nbsp;Time</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;:&nbsp;" + getTimeStamp(false) + "</th>");
            intTCStartTime = DateTime.Now.TimeOfDay.TotalSeconds;
			oFileWriter.WriteLine("<th>&nbsp;Iterations</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;:&nbsp;" + strCurrentTestIterationList + "</th>"); 
			oFileWriter.WriteLine("</tr>");
            
          /*
			oFileWriter.WriteLine("<tr class='subheading'>"); 
			oFileWriter.WriteLine("<th>&nbsp;Start&nbsp;Iteration</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;:&nbsp;1</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;End&nbsp;Iteration</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;:&nbsp;1</th>"); 
			oFileWriter.WriteLine("</tr>"); */
        
			oFileWriter.WriteLine("<tr class='subheading'>"); 
			oFileWriter.WriteLine("<th>&nbsp;Browser</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;:&nbsp;" + strCurrentBrowser + "</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;Executed&nbsp;on</th>"); 
			oFileWriter.WriteLine("<th>&nbsp;:&nbsp;" + Dns.GetHostName() + "</th>"); 
			oFileWriter.WriteLine("</tr>"); 
			oFileWriter.WriteLine("</thead>"); 
			oFileWriter.WriteLine("</table>"); 
        
			oFileWriter.WriteLine("<table id='main'>"); 
			oFileWriter.WriteLine("<colgroup>"); 
			oFileWriter.WriteLine("<col style='width: 8%' />"); 
			oFileWriter.WriteLine("<col style='width: 12%' />"); 
			oFileWriter.WriteLine("<col style='width: 62%' />"); 
			oFileWriter.WriteLine("<col style='width: 8%' />"); 
			oFileWriter.WriteLine("<col style='width: 10%' />"); 
			oFileWriter.WriteLine("</colgroup>"); 
			
			oFileWriter.WriteLine("<thead>"); 
			oFileWriter.WriteLine("<tr class='heading'>"); 
			oFileWriter.WriteLine("<th>Step_No</th>"); 
			oFileWriter.WriteLine("<th>Step_Name</th>"); 
			oFileWriter.WriteLine("<th>Description</th>"); 
			oFileWriter.WriteLine("<th>Status</th>"); 
			oFileWriter.WriteLine("<th>Step_Time</th>"); 
			oFileWriter.WriteLine("</tr>"); 
			oFileWriter.WriteLine("</thead>"); 
    		
            // Always close files.	
            oFileWriter.Close();
        }
        catch(IOException ex) {
            Console.WriteLine(
                "Error writing to file '"
                + strTestResHTMLFilePath + "'");
                Console.WriteLine(ex.StackTrace);
        }
	}
	
         
       
	public static void HTML_TC_Iteration_Initialize(){
        try {
        	
            StreamWriter oFileWriter = new StreamWriter(strTestResHTMLFilePath, true);
            string strIteration = "Iteration: " + intCurrentIteration;
            oFileWriter.WriteLine("<tbody>");
			oFileWriter.WriteLine("<tr class='section'>");
			oFileWriter.WriteLine("<td colspan='5' onclick=\"toggleMenu('Iteration" + intCurrentIteration + "')\">+ Iteration: " + intCurrentIteration + "</td>"); 
			oFileWriter.WriteLine("</tr>"); 
			oFileWriter.WriteLine("</tbody>"); 
			oFileWriter.WriteLine("<tbody id='Iteration" + intCurrentIteration + "' style='display:table-row-group'>");
    		
            // Always close files.
            oFileWriter.Close();
        }
        catch(IOException ex) {
            Console.WriteLine(
                "Error writing to file '"
                + strTestResHTMLFilePath + "'");
            Console.WriteLine(ex.StackTrace);
        }
	}
	
	public static void HTML_TC_Iteration_Footer(){
		
        try {

            StreamWriter oFileWriter = new StreamWriter(strTestResHTMLFilePath, true);            
            oFileWriter.WriteLine("</tbody>");
            
            // Always close files.
            oFileWriter.Close();
        }
        catch(IOException ex) {
            Console.WriteLine(
                "Error writing to file '"
                + strTestResHTMLFilePath + "'");
            Console.WriteLine(ex.StackTrace);
        }
	}
		
	public static void HTML_TC_BusFlowKeyword_Initialize(string strBusFlowKeyword){
		intStepNumber = 1;
		strCurrentBusFlowKeyword = strBusFlowKeyword;
        try {
        	
            StreamWriter oFileWriter = new StreamWriter(strTestResHTMLFilePath, true);
			oFileWriter.WriteLine("<tr class='subheading subsection'>"); 
			oFileWriter.WriteLine("<td colspan='5' onclick=\"toggleSubMenu('Iteration"+ intCurrentIteration + strCurrentBusFlowKeyword + "')\">&nbsp;+ " + strCurrentBusFlowKeyword + "</td>");  
			oFileWriter.WriteLine("</tr>");  
    		
            // Always close files.	
            oFileWriter.Close();
        }
        catch(IOException ex) {
            Console.WriteLine(
                "Error writing to file '"
                + strTestResHTMLFilePath + "'");
            Console.WriteLine(ex.StackTrace);
        }
	}
	
	public static void ReportEvent(string strStepName, string strStepDescription, string strStatus){
		
        try {
        	strStatus = strStatus.ToUpper();
            StreamWriter oFileWriter = new StreamWriter(strTestResHTMLFilePath, true);
            
            
            oFileWriter.WriteLine("<tr class='content' id='Iteration" + intCurrentIteration + strCurrentBusFlowKeyword + intStepNumber + "'>"); 
			oFileWriter.WriteLine("<td>" + intStepNumber + "</td>"); 
			oFileWriter.WriteLine("<td class='justified'>" + strStepName + "</td>");  
			oFileWriter.WriteLine("<td class='justified'>" + strStepDescription + "</td>");  
			if (((GlbVar.boolScreenshotForPass) && (strStatus == "PASS")) || ((GlbVar.boolScreenshotForFail) && (strStatus == "FAIL"))){				
				string strScreeshotPath = GlbVar.strTestRunResultPath +  "SCREENSHOTS" + GlbVar.sysFileSeperator + strCurrentApplication + strCurrentModule + "-" + 
														strCurrentScenarioID + strCurrentTestID + "-" +  intScreenshotCount + ".png";
				CaptureScreenShot(strScreeshotPath);
				
				oFileWriter.WriteLine("<td class='" + strStatus.ToLower() + "'><a href='" + strScreeshotPath +
										"'>" + strStatus + "</a></td>");
			}else{
				oFileWriter.WriteLine("<td class='" + strStatus.ToLower() + "'>" + strStatus + "</td>");				
			} 
			oFileWriter.WriteLine("<td><small>" + getTimeStamp(false) + "</small></td>"); 
			oFileWriter.WriteLine("</tr>"); 
    		
			
			if (strStatus == "PASS") intPassStepCount++;
			if (strStatus == "FAIL") {
				intFailStepCount++;
				strTCStatus = "FAILED";
			}
			
            // Always close files.	            
       		
       		
            oFileWriter.Close();
            
            intStepNumber++;
        }
        catch(IOException ex) {
            Console.WriteLine(
                "Error writing to file '"
                + strTestResHTMLFilePath + "'");
            Console.WriteLine(ex.StackTrace);
        }
	}
	
	//Capture Screenshot
	private static void CaptureScreenShot(string strScreenshotPath) {
		// capture the whole screen
        /*
        Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        Graphics graphics = Graphics.FromImage(bitmap as Image);
        graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
        bitmap.Save(strScreenshotPath, ImageFormat.Png);
		
         */
        Image MyImage = UITestControl.Desktop.CaptureImage();
        MyImage.Save(strScreenshotPath);
        intScreenshotCount++;
	}
	
	public static void HTML_TestCase_Footer() {
		
		try{
			
			StreamWriter oFileWriter = new StreamWriter(strTestResHTMLFilePath, true);
	        
	        
	        
	        
	        oFileWriter.WriteLine("</table>"); 
	        
			oFileWriter.WriteLine("<table id='footer'>");
			oFileWriter.WriteLine("<colgroup>");
			oFileWriter.WriteLine("<col style='width: 25%' />");
			oFileWriter.WriteLine("<col style='width: 25%' />");
			oFileWriter.WriteLine("<col style='width: 25%' />");
			oFileWriter.WriteLine("<col style='width: 25%' />");
			oFileWriter.WriteLine("</colgroup>");
			 
			oFileWriter.WriteLine("<tfoot>");
			oFileWriter.WriteLine("<tr class='heading'>");
            Thread.Sleep(5000);
			intTCEndTime = DateTime.Now.TimeOfDay.TotalSeconds;
			strTCDuration = getDuration(intTCStartTime, intTCEndTime) ;
			oFileWriter.WriteLine("<th colspan='4'>Execution Duration: " + strTCDuration + "</th>");
			oFileWriter.WriteLine("</tr>");
			oFileWriter.WriteLine("<tr class='subheading'>");
			oFileWriter.WriteLine("<td class='pass'>&nbsp;Steps passed</td>");
			oFileWriter.WriteLine("<td class='pass'>&nbsp;: " + intPassStepCount + "</td>");
			oFileWriter.WriteLine("<td class='fail'>&nbsp;Steps failed</td>");
			oFileWriter.WriteLine("<td class='fail'>&nbsp;: " + intFailStepCount + "</td>");
			oFileWriter.WriteLine("</tr>");
			oFileWriter.WriteLine("</tfoot>");
			oFileWriter.WriteLine("</table>");
			oFileWriter.WriteLine("</body>");
			oFileWriter.WriteLine("</html>");
			
	        // Always close files.	            
	   		
	   		
	        oFileWriter.Close();
	        
	        intStepNumber++;
	    }
	    catch (Exception ex) {
	        Console.WriteLine(
	            "Error writing to file '"
	            + strTestResHTMLFilePath + "'");
	        Console.WriteLine(ex.StackTrace);
	    }
		
}
	
	public static void HTML_Execution_Summary_TCAddLink(){
		
        try { 
        	
            StreamWriter oFileWriter = new StreamWriter(strExecSummaryHTMLFilePath, true);
            	            
            
			oFileWriter.WriteLine("<tr class='content' >");
			oFileWriter.WriteLine("<td class='justified'>" + strCurrentModule + "</td>");
			oFileWriter.WriteLine("<td class='justified'>" + strCurrentScenarioID + "</td>");
			oFileWriter.WriteLine("<td class='justified'><a href='" + strTestResHTMLFilePath + "' target='about_blank'>" + strCurrentTestID + "</a></td>");
			oFileWriter.WriteLine("<td class='justified'>" + strCurrentTestDesc + "</td>");
			oFileWriter.WriteLine("<td>" + strTCDuration + "</td>");			
			oFileWriter.WriteLine("<td class='" + strTCStatus.ToLower().Substring(0, 4) + "'>" + strTCStatus + "</td>");
			oFileWriter.WriteLine("</tr>");
     

			if (strTCStatus == "PASSED") intPassTCCount++;
			if (strTCStatus == "FAILED") intFailTCCount++;
			
            // Always close files.	
            
           
            oFileWriter.Close();
        }
        catch(IOException ex) {
            Console.WriteLine(
                "Error writing to file '"
                + strExecSummaryHTMLFilePath + "'");
            Console.WriteLine(ex.StackTrace);
        }
	}
	
	public static void HTML_Execution_Summary_Footer(){
		
        try { 
        	
            StreamWriter oFileWriter = new StreamWriter(strExecSummaryHTMLFilePath, true);
            	            
            
						
			oFileWriter.WriteLine("</tbody>");
			oFileWriter.WriteLine("</table>");
			 
			oFileWriter.WriteLine("<table id='footer'>");
			oFileWriter.WriteLine("<colgroup>");
			oFileWriter.WriteLine("<col style='width: 25%' />");
			oFileWriter.WriteLine("<col style='width: 25%' />");
			oFileWriter.WriteLine("<col style='width: 25%' />");
			oFileWriter.WriteLine("<col style='width: 25%' />");
			oFileWriter.WriteLine("</colgroup>");
			 
			oFileWriter.WriteLine("<tfoot>");
			oFileWriter.WriteLine("<tr class='heading'>");

			intExecEndTime = DateTime.Now.TimeOfDay.TotalSeconds;
			oFileWriter.WriteLine("<th colspan='4'>Total Duration: " + getDuration(intExecStartTime, intExecEndTime) + "</th>");
			oFileWriter.WriteLine("</tr>");
			oFileWriter.WriteLine("<tr class='subheading'>");
			oFileWriter.WriteLine("<td class='pass'>&nbsp;Tests passed</td>");
			oFileWriter.WriteLine("<td class='pass'>&nbsp;: " + intPassTCCount + "</td>");
			oFileWriter.WriteLine("<td class='fail'>&nbsp;Tests failed</td>");
			oFileWriter.WriteLine("<td class='fail'>&nbsp;: " + intFailTCCount + "</td>");
			oFileWriter.WriteLine("</tr>");
			oFileWriter.WriteLine("</tfoot>");
			oFileWriter.WriteLine("</table>");
			oFileWriter.WriteLine("</body>");
			oFileWriter.WriteLine("</html>");
     
            // Always close files.	
            
           
            oFileWriter.Close();
        }
        catch(IOException ex) {
            Console.WriteLine(
                "Error writing to file '"
                + strExecSummaryHTMLFilePath + "'");
            Console.WriteLine(ex.StackTrace);
        }
	}


    public static void GetEnvConfigDetails()
    {
        Reporter.strCurrentApplication = "";
        Reporter.strCurrentEnvironment = "";
        Reporter.strCurrentURL = "";
        Reporter.strCurrentUserID = "";
        Reporter.strCurrentPassword = "";
        Reporter.strCurrentBrowser = "";
        DataTable oTable = ExcelUtil.ExcelToTable(GlbVar.strRelativePath + "01_Manager_Tier" + GlbVar.sysFileSeperator + "EnvironmentFiles" + GlbVar.sysFileSeperator + "EnvironmentConfig.xlsx", "Env_Config");

        DataRow[] oDataRows = oTable.Select("Execution_Flag = 'Y'");
        if (oDataRows.Length < 1)
            Assert.Inconclusive("Execution Aborted! Please Set Execution flag in the Environment Config File");

        DataRow oDataRow = oDataRows[0];
        Reporter.strCurrentApplication = oDataRow["Application_Name"].ToString();
        Reporter.strCurrentEnvironment = oDataRow["Environment"].ToString();
        Reporter.strCurrentURL = oDataRow["Environment_URL"].ToString();
        Reporter.strCurrentUserID = oDataRow["UserName"].ToString();
        Reporter.strCurrentPassword = oDataRow["Password"].ToString();
        Reporter.strCurrentBrowser = oDataRow["Browser"].ToString();
    }
	
	private static string getDuration(double dblStartTime, double dblEndTime){
		string strDuration = "";
        int lngDiff = (int)(dblEndTime - dblStartTime);
		int intHour = lngDiff/(60*60);
		int intMin = (lngDiff % (60*60))/60;
		int intSec = (lngDiff % (60*60)) % 60;
		
		if (intHour > 0) {
			strDuration = intHour + " hour(s), " + intMin + " minute(s), " + intSec + " seconds";
		}else{
			strDuration = intMin + " minute(s), " + intSec + " seconds";
		}
		
		return strDuration;
	}
        

        /*
        private void createDirectory(string strDirectory){
            File files = new File(strDirectory);
            if (!files.exists()) {
                if (files.mkdirs()) {
                    //Console.WriteLine("Directory: " + strDirectory + " is created!");
                } else {
                    Console.WriteLine("Directory: " + strDirectory + " is not created!" + GlbVar.sysNewline +
                            "Please check the Directory path");
                }
            }

        }
	
        */
}
}
