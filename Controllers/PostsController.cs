using BackendLearnUdemy.DTO;
using BackendLearnUdemy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendLearnUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        IPostsService _postsService;
        public PostsController( IPostsService people) 
        {
            _postsService = people;
        }

    /*    [HttpGet]
        public async Task<IEnumerable<PostDTO>> GetPosts()
        {
            var dataResult= await _postsService.Get();
            if (dataResult == null) { return NotFound(); }
            return Ok(dataResult);
        }*/

        [HttpGet]
        public async Task<IEnumerable<PostDTO>> GetPostsAsync()
        {
            var dataResult = await _postsService.Get();
            return dataResult;
        }
    }
}
