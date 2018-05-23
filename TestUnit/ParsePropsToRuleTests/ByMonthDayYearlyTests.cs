using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
	public class ByMonthDayYearlyTests
    {    
        ParseRuleToProps parser;
        RecurrenceConverter converter;

        [SetUp]
        public void Init()
        {
            parser = new ParseRuleToProps();
            converter = new RecurrenceConverter();
        }

		[Test()]
		public void Valid1()
        {
			string rule = "FREQ=YEARLY;BYMONTHDAY=1;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTHDAY=1;", ruleResult);
        }   

		[Test()]
        public void Valid15()
        {
			string rule = "FREQ=YEARLY;BYMONTHDAY=15;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTHDAY=15;", ruleResult);
        }   

		[Test()]
        public void Valid31()
        {
			string rule = "FREQ=YEARLY;BYMONTHDAY=31;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTHDAY=31;", ruleResult);
        } 
    }
}
