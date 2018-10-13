using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System.Linq;
using Andgasm.Web.CUIT.Core;

namespace Andgasm.HelpInstruction.Web.UITests
{
    // These test must run in order:
    // See associated ordered test

    [CodedUITest]
    public class HelpInstructionCRUD_UITests
    {
        KendoGridAutomationMap _map = new KendoGridAutomationMap("https://localhost:44336/", "chrome", "Help Instruction Administration - Andgasm.HelpInstruction.Web");

        [TestMethod]
        public void HelpInstruction_ValidCreate()
        {
            // click create, enter data and update
            // assert that the data row is created
            _map.LaunchBroswerAndNavigate();
            _map.ClickLinkButtonByInnerText("Create", 500);
            _map.TypeToTextboxByName("hostKey", "HelpInstruction_ValidCreate_HOST");
            _map.TypeToTextboxByName("lookupKey", "HelpInstruction_ValidCreate_KEY");
            _map.TypeToTextboxByName("tooltipText", "HelpInstruction_ValidCreate_TOOLTIP");
            _map.ClickLinkButtonByInnerText("Update", 500);

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_ValidUpdate()
        {
            // filter to known existing item, click edit, enter data and update
            // assert that the data row is updated
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Key", "eq", "HelpInstruction_ValidCreate_KEY");
            _map.ClickLinkButtonByInnerText("Edit", 500);
            _map.TypeToTextboxByName("hostKey", "HelpInstruction_ValidCreate_HOST_UPD");
            _map.TypeToTextboxByName("lookupKey", "HelpInstruction_ValidCreate_KEY_UPD");
            _map.TypeToTextboxByName("tooltipText", "HelpInstruction_ValidCreate_TOOLTIP_UPD");
            _map.ClickLinkButtonByInnerText( "Update", 500);

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_ValidDelete()
        {
            // filter to known existing item, click delete, confirm
            // assert that the data row is deleted
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Key", "eq", "HelpInstruction_ValidCreate_KEY_UPD");
            _map.ClickLinkButtonByInnerText("Delete", 500);
            Keyboard.SendKeys("{ENTER}");
            Thread.Sleep(500);

            // TODO: assertions!

            _map.CloseBrowser();
        }
    }
}
