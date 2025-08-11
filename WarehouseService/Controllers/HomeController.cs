using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        [HttpGet("resources")]
        public IActionResult Resources()
        {
            return View();
        }

        [HttpGet("units")]
        public IActionResult Units()
        {
            return View();
        }

        [HttpGet("balances")]
        public IActionResult Balances()
        {
            return View();
        }

        [HttpGet("receipt-documents")]
        public IActionResult ReceiptDocuments()
        {
            return View();
        }
    }
}
