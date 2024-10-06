using APP.Context;
using APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace APP.Controllers
    
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly AppDbContext _db;
        public ItemsController(AppDbContext db,IHostingEnvironment host)
        {
            _db = db;
            _host = host;
        }
        private readonly IHostingEnvironment _host;
        public IActionResult Index()
        {
            IEnumerable<Item> ItemsList = _db.Items.Include(c=> c.Category).ToList();
            return View(ItemsList);
        }
        public IActionResult New()
        {

            createSelectList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            if (item.Name == "100") {
                ModelState.AddModelError("Name", "Name can't be eqaul 100");
            }
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (item.clientFile != null)
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "imgs");
                    fileName = item.clientFile.FileName;
                    string fullPath = Path.Combine(myUpload, fileName);
                    item.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    item.imgPath = fileName;
                }
                _db.Items.Add(item);
                _db.SaveChanges();
                TempData["Success"] = "Item has been added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }
        public void createSelectList(int selectId=1)
        {
            //List<Category>categories=new List<Category>
            //{
            //    new Category(){Id=0,Name="Select Category"},
            //    new Category(){Id=1,Name="Computers"},
            //    new Category(){Id=2,Name="Mobiles"},
            //    new Category(){Id=3,Name="Electric Machine"},
            //    new Category(){Id=4,Name="Clothes"},
            //};
            List<Category> categories =_db.Categories.ToList();
            SelectList listItems = new SelectList(categories,"Id","Name",selectId);
            ViewBag.CategoryList = listItems;
        }
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0) {return NotFound();}
            var item = _db.Items.FirstOrDefault(x => x.Id == Id);
            if (item == null) { return NotFound(); }
            createSelectList(item.CategoryId);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't be eqaul 100");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Update(item);
                _db.SaveChanges();
                TempData["Success"] = "Item has been updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0) { return NotFound(); }
            var item = _db.Items.FirstOrDefault(x => x.Id == Id);
            if (item == null) { return NotFound(); }
            createSelectList(item.CategoryId);

            return View(item);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int? Id)
        {
                var item =_db.Items.FirstOrDefault(x => x.Id ==Id);
                 if (item == null) { return NotFound(); }
                _db.Items.Remove(item);
                _db.SaveChanges();
            TempData["Success"] = "Item has been deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
