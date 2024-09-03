using Microsoft.AspNetCore.Mvc;
using MVCRepoPatternDemo.Service;
using MVCRepoPatternDemo.Models;
using MVCRepoPatternDemo.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MVCRepoPatternDemo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _ser;
        private readonly ICategory _category;        

        public ProductsController(IProduct productService,ICategory catser)
        {
            _ser = productService;
            _category = catser;
            
        }

        public IActionResult Index()
        {
            IEnumerable<ProductCl> products = _ser.GetAll();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _ser.GetProduct(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _category.GetAllCategories();
            if (categories == null)
            {
                // Handle the case where categories are null
                return View("Error"); // Redirect to an error page or handle appropriately
            }

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CatName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCl product)
        {
            if (ModelState.IsValid)
            {
                _ser.AddProduct(product);
                return RedirectToAction("Index");
            }
            // Repopulate ViewBag.Categories if ModelState is invalid
            ViewBag.Categories = new SelectList(_category.GetAllCategories(), "CategoryId", "CatName", product.CatId);
            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_ser.GetProduct(id));
        }
        [HttpPost]
        public IActionResult Edit(int id,ProductCl pro)
        {
            _ser.UpdateProduct(id,pro);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_ser.GetProduct(id));
        }
        [HttpPost]
        public IActionResult Delete(ProductCl pro)
        {
            _ser.DeleteProduct(pro);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            var products = _ser.SearchProducts(searchTerm);
            return View("Index", products);  
        }

        [HttpGet]
        public IActionResult Filter(decimal minprice,decimal maxprice)
        {
            var products = _ser.FilterProducts(minprice,maxprice);
            return View("Index", products);
        }

    }
}
