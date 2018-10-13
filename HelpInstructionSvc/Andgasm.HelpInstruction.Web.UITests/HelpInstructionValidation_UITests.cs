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
    [CodedUITest]
    public class HelpInstructionValidation_UITests
    {
        KendoGridAutomationMap _map = new KendoGridAutomationMap("https://localhost:44336/", "chrome", "Help Instruction Administration - Andgasm.HelpInstruction.Web");

        [TestMethod]
        public void HelpInstruction_InvalidHostKey()
        {
            // click create, enter data and update
            // assert that the data row is created
            _map.LaunchBroswerAndNavigate();
            _map.ClickLinkButtonByInnerText("Create", 500);
            _map.TypeToTextboxByName("hostKey", "");
            _map.TypeToTextboxByName("lookupKey", "HelpInstruction_ValidCreate_KEY");
            _map.TypeToTextboxByName("tooltipText", "HelpInstruction_ValidCreate_TOOLTIP");
            _map.ClickLinkButtonByInnerText("Update", 500);

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_InvalidLookupKey()
        {
            // click create, enter data and update
            // assert that the data row is created
            _map.LaunchBroswerAndNavigate();
            _map.ClickLinkButtonByInnerText("Create", 500);
            _map.TypeToTextboxByName("hostKey", "HelpInstruction_ValidCreate_HOST");
            _map.TypeToTextboxByName("lookupKey", "");
            _map.TypeToTextboxByName("tooltipText", "HelpInstruction_ValidCreate_TOOLTIP");
            _map.ClickLinkButtonByInnerText("Update", 500);

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_InvalidTooltip()
        {
            // click create, enter data and update
            // assert that the data row is created
            _map.LaunchBroswerAndNavigate();
            _map.ClickLinkButtonByInnerText("Create", 500);
            _map.TypeToTextboxByName("hostKey", "HelpInstruction_ValidCreate_HOST");
            _map.TypeToTextboxByName("lookupKey", "HelpInstruction_ValidCreate_KEY");
            _map.TypeToTextboxByName("tooltipText", "");
            _map.ClickLinkButtonByInnerText("Update", 500);

            // TODO: assertions!

            _map.CloseBrowser();
        }
    }
}
