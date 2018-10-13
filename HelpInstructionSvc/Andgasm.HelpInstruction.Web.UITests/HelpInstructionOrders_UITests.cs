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
    public class HelpInstructionOrders_UITests
    {
        KendoGridAutomationMap _map = new KendoGridAutomationMap("https://localhost:44336/", "chrome", "Help Instruction Administration - Andgasm.HelpInstruction.Web");

        #region Host Order Tests
        [TestMethod]
        public void HelpInstruction_OrderByHost_Asc()
        {
            // filter on host key
            // confirm the correct filter applied (3 rows)
            _map.LaunchBroswerAndNavigate();
            _map.OrderGrid("Host", "asc");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByHost_Desc()
        {
            // filter on host key
            // confirm the correct filter applied (0 rows)
            _map.LaunchBroswerAndNavigate();
            _map.OrderGrid("Host", "desc");

            // TODO: assertions!

            _map.CloseBrowser();
        }
        #endregion

        #region Key Order Tests
        [TestMethod]
        public void HelpInstruction_OrderByKey_Asc()
        {
            // filter on host key
            // confirm the correct filter applied (3 rows)
            _map.LaunchBroswerAndNavigate();
            _map.OrderGrid("Key", "asc");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByKey_Desc()
        {
            // filter on host key
            // confirm the correct filter applied (0 rows)
            _map.LaunchBroswerAndNavigate();
            _map.OrderGrid("Key", "desc");

            // TODO: assertions!

            _map.CloseBrowser();
        }
        #endregion

        #region Tooltip Order Tests
        [TestMethod]
        public void HelpInstruction_OrderByTooltip_Asc()
        {
            // filter on host key
            // confirm the correct filter applied (3 rows)
            _map.LaunchBroswerAndNavigate();
            _map.OrderGrid("Tooltip", "asc");

            // TODO: assertions!

            _map.CloseBrowser();
        }

        [TestMethod]
        public void HelpInstruction_FilterByTooltip_Desc()
        {
            // filter on host key
            // confirm the correct filter applied (0 rows)
            _map.LaunchBroswerAndNavigate();
            _map.OrderGrid("Tooltip", "desc");

            // TODO: assertions!

            _map.CloseBrowser();
        }
        #endregion
    }
}
