using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;

namespace Andgasm.HelpInstruction.Web.Component
{
    public class HelpInstructionRenderExtensions
    {
        string _apirooturl;

        public HelpInstructionRenderExtensions(string apirooturl)
        {
            _apirooturl = apirooturl;
        }

        public HtmlString RenderForKeyAsHtmlString(IViewComponentHelper component, string host, string key, string title = "", bool loadondemand = false)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            component.InvokeAsync("HelpInstruction", new { apirooturl = _apirooturl, hostkey = host, datakey = key, label = title, ondemand = loadondemand}).Result.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        public HtmlString RenderStandaloneScriptsAsHtmlString(IHtmlHelper html, bool ondemand)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            html.PartialAsync("Components/HelpInstruction/PartialScripts", new HelpInstructionModel() { loadOnDemand = ondemand }).Result.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        public HtmlString RenderStandaloneStylesAsHtmlString(IHtmlHelper html)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            html.PartialAsync("Components/HelpInstruction/PartialStyles").Result.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
