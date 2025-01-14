using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackendLearnUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncTestController : ControllerBase
    {
        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var Task = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("task hecho");
            });
            Task.Start();

            Console.WriteLine($"estamos en otra cosa");
            Console.WriteLine($"empezamos en {stopwatch.Elapsed}");

            await Task;
            Console.WriteLine($"task acaba en {stopwatch.Elapsed}");


            return Ok(stopwatch.Elapsed);
        }
    }
}
