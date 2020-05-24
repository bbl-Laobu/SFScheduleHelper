using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestNUnit.RecurrenceConverterTests
{
	[TestFixture()]
	public class RuleToPropsTests
	{  
        RecurrenceConverter converter;

        [SetUp]
        public void Init()
        {
            converter = new RecurrenceConverter();
        }

		[Test()]
		public void EmptyRule()
		{
			string rule = "";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            Assert.AreEqual(true, converter.HasError);
            Assert.AreEqual("Rule is empty", converter.ErrorMessage);
			Assert.AreEqual(null, props);
		}

		// Test Methods Just Type
		// ----------------------------------
		[Test()]
		public void Daily()
		{
			string rule = "FREQ=DAILY;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.NoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void Weekly()
		{
			string rule = "FREQ=Weekly;BYDAY=MO";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "WeeklyEveryNWeeks");
			//Assert.AreEqual(true, props.IsWeeklySaturday, "IsWeeklySaturday");
            Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.NoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void Monthly_BySetPos_ByMonthDay()
		{
            string rule = "FREQ=Monthly;BYSETPOS=4; BYMONTHDAY=15; ";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            Assert.AreEqual(true, converter.HasError);
            Assert.AreEqual("BYSETPOS/BYDAY or BYMONTHDAY should be set for Monthly, not both.", converter.ErrorMessage);
		}

        [Test()]
        public void Monthly_ByDAY_ByMonthDay()
        {
            string rule = "FREQ=Monthly;BYDAY=FR;BYSETPOS=4;BYMONTHDAY=15; ";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            Assert.AreEqual(true, converter.HasError);
            Assert.AreEqual("BYSETPOS/BYDAY or BYMONTHDAY should be set for Monthly, not both.", converter.ErrorMessage);
        }

        [Test()]
        public void Monthly_ByDAY_BySetPOS_ByMonthDay()
        {
            string rule = "FREQ=Monthly;BYDAY=FR;BYSETPOS=4;BYMONTHDAY=15; ";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            Assert.AreEqual(true, converter.HasError);
            Assert.AreEqual("BYSETPOS/BYDAY or BYMONTHDAY should be set for Monthly, not both.", converter.ErrorMessage);
        }

		// Test Methods from SFSchedule Docs
		// ----------------------------------

		// DAILY Tests
		// -------------
		[Test()]
		public void DailyExample1()
		{
			string rule = "FREQ=DAILY; INTERVAL=1";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
            Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.NoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void DailyExample2()
		{
			string rule = "FREQ=DAILY; INTERVAL=1; COUNT=5";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count, "IsRangeRecurrenceCount");
			Assert.AreEqual(5, props.RecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void DailyExample3()
		{
			string rule = "FREQ=DAILY; INTERVAL=1; UNTIL=20170620";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);


			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.EndDate, "IsRangeEndDate");
			Assert.AreEqual(new DateTime(2017, 06, 20, 23, 59, 59), props.EndDate, "RangeEndDate");
		}

		[Test()]
		public void DailyExample4()
		{
			string rule = "FREQ=DAILY; INTERVAL=2; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(2, props.Interval, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RecurrenceCount, "RangeRecurrenceCount");
		}

		// WEEKLY Tests
		// -------------
		[Test()]
		public void WeeklyExample1()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=1; BYDAY=MO, WE, FR";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "WeeklyEveryNWeeks");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Monday) == WeekDays.Monday, "IsWeeklyMonday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Wednesday) == WeekDays.Wednesday, "IsWeeklyWednesday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Friday) == WeekDays.Friday, "IsWeeklyFriday");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.NoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void WeeklyExample2()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=1; BYDAY=TH; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "WeeklyEveryNWeeks");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Thursday) == WeekDays.Thursday, "IsWeeklyThursday");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void WeeklyExample3()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=1; BYDAY=MO; UNTIL=20170720";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "WeeklyEveryNWeeks");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Monday) == WeekDays.Monday, "IsWeeklyMonday");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.EndDate, "IsRangeEndDate");
            Assert.AreEqual(new DateTime(2017, 07, 20, 23, 59, 59), props.EndDate, "RangeEndDate ");
		}

		[Test()]
		public void WeeklyExample4()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=2; BYDAY=MO, WE, FR; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(2, props.Interval, "WeeklyEveryNWeeks");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Monday) == WeekDays.Monday, "IsWeeklyMonday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Wednesday) == WeekDays.Wednesday, "IsWeeklyWednesday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Friday) == WeekDays.Friday, "IsWeeklyFriday ");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count, "IsRangeRecurrenceCount ");
			Assert.AreEqual(10, props.RecurrenceCount, "RangeRecurrenceCount ");
		}

		// Every Day Tests
		// -------------
		[Test()]
		public void EveryDayExample1()
		{
			string rule = "FREQ=WEEKLY; BYDAY=MO, TU, WE, TH, FR";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "WeeklyEveryNWeeks");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Monday) == WeekDays.Monday, "IsWeeklyMonday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Tuesday) == WeekDays.Tuesday, "IsWeeklyTuesday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Wednesday) == WeekDays.Wednesday, "IsWeeklyWednesday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Thursday) == WeekDays.Thursday, "IsWeeklyThursday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Friday) == WeekDays.Friday, "IsWeeklyFriday");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.NoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void EveryDayExample2()
		{
			string rule = "FREQ=WEEKLY; BYDAY=MO, TU, WE, TH, FR; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "WeeklyEveryNWeeks");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Monday) == WeekDays.Monday, "IsWeeklyMonday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Tuesday) == WeekDays.Tuesday,"IsWeeklyTuesday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Wednesday) == WeekDays.Wednesday, "IsWeeklyWednesday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Thursday) == WeekDays.Thursday, "IsWeeklyThursday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Friday) == WeekDays.Friday, "IsWeeklyFriday");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void EveryDayExample3()
		{
			string rule = "FREQ=WEEKLY; BYDAY=MO, TU, WE, TH, FR; UNTIL=20170715";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "WeeklyEveryNWeeks");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Monday) == WeekDays.Monday, "IsWeeklyMonday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Tuesday) == WeekDays.Tuesday, "IsWeeklyTuesday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Wednesday) == WeekDays.Wednesday, "IsWeeklyWednesday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Thursday) == WeekDays.Thursday, "IsWeeklyThursday");
            Assert.AreEqual(true, (props.WeekDays & WeekDays.Friday) == WeekDays.Friday, "IsWeeklyFriday");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.EndDate, "IsRangeEndDate");
			Assert.AreEqual(new DateTime(2017, 07, 15, 23, 59, 59), props.EndDate, "RangeEndDate");
		}

		// MONTHLY Tests
		// -------------
		[Test()]
		public void MonthlyExample1()
		{
			string rule = "FREQ=MONTHLY; BYMONTHDAY=15; INTERVAL=1";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "MonthlyEveryNMonths ");
			Assert.AreEqual(true, props.IsMonthlySpecific, "IsMonthlySpecific");
			Assert.AreEqual(15, props.DayOfMonth, "MonthlySpecificMonthDay");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.NoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void MonthlyExample2()
		{
			string rule = "FREQ=MONTHLY; BYMONTHDAY=16; INTERVAL=1; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "MonthlyEveryNMonths ");
			Assert.AreEqual(true, props.IsMonthlySpecific, "IsMonthlySpecific");
			Assert.AreEqual(16, props.DayOfMonth, "MonthlySpecificMonthDay");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void MonthlyExample3()
		{
			string rule = "FREQ=MONTHLY; BYMONTHDAY=16; INTERVAL=1; UNTIL=20180611";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "MonthlyEveryNMonths ");
			Assert.AreEqual(true, props.IsMonthlySpecific, "IsMonthlySpecific");
			Assert.AreEqual(16, props.DayOfMonth, "MonthlySpecificMonthDay");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.EndDate, "IsRangeEndDate");
            Assert.AreEqual(new DateTime(2018, 06, 11, 23, 59, 59), props.EndDate, "RangeEndDate");
		}

		[Test()]
		public void MonthlyExample4()
		{
			string rule = "FREQ=MONTHLY; BYDAY=FR; BYSETPOS=2; INTERVAL=1";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "MonthlyEveryNMonths ");
			Assert.AreEqual(2, props.Week, "MonthlyNthWeek");
			Assert.AreEqual(5, props.DayOfWeek, "MonthlyWeekDay");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.NoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void MonthlyExample5()
		{
			string rule = "FREQ=MONTHLY; BYDAY=WE; BYSETPOS=4; INTERVAL=1; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "MonthlyEveryNMonths ");
			Assert.AreEqual(4, props.Week, "MonthlyNthWeek");
			Assert.AreEqual(3, props.DayOfWeek, "MonthlyWeekDay");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void MonthlyExample6()
		{
			string rule = "FREQ=MONTHLY; BYDAY=FR; BYSETPOS=4; INTERVAL=1; UNTIL=20180611";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.Interval, "MonthlyEveryNMonths ");
			Assert.AreEqual(4, props.Week, "MonthlyNthWeek");
			Assert.AreEqual(5, props.DayOfWeek, "MonthlyWeekDay");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.EndDate, "IsRangeEndDate");
            Assert.AreEqual(new DateTime(2018, 06, 11, 23, 59, 59), props.EndDate, "RangeEndDate");
		}

		// YEARLY Tests
		// -------------
		[Test()]
		public void YearlyExample1()
		{
			string rule = "FREQ=YEARLY; BYMONTHDAY=15; BYMONTH=12; INTERVAL=1";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Yearly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(true, props.IsYearlySpecific, "IsYearlySpecific ");
			Assert.AreEqual(1, props.Interval, "YearlyEveryNYears");
			Assert.AreEqual(12, props.Month, "YearlySpecificMonth");
			Assert.AreEqual(15, props.DayOfMonth, "YearlySpecificMonthDay");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.NoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void YearlyExample2()
		{
			string rule = "FREQ=YEARLY; BYMONTHDAY=10; BYMONTH=12; INTERVAL=1; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Yearly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(true, props.IsYearlySpecific, "IsYearlySpecific ");
			Assert.AreEqual(1, props.Interval, "YearlyEveryNYears");
			Assert.AreEqual(12, props.Month, "YearlySpecificMonth");
            Assert.AreEqual(10, props.DayOfMonth, "YearlySpecificMonthDay");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void YearlyExample3()
		{
			string rule = "FREQ=YEARLY; BYMONTHDAY=12; BYMONTH=12; INTERVAL=1; UNTIL=20180611";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Yearly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(true, props.IsYearlySpecific, "IsYearlySpecific ");
			Assert.AreEqual(1, props.Interval, "YearlyEveryNYears");
			Assert.AreEqual(12, props.Month, "YearlySpecificMonth");
			Assert.AreEqual(12, props.DayOfMonth, "YearlySpecificMonthDay");
			Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.EndDate, "IsRangeEndDate");
            Assert.AreEqual(new DateTime(2018, 06, 11, 23, 59, 59), props.EndDate, "RangeEndDate");
		}   

		[Test()]
        public void BuggyRule1()
        {
			string rule = "FREQ=DAILY;INTERVAL=COUNT=10;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            Assert.AreEqual(true, converter.HasError);
            StringAssert.Contains("INTERVAL has non valid value ", converter.ErrorMessage);
            Assert.AreEqual(null, props);
        }


        [Test()]
        public void BuggyRuleIncomplete()
        {
            string rule = "FREQ=WEEKLY;INTERVAL=1;COUNT=10;";
            DateTime startDate = new DateTime(2018, 05, 23, 11, 30, 00);

            RecurrenceProperties props = converter.Convert(rule, startDate);

            Assert.AreEqual(true, converter.HasError);
            Assert.AreEqual("BYDAY should be set for Weekly", converter.ErrorMessage);
        }
	}
}
