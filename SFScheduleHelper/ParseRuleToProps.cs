using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Syncfusion.SfSchedule.XForms;

[assembly: InternalsVisibleTo("TestUnit")]

namespace Kareke.SFScheduleHelper
{
	internal static class ParseRuleToProps
	{
		// ParseRuleToProps
		static bool hasFreq;
		static bool hasCount;
		static bool hasUntil;
		static bool hasByDay;
		static bool hasByMonthDay;
		static bool hasByMonth;
		static bool hasBySetPos;
        
		static RecurrenceType freq;
		static int interval;
		static int count;
		static DateTime until;
		static List<string> byDay;
		static int byMonthDay;
		static int byMonth;
		static int bySetPos;

		static string _rule; 
		static DateTime _startDate;      
		static RecurrenceProperties recurrenceProperties;

		public static bool HasError { get; set; }
		public static string ErrorMessage { get; set; }
      

		public static RecurrenceProperties Convert(string rule, DateTime startDate)
		{
			_rule = rule;
            _startDate = startDate;

			if (ParseRule())
			{
				if (CreateRecurrenceProperties()) return recurrenceProperties;
			}

			return new RecurrenceProperties { RangeStartDate = _startDate };
		}

		// Parse Rule Methods
        // --------------------------------------
		static bool ParseRule()
		{
            // Rule is empty
			if (string.IsNullOrEmpty(_rule))
            {
                HasError = true;
                ErrorMessage = "Rule is empty";
                return false;
            }

			ClearValues();
			_rule = _rule.ToUpper().Trim();

			char[] propertySeperator = { ';' };
			string[] ruleProperties = _rule.Split(propertySeperator);

			char[] keyValueSeparator = { '=' };

            // Loop through all found properties
			for (int i = 0; i < ruleProperties.Length; i++)
			{
				string[] keyValues = ruleProperties[i].Split(keyValueSeparator);
				string key = keyValues[0].Trim();
				string value = keyValues.Length > 1 ? keyValues[1].Trim() : String.Empty;

				switch (key)
				{
					case "FREQ":
						if (!SetFreq(value)) return false;
						hasFreq = true;
						break;

					case "INTERVAL":
						if (!SetInterval(value)) return false;
						break;

					case "COUNT":
						if (!SetCount(value)) return false;
						hasCount = true;
						break;

					case "UNTIL":
						if (!SetUntil(value)) return false;
						hasUntil = true;
						break;

					case "BYDAY":
						if (!SetByDayList(value)) return false;
						hasByDay = true;
						break;

					case "BYMONTHDAY":
                        if (!SetByMonthDay(value)) return false;
						hasByMonthDay = true;
                        break;

					case "BYMONTH":
                        if (!SetByMonth(value)) return false;
                        hasByMonth = true;
                        break;

					case "BYSETPOS":
                        if (!SetBySetPos(value)) return false;
                        hasBySetPos = true;
                        break;

					case "":
						break;

					default:
						HasError = true;
						ErrorMessage = "Found non valid key " + key;
						return false;
				}            
			}
			return true;
		}

		static void ClearValues()
        {
            hasFreq = false;
            hasCount = false;
            hasUntil = false;
            hasByDay = false;
            hasByMonthDay = false;
            hasByMonth = false;
            hasBySetPos = false;

            freq = RecurrenceType.Daily;
            interval = 1;
            count = 0;
            until = DateTime.MinValue;
            byDay = new List<string>();
            byMonthDay = 0;
            byMonth = 0;
            bySetPos = 0;
        }

		static bool SetFreq(string type)
		{
			// FREQ parse
			switch (type)
            {
                case "DAILY":
                    freq = RecurrenceType.Daily;
                    break;
                case "WEEKLY":
                    freq = RecurrenceType.Weekly;
                    break;
                case "MONTHLY":
                    freq = RecurrenceType.Monthly;
                    break;
                case "YEARLY":
                    freq = RecurrenceType.Yearly;
                    break;
                default:
                    HasError = true;
                    ErrorMessage = "FREQ has non valid value " + type;
                    return false;
            }
			return true;
		}
        
		// INTERVAL parse
		static bool SetInterval(string value)
		{
			interval = StrintToInt(value);
            if (interval == -1) { HasError = true; ErrorMessage = "INTERVAL has non valid value " + value; return false; }
			return true;
		}

		// COUNT parse
		static bool SetCount(string value)
        {
			count = StrintToInt(value);
            if (count == -1) { HasError = true; ErrorMessage = "COUNT has non valid value " + value; return false; }
            return true;
        }      

