using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
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

            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            //Here we are using All Categories list in Dropdown for every product
            IEnumerable<SelectListItem> categories = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });

            //Ways to pass this list to Product view
            //1. ViewBag
            //ViewBag.CategoryList = categories;
            //2. ViewData
            //ViewData["CategoryList"] = categories;
            //3. Using new ViewModel like ProductViewModel

            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = categories
            };

            if (id == null || id == 0)
            {
                //Create
                return View(productViewModel);
            }
            else
            {
                //Update
                productViewModel.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productViewModel);
            }

        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                //We can pass Tempdata message to next rendered view. Once page is refreshed TempData value goes away. TempData is created only to pass on some messages to next Rendered view.
                TempData["success"] = "Product created sucessfully.";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
                return View(productVM);
            }
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
