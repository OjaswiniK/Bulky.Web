using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private readonly ApplicationDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //This is custom validation 
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //Add it to ModelState Name property to display the custom validation message
                ModelState.AddModelError("Name", "Custom message: Name and Display order cannot be same");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                //Add it to ModelState to display the custom validation message
                ModelState.AddModelError("", "Test is invalid value");
            }

            //ModelState.IsValid is for Server side validation which MaxLength and range
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                //_dbContext.Categories.Add(obj);
                //_dbContext.SaveChanges();
                //We can pass Tempdata message to next rendered view. Once page is refreshed TempData value goes away. TempData is created only to pass on some messages to next Rendered view.
                TempData["success"] = "Category created sucessfully.";
                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryRow = _unitOfWork.Category.Get(u => u.CategoryId == id);
            //Category? CategoryRow2 = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            //Category? categoryRow3 = _dbContext.Categories.Where(c => c.CategoryId == id).FirstOrDefault();

            if (categoryRow == null)
            {
                return NotFound();
            }
            return View(categoryRow);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            //ModelState.IsValid is for Server side validation which MaxLength and range
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                //_dbContext.Categories.Update(obj);
                //_dbContext.SaveChanges();
                TempData["success"] = "Category updated sucessfully.";
                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryRow = _unitOfWork.Category.Get(u => u.CategoryId == id);
            //Category? CategoryRow2 = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            //Category? categoryRow3 = _dbContext.Categories.Where(c => c.CategoryId == id).FirstOrDefault();

            if (categoryRow == null)
            {
                return NotFound();
            }
            return View(categoryRow);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryRow = _unitOfWork.Category.Get(u => u.CategoryId == id);

            if (categoryRow == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(categoryRow);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted sucessfully.";
            return RedirectToAction("Index");

        }
    }
}

