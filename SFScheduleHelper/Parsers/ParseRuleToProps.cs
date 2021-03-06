﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Syncfusion.SfSchedule.XForms;

[assembly: InternalsVisibleTo("TestNUnit")]

namespace Kareke.SFScheduleHelper
{
    internal class ParseRuleToProps
    {
        // ParseRuleToProps
        bool hasFreq;
        bool hasCount;
        bool hasUntil;
        bool hasByDay;
        bool hasByMonthDay;
        bool hasByMonth;
        bool hasBySetPos;

        RecurrenceType freq;
        int interval;
        int count;
        DateTime until;
        List<string> byDay;
        int byMonthDay;
        int byMonth;
        int bySetPos;

        string _rule;
        DateTime _startDate;
        RecurrenceProperties recurrenceProperties;

        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }


        public RecurrenceProperties Convert(string rule, DateTime startDate)
        {
            _rule = rule;
            _startDate = startDate;

            if (ParseRule())
            {
                if (CreateRecurrenceProperties()) return recurrenceProperties;
            }
            return null;
            //return new RecurrenceProperties { RangeStartDate = _startDate };
        }

        // Parse Rule Methods
        // --------------------------------------
        bool ParseRule()
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

            // Check Complete Rule Validity
            if (!CompleteRuleValidation()) return false;

