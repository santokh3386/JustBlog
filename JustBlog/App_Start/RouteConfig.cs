using System.Web.Mvc;
using System.Web.Routing;

namespace JustBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                                name: "Default",
                                url: "{controller}/{action}/{id}",
                                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                            );


            routes.MapRoute(
                                "Post",
                                "Archive/{year}/{month}/{title}",
                                new { controller = "Blog", action = "Post" }
                            );

            //routes.MapRoute(
            //    "Default",
            //    "{controller}/{action}/{id}",
            //    new { controller = "Blog", action = "Posts"}
            //);



            routes.MapRoute(
                            "Action",
                            "{action}",
                            new { controller = "Blog", action = "Posts" }
                            );

            routes.MapRoute(
                            "Category",
                            "Category/{category}",
                            new { controller = "Blog", action = "Category" }
                            );

            routes.MapRoute(
                            "Tag",
                            "Tag/{tag}",
                            new { controller = "Blog", action = "Tag" }
                             );
            //routes.MapRoute(
            //    "Default",
            //    "{controller}/{action}/{id}",
            //    new { controller = "Blog", action = "Posts", id = UrlParameter.Optional }
            //);




        }
    }
}