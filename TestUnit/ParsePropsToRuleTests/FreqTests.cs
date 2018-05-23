using Kareke.SFScheduleHelper;
using NUnit.Framework;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
    public class FreqTests
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
        public void ValidDaily()
        {
			string rule = "FREQ=DAILY;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=DAILY;INTERVAL=1;", ruleResult); 
        }

		[Test()]
		public void ValidWeekly()
        {
			string rule = "FREQ=WEEKLY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;", ruleResult);
        }

		[Test()]
        public void ValidMonthly()
        {
            string rule = "FREQ=monthly;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;", ruleResult);
        }

		[Test()]
        public void ValidYearly()
        {
            string rule = "freq=yearly;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;", ruleResult);
        }
    }
}
