using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshCodedUIProject._01_Manager_Tier.EnvironmentFiles;

namespace FreshCodedUIProject._02_Utility_Tier
{
    class WebUtility 
    {


        private static BrowserWindow mParentBrowserWindow { get; set; }
        public static BrowserWindow topParentWindow()
        {
            BrowserWindow browser = new BrowserWindow();

            //BrowserWindow.CurrentBrowser = "Firefox";
            Console.WriteLine(BrowserWindow.CurrentBrowser.ToString());
            browser.SearchProperties[BrowserWindow.PropertyNames.ClassName] = BrowserWindow.CurrentBrowser.ToString();
            //  browser.SearchProperties[BrowserWindow.PropertyNames.Name] = "";
            return browser;
           
        }

        public static BrowserWindow ParentWindow
        {
            get
            {
                if (mParentBrowserWindow == null)
                {
                    mParentBrowserWindow = topParentWindow();
                }
                return mParentBrowserWindow;
            }
        }

        //Generic Function to enter Text in Editbox, Text Area etc
        public static void EnterText<T>(PropertyType type, string propertyValue, string text) where T : HtmlControl 
        {
            
            HtmlControl genericControl = (T)Activator.CreateInstance(typeof(T), new object[] {ParentWindow});

            if (type == PropertyType.Name)
                genericControl.SearchProperties[HtmlControl.PropertyNames.Name] = propertyValue;
            else if (type == PropertyType.ID)
                genericControl.SearchProperties[HtmlControl.PropertyNames.Id] = propertyValue;

            Keyboard.SendKeys(genericControl, text);
            
        }

        //Generic Function to click Button, Image or Link
        public static void ClickItem<T>(PropertyType type, string propertyValue) where T : HtmlControl
        {
            HtmlControl genericControl = (T)Activator.CreateInstance(typeof(T), new object[] { ParentWindow });

            if (type == PropertyType.Name)
                genericControl.SearchProperties[HtmlControl.PropertyNames.Name] = propertyValue;
            else if (type == PropertyType.ID)
                genericControl.SearchProperties[HtmlControl.PropertyNames.Id] = propertyValue;
            else if (type == PropertyType.InnerText)
                genericControl.SearchProperties[HtmlControl.PropertyNames.InnerText] = propertyValue;

            Mouse.Click(genericControl);
        }

        //Generic Function to click Button, Image or Link
        public static bool WaitForItemExist<T>(PropertyType type, string propertyValue) where T : HtmlControl
        {
            HtmlControl genericControl = (T)Activator.CreateInstance(typeof(T), new object[] { ParentWindow });

            if (type == PropertyType.Name)
                genericControl.SearchProperties[HtmlControl.PropertyNames.Name] = propertyValue;
            else if (type == PropertyType.ID)
                genericControl.SearchProperties[HtmlControl.PropertyNames.Id] = propertyValue;
            else if (type == PropertyType.InnerText)
                genericControl.SearchProperties[HtmlControl.PropertyNames.InnerText] = propertyValue;

            if (genericControl.WaitForControlExist(GlbVar.WAIT_TIME))
                return true;
            else
                return false;
        }

        //Generic Function to click Button, Image or Link
        public static void ClickRadioOrCheckbox<T>(PropertyType type, string propertyValue, string strValue) where T : HtmlControl
        {
            HtmlControl genericControl = (T)Activator.CreateInstance(typeof(T), new object[] { ParentWindow });

            if (type == PropertyType.Name)
                genericControl.SearchProperties[HtmlControl.PropertyNames.Name] = propertyValue;
            else if (type == PropertyType.ID)
                genericControl.SearchProperties[HtmlControl.PropertyNames.Id] = propertyValue;
            else if (type == PropertyType.InnerText)
                genericControl.SearchProperties[HtmlControl.PropertyNames.InnerText] = propertyValue;

            genericControl.SearchProperties[HtmlControl.PropertyNames.ValueAttribute] = strValue;

            genericControl.SetProperty("Selected", true);
            //Mouse.DoubleClick(genericControl);
        }

        public static UITestControl clickWebPopupOKButton(){

            UITestControl popup = new UITestControl();
            popup.TechnologyName = "MSAA";
            popup.SearchProperties.Add("ClassName", "#32770", "Name", "Message from webpage");

            //popup.SearchProperties.Add("ClassName", "Chrome_WidgetWin_1", "Name", "The page at www.demo.guru99.com says:");
            //"ControlType", "Dialog",
            UITestControl btnOK = new UITestControl(popup);
            btnOK.TechnologyName = "MSAA";
            btnOK.SearchProperties.Add("ControlType", "Button", "Name", "OK");

            return btnOK;
        }
        public enum PropertyType
        {
            Name,
            ID,
            InnerText,
            ClassName,
            TagInstance
        }

    }
}
