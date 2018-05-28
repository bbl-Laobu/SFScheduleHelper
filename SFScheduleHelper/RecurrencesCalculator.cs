using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Syncfusion.SfSchedule.XForms;

namespace Kareke.SFScheduleHelper
{
    public class RecurrencesCalculator
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        RecurrenceConverter converter;

        string _rule;
        DateTime _startDate;
        RecurrenceProperties properties;
        int count;
        DateTime nextDate;

        ObservableCollection<DateTime> recurrenceDates;

        public RecurrencesCalculator()
        {
            converter = new RecurrenceConverter();
        }

        /// <summary>
        /// Calculate all recurrence dates
        /// </summary>
        /// <returns>StartDatime</returns>
        /// <param name="rule">Rule.</param>
        /// <param name="startDate">Start date.</param>
        /// <param name="maxReturns">Maximum number of dates returned. Default=200, Unlimited=0 </param>
        public IEnumerable<DateTime> AllRecurrenceDates(string rule, DateTime startDate, int maxReturns = 200)
        {
            // Init
            _rule = rule;
            _startDate = startDate;
            bool noLimit = maxReturns == 0 ? true : false;


            // convert rule to properties
            properties = converter.Convert(_rule, _startDate);
            if (converter.HasError) { HasError = converter.HasError; ErrorMessage = converter.ErrorMessage; return recurrenceDates; }

            // Calculate Recurrences
            GetAllDates(maxReturns, noLimit);

            return recurrenceDates;
        }

