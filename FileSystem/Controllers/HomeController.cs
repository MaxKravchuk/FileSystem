using FileSystem.Context;
using FileSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FileSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext context;

        public HomeController(DataContext _context)
        {
            context = _context;
        }

        public IActionResult Index(int? id = null)
        {
            var folders = context.Folders.Where(x => x.Parent_Id == id);
            ViewBag.Id = id;
            if (id != null)
            {
                ViewBag.Name = context.Folders.Where(x=>x.Id == id).SingleOrDefault().Name;
                ViewBag.ParentId = context.Folders.Where(x => x.Id == id).SingleOrDefault().Parent_Id;
            }
            else
            {
                ViewBag.Name = "Root";
            }
            return View(folders);
        }

        [HttpGet]
        public IActionResult Create(int? id = null)
        {
            ViewBag.ParrentId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Folder folder)
        {
            if (ModelState.IsValid)
            {   
                if(folder.Id!=0) folder.Id = 0;
                context.Folders.Add(folder);
                context.SaveChanges();
                return RedirectToAction("Index","Home", new {@id = folder.Parent_Id});
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var folderToDelete = context.Folders.SingleOrDefault(x => x.Id==id);
            foreach(var x in context.Folders.Where(x => x.Parent_Id == id))
            {
                context.Folders.Remove(x);
            }
            context.Folders.Remove(folderToDelete!);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Back(int id)
        {
            if(id == 0) return RedirectToAction("Index");
            return RedirectToAction("Index", "Home", new { @id = id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}