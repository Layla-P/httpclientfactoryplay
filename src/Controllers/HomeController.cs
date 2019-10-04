using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HttpClientFactory.Models;
using HttpClientFactory.Services;

namespace HttpClientFactory.Controllers
{
    public class HomeController : Controller
    {
        private readonly INumberDetailsService _numberDetailsService;

        public HomeController(INumberDetailsService numberDetailsService)
        {
            _numberDetailsService = numberDetailsService ?? throw new ArgumentException(nameof(numberDetailsService));
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var vm = new NumberSearchViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(NumberSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Messages = await _numberDetailsService.GetMessagesTo(model.Number);

            }
            return View(model);
        }
    }
}
