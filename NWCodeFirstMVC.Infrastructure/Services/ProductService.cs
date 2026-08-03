
using NWCodeFirstMVC.Domain.Contracts;
using NWCodeFirstMVC.Domain.PocoModels;
using NWCodeFirstMVC.Infrastructure;

namespace NWCodeFirstMVC.Infrastructure.Services
{
    // Implements the inteface. This is the depndency inversion
    // Which states that higher level components dont depend on lower level ones
    // So the interface implementation doenst depend on the controller.
    // Instead there is an intermediary. The service implements the interface and is used
    // inside the contorller and keeps higher level functions

    public class ProductService: GenericService<Product>,IProductService 
    {
        public ProductService(PgNwContext dc):base(dc)
        {

        }
    }
}
