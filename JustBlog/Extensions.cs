﻿using JustBlog.Core.Objects;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace JustBlog
{
  public static class Extensions
  {
    public static string ToConfigLocalTime(this DateTime utcDT)
    {
        var istTZ = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
        return String.Format("{0} ({1})", TimeZoneInfo.ConvertTimeFromUtc
            (utcDT, istTZ).ToShortDateString(), ConfigurationManager.AppSettings["TimezoneAbbr"]);
        //return null;
    }

    public static string Href(this Post post, UrlHelper helper)
    {
      return helper.RouteUrl(new { controller = "Blog", action = "Post", 
          year = post.PostedOn.Year, month = post.PostedOn.Month, title = post.UrlSlug });
        
    }
  }
}