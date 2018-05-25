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
        int monthDay;

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

            Init();

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

                    case RecurrenceType.Yearly:
                        YearlyCalculate();
                        break;
                    default:
                        count = int.MaxValue; //exit
                        break;
                }
            }
            return recurrenceDates;
        }

        private void Init()
        {
            count = 1;
            nextDate = _startDate;

            switch (properties.RecurrenceType)
            {
                case RecurrenceType.Monthly:
                    // For Specific MonthDay
                    // if First StartMonthday > monthDay => skip First month
                    if (properties.IsMonthlySpecific &&
                        properties.MonthlySpecificMonthDay > 0
                        && properties.MonthlySpecificMonthDay <= 31
                        && nextDate.Day > properties.MonthlySpecificMonthDay)
                    {
                        nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
                        nextDate = new DateTime(nextDate.Year, nextDate.Month, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
                    }
                    break;
                default:
                    break;
            }
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
            if (properties.IsMonthlySpecific &&
                        properties.MonthlySpecificMonthDay > 0 && properties.MonthlySpecificMonthDay <= 31)
            {
                monthDay = properties.MonthlySpecificMonthDay;
                CalculateMonthlyBySpecificMonthDay();
            }
            else
            {
                int currentYear = nextDate.Year;
                int currentMonth = nextDate.Month;
                DateTime monthStart = new DateTime(nextDate.Year, nextDate.Month, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
                var monthStartWeekday = (int)(monthStart.DayOfWeek);
                DateTime weekStartDate = monthStart.AddDays(-monthStartWeekday);

                int nthWeek;
                if (monthStartWeekday <= properties.MonthlyWeekDay) nthWeek = properties.MonthlyNthWeek - 1;
                    else nthWeek = properties.MonthlyNthWeek;

                nextDate = weekStartDate.AddDays((nthWeek) * 7);
                nextDate = nextDate.AddDays(properties.MonthlyWeekDay);

                if (currentMonth == nextDate.Month)
                {
                    if (nextDate.CompareTo(_startDate) < 0)
                    {
                        nextDate = nextDate.AddMonths(1);
                    }
                    else
                    {
                        recurrenceDates.Add(nextDate);
                        count++;
                        nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
                    }
                } else 
                {
                    nextDate = new DateTime(currentYear, currentMonth, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
                    nextDate = nextDate.AddMonths(1);
                }
            }
        }


        void CalculateMonthlyBySpecificMonthDay()
        {
            // 1 - 29
            if (monthDay <= 29)
            {
                // 29th of february
                if (nextDate.Month == 2 && monthDay == 29)
                {
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, DateTime.DaysInMonth(nextDate.Year, 2), nextDate.Hour, nextDate.Minute, nextDate.Second);
                    recurrenceDates.Add(nextDate);
                    count++;
                    nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
                }
                else
                {
                    // 1 - 29 
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, monthDay, nextDate.Hour, nextDate.Minute, nextDate.Second);
                    recurrenceDates.Add(nextDate);
                    count++;
                    nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
                }
            }
            else if (monthDay == 30)
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

        void YearlyCalculate()
        {
        }
    }
}
