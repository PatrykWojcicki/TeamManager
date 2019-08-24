using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamManager.ViewModels;

namespace TeamManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly KarateKidDbContext _context;

        public HomeController(KarateKidDbContext context)
        {
            _context = context;
            GroupsController GroupsController = new GroupsController(_context);
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Groups.ToListAsync());
        }

            public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Message"] = "Nowa zakladka.";

            return View();
        }

        public ViewResult CustomerList() => View();



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
