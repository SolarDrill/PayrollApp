using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Payroll_Frontend.Pages
{
	public class IndexModel : PageModel
	{
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        //public async Task OnGetAsync()
        //{
        //    var data = await _apiService.GetDataAsync("https://localhost:44326/api/AccountingEntries");
        //    ViewData["ApiData"] = data;
        //}
    }
}
