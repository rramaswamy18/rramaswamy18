using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace RetailSlnBusinessLayer
{
    public class NoCache : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }

        //private void ClearApplicationCache()
        //{
        //    List<string> keys = new List<string>();

        //    // retrieve application Cache enumerator
        //    IDictionaryEnumerator enumerator = Cache.GetEnumerator();

        //    // copy all keys that currently exist in Cache
        //    while (enumerator.MoveNext())
        //    {
        //        keys.Add(enumerator.Key.ToString());
        //    }

        //    // delete every key from cache
        //    for (int i = 0; i < keys.Count; i++)
        //    {
        //        Cache.Remove(keys[i]);
        //    }
        //}
    }
}
