using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyBook.Web.Razor.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("Category Name:")]
        [MaxLength(30, ErrorMessage = "Maximum length is 30 characters.")]
        public string Name { get; set; }

        [DisplayName("Display Order:")]
        [Range(1, 100, ErrorMessage = "Display order should be between 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
