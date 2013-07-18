using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Webdiyer.WebControls.Mvc;

namespace System.Web
{

    public static class PagerHelper
    {
        public static MvcHtmlString AjaxPager<T>(this HtmlHelper html, PagedList<T> pagedList, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
            {
                return AjaxPager(html, null, null);
            }
            return html.AjaxPager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, null, ((System.Web.Routing.RouteValueDictionary) null), ajaxOptions, ((IDictionary<string, object>) null));
        }

        private static MvcHtmlString AjaxPager(HtmlHelper html, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(html, null, pagerOptions, htmlAttributes).RenderPager();
        }

        public static MvcHtmlString AjaxPager<T>(this HtmlHelper html, PagedList<T> pagedList, string routeName, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
            {
                return AjaxPager(html, null, null);
            }
            return html.AjaxPager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, routeName, null, ((System.Web.Routing.RouteValueDictionary) null), ajaxOptions, ((IDictionary<string, object>) null));
        }

        public static MvcHtmlString AjaxPager<T>(this HtmlHelper html, PagedList<T> pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
            {
                return AjaxPager(html, pagerOptions, null);
            }
            return html.AjaxPager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, pagerOptions, ((System.Web.Routing.RouteValueDictionary) null), ajaxOptions, ((IDictionary<string, object>) null));
        }

        public static MvcHtmlString AjaxPager<T>(this HtmlHelper html, PagedList<T> pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return AjaxPager(html, pagerOptions, htmlAttributes);
            }
            return html.AjaxPager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString AjaxPager<T>(this HtmlHelper html, PagedList<T> pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return AjaxPager(html, pagerOptions, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            }
            return html.AjaxPager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString AjaxPager<T>(this HtmlHelper html, PagedList<T> pagedList, string routeName, object routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
            {
                return AjaxPager(html, pagerOptions, null);
            }
            return html.AjaxPager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, null);
        }

        public static MvcHtmlString AjaxPager<T>(this HtmlHelper html, PagedList<T> pagedList, string actionName, string controllerName, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
            {
                return AjaxPager(html, pagerOptions, null);
            }
            return html.AjaxPager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, actionName, controllerName, null, pagerOptions, ((System.Web.Routing.RouteValueDictionary) null), ajaxOptions, ((IDictionary<string, object>) null));
        }

        public static MvcHtmlString AjaxPager<T>(this HtmlHelper html, PagedList<T> pagedList, string routeName, object routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return AjaxPager(html, pagerOptions, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            }
            return html.AjaxPager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString AjaxPager<T>(this HtmlHelper html, PagedList<T> pagedList, string routeName, System.Web.Routing.RouteValueDictionary routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return AjaxPager(html, pagerOptions, htmlAttributes);
            }
            return html.AjaxPager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, int totalPageCount, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagerOptions == null)
            {
                pagerOptions = new PagerOptions();
            }
            pagerOptions.UseJqueryAjax = true;
            PagerBuilder builder = new PagerBuilder(html, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new System.Web.Routing.RouteValueDictionary(routeValues), ajaxOptions, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            return builder.RenderPager();
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, int totalPageCount, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, System.Web.Routing.RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagerOptions == null)
            {
                pagerOptions = new PagerOptions();
            }
            pagerOptions.UseJqueryAjax = true;
            PagerBuilder builder = new PagerBuilder(html, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes);
            return builder.RenderPager();
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList)
        {
            if (pagedList == null)
            {
                return Pager(helper, null, null);
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, null, ((System.Web.Routing.RouteValueDictionary) null), ((IDictionary<string, object>) null));
        }

        public static MvcHtmlString Pager<T>(this AjaxHelper ajax, PagedList<T> pagedList, AjaxOptions ajaxOptions)
        {
            if (pagedList != null)
            {
                return ajax.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, null, ((System.Web.Routing.RouteValueDictionary) null), ajaxOptions, ((IDictionary<string, object>) null));
            }
            return Pager(ajax, null, null);
        }

        private static MvcHtmlString Pager(AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(null, ajax, pagerOptions, htmlAttributes).RenderPager();
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList, PagerOptions pagerOptions)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, null);
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, pagerOptions, null, ((System.Web.Routing.RouteValueDictionary) null), ((IDictionary<string, object>) null));
        }

        private static MvcHtmlString Pager(HtmlHelper helper, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(helper, null, pagerOptions, htmlAttributes).RenderPager();
        }

        public static MvcHtmlString Pager<T>(this AjaxHelper ajax, PagedList<T> pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList != null)
            {
                return ajax.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, pagerOptions, ((System.Web.Routing.RouteValueDictionary) null), ajaxOptions, ((IDictionary<string, object>) null));
            }
            return Pager(ajax, pagerOptions, null);
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, htmlAttributes);
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList, PagerOptions pagerOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, pagerOptions, null, null, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this AjaxHelper ajax, PagedList<T> pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, pagerOptions, htmlAttributes);
            }
            return ajax.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this AjaxHelper ajax, PagedList<T> pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, pagerOptions, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            }
            return ajax.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, pagerOptions, null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList, string routeName, object routeValues, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, null, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList, string routeName, System.Web.Routing.RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, null, htmlAttributes);
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, null, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList, PagerOptions pagerOptions, string routeName, object routeValues)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, null);
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, null);
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList, PagerOptions pagerOptions, string routeName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, null);
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, null);
        }

        public static MvcHtmlString Pager<T>(this AjaxHelper ajax, PagedList<T> pagedList, string routeName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, null, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            }
            return ajax.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, routeName, null, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this AjaxHelper ajax, PagedList<T> pagedList, string routeName, System.Web.Routing.RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, null, htmlAttributes);
            }
            return ajax.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, routeName, null, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList, PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this HtmlHelper helper, PagedList<T> pagedList, PagerOptions pagerOptions, string routeName, System.Web.Routing.RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(helper, pagerOptions, htmlAttributes);
            }
            return helper.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, pagerOptions, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this AjaxHelper ajax, PagedList<T> pagedList, string routeName, object routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, pagerOptions, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            }
            return ajax.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager<T>(this AjaxHelper ajax, PagedList<T> pagedList, string routeName, System.Web.Routing.RouteValueDictionary routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
            {
                return Pager(ajax, pagerOptions, htmlAttributes);
            }
            return ajax.Pager(pagedList.TotalPageCount, pagedList.CurrentPageIndex, null, null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, int totalPageCount, int pageIndex, string actionName, string controllerName, PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
        {
            PagerBuilder builder = new PagerBuilder(helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new System.Web.Routing.RouteValueDictionary(routeValues), new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            return builder.RenderPager();
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, int totalPageCount, int pageIndex, string actionName, string controllerName, PagerOptions pagerOptions, string routeName, System.Web.Routing.RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            PagerBuilder builder = new PagerBuilder(helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, htmlAttributes);
            return builder.RenderPager();
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, int totalPageCount, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            PagerBuilder builder = new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new System.Web.Routing.RouteValueDictionary(routeValues), ajaxOptions, new System.Web.Routing.RouteValueDictionary(htmlAttributes));
            return builder.RenderPager();
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, int totalPageCount, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, System.Web.Routing.RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            PagerBuilder builder = new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes);
            return builder.RenderPager();
        }
    }
}

