using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace aspnetcore3._1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(SearchString))
                return;

            using var httpclient = new HttpClient();
            using var response = httpclient.GetAsync($"https://gasesandboxfunction.azurewebsites.net/api/HttpTrigger1?name={SearchString}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            ViewData["Message"] = content;
        }
    }
}
