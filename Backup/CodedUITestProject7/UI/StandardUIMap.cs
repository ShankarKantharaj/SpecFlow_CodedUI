namespace SpecflowAndCodedUI.UI.StandardUIMapClasses
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;

    public partial class StandardUIMap
    {
        public void TypeAValue(string inputValue)
        {
            #region Variable Declarations

            WinTitleBar uICalculatorTitleBar = this.UICalculatorWindow.UICalculatorTitleBar;
            WinText inputLableValue = this.UICalculatorWindow.InputLabel.InputLableValue;

            #endregion Variable Declarations

            // Click 'Calculator' title bar
            Mouse.Click(uICalculatorTitleBar, new Point(73, 5));

            // Type A Value in to 'Input' label
            Keyboard.SendKeys(inputLableValue, inputValue, ModifierKeys.None);
        }

        //    /// <summary>
        //    /// AssertTheResult - Use 'AssertTheResultExpectedValues' to pass parameters into this method.
        //    /// </summary>
        //    public void AssertTheResult()
        //    {
        //        #region Variable Declarations

        //        WinText inputLableValue = this.UICalculatorWindow.InputLabel.InputLableValue;

        //        #endregion Variable Declarations

        //        // Verify that 'Input' label's property 'DisplayText' equals 'Value'
        //        Assert.AreEqual(this.AssertTheResultExpectedValues.InputLableValueDisplayText, inputLableValue.DisplayText);
        //    }

        //    public virtual AssertTheResultExpectedValues AssertTheResultExpectedValues
        //    {
        //        get
        //        {
        //            if ((this.mAssertTheResultExpectedValues == null))
        //            {
        //                this.mAssertTheResultExpectedValues = new AssertTheResultExpectedValues();
        //            }
        //            return this.mAssertTheResultExpectedValues;
        //        }
        //    }

        //    private AssertTheResultExpectedValues mAssertTheResultExpectedValues;
        //}

        ///// <summary>
        ///// Parameters to be passed into 'AssertTheResult'
        ///// </summary>
        //[GeneratedCode("Coded UITest Builder", "10.0.40219.1")]
        //public class AssertTheResultExpectedValues
        //{
        //    #region Fields

        //    /// <summary>
        //    /// Verify that 'Input' label's property 'DisplayText' equals 'Value'
        //    /// </summary>
        //    public string InputLableValueDisplayText = "Value";

        //    #endregion Fields
    }
}