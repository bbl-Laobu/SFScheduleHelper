using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Syncfusion.SfSchedule.XForms;

[assembly: InternalsVisibleTo("TestUnit")]

namespace Kareke.SFScheduleHelper
{
	internal class ParsePropsToRule
	{
		string freq;
		string interval;
		string count;
		string until;
		string byDay;
		string byMonthDay;
		string byMonth;
		string bySetPos;

		string rule;       
		RecurrenceProperties _recurrenceProperties;

		public bool HasError { get; set; }
		public string ErrorMessage { get; set; }
      

		public string Convert(RecurrenceProperties recurrenceProperties)
		{
			_recurrenceProperties = recurrenceProperties;
			// props is null
			if (_recurrenceProperties == null)
            {
                HasError = true;
                ErrorMessage = "Properties are null";
                return string.Empty;
            }

			rule = string.Empty;

			if (!ParseProps()) return string.Empty;

			return rule;
		}

		// Parse Rule Methods
        // --------------------------------------
		bool ParseProps()
		{
			// FREQ
			freq = "FREQ=";
			if (_recurrenceProperties.RecurrenceType == RecurrenceType.Daily) freq += "DAILY;";
			if (_recurrenceProperties.RecurrenceType == RecurrenceType.Weekly) freq += "WEEKLY;";
			if (_recurrenceProperties.RecurrenceType == RecurrenceType.Monthly) freq += "MONTHLY;";
			if (_recurrenceProperties.RecurrenceType == RecurrenceType.Yearly) freq += "YEARLY;";

			// INTERVAL
			interval = "INTERVAL=";
			int intervalValue = 1;
			switch (_recurrenceProperties.RecurrenceType)
            {
                case RecurrenceType.Daily:
                    if(_recurrenceProperties.Interval > 0 && _recurrenceProperties.IsDailyEveryNDays == true) 
                        intervalValue = _recurrenceProperties.Interval;
                    break;
                case RecurrenceType.Weekly:
                    if (_recurrenceProperties.Interval > 0) 
                        intervalValue = _recurrenceProperties.Interval;
                    break;
                case RecurrenceType.Monthly:
                    if (_recurrenceProperties.Interval > 0) 
                        intervalValue = _recurrenceProperties.Interval;
                    break;
                case RecurrenceType.Yearly:
                    if (_recurrenceProperties.Interval > 0) 
                        intervalValue = _recurrenceProperties.Interval;
                    break;
            }
			interval += intervalValue + ";";

			// COUNT
			count = string.Empty;
            if (_recurrenceProperties.RecurrenceRange == RecurrenceRange.Count && _recurrenceProperties.RecurrenceCount > 0) 
                count = "COUNT=" + _recurrenceProperties.RecurrenceCount.ToString() + ";";

			// UNTIL
			until = string.Empty;
            if (_recurrenceProperties.RecurrenceRange == RecurrenceRange.EndDate && _recurrenceProperties.EndDate > DateTime.MinValue) 
                //until = "UNTIL=" + _recurrenceProperties.EndDate.Month.ToString() 
                //                                        + "/" + _recurrenceProperties.EndDate.Day.ToString()
                //                                        + "/" + _recurrenceProperties.EndDate.Year.ToString() + ";" ;
                until = "UNTIL=" + _recurrenceProperties.EndDate.Year.ToString() 
                                                        +  _recurrenceProperties.EndDate.Month.ToString("D2")
                                                        +  _recurrenceProperties.EndDate.Day.ToString("D2") + ";" ;

			// BYDAY
			byDay = string.Empty;
			string weekdays = string.Empty;
			switch (_recurrenceProperties.RecurrenceType)
            {
				case RecurrenceType.Weekly:
                    if ((_recurrenceProperties.WeekDays & WeekDays.Sunday) == WeekDays.Sunday) weekdays += "SU,";
                    if ((_recurrenceProperties.WeekDays & WeekDays.Monday) == WeekDays.Monday) weekdays += "MO,";
                    if ((_recurrenceProperties.WeekDays & WeekDays.Tuesday) == WeekDays.Tuesday) weekdays += "TU,";
                    if ((_recurrenceProperties.WeekDays & WeekDays.Wednesday) == WeekDays.Wednesday) weekdays += "WE,";
                    if ((_recurrenceProperties.WeekDays & WeekDays.Thursday) == WeekDays.Thursday) weekdays += "TH,";
                    if ((_recurrenceProperties.WeekDays & WeekDays.Friday) == WeekDays.Friday) weekdays += "FR,";
                    if ((_recurrenceProperties.WeekDays & WeekDays.Saturday) == WeekDays.Saturday) weekdays += "SA,";
					break;

				case RecurrenceType.Monthly:
                    if (_recurrenceProperties.Week > 0 && !_recurrenceProperties.IsMonthlySpecific)
                    {
                        if (_recurrenceProperties.DayOfWeek == 0) weekdays += "SU,";
                        if (_recurrenceProperties.DayOfWeek == 1) weekdays += "MO,";
                        if (_recurrenceProperties.DayOfWeek == 2) weekdays += "TU,";
                        if (_recurrenceProperties.DayOfWeek == 3) weekdays += "WE,";
                        if (_recurrenceProperties.DayOfWeek == 4) weekdays += "TH,";
                        if (_recurrenceProperties.DayOfWeek == 5) weekdays += "FR,";
                        if (_recurrenceProperties.DayOfWeek == 6) weekdays += "SA,";
                    }
                    break;
                case RecurrenceType.Yearly:
                    if (_recurrenceProperties.Week > 0)
                    {
                        if (_recurrenceProperties.DayOfWeek == 0) weekdays += "SU,";
                        if (_recurrenceProperties.DayOfWeek == 1) weekdays += "MO,";
                        if (_recurrenceProperties.DayOfWeek == 2) weekdays += "TU,";
                        if (_recurrenceProperties.DayOfWeek == 3) weekdays += "WE,";
                        if (_recurrenceProperties.DayOfWeek == 4) weekdays += "TH,";
                        if (_recurrenceProperties.DayOfWeek == 5) weekdays += "FR,";
                        if (_recurrenceProperties.DayOfWeek == 6) weekdays += "SA,";
                    }
                    break;
			}
			weekdays = (weekdays != string.Empty) ? weekdays.Substring(0, weekdays.Length - 1) : weekdays;
            byDay = (weekdays != string.Empty) ? "BYDAY=" + weekdays + ";" : string.Empty;

            // BYSETPOS
            bySetPos = string.Empty;
            switch (_recurrenceProperties.RecurrenceType)
            {
                case RecurrenceType.Weekly:
                    if (_recurrenceProperties.Week > 0)
                        bySetPos = "BYSETPOS=" + _recurrenceProperties.Week.ToString() + ";";
                    break;
                case RecurrenceType.Monthly:
                    if (_recurrenceProperties.Week > 0)
                        bySetPos = "BYSETPOS=" + _recurrenceProperties.Week.ToString() + ";";
                    break;
                case RecurrenceType.Yearly:
                    if (_recurrenceProperties.Week > 0)
                        bySetPos = "BYSETPOS=" + _recurrenceProperties.Week.ToString() + ";";
                    break;
            }

			// BYMONTHDAY
			byMonthDay = string.Empty;
            switch (_recurrenceProperties.RecurrenceType)
            {
                case RecurrenceType.Monthly:
					if (_recurrenceProperties.DayOfMonth > 0 && _recurrenceProperties.IsMonthlySpecific) 
                        byMonthDay = "BYMONTHDAY=" + _recurrenceProperties.DayOfMonth.ToString() + ";";
                    break;
                case RecurrenceType.Yearly:
                    if (_recurrenceProperties.DayOfMonth > 0 && _recurrenceProperties.Week <= 0)
                        byMonthDay = "BYMONTHDAY=" + _recurrenceProperties.DayOfMonth.ToString() + ";";
					break;
            }

			// BYMONTH
			byMonth = string.Empty;
            switch (_recurrenceProperties.RecurrenceType)
            {
                case RecurrenceType.Yearly:
					if (_recurrenceProperties.Month > 0 && _recurrenceProperties.IsYearlySpecific)
						byMonth = "BYMONTH=" + _recurrenceProperties.Month.ToString() + ";";
                    break;
            }



			rule = freq + interval + count + until + byDay + byMonthDay + byMonth + bySetPos;
			return true;
		}      
	}
}
