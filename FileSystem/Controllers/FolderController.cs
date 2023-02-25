using FileSystem.Context;
using FileSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Controllers
{
    public class FolderController : Controller
    {
        private readonly DataContext context;

        public FolderController(DataContext _context)
        {
            context = _context;
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
                if (folder.Id != 0) folder.Id = 0;
                context.Folders.Add(folder);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { @id = folder.Parent_Id });
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var folderToDelete = context.Folders.SingleOrDefault(x => x.Id == id);
            foreach (var x in context.Folders.Where(x => x.Parent_Id == id))
            {
                context.Folders.Remove(x);
            }
            context.Folders.Remove(folderToDelete!);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
