using BlogSystem.API.Controllers;
using BlogSystem.UI.Framework.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace BlogSystem.Tests.Api
{
    public class ApiTests
    {
        [Fact]
        public void TestGetPosts()
        {
            // Arrange
            var dbContext = DbContextMocker.GetDbContext(nameof(TestGetPosts));
            var controller = new BlogController(dbContext);

            // Act
            if (controller.GetPosts() is ObjectResult response)
            {
                // Assert that blog posts are exactly 1
                Assert.True(response.Value is IList<PostModel> value && value.Count == 1);
            }

            dbContext.Dispose();
        }

        [Fact]
        public void TestGetCategories()
        {
            // Arrange
            var dbContext = DbContextMocker.GetDbContext(nameof(TestGetCategories));
            var controller = new BlogController(dbContext);

            // Act
            if (controller.GetCategories() is ObjectResult response)
            {
                // Assert that categories are exactly 1
                Assert.True(response.Value is IList<CategoryModel> value && value.Count == 1);
            }

            dbContext.Dispose();
        }

        [Fact]
        public void TestCreatePost()
        {
            // Arrange
            var dbContext = DbContextMocker.GetDbContext(nameof(TestCreatePost));
            var controller = new BlogController(dbContext);

            // Act
            var postModel = new PostModel()
            {
                Name = "Post 2",
                PostCategories = new List<CategoryModel>()
                {
                    new CategoryModel()
                    {
                        Id = new Guid("A7C7E5BB-7805-4C80-A8E2-F2D3FD941FA0") // Category 1 Id from the DbContextMocker
                    }
                }
            };

            if (controller.CreatePost(postModel) is ObjectResult response)
            {
                // Assert that blog posts are now 2
                Assert.True(response.Value is IList<PostModel> value && value.Count == 2);
            }

            dbContext.Dispose();
        }
    }
}
