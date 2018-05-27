using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
	public class ByMonthTests
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
            string rule = "FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=3;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTHDAY=3;BYMONTH=1;", ruleResult);
        }   

		[Test()]
        public void Valid6()
        {
            string rule = "FREQ=YEARLY;BYMONTH=6;BYMONTHDAY=5;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTHDAY=5;BYMONTH=6;", ruleResult);
        }   

		[Test()]
        public void Valid12()
        {
            string rule = "FREQ=YEARLY;BYMONTH=12;BYMONTHDAY=24;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTHDAY=24;BYMONTH=12;", ruleResult);
        } 
    }
}
