using Webdiyer.WebControls.Mvc;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
namespace System.Web
{
    public static class PageLinqExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            int i = 1;
            if (pageIndex >= i)
            {
                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }
                int count = 0;
                count = (pageIndex - 1) * pageSize;

                IQueryable<T> items = allItems.Skip<T>(count).Take<T>(pageSize);

                PagedList<T> result = new PagedList<T>(items, pageIndex, pageSize, allItems.Count<T>());
                if (result.CurrentPageIndex < pageIndex)
                {
                    pageIndex = pageIndex - 1;
                    count = (pageIndex - 1) * pageSize;
                    if (count < 0) { count = 0; }
                    items = allItems.Skip<T>(count).Take<T>(pageSize);
                    result = new PagedList<T>(items, pageIndex, pageSize, allItems.Count<T>());
                }
                return result;


            }
            return null;
        }
    }
}

