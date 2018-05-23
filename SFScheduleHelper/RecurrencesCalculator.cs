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

            int count = 1;
            DateTime nextDate = _startDate;
            while (((properties.IsRangeEndDate && nextDate <= properties.RangeEndDate)
                    || (properties.IsRangeNoEndDate && count <= properties.RangeRecurrenceCount))
                   && count <= _maxReturns)
            {
                recurrenceDates.Add(nextDate);

                switch (properties.RecurrenceType)
                {
                    case RecurrenceType.Daily:
                        nextDate = DailyCalculate(nextDate);
                        break;

                    case RecurrenceType.Weekly:
                        nextDate = DailyCalculate(nextDate);
                        break;
                    default:
                        break;
                }

                count++;
            }

            return recurrenceDates;
        }

        DateTime DailyCalculate(DateTime nextDate)
        {
            return nextDate.AddDays(properties.DailyNDays);
        }

        DateTime WeeklyCalculate(DateTime nextDate)
        {
            return nextDate.AddDays(properties.DailyNDays);
        }
    }
}
