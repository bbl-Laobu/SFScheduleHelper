using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.RecurrenceConverterTests
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
			Assert.AreEqual(1, props.DailyNDays, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
			Assert.AreEqual(true, props.IsRangeNoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void Weekly()
		{
			string rule = "FREQ=Weekly;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.WeeklyEveryNWeeks, "WeeklyEveryNWeeks");
			//Assert.AreEqual(true, props.IsWeeklySaturday, "IsWeeklySaturday");
			Assert.AreEqual(true, props.IsRangeNoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void Monthly()
		{
			string rule = "FREQ=Monthly;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.MonthlyEveryNMonths, "MonthlyEveryNMonths ");
			//Assert.AreEqual(true, props.IsMonthlySpecific, "IsMonthlySpecific");
			//Assert.AreEqual(1, props.MonthlySpecificMonthDay, "MonthlySpecificMonthDay");
			Assert.AreEqual(true, props.IsRangeNoEndDate, "IsRangeNoEndDate");
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
			Assert.AreEqual(1, props.DailyNDays, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
			Assert.AreEqual(true, props.IsRangeNoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void DailyExample2()
		{
			string rule = "FREQ=DAILY; INTERVAL=1; COUNT=5";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.DailyNDays, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
			Assert.AreEqual(true, props.IsRangeRecurrenceCount, "IsRangeRecurrenceCount");
			Assert.AreEqual(5, props.RangeRecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void DailyExample3()
		{
			string rule = "FREQ=DAILY; INTERVAL=1; UNTIL=06/20/2017";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);


			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.DailyNDays, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
			Assert.AreEqual(true, props.IsRangeEndDate, "IsRangeEndDate");
			Assert.AreEqual(new DateTime(2017, 06, 20, 23, 59, 59), props.RangeEndDate, "RangeEndDate");
		}

		[Test()]
		public void DailyExample4()
		{
			string rule = "FREQ=DAILY; INTERVAL=2; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(2, props.DailyNDays, "DailyNDays");
			Assert.AreEqual(true, props.IsDailyEveryNDays, "IsDailyEveryNDays");
			Assert.AreEqual(true, props.IsRangeRecurrenceCount, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RangeRecurrenceCount, "RangeRecurrenceCount");
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
			Assert.AreEqual(1, props.WeeklyEveryNWeeks, "WeeklyEveryNWeeks");
			Assert.AreEqual(true, props.IsWeeklyMonday, "IsWeeklyMonday");
			Assert.AreEqual(true, props.IsWeeklyWednesday, "IsWeeklyWednesday");
			Assert.AreEqual(true, props.IsWeeklyFriday, "IsWeeklyFriday");
			Assert.AreEqual(true, props.IsRangeNoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void WeeklyExample2()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=1; BYDAY=TH; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.WeeklyEveryNWeeks, "WeeklyEveryNWeeks");
			Assert.AreEqual(true, props.IsWeeklyThursday, "IsWeeklyThursday");
			Assert.AreEqual(true, props.IsRangeRecurrenceCount, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RangeRecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void WeeklyExample3()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=1; BYDAY=MO; UNTIL=07/20/2017";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.WeeklyEveryNWeeks, "WeeklyEveryNWeeks");
			Assert.AreEqual(true, props.IsWeeklyMonday, "IsWeeklyMonday");
			Assert.AreEqual(true, props.IsRangeEndDate, "IsRangeEndDate");
            Assert.AreEqual(new DateTime(2017, 07, 20, 23, 59, 59), props.RangeEndDate, "RangeEndDate ");
		}

		[Test()]
		public void WeeklyExample4()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=2; BYDAY=MO, WE, FR; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(2, props.WeeklyEveryNWeeks, "WeeklyEveryNWeeks");
			Assert.AreEqual(true, props.IsWeeklyMonday, "IsWeeklyMonday");
			Assert.AreEqual(true, props.IsWeeklyWednesday, "IsWeeklyWednesday");
			Assert.AreEqual(true, props.IsWeeklyFriday, "IsWeeklyFriday ");
			Assert.AreEqual(true, props.IsRangeRecurrenceCount, "IsRangeRecurrenceCount ");
			Assert.AreEqual(10, props.RangeRecurrenceCount, "RangeRecurrenceCount ");
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
			Assert.AreEqual(1, props.WeeklyEveryNWeeks, "WeeklyEveryNWeeks");
			Assert.AreEqual(true, props.IsWeeklyMonday, "IsWeeklyMonday");
			Assert.AreEqual(true, props.IsWeeklyTuesday, "IsWeeklyTuesday");
			Assert.AreEqual(true, props.IsWeeklyWednesday, "IsWeeklyWednesday");
			Assert.AreEqual(true, props.IsWeeklyThursday, "IsWeeklyThursday");
			Assert.AreEqual(true, props.IsWeeklyFriday, "IsWeeklyFriday");
			Assert.AreEqual(true, props.IsRangeNoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void EveryDayExample2()
		{
			string rule = "FREQ=WEEKLY; BYDAY=MO, TU, WE, TH, FR; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.WeeklyEveryNWeeks, "WeeklyEveryNWeeks");
			Assert.AreEqual(true, props.IsWeeklyMonday, "IsWeeklyMonday");
			Assert.AreEqual(true, props.IsWeeklyTuesday, "IsWeeklyTuesday");
			Assert.AreEqual(true, props.IsWeeklyWednesday, "IsWeeklyWednesday");
			Assert.AreEqual(true, props.IsWeeklyThursday, "IsWeeklyThursday");
			Assert.AreEqual(true, props.IsWeeklyFriday, "IsWeeklyFriday");
			Assert.AreEqual(true, props.IsRangeRecurrenceCount, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RangeRecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void EveryDayExample3()
		{
			string rule = "FREQ=WEEKLY; BYDAY=MO, TU, WE, TH, FR; UNTIL=07/15/2017";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.WeeklyEveryNWeeks, "WeeklyEveryNWeeks");
			Assert.AreEqual(true, props.IsWeeklyMonday, "IsWeeklyMonday");
			Assert.AreEqual(true, props.IsWeeklyTuesday, "IsWeeklyTuesday");
			Assert.AreEqual(true, props.IsWeeklyWednesday, "IsWeeklyWednesday");
			Assert.AreEqual(true, props.IsWeeklyThursday, "IsWeeklyThursday");
			Assert.AreEqual(true, props.IsWeeklyFriday, "IsWeeklyFriday");
			Assert.AreEqual(true, props.IsRangeEndDate, "IsRangeEndDate");
			Assert.AreEqual(new DateTime(2017, 07, 15, 23, 59, 59), props.RangeEndDate, "RangeEndDate");
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
			Assert.AreEqual(1, props.MonthlyEveryNMonths, "MonthlyEveryNMonths ");
			Assert.AreEqual(true, props.IsMonthlySpecific, "IsMonthlySpecific");
			Assert.AreEqual(15, props.MonthlySpecificMonthDay, "MonthlySpecificMonthDay");
			Assert.AreEqual(true, props.IsRangeNoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void MonthlyExample2()
		{
			string rule = "FREQ=MONTHLY; BYMONTHDAY=16; INTERVAL=1; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.MonthlyEveryNMonths, "MonthlyEveryNMonths ");
			Assert.AreEqual(true, props.IsMonthlySpecific, "IsMonthlySpecific");
			Assert.AreEqual(16, props.MonthlySpecificMonthDay, "MonthlySpecificMonthDay");
			Assert.AreEqual(true, props.IsRangeRecurrenceCount, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RangeRecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void MonthlyExample3()
		{
			string rule = "FREQ=MONTHLY; BYMONTHDAY=16; INTERVAL=1; UNTIL=06/11/2018";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.MonthlyEveryNMonths, "MonthlyEveryNMonths ");
			Assert.AreEqual(true, props.IsMonthlySpecific, "IsMonthlySpecific");
			Assert.AreEqual(16, props.MonthlySpecificMonthDay, "MonthlySpecificMonthDay");
			Assert.AreEqual(true, props.IsRangeEndDate, "IsRangeEndDate");
            Assert.AreEqual(new DateTime(2018, 06, 11, 23, 59, 59), props.RangeEndDate, "RangeEndDate");
		}

		[Test()]
		public void MonthlyExample4()
		{
			string rule = "FREQ=MONTHLY; BYDAY=FR; BYSETPOS=2; INTERVAL=1";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.MonthlyEveryNMonths, "MonthlyEveryNMonths ");
			Assert.AreEqual(2, props.MonthlyNthWeek, "MonthlyNthWeek");
			Assert.AreEqual(5, props.MonthlyWeekDay, "MonthlyWeekDay");
			Assert.AreEqual(true, props.IsRangeNoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void MonthlyExample5()
		{
			string rule = "FREQ=MONTHLY; BYDAY=WE; BYSETPOS=4; INTERVAL=1; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.MonthlyEveryNMonths, "MonthlyEveryNMonths ");
			Assert.AreEqual(4, props.MonthlyNthWeek, "MonthlyNthWeek");
			Assert.AreEqual(3, props.MonthlyWeekDay, "MonthlyWeekDay");
			Assert.AreEqual(true, props.IsRangeRecurrenceCount, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RangeRecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void MonthlyExample6()
		{
			string rule = "FREQ=MONTHLY; BYDAY=FR; BYSETPOS=4; INTERVAL=1; UNTIL=06/11/2018";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(1, props.MonthlyEveryNMonths, "MonthlyEveryNMonths ");
			Assert.AreEqual(4, props.MonthlyNthWeek, "MonthlyNthWeek");
			Assert.AreEqual(5, props.MonthlyWeekDay, "MonthlyWeekDay");
			Assert.AreEqual(true, props.IsRangeEndDate, "IsRangeEndDate");
            Assert.AreEqual(new DateTime(2018, 06, 11, 23, 59, 59), props.RangeEndDate, "RangeEndDate");
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
			Assert.AreEqual(1, props.YearlyEveryNYears, "YearlyEveryNYears");
			Assert.AreEqual(12, props.YearlySpecificMonth, "YearlySpecificMonth");
			Assert.AreEqual(15, props.YearlySpecificMonthDay, "YearlySpecificMonthDay");
			Assert.AreEqual(true, props.IsRangeNoEndDate, "IsRangeNoEndDate");
		}

		[Test()]
		public void YearlyExample2()
		{
			string rule = "FREQ=YEARLY; BYMONTHDAY=10; BYMONTH=12; INTERVAL=1; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Yearly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(true, props.IsYearlySpecific, "IsYearlySpecific ");
			Assert.AreEqual(1, props.YearlyEveryNYears, "YearlyEveryNYears");
			Assert.AreEqual(12, props.YearlySpecificMonth, "YearlySpecificMonth");
			Assert.AreEqual(10, props.YearlySpecificMonthDay, "YearlySpecificMonthDay");
			Assert.AreEqual(true, props.IsRangeRecurrenceCount, "IsRangeRecurrenceCount");
			Assert.AreEqual(10, props.RangeRecurrenceCount, "RangeRecurrenceCount");
		}

		[Test()]
		public void YearlyExample3()
		{
			string rule = "FREQ=YEARLY; BYMONTHDAY=12; BYMONTH=12; INTERVAL=1; UNTIL=06/11/2018";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Yearly, props.RecurrenceType, "RecurrenceType");
			Assert.AreEqual(true, props.IsYearlySpecific, "IsYearlySpecific ");
			Assert.AreEqual(1, props.YearlyEveryNYears, "YearlyEveryNYears");
			Assert.AreEqual(12, props.YearlySpecificMonth, "YearlySpecificMonth");
			Assert.AreEqual(12, props.YearlySpecificMonthDay, "YearlySpecificMonthDay");
			Assert.AreEqual(true, props.IsRangeEndDate, "IsRangeEndDate");
            Assert.AreEqual(new DateTime(2018, 06, 11, 23, 59, 59), props.RangeEndDate, "RangeEndDate");
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
	}
}
