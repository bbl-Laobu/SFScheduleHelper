using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Syncfusion.SfSchedule.XForms;

namespace Kareke.SFScheduleHelper
{
    public class RecurrencesCalculator
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

        string _rule;
        DateTime _startDate;
        int _maxReturns;
        RecurrenceProperties properties;
        int count;

        ObservableCollection<DateTime> recurrenceDates;

        public IEnumerable<DateTime> GetRecurrences(string rule, DateTime startDate, int maxReturns = 200)
        {
            _rule = rule;
            _startDate = startDate;
            _maxReturns = maxReturns;
            RecurrenceConverter converter = new RecurrenceConverter();
            recurrenceDates = new ObservableCollection<DateTime>();

            properties = converter.Convert(_rule, _startDate);

            if (converter.HasError)
            {
                HasError = converter.HasError;
                ErrorMessage = converter.ErrorMessage;
                return null;
            }

            count = 1;
            DateTime nextDate = _startDate;
            while (((properties.IsRangeEndDate && nextDate <= properties.RangeEndDate)
                    || (properties.IsRangeNoEndDate && count <= properties.RangeRecurrenceCount))
                   && count <= _maxReturns)
            {
                switch (properties.RecurrenceType)
                {
                    case RecurrenceType.Daily:
                        recurrenceDates.Add(nextDate);
                        count++;
                        nextDate = DailyCalculate(nextDate);
                        break;

                    case RecurrenceType.Weekly:
                        nextDate = WeeklyCalculate(nextDate);
                        break;
                    default:
                        break;
                }


            }

            return recurrenceDates;
        }

        DateTime DailyCalculate(DateTime nextDate)
        {
            return nextDate.AddDays(properties.DailyNDays);
        }

        DateTime WeeklyCalculate(DateTime nextDate)
        {
            if (nextDate.DayOfWeek == DayOfWeek.Sunday && properties.IsWeeklySunday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Monday && properties.IsWeeklyMonday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Tuesday && properties.IsWeeklyTuesday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Wednesday && properties.IsWeeklyWednesday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Thursday && properties.IsWeeklyThursday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Friday && properties.IsWeeklyFriday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Saturday && properties.IsWeeklySaturday) { recurrenceDates.Add(nextDate); count++; }

            nextDate = nextDate.DayOfWeek == DayOfWeek.Saturday ? nextDate.AddDays(((properties.WeeklyEveryNWeeks - 1) * 7) + 1) : nextDate.AddDays(1);

            return nextDate;
        }
    }
}
