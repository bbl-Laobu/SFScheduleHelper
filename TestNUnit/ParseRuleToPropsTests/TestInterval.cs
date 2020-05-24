using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestNUnit.ParseRuleToPropsTests
{
    [TestFixture()]
    public class TestInterval
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
			string rule = "FREQ=WEEKLY;INTERVAL;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("INTERVAL has non valid value ", parser.ErrorMessage);
        }

		[Test()]
        public void Invalid()
        {
			string rule = "FREQ=WEEKLY;INTERVAL=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("INTERVAL has non valid value ", parser.ErrorMessage);
        }

		[Test()]
        public void InvalidNegative1()
        {
            string rule = "FREQ=DAily;INTERVAL=-1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("INTERVAL has non valid value ", parser.ErrorMessage);
        } 
        
		[Test()]
        public void ValidDaily1()
        {
			string rule = "FREQ=DAILY;INTERVAL=1;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType);
			Assert.AreEqual(true, props.IsDailyEveryNDays);
			Assert.AreEqual(1, props.Interval);
        }      

		[Test()]
        public void ValidWeekly2()
        {
            string rule = "FREQ=Weekly;INTERVAL=2;BYDAY=TH;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType);
			Assert.AreEqual(2, props.Interval);
        }

		[Test()]
        public void ValidMonthly3()
        {
            string rule = "FREQ=monthly;INTERVAL=3;BYMONTHDAY=15;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType);
			Assert.AreEqual(3, props.Interval);
        } 

		[Test()]
        public void ValidYearly4()
        {
            string rule = "FREQ=YEARLY;INTERVAL=4;ByMonth=3;ByDay=FR;BySetPos=3;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(RecurrenceType.Yearly, props.RecurrenceType);
            Assert.AreEqual(4, props.Interval);
        } 

		[Test()]
        public void NoIntervalWeekly()
        {
            string rule = "FREQ=Weekly;BYDAY=MO,TU,WE;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType);
			Assert.AreEqual(1, props.Interval);
        } 

        [Test()]
        public void NoIntervalDaily()
        {
            string rule = "FREQ=DAily;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType);
			Assert.AreEqual(true, props.IsDailyEveryNDays);
            Assert.AreEqual(1, props.Interval);
        } 
       
    }
}
