using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParseRuleToPropsTests
{
    [TestFixture()]
    public class TestByMonthDayMonthly
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
			string rule = "FREQ=MONTHLY;BYMONTHDAY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }

		[Test()]
		public void InValid()
        {
			string rule = "FREQ=MONTHLY;BYMONTHDAY=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }
        
		[Test()]
        public void InValid0()
        {
			string rule = "FREQ=MONTHLY;BYMONTHDAY=0;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }  

		[Test()]
        public void InValid32()
        {
			string rule = "FREQ=MONTHLY;BYMONTHDAY=32;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }  

		[Test()]
        public void InValiNegative1()
        {
			string rule = "FREQ=MONTHLY;BYMONTHDAY=-1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }

		[Test()]
        public void InValiNegative()
        {
			string rule = "FREQ=MONTHLY;BYMONTHDAY=-31;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }

		[Test()]
		public void Valid1()
        {
			string rule = "FREQ=MONTHLY;BYMONTHDAY=1;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(true, props.IsMonthlySpecific);
			Assert.AreEqual(1, props.MonthlySpecificMonthDay);
        }   

		[Test()]
        public void Valid15()
        {
			string rule = "FREQ=MONTHLY;BYMONTHDAY=15;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(true, props.IsMonthlySpecific);
            Assert.AreEqual(15, props.MonthlySpecificMonthDay);
        }   

		[Test()]
        public void Valid31()
        {
            string rule = "FREQ=MONTHLY;BYMONTHDAY=31;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(true, props.IsMonthlySpecific);
            Assert.AreEqual(31, props.MonthlySpecificMonthDay);
        } 
         
        [Test()]
		public void NoBYMONTHDAY()
        {
			string rule = "FREQ=MONTHLY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(true, parser.HasError);
            Assert.AreEqual("BYDAY or BYMONTHDAY should be set for Monthly", parser.ErrorMessage);
        } 
        
    }
}
