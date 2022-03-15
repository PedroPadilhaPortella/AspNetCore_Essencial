//using Microsoft.AspNetCore.Mvc;

//namespace APICatalogo.Controllers
//{
//    //https://localhost:44358/api/teste?api-version=2.0
//    [ApiVersion("2.0")]
//    [ApiVersion("3.0")]
//    //https://localhost:44358/api/2.0/teste
//    [Route("api/{v:apiVersion}/teste")]
//    [ApiController]
//    public class TesteV2Controller : ControllerBase
//    {
//        [HttpGet, MapToApiVersion("2.0")]
//        public IActionResult Get2()
//        {
//            return Content("<html><body><h1>Teste Controller v2.0</h1></body></html>", "text/html");
//        }

//        [HttpGet, MapToApiVersion("3.0")]
//        public IActionResult Get3()
//        {
//            return Content("<html><body><h1>Teste Controller v3.0</h1></body></html>", "text/html");
//        }
//    }
//}
