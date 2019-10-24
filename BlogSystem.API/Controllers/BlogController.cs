using BlogSystem.API.Data;
using BlogSystem.API.Domain;
using BlogSystem.UI.Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogSystem.API.Controllers
{
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogDbContext _dbContext;

        public BlogController(BlogDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpPost]
        [Route("posts")]
        public IActionResult CreatePost([FromBody]PostModel model)
        {
            Post post = new Post()
            {
                Id = new Guid(),
                Name = model.Name
            };

            _dbContext.Posts.Add(post);

            foreach (var postCategoryModel in model.PostCategories)
            {
                PostCategory postCategory = new PostCategory()
                {
                    PostId = post.Id,
                    CategoryId = postCategoryModel.Id
                };

                _dbContext.PostCategories.Add(postCategory);
            }

            _dbContext.SaveChanges();

            var postModels = PrepareBlogPostModels();

            return Ok(postModels);
        }

        [HttpGet]
        [Route("posts")]
        public IActionResult GetPosts()
        {
            var postModels = PrepareBlogPostModels();

            return Ok(postModels);
        }

        [HttpGet]
        [Route("categories")]
        public IActionResult GetCategories()
        {
            var categoryModels = PrepareCategoryModels();

            return Ok(categoryModels);
        }

        [HttpGet]
        [Route("home")]

        public IActionResult Index()
        {
            return Ok();
        }

        private IList<PostModel> PrepareBlogPostModels()
        {
            var posts = _dbContext.Posts
                .Include(pc => pc.PostCategories)
                .ThenInclude(x => x.Category)
                .OrderBy(p => p.Name)
                .ToList();

            var postModels = posts.Select(p => new PostModel()
            {
                Id = p.Id,
                Name = p.Name,
                PostCategories = p.PostCategories.Select(c => new CategoryModel()
                {
                    Id = c.CategoryId,
                    Name = c.Category.Name
                }).ToList()
            }).ToList();

            return postModels;
        }

        private IList<CategoryModel> PrepareCategoryModels()
        {
            var categories = _dbContext.Categories
                .OrderBy(p => p.Name)
                .ToList();

            var categoryModels = categories.Select(c => new CategoryModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return categoryModels;
        }
    }
}