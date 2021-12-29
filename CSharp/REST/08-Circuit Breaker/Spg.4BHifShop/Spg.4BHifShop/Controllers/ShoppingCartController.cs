using Microsoft.AspNetCore.Mvc;
using Spg._4BHifShop.Models;

namespace Spg._4BHifShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            List<SchoppingCartItemDto> result = new()
            {
                new SchoppingCartItemDto() { Name = "Schraubenzieher", Pieces = 1, Price = 45.80M, Tax = 20 },
                new SchoppingCartItemDto() { Name = "Milch", Pieces = 5, Price = 7.94M, Tax = 20 },
                new SchoppingCartItemDto() { Name = "Unterhose", Pieces = 12, Price = 55.96M, Tax = 20 },
                new SchoppingCartItemDto() { Name = "Smart TV 80-Zoll", Pieces = 3, Price = 148936.32M, Tax = 20 },
            };

            return View(result);
        }
    }
}
