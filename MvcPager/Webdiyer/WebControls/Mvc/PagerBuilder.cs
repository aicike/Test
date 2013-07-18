namespace Webdiyer.WebControls.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Routing;

    internal class PagerBuilder
    {
        private readonly string _actionName;
        private readonly AjaxHelper _ajax;
        private readonly AjaxOptions _ajaxOptions;
        private readonly string _controllerName;
        private readonly int _endPageIndex;
        private readonly HtmlHelper _html;
        private IDictionary<string, object> _htmlAttributes;
        private readonly bool _msAjaxPaging;
        private readonly int _pageIndex;
        private readonly PagerOptions _pagerOptions;
        private readonly string _routeName;
        private readonly System.Web.Routing.RouteValueDictionary _routeValues;
        private readonly int _startPageIndex;
        private readonly int _totalPageCount;
        private const string CopyrightText = "Aicike";
        private const string GoToPageScript = "function _MvcPager_GoToPage(_pib,_mp){var pageIndex;if(_pib.tagName==\"SELECT\"){pageIndex=_pib.options[_pib.selectedIndex].value;}else{pageIndex=_pib.value;var r=new RegExp(\"^\\\\s*(\\\\d+)\\\\s*$\");if(!r.test(pageIndex)){alert(\"%InvalidPageIndexErrorMessage%\");return;}else if(RegExp.$1<1||RegExp.$1>_mp){alert(\"%PageIndexOutOfRangeErrorMessage%\");return;}}var _hl=document.getElementById(_pib.id+'link').childNodes[0];var _lh=_hl.href;_hl.href=_lh.replace('*_MvcPager_PageIndex_*',pageIndex);%ClickHandler%;_hl.href=_lh;}";
        private const string JqCheckScript = "if(typeof(jQuery)==\"undefined\"){alert(\"未检测到jQuery脚本库，ASP.NET MvcPager无法实现jQuery Ajax分页！\");}";
        private const string JqueryScriptCheckItemKey = "_MvcPager_CheckjQueryScript";
        private const string KeyDownScript = "function _MvcPager_Keydown(e){var _kc,_pib;if(window.event){_kc=e.keyCode;_pib=e.srcElement;}else if(e.which){_kc=e.which;_pib=e.target;}var validKey=(_kc==8||_kc==46||_kc==37||_kc==39||(_kc>=48&&_kc<=57)||(_kc>=96&&_kc<=105));if(!validKey){if(_kc==13){ _MvcPager_GoToPage(_pib,%TotalPageCount%);}if(e.preventDefault){e.preventDefault();}else{event.returnValue=false;}}}";
        private const string ScriptPageIndexName = "*_MvcPager_PageIndex_*";

        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            this._totalPageCount = 1;
            this._startPageIndex = 1;
            this._endPageIndex = 1;
            if (pagerOptions == null)
            {
                pagerOptions = new PagerOptions();
            }
            this._html = html;
            this._ajax = ajax;
            this._pagerOptions = pagerOptions;
            this._htmlAttributes = htmlAttributes;
        }

        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, System.Web.Routing.RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) : this(helper, null, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, null, htmlAttributes)
        {
        }

        internal PagerBuilder(AjaxHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, System.Web.Routing.RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes) : this(null, helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes)
        {
        }

        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, System.Web.Routing.RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes) : this(helper, null, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes)
        {
        }

        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, System.Web.Routing.RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            this._totalPageCount = 1;
            this._startPageIndex = 1;
            this._endPageIndex = 1;
            this._msAjaxPaging = ajax != null;
            if (string.IsNullOrEmpty(actionName))
            {
                if (ajax != null)
                {
                    actionName = (string) ajax.ViewContext.RouteData.Values["action"];
                }
                else
                {
                    actionName = (string)html.ViewContext.RouteData.Values["action"];
                }
            }
            if (string.IsNullOrEmpty(controllerName))
            {
                if (ajax != null)
                {
                    controllerName = (string)ajax.ViewContext.RouteData.Values["controller"];
                }
                else
                {
                    controllerName = (string)html.ViewContext.RouteData.Values["controller"];
                }
            }
            if (pagerOptions == null)
            {
                pagerOptions = new PagerOptions();
            }
            this._html = html;
            this._ajax = ajax;
            this._actionName = actionName;
            this._controllerName = controllerName;
            this._totalPageCount = totalPageCount;
            this._pageIndex = pageIndex;
            this._pagerOptions = pagerOptions;
            this._routeName = routeName;
            this._routeValues = routeValues;
            this._ajaxOptions = ajaxOptions;
            this._htmlAttributes = htmlAttributes;
            this._startPageIndex = pageIndex - (pagerOptions.NumericPagerItemCount / 2);
            if ((this._startPageIndex + pagerOptions.NumericPagerItemCount) > this._totalPageCount)
            {
                this._startPageIndex = (this._totalPageCount + 1) - pagerOptions.NumericPagerItemCount;
            }
            if (this._startPageIndex < 1)
            {
                this._startPageIndex = 1;
            }
            this._endPageIndex = (this._startPageIndex + this._pagerOptions.NumericPagerItemCount) - 1;
            if (this._endPageIndex > this._totalPageCount)
            {
                this._endPageIndex = this._totalPageCount;
            }
        }

        private void AddFirst(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerOptions.FirstPageText, 1, this._pageIndex == 1, PagerItemType.FirstPage);
            if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddLast(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerOptions.LastPageText, this._totalPageCount, this._pageIndex >= this._totalPageCount, PagerItemType.LastPage);
            if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddMoreAfter(ICollection<PagerItem> results)
        {
            if (this._endPageIndex < this._totalPageCount)
            {
                int pageIndex = this._startPageIndex + this._pagerOptions.NumericPagerItemCount;
                if (pageIndex > this._totalPageCount)
                {
                    pageIndex = this._totalPageCount;
                }
                PagerItem item = new PagerItem(this._pagerOptions.MorePageText, pageIndex, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddMoreBefore(ICollection<PagerItem> results)
        {
            if ((this._startPageIndex > 1) && this._pagerOptions.ShowMorePagerItems)
            {
                int pageIndex = this._startPageIndex - 1;
                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }
                PagerItem item = new PagerItem(this._pagerOptions.MorePageText, pageIndex, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddNext(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerOptions.NextPageText, this._pageIndex + 1, this._pageIndex >= this._totalPageCount, PagerItemType.NextPage);
            if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddPageNumbers(ICollection<PagerItem> results)
        {
            for (int i = this._startPageIndex; i <= this._endPageIndex; i++)
            {
                string str = i.ToString();
                if ((i == this._pageIndex) && !string.IsNullOrEmpty(this._pagerOptions.CurrentPageNumberFormatString))
                {
                    str = string.Format(this._pagerOptions.CurrentPageNumberFormatString, str);
                }
                else if (!string.IsNullOrEmpty(this._pagerOptions.PageNumberFormatString))
                {
                    str = string.Format(this._pagerOptions.PageNumberFormatString, str);
                }
                PagerItem item = new PagerItem(str, i, false, PagerItemType.NumericPage);
                results.Add(item);
            }
        }

        private void AddPrevious(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerOptions.PrevPageText, this._pageIndex - 1, this._pageIndex == 1, PagerItemType.PrevPage);
            if (!item.Disabled || (item.Disabled && this._pagerOptions.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private string BuildGoToPageSection(ref string pagerScript)
        {
            int num;
            ViewContext context = this._msAjaxPaging ? this._ajax.ViewContext : this._html.ViewContext;
            if (int.TryParse((string) context.HttpContext.Items["_MvcPager_ControlIndex"], out num))
            {
                num++;
            }
            context.HttpContext.Items["_MvcPager_ControlIndex"] = num.ToString();
            string str = "_MvcPager_Ctrl" + num;
            string str2 = this.GenerateAnchor(new PagerItem("0", 0, false, PagerItemType.NumericPage));
            string newValue = "_hl.click()";
            string str4 = context.HttpContext.Request.Browser.Browser.ToLower();
            if (str4.Contains("safari") || str4.Contains("firefox"))
            {
                newValue = "var evt=document.createEvent('MouseEvents');evt.initEvent('click',true,true);_hl.dispatchEvent(evt)";
            }
            if (num == 0)
            {
                pagerScript = pagerScript + "function _MvcPager_Keydown(e){var _kc,_pib;if(window.event){_kc=e.keyCode;_pib=e.srcElement;}else if(e.which){_kc=e.which;_pib=e.target;}var validKey=(_kc==8||_kc==46||_kc==37||_kc==39||(_kc>=48&&_kc<=57)||(_kc>=96&&_kc<=105));if(!validKey){if(_kc==13){ _MvcPager_GoToPage(_pib,%TotalPageCount%);}if(e.preventDefault){e.preventDefault();}else{event.returnValue=false;}}}".Replace("%TotalPageCount%", this._totalPageCount.ToString()) + "function _MvcPager_GoToPage(_pib,_mp){var pageIndex;if(_pib.tagName==\"SELECT\"){pageIndex=_pib.options[_pib.selectedIndex].value;}else{pageIndex=_pib.value;var r=new RegExp(\"^\\\\s*(\\\\d+)\\\\s*$\");if(!r.test(pageIndex)){alert(\"%InvalidPageIndexErrorMessage%\");return;}else if(RegExp.$1<1||RegExp.$1>_mp){alert(\"%PageIndexOutOfRangeErrorMessage%\");return;}}var _hl=document.getElementById(_pib.id+'link').childNodes[0];var _lh=_hl.href;_hl.href=_lh.replace('*_MvcPager_PageIndex_*',pageIndex);%ClickHandler%;_hl.href=_lh;}".Replace("%InvalidPageIndexErrorMessage%", this._pagerOptions.InvalidPageIndexErrorMessage).Replace("%PageIndexOutOfRangeErrorMessage%", this._pagerOptions.PageIndexOutOfRangeErrorMessage).Replace("%ClickHandler%", newValue);
            }
            string str5 = null;
            if (!this._pagerOptions.ShowGoButton)
            {
                str5 = " onchange=\"_MvcPager_GoToPage(this," + this._totalPageCount + ")\"";
            }
            StringBuilder builder = new StringBuilder();
            if (this._pagerOptions.PageIndexBoxType == PageIndexBoxType.DropDownList)
            {
                int num2 = this._pageIndex - (this._pagerOptions.MaximumPageIndexItems / 2);
                if ((num2 + this._pagerOptions.MaximumPageIndexItems) > this._totalPageCount)
                {
                    num2 = (this._totalPageCount + 1) - this._pagerOptions.MaximumPageIndexItems;
                }
                if (num2 < 1)
                {
                    num2 = 1;
                }
                int num3 = (num2 + this._pagerOptions.MaximumPageIndexItems) - 1;
                if (num3 > this._totalPageCount)
                {
                    num3 = this._totalPageCount;
                }
                builder.AppendFormat("<select id=\"{0}\"{1}>", str + "_pib", str5);
                for (int i = num2; i <= num3; i++)
                {
                    builder.AppendFormat("<option value=\"{0}\"", i);
                    if (i == this._pageIndex)
                    {
                        builder.Append(" selected=\"selected\"");
                    }
                    builder.AppendFormat(">{0}</option>", i);
                }
                builder.Append("</select>");
            }
            else
            {
                builder.AppendFormat("<input type=\"text\" id=\"{0}\" value=\"{1}\" onkeydown=\"_MvcPager_Keydown(event)\"{2}/>", str + "_pib", this._pageIndex, str5);
            }
            if (!string.IsNullOrEmpty(this._pagerOptions.PageIndexBoxWrapperFormatString))
            {
                builder = new StringBuilder(string.Format(this._pagerOptions.PageIndexBoxWrapperFormatString, builder));
            }
            if (this._pagerOptions.ShowGoButton)
            {
                builder.AppendFormat("<input type=\"button\" value=\"{0}\" onclick=\"_MvcPager_GoToPage(document.getElementById('{1}')," + this._totalPageCount + ")\"/>", this._pagerOptions.GoButtonText, str + "_pib");
            }
            builder.AppendFormat("<span id=\"{0}\" style=\"display:none;width:0px;height:0px\">{1}</span>", str + "_piblink", str2);
            if (!string.IsNullOrEmpty(this._pagerOptions.GoToPageSectionWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
            {
                return string.Format(this._pagerOptions.GoToPageSectionWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, builder);
            }
            return builder.ToString();
        }

        private MvcHtmlString CreateWrappedPagerElement(PagerItem item, string el)
        {
            string str = el;
            switch (item.Type)
            {
                case PagerItemType.FirstPage:
                case PagerItemType.NextPage:
                case PagerItemType.PrevPage:
                case PagerItemType.LastPage:
                    if (!string.IsNullOrEmpty(this._pagerOptions.NavigationPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
                    {
                        str = string.Format(this._pagerOptions.NavigationPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
                    }
                    break;

                case PagerItemType.MorePage:
                    if (!string.IsNullOrEmpty(this._pagerOptions.MorePagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
                    {
                        str = string.Format(this._pagerOptions.MorePagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
                    }
                    break;

                case PagerItemType.NumericPage:
                    if ((item.PageIndex != this._pageIndex) || (string.IsNullOrEmpty(this._pagerOptions.CurrentPagerItemWrapperFormatString) && string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString)))
                    {
                        if (!string.IsNullOrEmpty(this._pagerOptions.NumericPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
                        {
                            str = string.Format(this._pagerOptions.NumericPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
                        }
                        break;
                    }
                    str = string.Format(this._pagerOptions.CurrentPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, el);
                    break;
            }
            return MvcHtmlString.Create(str + this._pagerOptions.SeparatorHtml);
        }

        private string GenerateAnchor(PagerItem item)
        {
            if (this._msAjaxPaging)
            {
                System.Web.Routing.RouteValueDictionary currentRouteValues = this.GetCurrentRouteValues(this._ajax.ViewContext);
                if (item.PageIndex == 0)
                {
                    currentRouteValues[this._pagerOptions.PageIndexParameterName] = "*_MvcPager_PageIndex_*";
                }
                else
                {
                    currentRouteValues[this._pagerOptions.PageIndexParameterName] = item.PageIndex;
                }
                if (!string.IsNullOrEmpty(this._routeName))
                {
                    return AjaxExtensions.RouteLink(this._ajax, item.Text, this._routeName, currentRouteValues, this._ajaxOptions).ToString();
                }
                return AjaxExtensions.RouteLink(this._ajax, item.Text, currentRouteValues, this._ajaxOptions).ToString();
            }
            string str = this.GenerateUrl(item.PageIndex);
            if (!this._pagerOptions.UseJqueryAjax)
            {
                return ("<a href=\"" + str + "\" onclick=\"window.open(this.attributes.getNamedItem('href').value,'_self')\"></a>");
            }
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(this._ajaxOptions.OnFailure) || !string.IsNullOrEmpty(this._ajaxOptions.OnBegin))
            {
                builder.Append("$.ajax({url:$(this).attr('href'),success:function(data,status,xhr){$('#");
                builder.Append(this._ajaxOptions.UpdateTargetId).Append("').html(data);}");
                if (!string.IsNullOrEmpty(this._ajaxOptions.OnFailure))
                {
                    builder.Append(",error:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnFailure));
                }
                if (!string.IsNullOrEmpty(this._ajaxOptions.OnBegin))
                {
                    builder.Append(",beforeSend:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnBegin));
                }
                if (!string.IsNullOrEmpty(this._ajaxOptions.OnComplete))
                {
                    builder.Append(",complete:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnComplete));
                }
                builder.Append("});return false;");
            }
            else
            {
                builder.Append("$('#").Append(this._ajaxOptions.UpdateTargetId);
                builder.Append("').load($(this).attr('href')");
                if (!string.IsNullOrEmpty(this._ajaxOptions.OnComplete))
                {
                    builder.Append(",").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnComplete));
                }
                builder.Append(");return false;");
            }
            if (!string.IsNullOrEmpty(str))
            {
                return string.Format(CultureInfo.InvariantCulture, "<a href=\"{0}\" onclick=\"{1}\">{2}</a>", new object[] { this.GenerateUrl(item.PageIndex), builder, item.Text });
            }
            return this._html.Encode(item.Text);
        }

        private MvcHtmlString GenerateJqAjaxPagerElement(PagerItem item)
        {
            if (item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            }
            return this.CreateWrappedPagerElement(item, this.GenerateAnchor(item));
        }

        private MvcHtmlString GenerateMsAjaxPagerElement(PagerItem item)
        {
            if ((item.PageIndex == this._pageIndex) && !item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, item.Text);
            }
            if (item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            }
            if ((item.PageIndex >= 1) && (item.PageIndex <= this._totalPageCount))
            {
                return this.CreateWrappedPagerElement(item, this.GenerateAnchor(item));
            }
            return null;
        }

        private MvcHtmlString GeneratePagerElement(PagerItem item)
        {
            string str = this.GenerateUrl(item.PageIndex);
            if (item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            }
            return this.CreateWrappedPagerElement(item, string.IsNullOrEmpty(str) ? this._html.Encode(item.Text) : string.Format("<a href='{0}'>{1}</a>", str, item.Text));
        }

        private string GenerateUrl(int pageIndex)
        {
            if ((pageIndex > this._totalPageCount) || (pageIndex == this._pageIndex))
            {
                return null;
            }
            System.Web.Routing.RouteValueDictionary dictionary = this._routeValues ?? new System.Web.Routing.RouteValueDictionary();
            if (pageIndex == 0)
            {
                dictionary[this._pagerOptions.PageIndexParameterName] = "*_MvcPager_PageIndex_*";
            }
            else
            {
                dictionary[this._pagerOptions.PageIndexParameterName] = pageIndex;
            }
            NameValueCollection queryString = this._html.ViewContext.HttpContext.Request.QueryString;
            foreach (string str in queryString.Keys)
            {
                if (str != this._pagerOptions.PageIndexParameterName)
                {
                    dictionary[str] = queryString[str];
                }
            }
            dictionary["action"] = this._actionName;
            dictionary["controller"] = this._controllerName;
            UrlHelper helper = new UrlHelper(this._html.ViewContext.RequestContext);
            if (!string.IsNullOrEmpty(this._routeName))
            {
                return helper.RouteUrl(this._routeName, dictionary);
            }
            return helper.RouteUrl(dictionary);
        }

        private System.Web.Routing.RouteValueDictionary GetCurrentRouteValues(ViewContext viewContext)
        {
            System.Web.Routing.RouteValueDictionary dictionary = this._routeValues ?? new System.Web.Routing.RouteValueDictionary();
            NameValueCollection queryString = viewContext.HttpContext.Request.QueryString;
            foreach (string str in queryString.Keys)
            {
                if (((str != this._pagerOptions.PageIndexParameterName) && (str.ToLower() != "x-requested-with")) && (queryString[str].ToLower() != "xmlhttprequest"))
                {
                    dictionary[str] = queryString[str];
                }
            }
            dictionary["action"] = this._actionName;
            dictionary["controller"] = this._controllerName;
            return dictionary;
        }

        internal MvcHtmlString RenderPager()
        {
            if ((this._totalPageCount <= 1) && this._pagerOptions.AutoHide)
            {
                //return MvcHtmlString.Create("\r\n<!--ASP.NET MvcPager 1.3 for ASP.NET MVC 2.0 版权所有：陕西省吴起县博杨计算机有限公司 (http://www.webdiyer.com)-->\r\n");
                return MvcHtmlString.Create("");
            }
            if (((this._pageIndex > this._totalPageCount) && (this._totalPageCount > 0)) || (this._pageIndex < 1))
            {
                return MvcHtmlString.Create(string.Format("<div style=\"color:red;font-weight:bold\">{0}</div>", this._pagerOptions.PageIndexOutOfRangeErrorMessage));
            }
            List<PagerItem> results = new List<PagerItem>();
            if (this._pagerOptions.ShowFirstLast)
            {
                this.AddFirst(results);
            }
            if (this._pagerOptions.ShowPrevNext)
            {
                this.AddPrevious(results);
            }
            if (this._pagerOptions.ShowNumericPagerItems)
            {
                if (this._pagerOptions.AlwaysShowFirstLastPageNumber && (this._startPageIndex > 1))
                {
                    results.Add(new PagerItem("1", 1, false, PagerItemType.NumericPage));
                }
                if (this._pagerOptions.ShowMorePagerItems)
                {
                    this.AddMoreBefore(results);
                }
                this.AddPageNumbers(results);
                if (this._pagerOptions.ShowMorePagerItems)
                {
                    this.AddMoreAfter(results);
                }
                if (this._pagerOptions.AlwaysShowFirstLastPageNumber && (this._endPageIndex < this._totalPageCount))
                {
                    results.Add(new PagerItem(this._totalPageCount.ToString(), this._totalPageCount, false, PagerItemType.NumericPage));
                }
            }
            if (this._pagerOptions.ShowPrevNext)
            {
                this.AddNext(results);
            }
            if (this._pagerOptions.ShowFirstLast)
            {
                this.AddLast(results);
            }
            StringBuilder builder = new StringBuilder();
            if (this._msAjaxPaging)
            {
                foreach (PagerItem item in results)
                {
                    builder.Append(this.GenerateMsAjaxPagerElement(item));
                }
            }
            else if (this._pagerOptions.UseJqueryAjax)
            {
                foreach (PagerItem item2 in results)
                {
                    builder.Append(this.GenerateJqAjaxPagerElement(item2));
                }
            }
            else
            {
                foreach (PagerItem item3 in results)
                {
                    builder.Append(this.GeneratePagerElement(item3));
                }
            }
            TagBuilder builder2 = new TagBuilder(this._pagerOptions.ContainerTagName);
            if (!string.IsNullOrEmpty(this._pagerOptions.Id))
            {
                builder2.GenerateId(this._pagerOptions.Id);
            }
            if (!string.IsNullOrEmpty(this._pagerOptions.CssClass))
            {
                builder2.AddCssClass(this._pagerOptions.CssClass);
            }
            if (!string.IsNullOrEmpty(this._pagerOptions.HorizontalAlign))
            {
                string str = "text-align:" + this._pagerOptions.HorizontalAlign.ToLower();
                if (this._htmlAttributes == null)
                {
                    System.Web.Routing.RouteValueDictionary dictionary = new System.Web.Routing.RouteValueDictionary();
                    dictionary.Add("style", str);
                    this._htmlAttributes = dictionary;
                }
                else if (this._htmlAttributes.Keys.Contains("style"))
                {
                    IDictionary<string, object> dictionary2;
                    (dictionary2 = this._htmlAttributes)["style"] = dictionary2["style"] + ";" + str;
                }
            }
            builder2.MergeAttributes<string, object>(this._htmlAttributes, true);
            string pagerScript = string.Empty;
            if (this._pagerOptions.UseJqueryAjax && (((string) this._html.ViewContext.HttpContext.Items["_MvcPager_CheckjQueryScript"]) != "1"))
            {
                pagerScript = "if(typeof(jQuery)==\"undefined\"){alert(\"未检测到jQuery脚本库，ASP.NET MvcPager无法实现jQuery Ajax分页！\");}";
                this._html.ViewContext.HttpContext.Items["_MvcPager_CheckjQueryScript"] = "1";
            }
            if (this._pagerOptions.ShowPageIndexBox)
            {
                builder.Append(this.BuildGoToPageSection(ref pagerScript));
            }
            else
            {
                builder.Length -= this._pagerOptions.SeparatorHtml.Length;
            }
            builder2.InnerHtml = builder.ToString();
            if (!string.IsNullOrEmpty(pagerScript))
            {
                pagerScript = "<script language=\"javascript\" type=\"text/javascript\">" + pagerScript + "</script>";
            }
            return MvcHtmlString.Create(pagerScript + builder2.ToString((TagRenderMode) TagRenderMode.Normal));
        }
    }
}

