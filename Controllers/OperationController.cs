using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendLearnUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpGet]
        public decimal Get(decimal a, decimal b)
        {
            return a + b;
        }


        [HttpPost]
        public decimal Post(Numbers num, [FromHeader] string Host, [FromHeader(Name = "Content-Length")] string ContentLength)
        {
            Console.WriteLine(Host);
            Console.WriteLine(ContentLength);
            return num.A - num.B;
        }

        [HttpPut]
        public decimal Put(decimal a, decimal b)
        {
            return a;
        }

        [HttpDelete]
        public decimal Delete(decimal a, decimal b)
        {
            return b;
        }
    }

    public class Numbers
    {
        public decimal A { get; set; }
        public decimal B { get; set; }
    }
}
