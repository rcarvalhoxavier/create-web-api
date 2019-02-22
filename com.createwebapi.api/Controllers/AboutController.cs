using System;
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
            try
            {
                _transactionInterceptor.TestConection();
                return new JsonResult("Teste Conexão com sucesso");
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
