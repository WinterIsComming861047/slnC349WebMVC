using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjC349WebMVC.Validation
{
    public class DomesticForeignValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentException(nameof(context));
            }
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-domesticforeign", "前端驗證-必須是內銷或外銷");
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;            
        }

        public override bool IsValid(object value)
        {
            var Input = (string)value;
            if(Input=="內銷" || Input =="外銷")
            {
                return true;
            }
            return false;            
        }
    }
}