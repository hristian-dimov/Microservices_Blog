using BlogSystem.UI.Framework.Models;
using BlogSystem.UI.Framework.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogSystem.UI.Controllers
{
    public class BlogController : Controller
    {
        //gateway URL
        private const string BaseGatewayUrl = "http://localhost:59478/";

        private const string PostsUrl = BaseGatewayUrl + "posts";
        private const string CategoriesUrl = BaseGatewayUrl + "categories";

        [HttpPost]
        public async Task<IActionResult> Posts([FromBody] PostModel model)
        {
            string blogPostsResult = await HttpClientWrapper.PostAsync(PostsUrl, model);

            IList<PostModel> blogPostModels = JsonConvert.DeserializeObject<IList<PostModel>>(blogPostsResult);

            return PartialView("_Posts", blogPostModels);
        }

        [HttpGet]
        public async Task<IActionResult> Posts()
        {
            string blogPostsResult = await HttpClientWrapper.GetAsync(PostsUrl);

            IList<PostModel> blogPostModels = JsonConvert.DeserializeObject<IList<PostModel>>(blogPostsResult);

            return PartialView("_Posts", blogPostModels);
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            string categoriesResult = await HttpClientWrapper.GetAsync(CategoriesUrl);

            IList<CategoryModel> categoryModels = JsonConvert.DeserializeObject<IList<CategoryModel>>(categoriesResult);

            return PartialView("_Categories", categoryModels);
        }
    }
}