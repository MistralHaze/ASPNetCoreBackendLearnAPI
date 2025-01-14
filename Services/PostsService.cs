using BackendLearnUdemy.DTO;
using System.Text.Json;

namespace BackendLearnUdemy.Services
{
    public class PostsService: IPostsService
    {
        private HttpClient _httpClient;

        public PostsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDTO>> Get()
        {
            var getURLInfo = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var body = await getURLInfo.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var post = JsonSerializer.Deserialize<IEnumerable<PostDTO>>(body, options);

            return post;
        }
    }
}
