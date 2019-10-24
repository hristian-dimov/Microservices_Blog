using BlogSystem.API.Data;
using BlogSystem.API.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlogSystem.Tests.Api
{
    public static class DbContextMocker
    {
        public static BlogDbContext GetDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new BlogDbContext(options);

            var post = new Post()
            {
                Id = new Guid("2C7D7CE9-BD5A-4328-9E15-88D57A590FA0"),
                Name = "Post 1"
            };

            var category = new Category()
            {
                Id = new Guid("A7C7E5BB-7805-4C80-A8E2-F2D3FD941FA0"),
                Name = "Category 1"
            };

            var postCategory = new PostCategory()
            {
                PostId = post.Id,
                CategoryId = category.Id
            };

            dbContext.Posts.Add(post);

            dbContext.Categories.Add(category);

            dbContext.PostCategories.Add(postCategory);

            dbContext.SaveChanges();

            return dbContext;
        }
    }
}
