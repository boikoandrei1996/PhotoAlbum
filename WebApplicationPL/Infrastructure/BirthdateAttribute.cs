using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPL.Infrastructure
{
    public class BirthdateAttribute : ValidationAttribute
    {
        private bool isNullable;

        public BirthdateAttribute(bool isNullable)
        {
            this.isNullable = isNullable;
        }

        public override bool IsValid(object value)
        {
            if (isNullable && value == null)
                return true;

            if (value != null)
            {
                DateTime birthdate;
                try
                { 
                    birthdate = ((DateTime)value).Date;
                }
                catch(InvalidCastException)
                {
                    return false;
                }

                var minDate = DateTime.ParseExact("01.01.1900", "dd.MM.yyyy", new System.Globalization.CultureInfo("ru-RU")).Date;
                if (birthdate > DateTime.Today.Date || birthdate < minDate)
                    return false;
                else
                    return true;
            }

            return false;
        }
    }
}