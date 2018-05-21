using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit
{
    [TestFixture()]
    public class TestBySetPosMonthly
    {
		[Test()]
		public void InValidEmpty()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);

			Assert.AreEqual(true, RecurrenceConverter.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", RecurrenceConverter.ErrorMessage);
        }

		[Test()]
		public void Invalid()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);

			Assert.AreEqual(true, RecurrenceConverter.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", RecurrenceConverter.ErrorMessage);
        }
        
		[Test()]
        public void InValid0()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=0;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);

            Assert.AreEqual(true, RecurrenceConverter.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", RecurrenceConverter.ErrorMessage);
        }  

		[Test()]
        public void InValid53()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=53;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);

            Assert.AreEqual(true, RecurrenceConverter.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", RecurrenceConverter.ErrorMessage);
        }  

		[Test()]
        public void InValiNegative1()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=-1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);

            Assert.AreEqual(true, RecurrenceConverter.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", RecurrenceConverter.ErrorMessage);
        }

		[Test()]
        public void InValiNegative()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=-31;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);

            Assert.AreEqual(true, RecurrenceConverter.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", RecurrenceConverter.ErrorMessage);
        }

		[Test()]
		public void Valid1()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=1;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);
         
			Assert.AreEqual(1, props.MonthlyNthWeek);
        }   

		[Test()]
        public void Valid26()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=26;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);
         
			Assert.AreEqual(26, props.MonthlyNthWeek);
        }   

		[Test()]
        public void Valid52()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=52;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);
         
			Assert.AreEqual(52, props.MonthlyNthWeek);
        } 
         
        [Test()]
		public void NoBySetPos()
        {
			string rule = "FREQ=MONTHLY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RecurrenceConverter.Convert(rule, startDate);
         
			Assert.AreEqual(0, props.MonthlyNthWeek);
        } 
        
    }
}