            return true;
        }

        void ClearValues()
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

        bool SetFreq(string type)
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
        bool SetInterval(string value)
        {
            interval = StrintToInt(value);
            if (interval == -1) { HasError = true; ErrorMessage = "INTERVAL has non valid value " + value; return false; }
            return true;
        }

        // COUNT parse
        bool SetCount(string value)
        {
            count = StrintToInt(value);
            if (count == -1) { HasError = true; ErrorMessage = "COUNT has non valid value " + value; return false; }
            return true;
        }

        // UNTIL parse
        bool SetUntil(string value)
        {
            until = StringToDate(value);
            if (until == DateTime.MinValue) { HasError = true; ErrorMessage = "UNTIL has non valid value " + value; return false; }
            until = until + new TimeSpan(23, 59, 59);
            return true;
        }

        // BYDAY parse
        bool SetByDayList(string dayString)
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
        bool SetByMonthDay(string value)
        {
            byMonthDay = StrintToInt(value);
            if (byMonthDay < 1 || byMonthDay > 31) { HasError = true; ErrorMessage = "BYMONTHDAY has non valid value " + value; return false; }
            return true;
        }

        // BYMONTH parse 
        bool SetByMonth(string value)
        {
            byMonth = StrintToInt(value);
            if (byMonth < 1 || byMonth > 12) { HasError = true; ErrorMessage = "BYMONTH has non valid value " + value; return false; }
            return true;
        }

        // BYSETPOS parse 
        bool SetBySetPos(string value)
        {
            bySetPos = StrintToInt(value);
            if (bySetPos < 1 || bySetPos > 52) { HasError = true; ErrorMessage = "BYSETPOS has non valid value " + value; return false; }
            return true;
        }

        // Complete Rule Validation
        bool CompleteRuleValidation()
        {
            switch (freq)
            {
                case RecurrenceType.Weekly:
                    if (!hasByDay)
                    {
                        HasError = true; ErrorMessage = "BYDAY should be set for Weekly"; return false;
                    }
                    break;
                case RecurrenceType.Monthly:
                    if (!(hasByDay && hasBySetPos) & !hasByMonthDay)
                    {
                        HasError = true; ErrorMessage = "BYDAY or BYMONTHDAY should be set for Monthly"; return false;
                    }
                    if ((hasBySetPos || hasBySetPos) && hasByMonthDay)
                    {
                        HasError = true; ErrorMessage = "BYSETPOS/BYDAY or BYMONTHDAY should be set for Monthly, not both."; return false;
                    }
                    break;
                case RecurrenceType.Yearly:
                    if (!hasByMonth)
                    {
                        HasError = true; ErrorMessage = "BYMONTH should be set for Yearly"; return false;
                    }
                    if (!(hasByDay && hasBySetPos) & !hasByMonthDay)
                    {
                        HasError = true; ErrorMessage = "BYSETPOS/BYDAY or BYMONTHDAY should be set for Yearly"; return false;
                    }
                    if ((hasBySetPos || hasBySetPos) && hasByMonthDay)
                    {
                        HasError = true; ErrorMessage = "BYSETPOS/BYDAY or BYMONTHDAY should be set for Yearly, not both."; return false;
                    }
                    break;
            }
            return true;
        }


        // Parse Helper Methods
        // ---------------------
        int StrintToInt(string intString)
        {
            bool parsed = Int32.TryParse(intString, out int numValue);

            if (parsed) return numValue;
            else return -1;
        }

        DateTime StringToDate(string dateString)
        {
            DateTime untilDate;
            if (string.IsNullOrEmpty(dateString) || dateString.Length != 8 || !IsDigitsOnly(dateString)) return DateTime.MinValue;

            try
            {
                int year = Int32.Parse(dateString.Substring(0,4));
                int month = Int32.Parse(dateString.Substring(4, 2));
                int day = Int32.Parse(dateString.Substring(6, 2));
                untilDate = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                untilDate = DateTime.MinValue;
            }

            /*
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
            */

            return untilDate;
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        string GetWeekDay(string weekDay)
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
        bool CreateRecurrenceProperties()
        {
            // Instantiate
            recurrenceProperties = new RecurrenceProperties
            {
                StartDate = _startDate
            };

            // FREQ set
            if (!hasFreq) { HasError = true; ErrorMessage = "FREQ not defined"; return false; }
            recurrenceProperties.RecurrenceType = freq;

            // INTERVAL set
            interval = interval < 1 ? 1 : interval;
            switch (freq)
            {
                case RecurrenceType.Daily:
                    recurrenceProperties.Interval = interval;
                    recurrenceProperties.IsDailyEveryNDays = true;
                    break;
                case RecurrenceType.Weekly:
                    recurrenceProperties.Interval = interval;
                    break;
                case RecurrenceType.Monthly:
                    recurrenceProperties.Interval = interval;
                    break;
                case RecurrenceType.Yearly:
                    recurrenceProperties.Interval = interval;
                    break;
            }

            // COUNT set
            if (hasCount && count > 0)
            {
                recurrenceProperties.RecurrenceRange = RecurrenceRange.Count;
                recurrenceProperties.RecurrenceCount = count;
            }

            // UNTIL set
            if (hasUntil)
            {
                recurrenceProperties.RecurrenceRange = RecurrenceRange.EndDate;
                recurrenceProperties.EndDate = until;
            }
            else if (!hasCount) recurrenceProperties.RecurrenceRange = RecurrenceRange.NoEndDate;

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
                        recurrenceProperties.DayOfWeek = GetWeekDayValue(byDay[0]);
                        break;

                    case RecurrenceType.Yearly:
                        recurrenceProperties.DayOfWeek = GetWeekDayValue(byDay[0]);
                        break;
                }
            }

            // BYMONTHDAY set
            if (hasByMonthDay)
            {
                switch (freq)
                {
                    case RecurrenceType.Monthly:
                        recurrenceProperties.DayOfMonth = byMonthDay;
                        recurrenceProperties.IsMonthlySpecific = true;
                        break;
                    case RecurrenceType.Yearly:
                        recurrenceProperties.DayOfMonth = byMonthDay;
                        recurrenceProperties.IsMonthlySpecific = false;
                        break;
                }
            }

            // BYMONTH set
            if (hasByMonth)
            {
                switch (freq)
                {
                    case RecurrenceType.Yearly:
                        recurrenceProperties.Month = byMonth;
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
                        recurrenceProperties.Week = bySetPos;
                        break;
                    case RecurrenceType.Monthly:
                        recurrenceProperties.Week = bySetPos;
                        break;
                    case RecurrenceType.Yearly:
                        recurrenceProperties.Week = bySetPos;
                        break;
                }
            }

            return true;
        }

        void SetWeeklyWeekDayRule(string weekDay)
        {
            weekDay = string.IsNullOrEmpty(weekDay) ? string.Empty : weekDay.ToUpper().Trim();
            switch (weekDay)
            {
                case "SU": recurrenceProperties.WeekDays |= WeekDays.Sunday; break;
                case "MO": recurrenceProperties.WeekDays |= WeekDays.Monday; break;
                case "TU": recurrenceProperties.WeekDays |= WeekDays.Tuesday; break;
                case "WE": recurrenceProperties.WeekDays |= WeekDays.Wednesday; break;
                case "TH": recurrenceProperties.WeekDays |= WeekDays.Thursday; break;
                case "FR": recurrenceProperties.WeekDays |= WeekDays.Friday; break;
                case "SA": recurrenceProperties.WeekDays |= WeekDays.Saturday; break;
                default: break;
            }
        }

        void SetWeeklyWeekDayRule(int weekDay)
        {
            switch (weekDay)
            {
                case 0: recurrenceProperties.WeekDays = WeekDays.Sunday; break;
                case 1: recurrenceProperties.WeekDays = WeekDays.Monday; break;
                case 2: recurrenceProperties.WeekDays = WeekDays.Tuesday; break;
                case 3: recurrenceProperties.WeekDays = WeekDays.Wednesday; break;
                case 4: recurrenceProperties.WeekDays = WeekDays.Thursday; break;
                case 5: recurrenceProperties.WeekDays = WeekDays.Friday; break;
                case 6: recurrenceProperties.WeekDays = WeekDays.Friday; break;
                default: break;
            }
        }

        void SetMonthlyWeekDayRussle(string weekDay)
        {
            weekDay = string.IsNullOrEmpty(weekDay) ? string.Empty : weekDay.ToUpper().Trim();
            switch (weekDay)
            {
                case "SU": recurrenceProperties.DayOfWeek = 0; break;
                case "MO": recurrenceProperties.DayOfWeek = 1; break;
                case "TU": recurrenceProperties.DayOfWeek = 2; break;
                case "WE": recurrenceProperties.DayOfWeek = 3; break;
                case "TH": recurrenceProperties.DayOfWeek = 4; break;
                case "FR": recurrenceProperties.DayOfWeek = 5; break;
                case "SA": recurrenceProperties.DayOfWeek = 6; break;
                default: break;
            }
        }

        int GetWeekDayValue(string weekDay)
        {
            weekDay = string.IsNullOrEmpty(weekDay) ? string.Empty : weekDay.ToUpper().Trim();
            switch (weekDay)
            {
                case "SU": return 0;
                case "MO": return 1;
                case "TU": return 2;
                case "WE": return 3;
                case "TH": return 4;
                case "FR": return 5;
                case "SA": return 6;
                default: return 0;
            }
        }
    }
}
