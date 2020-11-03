using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment.UI.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Assignment.Domain.Entity;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using Assignment.Domain.Models;
using System.Text;
using Assignment.UI.Logic;

namespace Assignment.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;
        private readonly IReadFileLogic _Logic;

        public HomeController(IConfiguration configuration, IReadFileLogic logic)
        {
            _Configuration = configuration;
            _Logic = logic;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ItemDetail(string key)
        {
            var url =$"{_Configuration.GetValue<string>("ApiUrl")}?key={key}";
            using var clinet = new HttpClient();
            var httpResponseMessage = await clinet.GetAsync(url);
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                ViewBag.Message = "Item not found";
                return View("index");
            }
            
            var Content = await httpResponseMessage.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<AssignmentTable>(Content, new JsonSerializerOptions { PropertyNamingPolicy = null });
            
            return View(model);
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            string fileExtension = file.FileName.Split(".").LastOrDefault();
            var stream = file.OpenReadStream();
            Result result = _Logic.ImportDataFromFile(stream, fileExtension);
            
            if (string.IsNullOrWhiteSpace(result.Message))
                ViewBag.Message = "Success";

            ViewBag.Message=result.Message;
            return View();            
        }
    }
}
