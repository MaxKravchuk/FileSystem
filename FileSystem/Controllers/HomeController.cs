using FileSystem.Context;
using FileSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FileSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext context;

        public HomeController(ILogger<HomeController> logger, DataContext _context)
        {
            _logger = logger;
            context = _context;
        }

        public IActionResult Index(int? id = null)
        {
            var folders = context.Folders.Where(x => x.Parent_Id == id);
            ViewBag.Id = id;
            return View(folders);
        }

        [HttpGet]
        public IActionResult Create(int? id = null)
        {
            ViewBag.ParrentId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Folder folder)
        {
            if (ModelState.IsValid)
            {   
                if(folder.Id!=0) folder.Id = 0;
                context.Folders.Add(folder);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}