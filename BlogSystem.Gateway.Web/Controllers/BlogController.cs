using BlogSystem.UI.Framework.Models;
using BlogSystem.UI.Framework.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogSystem.Gateway.Web.Controllers
{
    [ApiController]
    public class BlogController : ControllerBase
    {
        //API URLs
        private const string BaseApiUrl = "http://localhost:59502/";

        private const string PostsUrl = BaseApiUrl + "posts";
        private const string CategoriesUrl = BaseApiUrl + "categories";

        [HttpPost]
        [Route("posts")]
        public async Task<string> CreatePost([FromBody]PostModel model)
        {
            // do something with the data, if necessary

            string result = await HttpClientWrapper.PostAsync(PostsUrl, model);

            return result;
        }

        [HttpGet]
        [Route("posts")]
        public async Task<string> GetPosts()
        {
            string result = await HttpClientWrapper.GetAsync(PostsUrl);

            // do something with the data, if necessary

            return result;
        }

        [HttpGet]
        [Route("categories")]
        public async Task<string> GetCategories()
        {
            string result = await HttpClientWrapper.GetAsync(CategoriesUrl);

            // do something with the data, if necessary

            return result;
        }

        [HttpGet]
        [Route("home")]

        public IActionResult Index()
        {
            return Ok();
        }
    }
}