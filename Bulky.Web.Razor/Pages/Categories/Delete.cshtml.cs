using BulkyBook.Web.Razor.Data;
using BulkyBook.Web.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBook.Web.Razor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDBContext _dbContext;
        public Category? Category { get; set; }

        public DeleteModel(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public void OnGet(int? id)
        {
            if(id != null && id != 0)
            {
                Category = _dbContext.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            _dbContext.Categories.Remove(Category); ;
            _dbContext.SaveChanges();

            TempData["success"] = "Category deleted succesfully";
            return RedirectToPage("Index");
        }
    }
}