		// UNTIL parse
		static bool SetUntil(string value)
        {
			until = StringToDate(value);
            if (until == DateTime.MinValue) { HasError = true; ErrorMessage = "UNTIL has non valid value " + value; return false; }
            return true;
        }  

        // BYDAY parse
		static bool SetByDayList(string dayString)
        {
            char[] daySeperator = { ',' };
            string[] weekDays = dayString.Split(daySeperator);

            if (weekDays == null && weekDays.Length == 0) { HasError = true; ErrorMessage = "BYDAY has non valid value " + dayString; return false; }

            for (int i = 0; i < weekDays.Length; i++)
            {
                string weekDay = GetWeekDay(weekDays[i]);
                if (!string.IsNullOrEmpty(weekDay)) byDay.Add(weekDay);
                else
                {
                    HasError = true; ErrorMessage = "BYDAY has non valid value " + weekDays[i]; return false;
                }
            }         
            return true;
        }

		// BYMONTHDAY parse
        static bool SetByMonthDay(string value)
        {
            byMonthDay = StrintToInt(value);
			if (byMonthDay < 1 || byMonthDay > 31) { HasError = true; ErrorMessage = "BYMONTHDAY has non valid value " + value; return false; }
            return true;
        } 

		// BYMONTH parse 
        static bool SetByMonth(string value)
        {
            byMonth = StrintToInt(value);
			if (byMonth < 1 || byMonth > 12) { HasError = true; ErrorMessage = "BYMONTH has non valid value " + value; return false; }
            return true;
        } 

		// BYSETPOS parse 
        static bool SetBySetPos(string value)
        {
            bySetPos = StrintToInt(value);
			if (bySetPos < 1 || bySetPos > 52) { HasError = true; ErrorMessage = "BYSETPOS has non valid value " + value; return false; }
            return true;
        } 
       

		// Parse Helper Methods
        // ---------------------
		static int StrintToInt(string intString)
		{
			bool parsed = Int32.TryParse(intString, out int numValue);

			if (parsed) return numValue;
			else return -1;
		}

		static DateTime StringToDate(string dateString)
        {
			DateTime untilDate; 
			char[] dateSeperator = { '/' };
			string[] date = dateString.Split(dateSeperator);
			if (date != null && date.Length == 3)
			{
				try
				{
					int year = Int32.Parse(date[2]);
					int month = Int32.Parse(date[0]);
					int day = Int32.Parse(date[1]);
					untilDate = new DateTime(year, month, day);
				}
				catch (Exception)
				{
					untilDate = DateTime.MinValue;
				}

			}
			else untilDate = DateTime.MinValue;

			return untilDate;
        }
              
		static string GetWeekDay(string weekDay)
        {
			weekDay = string.IsNullOrEmpty(weekDay) ? string.Empty : weekDay.ToUpper().Trim();
			switch (weekDay)
            {
				case "SU": return "SU";
				case "MO": return "MO";
				case "TU": return "TU";
				case "WE": return "WE";
                case "TH": return "TH";
                case "FR": return "FR";
				case "SA": return "SA";
				default: return string.Empty;
            }
        }


