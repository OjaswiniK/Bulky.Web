using BulkyBook.Web.Razor.Data;
using BulkyBook.Web.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBook.Web.Razor.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDBContext _dbContext;
        public Category Category { get; set; }

        public CreateModel(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost() {

            _dbContext.Categories.Add(Category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category created sucessfully.";

            return RedirectToPage("Index");
        }
    }
}