        /// <summary>
        /// Calculate Final recurrence End Date
        /// </summary>
        /// <returns>The end date.</returns>
        /// <param name="rule">Rule.</param>
        /// <param name="startDate">Start date.</param>
        /// <param name="duration">Duration of the appointment.</param>
        public DateTime FinalEndDate(string rule, DateTime startDate, TimeSpan duration)
        {
            // Init
            _rule = rule;
            _startDate = startDate;
            DateTime endDate;

            // convert rule to properties
            properties = converter.Convert(_rule, _startDate);
            if (converter.HasError) { HasError = converter.HasError; ErrorMessage = converter.ErrorMessage; return DateTime.MinValue; }

            // Return Last EndDate
            GetAllDates(5000, false);

            if (recurrenceDates != null)
            {
                endDate = recurrenceDates.Last();
                return endDate + duration;
            }
            else
            {
                HasError = true; ErrorMessage = "No Final Date Found";
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Calculates the Next recurrence(s) .
        /// </summary>
        /// <returns>The recurrences.</returns>
        /// <param name="rule">Rule.</param>
        /// <param name="startDate">Start date.</param>
        /// <param name="currentDate">Current date.</param>
        /// <param name="maxDatesReturned">Maximum number of dates returned.</param>
        public IEnumerable<DateTime> NextRecurrences(string rule, DateTime startDate, DateTime currentDate, int maxDatesReturned = 10)
        {
            // Init
            _rule = rule;
            _startDate = startDate;

            // convert rule to properties
            properties = converter.Convert(_rule, _startDate);
            if (converter.HasError) { HasError = converter.HasError; ErrorMessage = converter.ErrorMessage; return null; }

            // return Next Date
            count = 1;
            nextDate = _startDate;
            int maxReturns = 5000;
            int datesFound = 0;
            recurrenceDates = new ObservableCollection<DateTime>();
            ObservableCollection<DateTime> nextRecurrenceDates = new ObservableCollection<DateTime>(); 

            while (((properties.IsRangeEndDate && nextDate <= properties.RangeEndDate)
                                || (properties.IsRangeNoEndDate && count <= properties.RangeRecurrenceCount))
                   && count <= maxReturns && datesFound < maxDatesReturned)
            {
                CalculateForRecurrenceType();

                if (recurrenceDates.Count > 0 && recurrenceDates.Last() > currentDate)
                {
                    var found = nextRecurrenceDates.Where(q => q == recurrenceDates.Last()).ToList();
                    if (found.Count == 0)
                    {
                        nextRecurrenceDates.Add(recurrenceDates.Last());
                        datesFound++;
                    }
                }
            }
            return nextRecurrenceDates;
        }

        /// <summary>
        /// Currents or Last recurrence.
        /// </summary>
        /// <returns>The recurrence.</returns>
        /// <param name="rule">Rule.</param>
        /// <param name="startDate">Start date.</param>
        /// <param name="currentDate">Current date.</param>
        public DateTime CurrentRecurrence(string rule, DateTime startDate, DateTime currentDate)
        {
            // Init
            _rule = rule;
            _startDate = startDate;

            // convert rule to properties
            properties = converter.Convert(_rule, _startDate);
            if (converter.HasError) { HasError = converter.HasError; ErrorMessage = converter.ErrorMessage; return DateTime.MinValue; }

            // Return current Date
            count = 1;
            nextDate = _startDate;
            int maxReturns = 5000;
            recurrenceDates = new ObservableCollection<DateTime> { DateTime.MinValue };
            DateTime currentRecurrence = DateTime.MinValue;

            while (((properties.IsRangeEndDate && nextDate <= properties.RangeEndDate)
                                || (properties.IsRangeNoEndDate && count <= properties.RangeRecurrenceCount))
                   && count <= maxReturns && recurrenceDates.Last() <= currentDate)
            {
                CalculateForRecurrenceType();

                if (recurrenceDates.Last() <= currentDate) currentRecurrence = recurrenceDates.Last();
            }
            return currentRecurrence;
        }



        // 
        // PRIVATE Calculation Methods
        // ----------------------------------------------------------
        void GetAllDates(int maxReturns, bool noLimit)
        {
            count = 1;
            nextDate = _startDate;
            recurrenceDates = new ObservableCollection<DateTime>();
            while (((properties.IsRangeEndDate && nextDate <= properties.RangeEndDate)
                                || (properties.IsRangeNoEndDate && count <= properties.RangeRecurrenceCount))
                   && (count <= maxReturns || noLimit == true))
            {
                CalculateForRecurrenceType();
            }
        }

        void CalculateForRecurrenceType()
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

        // DAILY
        // ----------
        void DailyCalculate()
        {
            AddDateToRecurrences();
            nextDate = nextDate.AddDays(properties.DailyNDays);
        }

        // WEEKLY
        // ----------
        void WeeklyCalculate()
        {
            if (nextDate.DayOfWeek == DayOfWeek.Sunday && properties.IsWeeklySunday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Monday && properties.IsWeeklyMonday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Tuesday && properties.IsWeeklyTuesday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Wednesday && properties.IsWeeklyWednesday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Thursday && properties.IsWeeklyThursday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Friday && properties.IsWeeklyFriday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Saturday && properties.IsWeeklySaturday) AddDateToRecurrences();

            nextDate = nextDate.DayOfWeek == DayOfWeek.Saturday ? nextDate.AddDays(((properties.WeeklyEveryNWeeks - 1) * 7) + 1) : nextDate.AddDays(1);
        }

        // MONTHLY
        // ----------
        void MonthlyCalculate()
        {
            if (properties.IsMonthlySpecific && properties.MonthlySpecificMonthDay >= 1
                && properties.MonthlySpecificMonthDay <= 31)
            {
                CalculateMonthlyBySpecificMonthDay();
            }
            else
            {
                CalculateMonthlyByDay();
            }
        }

        void CalculateMonthlyBySpecificMonthDay()
        {
            // if First StartMonthday > monthDay => skip First month
            if (properties.IsMonthlySpecific
                        && properties.MonthlySpecificMonthDay > 0 && properties.MonthlySpecificMonthDay <= 31
                        && nextDate.Day > properties.MonthlySpecificMonthDay)
            {
                nextDate = nextDate.AddMonths(1);
                nextDate = new DateTime(nextDate.Year, nextDate.Month, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
            }

            // 1 - 29
            if (properties.MonthlySpecificMonthDay <= 29)
            {
                // 29th of february
                if (nextDate.Month == 2 && properties.MonthlySpecificMonthDay == 29)
                {
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, DateTime.DaysInMonth(nextDate.Year, 2), nextDate.Hour, nextDate.Minute, nextDate.Second);
                    AddDateToRecurrences();
                    nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
                }
                else
                {
                    // 1 - 29 
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, properties.MonthlySpecificMonthDay, nextDate.Hour, nextDate.Minute, nextDate.Second);
                    AddDateToRecurrences();
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

                AddDateToRecurrences();
                nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
            }
            else
            {
                // 31 
                nextDate = new DateTime(nextDate.Year, nextDate.Month, DateTime.DaysInMonth(nextDate.Year, nextDate.Month), nextDate.Hour, nextDate.Minute, nextDate.Second);
                AddDateToRecurrences();
                nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
            }
        }



        void CalculateMonthlyByDay()
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
                    AddDateToRecurrences();
                    nextDate = nextDate.AddMonths(properties.MonthlyEveryNMonths);
                }
            }
            else
            {
                nextDate = new DateTime(currentYear, currentMonth, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
                nextDate = nextDate.AddMonths(1);
            }
        }