		// Update in RecurrenceProperties Methods
        // ----------------------------------------------------
		static bool CreateRecurrenceProperties()
		{
			// Instantiate
			recurrenceProperties = new RecurrenceProperties
			{
				RangeStartDate = _startDate
			};

			// FREQ set
			if (!hasFreq) { HasError = true; ErrorMessage = "FREQ not defined"; return false; }
			recurrenceProperties.RecurrenceType = freq;

			// INTERVAL set
			interval = interval < 1 ? 1 : interval;
			switch (freq)
			{
				case RecurrenceType.Daily:
					recurrenceProperties.DailyNDays = interval;
					recurrenceProperties.IsDailyEveryNDays = true;
					break;
				case RecurrenceType.Weekly:
					recurrenceProperties.WeeklyEveryNWeeks = interval;
					break;
				case RecurrenceType.Monthly:
					recurrenceProperties.MonthlyEveryNMonths = interval;
					break;
				case RecurrenceType.Yearly:
					recurrenceProperties.YearlyEveryNYears = interval;
					break;
			}
            
			// COUNT set
            if (hasCount && count > 0)
			{
				recurrenceProperties.IsRangeRecurrenceCount = true;
				recurrenceProperties.RangeRecurrenceCount = count;
			}

			// UNTIL set
			if (hasUntil)
			{
				recurrenceProperties.IsRangeEndDate = true;
				recurrenceProperties.RangeEndDate = until;
			}
			else recurrenceProperties.IsRangeNoEndDate = true;

			// BYDAY set
            if (hasByDay)
			{
				switch (freq)
				{ 
					case RecurrenceType.Weekly:
						foreach (var weekDay in byDay)
                        {
                            SetWeeklyWeekDayRule(weekDay);
                        }
                        break;
                    case RecurrenceType.Monthly:
						SetMonthlyWeekDayRule(byDay[0]);
						break;
				}
			} else
			{
				//switch (freq)
                //{
                //    case RecurrenceType.Weekly:
				//		int weekday = (int)recurrenceProperties.RangeStartDate.DayOfWeek;
				//		SetWeeklyWeekDayRule(weekday);
                //        break;
                //}
			}

			// BYMONTHDAY set
			if (hasByMonthDay)
            {
				switch (freq)
                {
                    case RecurrenceType.Monthly:
						recurrenceProperties.MonthlySpecificMonthDay = byMonthDay;
                        recurrenceProperties.IsMonthlySpecific = true;
                        break;
                    case RecurrenceType.Yearly:
						recurrenceProperties.YearlySpecificMonthDay = byMonthDay;
                        recurrenceProperties.IsMonthlySpecific = false;
                        break;
                }
            }

			// BYMONTHDAY set
            if (hasByMonth)
            {
                switch (freq)
                {
                    case RecurrenceType.Yearly:
						recurrenceProperties.YearlySpecificMonth = byMonth;
						recurrenceProperties.IsYearlySpecific = true;
                        break;

                }
            }

			// BYSETPOS set
            if (hasBySetPos)
            {
                switch (freq)
                {
					case RecurrenceType.Weekly:
                        recurrenceProperties.NthWeek = bySetPos;
                        break;
                    case RecurrenceType.Monthly:
						recurrenceProperties.MonthlyNthWeek = bySetPos;
                        break;
                    case RecurrenceType.Yearly:
						recurrenceProperties.YearlyNthWeek = bySetPos;
                        break;
                }
            }

			return true;
		}
        
		static void SetWeeklyWeekDayRule(string weekDay)
        {
            weekDay = string.IsNullOrEmpty(weekDay) ? string.Empty : weekDay.ToUpper().Trim();
            switch (weekDay)
            {
				case "SU": recurrenceProperties.IsWeeklySunday = true; break;
				case "MO": recurrenceProperties.IsWeeklyMonday = true; break;
				case "TU": recurrenceProperties.IsWeeklyTuesday = true; break;
				case "WE": recurrenceProperties.IsWeeklyWednesday = true; break;
				case "TH": recurrenceProperties.IsWeeklyThursday = true; break;
				case "FR": recurrenceProperties.IsWeeklyFriday = true; break;
				case "SA": recurrenceProperties.IsWeeklySaturday = true; break;
				default: break;
            }
        }

		static void SetWeeklyWeekDayRule(int weekDay)
        {
            switch (weekDay)
            {
                case 0: recurrenceProperties.IsWeeklySunday = true; break;
                case 1: recurrenceProperties.IsWeeklyMonday = true; break;
                case 2: recurrenceProperties.IsWeeklyTuesday = true; break;
                case 3: recurrenceProperties.IsWeeklyWednesday = true; break;
                case 4: recurrenceProperties.IsWeeklyThursday = true; break;
                case 5: recurrenceProperties.IsWeeklyFriday = true; break;
                case 6: recurrenceProperties.IsWeeklySaturday = true; break;
                default: break;
            }
        }

		static void SetMonthlyWeekDayRule(string weekDay)
        {
            weekDay = string.IsNullOrEmpty(weekDay) ? string.Empty : weekDay.ToUpper().Trim();
            switch (weekDay)
            {
				case "SU": recurrenceProperties.MonthlyWeekDay = 1; break;
				case "MO": recurrenceProperties.MonthlyWeekDay = 2; break;
				case "TU": recurrenceProperties.MonthlyWeekDay = 3; break;
				case "WE": recurrenceProperties.MonthlyWeekDay = 4; break;
				case "TH": recurrenceProperties.MonthlyWeekDay = 5; break;
				case "FR": recurrenceProperties.MonthlyWeekDay = 6; break;
				case "SA": recurrenceProperties.MonthlyWeekDay = 7; break;
				default: break;
            }
        }


	}
}
