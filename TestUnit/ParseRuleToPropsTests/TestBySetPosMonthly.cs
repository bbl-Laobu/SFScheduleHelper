using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParseRuleToPropsTests
{
    [TestFixture()]
    public class TestBySetPosMonthly
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
			string rule = "FREQ=MONTHLY;BYSETPOS;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", parser.ErrorMessage);
        }

		[Test()]
		public void Invalid()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", parser.ErrorMessage);
        }
        
		[Test()]
        public void InValid0()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=0;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", parser.ErrorMessage);
        }  

		[Test()]
        public void InValid53()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=53;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", parser.ErrorMessage);
        }  

		[Test()]
        public void InValidNegative1()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=-1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", parser.ErrorMessage);
        }

		[Test()]
        public void InValidNegative()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=-31;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYSETPOS has non valid value ", parser.ErrorMessage);
        }

        [Test()]
        public void InValidMoByDay()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYDAY or BYMONTHDAY should be set for Monthly", parser.ErrorMessage);
        }

		[Test()]
		public void Valid1()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=1;BYDAY=SU;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(1, props.MonthlyNthWeek);
            Assert.AreEqual(0, props.MonthlyWeekDay);
        }   

		[Test()]
        public void Valid26()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=2;BYDAY=SA;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(2, props.MonthlyNthWeek);
            Assert.AreEqual(6, props.MonthlyWeekDay);
        }   

		[Test()]
        public void Valid43()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=4;BYDAY=WE;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(4, props.MonthlyNthWeek);
            Assert.AreEqual(3, props.MonthlyWeekDay);
        } 
    }
}
