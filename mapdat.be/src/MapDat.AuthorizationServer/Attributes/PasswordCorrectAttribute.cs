using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AuthorizationServer.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class PasswordCorrectAttribute : ValidationAttribute
    {
        private readonly Regex _digits;
        private readonly string _errorMessage;

        protected PasswordCorrectAttribute(string pattern, string errorMessage)
        {
            _digits = new Regex(pattern);
            _errorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var valid = _digits.IsMatch(value.ToString());
            if (!valid)
            {
                ErrorMessage = _errorMessage;
            }

            return valid;
        }
    }
}