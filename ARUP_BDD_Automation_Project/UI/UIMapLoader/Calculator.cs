using Microsoft.VisualStudio.TestTools.UITest.Common.UIMap;
using SpecflowAndCodedUI.UI.GeneralUIMapClasses;
using SpecflowAndCodedUI.UI.ScientificUIMapClasses;
using SpecflowAndCodedUI.UI.StandardUIMapClasses;

namespace SpecflowAndCodedUI.UI.UIMapLoader
{
    public static class Calculator
    {
        public static ScientificUIMap Scientific
        {
            get { return _scientific ?? (_scientific = new ScientificUIMap()); }
        }

        private static ScientificUIMap _scientific;

        public static StandardUIMap Standard
        {
            get { return _standard ?? (_standard = new StandardUIMap()); }
        }

        private static StandardUIMap _standard;

        public static GeneralUIMap General
        {
            get { return _general ?? (_general = new GeneralUIMap()); }
        }

        private static GeneralUIMap _general;
    }
}