using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using Poco;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.Html
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);
            string fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("null");
            }

            TagBuilder tagBuilder = new TagBuilder("select");
            if (htmlAttributes != null)
            {
                tagBuilder.MergeAttributes(htmlAttributes);
            }
            tagBuilder.MergeAttribute("name", fullName, true);

            var attrs = metadata.ContainerType.GetProperty(fullName).GetCustomAttributes(true);
            foreach (var item in attrs)
            {
                var attr = item as DropDownListAttribute;
                if (attr != null)
                {
                    tagBuilder.MergeAttribute("data-val", "true", true);
                    tagBuilder.MergeAttribute("data-val-dropdownlist", attr.ErrorMessage, true);
                }
            }

            tagBuilder.GenerateId(fullName);

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (helper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            //tagBuilder.MergeAttributes(helper.GetUnobtrusiveValidationAttributes(name, metadata));

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }

}