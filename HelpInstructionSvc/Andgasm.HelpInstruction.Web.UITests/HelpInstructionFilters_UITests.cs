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
    public class HelpInstructionFilters_UITests
    {
        KendoGridAutomationMap _map = new KendoGridAutomationMap("https://localhost:44336/", "chrome", "Help Instruction Administration - Andgasm.HelpInstruction.Web");

        #region Host Filter Tests
        [TestMethod]
        public void HelpInstruction_FilterByHost_Equals()
        {
            // filter on host key
            // confirm the correct filter applied (3 rows)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Host", "eq", "HelpInstructionSvc");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByHost_NotEquals()
        {
            // filter on host key
            // confirm the correct filter applied (0 rows)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Host", "neq", "HelpInstructionSvc");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByHost_Contains()
        {
            // filter on host key
            // confirm the correct filter applied (3 rows)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Host", "contains", "HelpInstruction");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByHost_StartsWith()
        {
            // filter on host key
            // confirm the correct filter applied (3 rows)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Host", "startswith", "Help");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByHost_EndsWith()
        {
            // filter on host key
            // confirm the correct filter applied (3 rows)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Host", "endswith", "InstructionSvc");

            // TODO: assertions!

            _map.CloseBrowser();
        }
        #endregion

        #region Key Filter Tests
        [TestMethod]
        public void HelpInstruction_FilterByKey_Equals()
        {
            // filter on host key
            // confirm the correct filter applied (1 row)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Key", "eq", "Host");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByKey_NotEquals()
        {
            // filter on host key
            // confirm the correct filter applied (2 rows)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Key", "neq", "Host");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByKey_Contains()
        {
            // filter on host key
            // confirm the correct filter applied (1 row)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Key", "contains", "Tooltip");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByKey_StartsWith()
        {
            // filter on host key
            // confirm the correct filter applied (1 row)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Key", "startswith", "Lookup");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByKey_EndsWith()
        {
            // filter on host key
            // confirm the correct filter applied (1 row)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Key", "endswith", "Text");

            // TODO: assertions!

            _map.CloseBrowser();
        }
        #endregion

        #region Tooltip Filter Tests
        [TestMethod]
        public void HelpInstruction_FilterByTooltip_Equals()
        {
            // filter on host key
            // confirm the correct filter applied (1 row)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Tooltip", "eq", "I am the host tooltip text!");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByTooltip_NotEquals()
        {
            // filter on host key
            // confirm the correct filter applied (2 rows)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Tooltip", "neq", "I am the host tooltip text!");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByTooltip_Contains()
        {
            // filter on host key
            // confirm the correct filter applied (1 row)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Tooltip", "contains", "I am the tooltip");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByTooltip_StartsWith()
        {
            // filter on host key
            // confirm the correct filter applied (1 row)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Tooltip", "startswith", "I am the tooltip");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByTooltip_EndsWith()
        {
            // filter on host key
            // confirm the correct filter applied (1 row)
            _map.LaunchBroswerAndNavigate();
            _map.FilterGrid("Tooltip", "endswith", "text tooltip text");

            // TODO: assertions!

            _map.CloseBrowser();
        }
        #endregion
    }
}
