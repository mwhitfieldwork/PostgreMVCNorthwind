using Microsoft.AspNetCore.Mvc;
using NWCodeFirstMVC.Domain;
using NWCodeFirstMVC.Domain.Models;

namespace NWCodeFirstMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly northwindContext _dc;
        public ProductsController(northwindContext dc)
        {
            _dc = dc;
        }

        //[HttpGet]
        //public JsonResult GetAllProducts()
        //{
        //    IQueryable<Product> products = _dc.Products;

        //    var results = products.Select(x =>
        //    new
        //    {
        //        productId = x.ProductId,
        //        ProductName = x.ProductName,
        //        SupplierId = x.SupplierId,
        //        CategoryId = x.CategoryId,
        //        QuantityPerUnit = x.QuantityPerUnit,
        //        UnitPrice = x.UnitPrice,
        //        UnitsInStock = x.UnitsInStock,
        //        UnitsOnOrder = x.UnitsOnOrder,
        //        ReorderLevel = x.ReorderLevel,
        //        Discontinued = x.Discontinued
        //    }).ToList();

        //    return Json(results);
        //}

        public ActionResult Index()
        {
            return View(_dc.Products.ToList());
        }


        public ActionResult View()
        {
            Category model = new Category();
            //model.Initialize(_dc);

            return View(model); 
        }
    }
}
