using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using SampleASPCore.Models;

namespace SampleASPCore.Controllers
{
    public class HelloController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluQGdtYWlsLmNvbSIsIm5iZiI6MTU2NTk0MTAwNSwiZXhwIjoxNTY1OTYyNjA1LCJpYXQiOjE1NjU5NDEwMDV9.D3meoBIwIYqJArqPaX2IVqUDku9PWjtneI5h4erZ5zQ");
            HttpResponseMessage response = await _client.GetAsync("https://localhost:44338/api/Barang");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<List<Barang>>(content);

            return View(results);
        }

        public IActionResult About()
        {
            return Content("About");
        }

        public IActionResult Contact()
        {
            return Content("Contact");
        }

        public IActionResult Tentang()
        {
            return Content("tentang aplikasi ini");
        }
    }
}