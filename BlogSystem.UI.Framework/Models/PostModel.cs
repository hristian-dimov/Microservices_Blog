using System;
using System.Collections.Generic;

namespace BlogSystem.UI.Framework.Models
{
    public class PostModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<CategoryModel> PostCategories { get; set; }
    }
}
