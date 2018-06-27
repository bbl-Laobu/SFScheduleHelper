using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.RecurrenceConverterTests
{
	[TestFixture()]
	public class PropsToRuleTests
	{  
        RecurrenceConverter converter;

        [SetUp]
        public void Init()
        {
            converter = new RecurrenceConverter();
        }

		[Test()]
		public void EmptyProps()
		{
            string rule = converter.Convert(null);

            Assert.AreEqual(true, converter.HasError);
            Assert.AreEqual("Properties are null", converter.ErrorMessage);
			Assert.AreEqual(string.Empty, rule);
		}

		// Test Methods Just Type
		// ----------------------------------
		[Test()]
		public void Daily()
		{
			string rule = "FREQ=DAILY;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);
            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=DAILY;INTERVAL=1;", ruleResult);
		}

		[Test()]
		public void Weekly()
		{
            string rule = "FREQ=Weekly;BYDAY=SU;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=SU;", ruleResult);
		}

		[Test()]
		public void Monthly()
		{
            string rule = "FREQ=Monthly;BYDAY=WE; BYSETPOS=4;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=WE;BYSETPOS=4;", ruleResult);
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

            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=DAILY;INTERVAL=1;", ruleResult);
		}

		[Test()]
		public void DailyExample2()
		{
			string rule = "FREQ=DAILY; INTERVAL=1; COUNT=5";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=DAILY;INTERVAL=1;COUNT=5;", ruleResult);
		}

		[Test()]
		public void DailyExample3()
		{
			string rule = "FREQ=DAILY; INTERVAL=1; UNTIL=20170601";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=DAILY;INTERVAL=1;UNTIL=20170601;", ruleResult);
		}

		[Test()]
		public void DailyExample4()
		{
			string rule = "FREQ=DAILY; INTERVAL=2; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=DAILY;INTERVAL=2;COUNT=10;", ruleResult);
		}

		// WEEKLY Tests
		// -------------
		[Test()]
		public void WeeklyExample1()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=1; BYDAY=MO, WE, FR";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,WE,FR;", ruleResult);
		}

		[Test()]
		public void WeeklyExample2()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=1; BYDAY=TH; COUNT=10;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;COUNT=10;BYDAY=TH;", ruleResult);
		}

		[Test()]
		public void WeeklyExample3()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=1; BYDAY=MO; UNTIL=20170720";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;UNTIL=20170720;BYDAY=MO;", ruleResult);
		}

		[Test()]
		public void WeeklyExample4()
		{
			string rule = "FREQ=WEEKLY; INTERVAL=2; BYDAY=MO, WE, FR; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=2;COUNT=10;BYDAY=MO,WE,FR;", ruleResult);
		}

		// Every Day Tests
		// -------------
		[Test()]
		public void EveryDayExample1()
		{
			string rule = "FREQ=WEEKLY; BYDAY=MO, TU, WE, TH, FR";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU,WE,TH,FR;", ruleResult);
		}

		[Test()]
		public void EveryDayExample2()
		{
			string rule = "FREQ=WEEKLY; BYDAY=MO, TU, WE, TH, FR; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;COUNT=10;BYDAY=MO,TU,WE,TH,FR;", ruleResult);
		}

		[Test()]
		public void EveryDayExample3()
		{
			string rule = "FREQ=WEEKLY; BYDAY=MO, TU, WE, TH, FR; UNTIL=20170715";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;UNTIL=20170715;BYDAY=MO,TU,WE,TH,FR;", ruleResult);
		}

		// MONTHLY Tests
		// -------------
		[Test()]
		public void MonthlyExample1()
		{
			string rule = "FREQ=MONTHLY; BYMONTHDAY=15; INTERVAL=1";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15;", ruleResult);
		}

		[Test()]
		public void MonthlyExample2()
		{
			string rule = "FREQ=MONTHLY; BYMONTHDAY=16; INTERVAL=1; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;COUNT=10;BYMONTHDAY=16;", ruleResult); 
		}

		[Test()]
		public void MonthlyExample3()
		{
			string rule = "FREQ=MONTHLY; BYMONTHDAY=16; INTERVAL=1; UNTIL=20180611";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;UNTIL=20180611;BYMONTHDAY=16;", ruleResult); 
		}

		[Test()]
		public void MonthlyExample4()
		{
			string rule = "FREQ=MONTHLY; BYDAY=FR; BYSETPOS=2; INTERVAL=1";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=FR;BYSETPOS=2;", ruleResult); 
		}

		[Test()]
		public void MonthlyExample5()
		{
			string rule = "FREQ=MONTHLY; BYDAY=WE; BYSETPOS=4; INTERVAL=1; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;COUNT=10;BYDAY=WE;BYSETPOS=4;", ruleResult); 
		}

		[Test()]
		public void MonthlyExample6()
		{
			string rule = "FREQ=MONTHLY; BYDAY=FR; BYSETPOS=4; INTERVAL=1; UNTIL=20180611";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;UNTIL=20180611;BYDAY=FR;BYSETPOS=4;", ruleResult);
		}

		// YEARLY Tests
		// -------------
		[Test()]
		public void YearlyExample1()
		{
			string rule = "FREQ=YEARLY; BYMONTHDAY=15; BYMONTH=12; INTERVAL=1";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTHDAY=15;BYMONTH=12;", ruleResult);
		}

		[Test()]
		public void YearlyExample2()
		{
			string rule = "FREQ=YEARLY; BYMONTHDAY=10; BYMONTH=12; INTERVAL=1; COUNT=10";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;COUNT=10;BYMONTHDAY=10;BYMONTH=12;", ruleResult);
		}

		[Test()]
		public void YearlyExample3()
		{
			string rule = "FREQ=YEARLY; BYMONTHDAY=12; BYMONTH=12; INTERVAL=1; UNTIL=20180611";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = converter.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;UNTIL=20180611;BYMONTHDAY=12;BYMONTH=12;", ruleResult);
		}      
	}
}