        // YEARLY
        // ----------
        void YearlyCalculate()
        {
            if (properties.IsYearlySpecific && properties.YearlySpecificMonth >= 1 && properties.YearlySpecificMonth <= 12
                && properties.YearlySpecificMonthDay > 0)
            {
                CalculateYealyBySpecificMonth();
            }
            else
            {
                CalculateYearlyByWeek();
            }
        }

        void CalculateYealyBySpecificMonth()
        {
            int daysInMonth = DateTime.DaysInMonth(nextDate.Year, properties.YearlySpecificMonth);
            int monthDay = properties.YearlySpecificMonthDay <= daysInMonth ? properties.YearlySpecificMonthDay : daysInMonth;
            nextDate = new DateTime(nextDate.Year, properties.YearlySpecificMonth, monthDay, nextDate.Hour, nextDate.Minute, nextDate.Second);
            if (nextDate.CompareTo(_startDate) < 0)
            {
                nextDate = nextDate.AddYears(1);
            }
            else
            {
                AddDateToRecurrences();
                nextDate = nextDate.AddYears(properties.YearlyEveryNYears);
            }
        }

        void CalculateYearlyByWeek()
        {

            DateTime monthStart = new DateTime(nextDate.Year, properties.YearlySpecificMonth, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
            var monthStartWeekday = (int)(monthStart.DayOfWeek);
            DateTime weekStartDate = monthStart.AddDays(-monthStartWeekday);

            int nthWeek;
            if (monthStartWeekday <= properties.MonthlyWeekDay) nthWeek = properties.YearlyNthWeek - 1;
            else nthWeek = properties.YearlyNthWeek;

            nextDate = weekStartDate.AddDays((nthWeek) * 7);
            nextDate = nextDate.AddDays(properties.YearlyWeekDay);

            if (monthStart.Month == nextDate.Month)
            {
                if (nextDate.CompareTo(_startDate) < 0)
                {
                    nextDate = nextDate.AddYears(1);
                }
                else
                {
                    AddDateToRecurrences();
                    nextDate = nextDate.AddYears(properties.YearlyEveryNYears);
                }
            }
            else
            {
                nextDate = new DateTime(monthStart.Year, monthStart.Month, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
                nextDate = nextDate.AddYears(1);
            }

        }

        void AddDateToRecurrences()
        {
            if (properties.IsRangeEndDate && nextDate > properties.RangeEndDate) return;

            recurrenceDates.Add(nextDate);
            count++;
        }
    }
}