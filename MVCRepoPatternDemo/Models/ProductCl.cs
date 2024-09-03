using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCRepoPatternDemo.Models
{
    [Table("ProductsTable")]
    public class ProductCl //One products will be mapped to one category
    {
        [Key]
        [Column("ProdId")]
        public int ProId { get; set; }
        [Required]
        [StringLength(30,MinimumLength =5,ErrorMessage ="Product name should have minimum of 5 characters")]
        public string? ProName { get; set; }
        [Range(1000,100000,ErrorMessage ="Product price should be between 1000 and 100000")]
        public decimal Price { get; set; }
        public int WarrantyinYears { get; set; }
        public int CatId { get; set; }
        [ForeignKey("CatId")]
        public Category? categories { get; set; }
       
    }
}
