using System;
using System.Diagnostics;
using System.Reflection;
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
                var assembly = Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                var version = fvi.FileVersion;
                var buildNumber = fvi.ProductVersion;
                var date = System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location);

                return Ok($"{version} - {date:dd/MM/yyyy} -  {buildNumber}");
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
