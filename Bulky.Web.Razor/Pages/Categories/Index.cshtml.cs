using BulkyBook.Web.Razor.Data;
using BulkyBook.Web.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBook.Web.Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _dbContext;
        public List<Category> Categories { get; set; }

        public IndexModel(ApplicationDBContext dBContext)
        {
                _dbContext = dBContext;
        }
        
        public void OnGet()
        {
            Categories =_dbContext.Categories.ToList();
        }
    }
}
