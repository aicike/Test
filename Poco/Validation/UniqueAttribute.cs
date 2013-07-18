using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueAttribute : ValidationAttribute, IClientValidatable
    {
        public string Table { get; private set; }

        public string Field { get; private set; }

        public UniqueAttribute(string table, string field)
        {
            Table = table;
            Field = field;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "unique"
            };

            //需要參數的話可以加在這裡
            rule.ValidationParameters["table"] = Table;
            rule.ValidationParameters["field"] = Field;
            yield return rule;
        }
    }
}
