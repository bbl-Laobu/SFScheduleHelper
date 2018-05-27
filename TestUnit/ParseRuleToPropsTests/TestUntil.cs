using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParseRuleToPropsTests
{
    [TestFixture()]
    public class TestUntil
    {
        ParseRuleToProps parser;

        [SetUp]
        public void Init()
        {
            parser = new ParseRuleToProps();
        }

		[Test()]
		public void InValidEmpty()
        {
			string rule = "FREQ=WEEKLY;UNTIL;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("UNTIL has non valid value ", parser.ErrorMessage);
        }
             
		[Test()]
        public void Invalid()
        {
			string rule = "FREQ=WEEKLY;UNTIL=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("UNTIL has non valid value ", parser.ErrorMessage);
        }

		[Test()]
        public void InvalidDate()
        {
            string rule = "FREQ=WEEKLY;UNTIL=15/33/2018";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("UNTIL has non valid value ", parser.ErrorMessage);
        }
        
		[Test()]
        public void ValidDaily()
        {   
			string rule = "FREQ=DAILY;UNTIL=10/15/2018;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			DateTime expectedDate = new DateTime(2018, 10, 15,23,59,59);
			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType);
			Assert.AreEqual(1, props.DailyNDays);
			Assert.AreEqual(true, props.IsRangeEndDate);
			Assert.AreEqual(expectedDate.Ticks, props.RangeEndDate.Ticks);
        }   

		[Test()]
        public void ValidWeekly()
        {   
			string rule = "FREQ=Weekly;INTERVAL=2;COUNT=4;UNTIL=12/31/2018;BYDAY=WE";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			DateTime expectedDate = new DateTime(2018, 12, 31, 23, 59, 59);
			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType);
            Assert.AreEqual(2, props.WeeklyEveryNWeeks);
            Assert.AreEqual(true, props.IsRangeRecurrenceCount);
            Assert.AreEqual(4, props.RangeRecurrenceCount);
            Assert.AreEqual(true, props.IsRangeEndDate);
            Assert.AreEqual(expectedDate.Ticks, props.RangeEndDate.Ticks);
        } 

		[Test()]
        public void ValidNoUntil()
        {
            string rule = "FREQ=WEEKLY;BYDAY=MO";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(false, props.IsRangeEndDate);
            Assert.AreEqual(true, props.IsRangeNoEndDate);
        }


    }
}
