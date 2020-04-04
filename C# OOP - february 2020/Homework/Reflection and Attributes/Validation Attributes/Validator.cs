using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ValidationAttributes
{
   public static class Validator
    {
        public static bool IsValid (object obj)
        {
            PropertyInfo[] objectProparties = obj.GetType().GetProperties();

            foreach (var propertyInfo in objectProparties)
            {
                IEnumerable<MyValidationAttribute> propartyAtributes = propertyInfo
                    .GetCustomAttributes()
                    .Where(a=>a is MyValidationAttribute)
                    .Cast<MyValidationAttribute>();

                foreach (var item in propartyAtributes)
                {
                    bool result = item.IsValid(propertyInfo.GetValue(obj));

                    if (!result)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
