using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
                _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            IEnumerable<SelectListItem> categories = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();                
                //We can pass Tempdata message to next rendered view. Once page is refreshed TempData value goes away. TempData is created only to pass on some messages to next Rendered view.
                TempData["success"] = "Product created sucessfully.";
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
            Product? productRow = _unitOfWork.Product.Get(u => u.Id == id);            

            if (productRow == null)
            {
                return NotFound();
            }
            return View(productRow);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            //ModelState.IsValid is for Server side validation which MaxLength and range
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();                
                TempData["success"] = "Product updated sucessfully.";
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
            Product? productRow = _unitOfWork.Product.Get(u => u.Id == id);            

            if (productRow == null)
            {
                return NotFound();
            }
            return View(productRow);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? productRow = _unitOfWork.Product.Get(u => u.Id == id);

            if (productRow == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(productRow);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted sucessfully.";
            return RedirectToAction("Index");

        }
    }
}
