using com.createwebapi.data.Helper;
using Microsoft.AspNetCore.Mvc;

namespace com.createwebapi.webapi.Controllers
{
    [Route("/api")]
    [ApiController]
    public class AboutController : Controller
    {

        private readonly TransactionInterceptor _transactionInterceptor;

        public AboutController(TransactionInterceptor transactionInterceptor)
        {
            _transactionInterceptor = transactionInterceptor;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _transactionInterceptor.TestConection();
            return Ok();
        }

    }
}
