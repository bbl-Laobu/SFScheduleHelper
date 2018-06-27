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

            while (((properties.RecurrenceRange == RecurrenceRange.EndDate && nextDate <= properties.EndDate)
                    || (properties.RecurrenceRange == RecurrenceRange.Count && count <= properties.RecurrenceCount)
                    || (properties.RecurrenceRange == RecurrenceRange.NoEndDate))
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

            while (((properties.RecurrenceRange == RecurrenceRange.EndDate && nextDate <= properties.EndDate)
                        || (properties.RecurrenceRange == RecurrenceRange.Count && count <= properties.RecurrenceCount)
                        || (properties.RecurrenceRange == RecurrenceRange.NoEndDate))
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
            while (((properties.RecurrenceRange == RecurrenceRange.EndDate && nextDate <= properties.EndDate)
                    || (properties.RecurrenceRange == RecurrenceRange.Count && count <= properties.RecurrenceCount)
                    || (properties.RecurrenceRange == RecurrenceRange.NoEndDate))
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
            nextDate = nextDate.AddDays(properties.Interval);
        }

        // WEEKLY
        // ----------
        void WeeklyCalculate()
        {
            if (nextDate.DayOfWeek == DayOfWeek.Sunday && (properties.WeekDays & WeekDays.Sunday) == WeekDays.Sunday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Monday && (properties.WeekDays & WeekDays.Monday) == WeekDays.Monday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Tuesday && (properties.WeekDays & WeekDays.Tuesday) == WeekDays.Tuesday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Wednesday && (properties.WeekDays & WeekDays.Wednesday) == WeekDays.Wednesday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Thursday && (properties.WeekDays & WeekDays.Thursday) == WeekDays.Thursday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Friday && (properties.WeekDays & WeekDays.Friday) == WeekDays.Friday) AddDateToRecurrences();
            if (nextDate.DayOfWeek == DayOfWeek.Saturday && (properties.WeekDays & WeekDays.Saturday) == WeekDays.Saturday) AddDateToRecurrences();

            nextDate = nextDate.DayOfWeek == DayOfWeek.Saturday ? nextDate.AddDays(((properties.Interval - 1) * 7) + 1) : nextDate.AddDays(1);
        }

        // MONTHLY
        // ----------
        void MonthlyCalculate()
        {
            if (properties.IsMonthlySpecific && properties.DayOfMonth >= 1
                && properties.DayOfMonth <= 31)
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
                        && properties.DayOfMonth > 0 && properties.DayOfMonth <= 31
                        && nextDate.Day > properties.DayOfMonth)
            {
                nextDate = nextDate.AddMonths(1);
                nextDate = new DateTime(nextDate.Year, nextDate.Month, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
            }

            // 1 - 29
            if (properties.DayOfMonth <= 29)
            {
                // 29th of february
                if (nextDate.Month == 2 && properties.DayOfMonth == 29)
                {
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, DateTime.DaysInMonth(nextDate.Year, 2), nextDate.Hour, nextDate.Minute, nextDate.Second);
                    AddDateToRecurrences();
                    nextDate = nextDate.AddMonths(properties.Interval);
                }
                else
                {
                    // 1 - 29 
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, properties.DayOfMonth, nextDate.Hour, nextDate.Minute, nextDate.Second);
                    AddDateToRecurrences();
                    nextDate = nextDate.AddMonths(properties.Interval);
                }
            }
            else if (properties.DayOfMonth == 30)
            {
                // 30 
                if (nextDate.Month == 2) // check february
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, DateTime.DaysInMonth(nextDate.Year, nextDate.Month), nextDate.Hour, nextDate.Minute, nextDate.Second);
                else
                    nextDate = new DateTime(nextDate.Year, nextDate.Month, 30, nextDate.Hour, nextDate.Minute, nextDate.Second);

                AddDateToRecurrences();
                nextDate = nextDate.AddMonths(properties.Interval);
            }
            else
            {
                // 31 
                nextDate = new DateTime(nextDate.Year, nextDate.Month, DateTime.DaysInMonth(nextDate.Year, nextDate.Month), nextDate.Hour, nextDate.Minute, nextDate.Second);
                AddDateToRecurrences();
                nextDate = nextDate.AddMonths(properties.Interval);
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
            if (monthStartWeekday <= properties.DayOfWeek) nthWeek = properties.Week - 1;
            else nthWeek = properties.Week;

            nextDate = weekStartDate.AddDays((nthWeek) * 7);
            nextDate = nextDate.AddDays(properties.DayOfWeek);

            if (currentMonth == nextDate.Month)
            {
                if (nextDate.CompareTo(_startDate) < 0)
                {
                    nextDate = nextDate.AddMonths(1);
                }
                else
                {
                    AddDateToRecurrences();
                    nextDate = nextDate.AddMonths(properties.Interval);
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
            if (properties.Week <= 0 && properties.Month >= 1 && properties.Month <= 12
                && properties.DayOfMonth > 0)
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
            int daysInMonth = DateTime.DaysInMonth(nextDate.Year, properties.Month);
            int monthDay = properties.DayOfMonth <= daysInMonth ? properties.DayOfMonth : daysInMonth;
            nextDate = new DateTime(nextDate.Year, properties.Month, monthDay, nextDate.Hour, nextDate.Minute, nextDate.Second);
            if (nextDate.CompareTo(_startDate) < 0)
            {
                nextDate = nextDate.AddYears(1);
            }
            else
            {
                AddDateToRecurrences();
                nextDate = nextDate.AddYears(properties.Interval);
            }
        }

        void CalculateYearlyByWeek()
        {

            DateTime monthStart = new DateTime(nextDate.Year, properties.Month, 1, nextDate.Hour, nextDate.Minute, nextDate.Second);
            var monthStartWeekday = (int)(monthStart.DayOfWeek);
            DateTime weekStartDate = monthStart.AddDays(-monthStartWeekday);

            int nthWeek;
            if (monthStartWeekday <= properties.DayOfWeek) nthWeek = properties.Week - 1;
            else nthWeek = properties.Week;

            nextDate = weekStartDate.AddDays((nthWeek) * 7);
            nextDate = nextDate.AddDays(properties.DayOfWeek);

            if (monthStart.Month == nextDate.Month)
            {
                if (nextDate.CompareTo(_startDate) < 0)
                {
                    nextDate = nextDate.AddYears(1);
                }
                else
                {
                    AddDateToRecurrences();
                    nextDate = nextDate.AddYears(properties.Interval);
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
            if (properties.RecurrenceRange == RecurrenceRange.EndDate && nextDate > properties.EndDate) return;

            recurrenceDates.Add(nextDate);
            count++;
        }
    }
}