using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FreshCodedUIProject._01_Manager_Tier.EnvironmentFiles
{
    
public class GlbVar {
	
	//Set Relative & Test result path
	public static string strRelativePath;
	public static string strTestRunResultPath;
	public static string strExecSummaryHTMLFilePath;
	public static string strTestResHTMLFilePath;

	public static string strCurrentApplication;
	public static string strCurrentEnvironment;
	public static string strCurrentModule;
	public static string strOnError;
	public static string strCurrentScenarioID;
	public static string strCurrentScenarioDesc;
	public static string strCurrentTestID;
	public static string strCurrentTestDesc;
	public static string strCurrentTestIterationList;
	public static string strCurrentTestIteration;
	
	public static string strCurrentUserID;
	public static string strCurrentPassword;
	public static string strCurrentURL;
	public static string strCurrentBrowser;

	public static bool  boolScreenshotForPass = true;
	public static bool  boolScreenshotForFail = true;
	public static bool strCurrentTestStatus;
	public static int intScreenShotCount = 0;
	
	public static string strExpLogHeader = "===================================================================================================";
	
	//System Dependent Variables
	public static string sysNewline = Environment.NewLine;
    public static string sysFileSeperator = Path.DirectorySeparatorChar.ToString();
	
	/* You can change the Path of FireFox based on your environment here */
	public static string FIREFOX_PATH = "C:\\Documents and Settings\\veluswar\\Local Settings\\Application Data\\Mozilla Firefox\\firefox.exe";
	
	// Time to wait when searching for a GUI element in Milli seconds
	public static int WAIT_TIME = 10000; 
											

}

}
