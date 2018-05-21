using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit
{
    [TestFixture()]
    public class TestCount
    {
		[Test()]
		public void InValidEmpty()
        {
			string rule = "FREQ=WEEKLY;COUNT;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);

			Assert.AreEqual(true, RecurrenceConverter.HasError);
			StringAssert.Contains("COUNT has non valid value ", RecurrenceConverter.ErrorMessage);
        }

		[Test()]
        public void Invalid()
        {
			string rule = "FREQ=WEEKLY;COUNT=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);

			Assert.AreEqual(true, RecurrenceConverter.HasError);
			StringAssert.Contains("COUNT has non valid value ", RecurrenceConverter.ErrorMessage);
        }
        
		[Test()]
        public void ValidCountDaily()
        {
			string rule = "FREQ=DAILY;COUNT=2;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);
         
			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType);
			Assert.AreEqual(1, props.DailyNDays);
			Assert.AreEqual(true, props.IsRangeRecurrenceCount);
			Assert.AreEqual(2, props.RangeRecurrenceCount);
        }   

		[Test()]
        public void ValidCountWeekly()
        {
			string rule = "FREQ=Weekly;INTERVAL=2;COUNT=4;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);
         
            Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType);
            Assert.AreEqual(2, props.WeeklyEveryNWeeks);
            Assert.AreEqual(true, props.IsRangeRecurrenceCount);
            Assert.AreEqual(4, props.RangeRecurrenceCount);
        }   

        [Test()]
        public void NoCount()
        {
            string rule = "FREQ=DAILY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);
         
            Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType);
            Assert.AreEqual(1, props.DailyNDays);
			Assert.AreEqual(false, props.IsRangeRecurrenceCount);
            Assert.AreEqual(1, props.RangeRecurrenceCount);
        } 

		[Test()]
        public void NegativeCountDaily()
        {
			string rule = "FREQ=DAily;COUNT=-1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);
         
			Assert.AreEqual(true, RecurrenceConverter.HasError);
            StringAssert.Contains("COUNT has non valid value ", RecurrenceConverter.ErrorMessage);
        } 
    }
}
