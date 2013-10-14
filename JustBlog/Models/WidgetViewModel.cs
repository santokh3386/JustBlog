using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JustBlog.Core.Interface;
using JustBlog.Core.Objects;

namespace JustBlog.Models
{
    public class WidgetViewModel
    {
        public WidgetViewModel(IBlogRepository blogRepository)
        {
            Categories = blogRepository.Categories();
            Tags = blogRepository.Tags();
            LatestPosts = blogRepository.Posts(1, 1);
        }

        public IList<Category> Categories { get; internal set; }
        public IList<Tag> Tags { get; set; }
        public IList<Post> LatestPosts { get; set; }
    }
}