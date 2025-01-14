using BackendLearnUdemy.DTO;

namespace BackendLearnUdemy.Services
{
    public interface IPostsService
    {
        public Task<IEnumerable<PostDTO>> Get();
    }
}
