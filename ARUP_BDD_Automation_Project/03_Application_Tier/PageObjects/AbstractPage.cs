using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace FreshCodedUIProject.PageObjects
{
    class AbstractPage
    {
        protected BrowserWindow oBrowser;

        public AbstractPage()
        {

        }
        public AbstractPage(BrowserWindow oBrowser)
        {
            this.oBrowser = oBrowser;
        }

        
        public LoginPage launchApplication()
        {
            oBrowser = BrowserWindow.Launch("http://demo.guru99.com/V4");
            return new LoginPage();
        }

    }
}
