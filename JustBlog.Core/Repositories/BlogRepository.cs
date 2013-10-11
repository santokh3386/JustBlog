using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustBlog.Core.Objects;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;

using JustBlog.Core.Interface;




namespace JustBlog.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {

        ISession _session;

        public BlogRepository(ISession _session)
        {
            this._session = _session;
        }

        public IList<Post> Posts(int pageNo, int pageSize)
        {
            var query = _session.Query<Post>()
                       .Where(p => p.Published)
                       .OrderByDescending(p => p.PostedOn)
                       .Skip(pageNo * pageSize)
                       .Take(pageSize)
                       .Fetch(p => p.Category);
                                
           query.FetchMany(p => p.Tags).ToFuture();


            return  query.ToFuture().ToList();
        }

        public int TotalPosts()
        {
            return _session.Query<Post>().Where(p => p.Published).Count();
        }


        public System.Collections.Generic.IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            var query = _session.Query<Post>()
                   .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                   .OrderByDescending(p => p.PostedOn)
                   .Skip(pageNo * pageSize)
                   .Take(pageSize)
                   .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _session.Query<Post>()
                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                .Count();
        }

        public Category Category(string categorySlug)
        {
            return _session.Query<Category>()
               .FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }


        public System.Collections.Generic.IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            var query = _session.Query<Post>()
                       .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                       .OrderByDescending(p => p.PostedOn)
                       .Skip(pageNo * pageSize)
                       .Take(pageSize)
                       .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _session.Query<Post>()
                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                .Count();
        }

        public Tag Tag(string tagSlug)
        {
            return _session.Query<Tag>()
               .FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }


        public System.Collections.Generic.IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            var query = _session.Query<Post>()
                          .Where(p => p.Published && (p.Title.Contains(search) || 
                              p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                          .OrderByDescending(p => p.PostedOn)
                          .Skip(pageNo * pageSize)
                          .Take(pageSize)
                          .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public int TotalPostsForSearch(string search)
        {
            return _session.Query<Post>()
            .Where(p => p.Published && (p.Title.Contains(search) ||
                p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
            .Count();
        }


        public Post Post(int year, int month, string titleSlug)
        {
            var query = _session.Query<Post>()
                       .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug))
                       .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().Single();
        }


        public IList<Category> Categories()
        {
            return _session.Query<Category>().OrderBy(p => p.Name).ToList();
        }


        public IList<Tag> Tags()
        {
            return _session.Query<Tag>().OrderBy(x => x.Name).ToList();
        }
    }
}
