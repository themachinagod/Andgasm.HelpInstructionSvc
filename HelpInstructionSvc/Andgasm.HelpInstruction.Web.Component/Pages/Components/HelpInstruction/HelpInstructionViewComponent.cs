using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Andgasm.HelpInstruction.Web.Component
{
    // DBr: view components when used as tag helper dont support optionals just now and dont look like they will,
    //      option is to break out roslyn code and try to make a fix and check in for review!!
    public class HelpInstructionViewComponent : ViewComponent
    {
        HttpClient _client = new HttpClient();

        string _apirooturl;
        string _label;
        string _hostkey;
        string _datakey;
        bool _suppressScripts;
        bool _suppressStyles;
        bool _loadondemand;

        public HelpInstructionViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(string apirooturl, string hostkey, string datakey, 
                                                            string label = "", bool suppressscripts = false, bool suppressstyles = false, bool ondemand = false)
        {
            _apirooturl = apirooturl;
            _label = label;
            _hostkey = hostkey;
            _datakey = datakey;
            _suppressScripts = suppressscripts;
            _suppressStyles = suppressstyles;
            _loadondemand = ondemand;

            if (ondemand) return View(GetTooltipForKeyAsyncOnDemand());
            else return View(await GetTooltipForKeyAsync());
        }

        private async Task<HelpInstructionModel> GetTooltipForKeyAsync()
        {
            using (var response = await _client.GetAsync($"{_apirooturl}/api/helpinstruction/Lookup/{_datakey}"))
            {
                HelpInstructionModel data;
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsJsonAsync<HelpInstructionModel>();
                }
                else data = new HelpInstructionModel() { lookupKey = _datakey, tooltipText = $"No data exists for key '{_hostkey}:{_datakey}'" };
                InitialiseTooltip(data);
                return data;
            };
        }

        private HelpInstructionModel GetTooltipForKeyAsyncOnDemand()
        {
            HelpInstructionModel data;
            data = new HelpInstructionModel() { lookupKey = _datakey, tooltipText = $"Awaiting data response from server for key '{_hostkey}:{_datakey}'" };
            InitialiseTooltip(data);
            return data;
        }

        private void InitialiseTooltip(HelpInstructionModel data)
        {
            data.label = _label;
            data.suppressScripts = _suppressScripts;
            data.suppressStyles = _suppressStyles;
            data.loadOnDemand = _loadondemand;
            data.apiUrl = _apirooturl;
            data.imageurl = $"{Request.Scheme}://{Request.Host}/images/help.png";
        }
    }

    public static class HttpContentExtensions
    {
        // TODO: move to extensions helper
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }
    }
}
