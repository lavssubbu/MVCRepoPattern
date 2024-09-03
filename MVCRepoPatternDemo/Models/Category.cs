using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCRepoPatternDemo.Models
{
    public class Category //one category will be mapped with many products
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(25)]
        [DisplayName("CategoryName")]
        public string? CatName { get; set; }

        public ICollection<ProductCl>? products { get; set; }
    }
}
