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
        DateTime nextDate;

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
                return recurrenceDates;
            }

            count = 1;
            nextDate = _startDate;
            while (((properties.IsRangeEndDate && nextDate <= properties.RangeEndDate)
                    || (properties.IsRangeNoEndDate && count <= properties.RangeRecurrenceCount))
                   && count <= _maxReturns)
            {
                switch (properties.RecurrenceType)
                {
                    case RecurrenceType.Daily:
                        DailyCalculate();
                        break;

                    case RecurrenceType.Weekly:
                        WeeklyCalculate();
                        break;

                    case RecurrenceType.Monthly:
                        MonthlyCalculate();
                        break;
                    default:
                        break;
                }
            }
            return recurrenceDates;
        }

        void DailyCalculate()
        {
            recurrenceDates.Add(nextDate);
            count++;
            nextDate = nextDate.AddDays(properties.DailyNDays);
        }

        void WeeklyCalculate()
        {
            if (nextDate.DayOfWeek == DayOfWeek.Sunday && properties.IsWeeklySunday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Monday && properties.IsWeeklyMonday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Tuesday && properties.IsWeeklyTuesday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Wednesday && properties.IsWeeklyWednesday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Thursday && properties.IsWeeklyThursday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Friday && properties.IsWeeklyFriday) { recurrenceDates.Add(nextDate); count++; }
            if (nextDate.DayOfWeek == DayOfWeek.Saturday && properties.IsWeeklySaturday) { recurrenceDates.Add(nextDate); count++; }

            nextDate = nextDate.DayOfWeek == DayOfWeek.Saturday ? nextDate.AddDays(((properties.WeeklyEveryNWeeks - 1) * 7) + 1) : nextDate.AddDays(1);
        }

        void MonthlyCalculate()
        {
            // BYMONTHDAY
            if (properties.IsMonthlySpecific &&
                properties.MonthlySpecificMonthDay > 0 && properties.MonthlySpecificMonthDay <= 31)
            {
                ByMonthDayCalculate();
            }
            else if (true)
            {

            }
        }

        private void ByMonthDayCalculate()
        {
            // if First StartMonthday > BYMONTHDAY => skip this month
            if (nextDate.Day > properties.MonthlySpecificMonthDay)
            {
                nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
                nextDate = new DateTime(nextDate.Year, nextDate.Month, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
            }

            // 1 - 29
            if (properties.MonthlySpecificMonthDay <= 29)
            {
                // 29 of february
                if (nextDate.Month == 2 && properties.MonthlySpecificMonthDay == 29)
                {
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, DateTime.DaysInMonth(nextDate.Year, 2), nextDate.Hour, nextDate.Minute, nextDate.Second);
                    recurrenceDates.Add(nextDate);
                    count++;
                    nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
                }
                else
                {
                    // 1 - 29 
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, properties.MonthlySpecificMonthDay, nextDate.Hour, nextDate.Minute, nextDate.Second);
                    recurrenceDates.Add(nextDate);
                    count++;
                    nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
                }
            }
            else if (properties.MonthlySpecificMonthDay == 30)
            {
                // 30 
                if (nextDate.Month == 2) // check february
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, DateTime.DaysInMonth(nextDate.Year, nextDate.Month), nextDate.Hour, nextDate.Minute, nextDate.Second);
                else
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, 30, nextDate.Hour, nextDate.Minute, nextDate.Second);
                recurrenceDates.Add(nextDate);
                count++;
                nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
            }
            else
            {
                // 31 
                nextDate = new DateTime(nextDate.Year, nextDate.Month, DateTime.DaysInMonth(nextDate.Year, nextDate.Month), nextDate.Hour, nextDate.Minute, nextDate.Second);
                recurrenceDates.Add(nextDate);
                count++;
                nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
            }
        }
    }
}
