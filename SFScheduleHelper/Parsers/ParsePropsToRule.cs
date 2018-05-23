﻿using System;
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
					if(_recurrenceProperties.DailyNDays > 0 &&  _recurrenceProperties.IsDailyEveryNDays) 
						intervalValue = _recurrenceProperties.DailyNDays;
                    break;
                case RecurrenceType.Weekly:
					if (_recurrenceProperties.WeeklyEveryNWeeks > 0) 
						intervalValue = _recurrenceProperties.WeeklyEveryNWeeks;
                    break;
                case RecurrenceType.Monthly:
					if (_recurrenceProperties.MonthlyEveryNMonths > 0) 
						intervalValue = _recurrenceProperties.MonthlyEveryNMonths;
                    break;
                case RecurrenceType.Yearly:
					if (_recurrenceProperties.YearlyEveryNYears > 0) 
						intervalValue = _recurrenceProperties.YearlyEveryNYears;
                    break;
            }
			interval += intervalValue + ";";

			// COUNT
			count = string.Empty;
			if (_recurrenceProperties.RangeRecurrenceCount > 0 && _recurrenceProperties.IsRangeRecurrenceCount) 
				count = "COUNT=" + _recurrenceProperties.RangeRecurrenceCount.ToString() + ";";

			// UNTIL
			until = string.Empty;
			if (_recurrenceProperties.RangeEndDate > DateTime.MinValue && _recurrenceProperties.IsRangeEndDate) 
				until = "UNTIL=" + _recurrenceProperties.RangeEndDate.Month.ToString() 
				                              + "/" + _recurrenceProperties.RangeEndDate.Day.ToString()
				                              + "/" + _recurrenceProperties.RangeEndDate.Year.ToString() + ";" ;

			// BYDAY
			byDay = string.Empty;
			string weekdays = string.Empty;
			switch (_recurrenceProperties.RecurrenceType)
            {
				case RecurrenceType.Weekly:
			    	if (_recurrenceProperties.IsWeeklySunday) weekdays += "SU,";
			    	if (_recurrenceProperties.IsWeeklyMonday) weekdays += "MO,";
		    		if (_recurrenceProperties.IsWeeklyTuesday) weekdays += "TU,";
		    		if (_recurrenceProperties.IsWeeklyWednesday) weekdays += "WE,";
		    		if (_recurrenceProperties.IsWeeklyThursday) weekdays += "TH,";
		    		if (_recurrenceProperties.IsWeeklyFriday) weekdays += "FR,";
    				if (_recurrenceProperties.IsWeeklySaturday) weekdays += "SA,";
					break;

				case RecurrenceType.Monthly:
					if (_recurrenceProperties.MonthlyWeekDay == 1) weekdays += "SU,";
					if (_recurrenceProperties.MonthlyWeekDay == 2) weekdays += "MO,";
					if (_recurrenceProperties.MonthlyWeekDay == 3) weekdays += "TU,";
					if (_recurrenceProperties.MonthlyWeekDay == 4) weekdays += "WE,";
					if (_recurrenceProperties.MonthlyWeekDay == 5) weekdays += "TH,";
					if (_recurrenceProperties.MonthlyWeekDay == 6) weekdays += "FR,";
					if (_recurrenceProperties.MonthlyWeekDay == 7) weekdays += "SA,";
                    break;
			}
			weekdays = (weekdays != string.Empty) ? weekdays.Substring(0, weekdays.Length - 1) : weekdays;
            byDay = (weekdays != string.Empty) ? "BYDAY=" + weekdays + ";" : string.Empty;

			// BYMONTHDAY
			byMonthDay = string.Empty;
            switch (_recurrenceProperties.RecurrenceType)
            {
                case RecurrenceType.Monthly:
					if (_recurrenceProperties.MonthlySpecificMonthDay > 0 && _recurrenceProperties.IsMonthlySpecific) 
						byMonthDay = "BYMONTHDAY=" + _recurrenceProperties.MonthlySpecificMonthDay.ToString() + ";";
                    break;
                case RecurrenceType.Yearly:
					if (_recurrenceProperties.YearlySpecificMonthDay > 0 && !_recurrenceProperties.IsMonthlySpecific)
						byMonthDay = "BYMONTHDAY=" + _recurrenceProperties.YearlySpecificMonthDay.ToString() + ";";
					break;
            }

			// BYMONTH
			byMonth = string.Empty;
            switch (_recurrenceProperties.RecurrenceType)
            {
                case RecurrenceType.Yearly:
					if (_recurrenceProperties.YearlySpecificMonth > 0 && _recurrenceProperties.IsYearlySpecific)
						byMonth = "BYMONTH=" + _recurrenceProperties.YearlySpecificMonth.ToString() + ";";
                    break;
            }

			// BYSETPOS
			bySetPos = string.Empty;
            switch (_recurrenceProperties.RecurrenceType)
            {
				case RecurrenceType.Weekly:
					if (_recurrenceProperties.NthWeek > 0)
						bySetPos = "BYSETPOS=" + _recurrenceProperties.NthWeek.ToString() + ";";
                    break;
                case RecurrenceType.Monthly:
					if (_recurrenceProperties.MonthlyNthWeek > 0)
						bySetPos = "BYSETPOS=" + _recurrenceProperties.MonthlyNthWeek.ToString() + ";";
                    break;
                case RecurrenceType.Yearly:
					if (_recurrenceProperties.YearlyNthWeek > 0)
						bySetPos = "BYSETPOS=" + _recurrenceProperties.YearlyNthWeek.ToString() + ";";
                    break;
            }

			rule = freq + interval + count + until + byDay + byMonthDay + byMonth + bySetPos;
			return true;
		}      
	}
}