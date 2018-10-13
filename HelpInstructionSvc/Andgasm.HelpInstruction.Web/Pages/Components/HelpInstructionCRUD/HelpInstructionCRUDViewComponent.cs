using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Andgasm.HelpInstruction.Web
{
    public class HelpInstructionCRUDViewComponent : ViewComponent
    {
        public HelpInstructionCRUDViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
