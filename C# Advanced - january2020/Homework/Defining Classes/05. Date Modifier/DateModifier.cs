using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    public static class DateModifier
    {
        public static double GetDifferentInDaysBetweenTwoDates(string firstDate, string secondDate)
        {
            DateTime startDate = DateTime.Parse(firstDate);
            DateTime endDate = DateTime.Parse(secondDate);
            double result = (startDate - endDate).TotalDays;
            double absoluteValue = Math.Abs(result);

            return absoluteValue;
        }
    }
}
