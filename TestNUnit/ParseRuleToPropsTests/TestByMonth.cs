using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestNUnit.ParseRuleToPropsTests
{
    [TestFixture()]
    public class TestByMonth
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
			string rule = "FREQ=YEARLY;BYMONTH;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYMONTH has non valid value ", parser.ErrorMessage);
        }

		[Test()]
		public void InValid()
        {
			string rule = "FREQ=YEARLY;BYMONTH=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYMONTH has non valid value ", parser.ErrorMessage);
        }
        
		[Test()]
        public void InValid0()
        {
			string rule = "FREQ=YEARLY;BYMONTH=0;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYMONTH has non valid value ", parser.ErrorMessage);
        }  

		[Test()]
        public void InValid13()
        {
			string rule = "FREQ=YEARLY;BYMONTH=13;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYMONTH has non valid value ", parser.ErrorMessage);
        }  

		[Test()]
        public void InValiNegative1()
        {
			string rule = "FREQ=YEARLY;BYMONTH=-1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYMONTH has non valid value ", parser.ErrorMessage);
        }

		[Test()]
        public void InValiNegative()
        {
			string rule = "FREQ=YEARLY;BYMONTH=-12;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYMONTH has non valid value ", parser.ErrorMessage);
        }

		[Test()]
		public void Valid1()
        {
            string rule = "FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=15;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(true, props.IsYearlySpecific);
			Assert.AreEqual(1, props.Month);
        }   

		[Test()]
        public void Valid6()
        {
            string rule = "FREQ=YEARLY;BYMONTH=6;BYDAY=SU; BYSETPOS=2;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, props.IsYearlySpecific);
            Assert.AreEqual(6, props.Month);
        }   

		[Test()]
        public void Valid12()
        {
            string rule = "FREQ=YEARLY;BYMONTH=12;BYDAY=FR; BYSETPOS=2;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(true, props.IsYearlySpecific);
            Assert.AreEqual(12, props.Month);
        } 
         
        [Test()]
		public void NoByMonth()
        {
			string rule = "FREQ=YEARLY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(true, parser.HasError);
            Assert.AreEqual("BYMONTH should be set for Yearly", parser.ErrorMessage);
        } 
        
    }
}
