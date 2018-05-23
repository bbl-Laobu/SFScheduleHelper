using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
    public class CountTests
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
        public void ValidCountDaily()
        {
			string rule = "FREQ=DAILY;COUNT=2;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=DAILY;INTERVAL=1;COUNT=2;", ruleResult); 
        }   

		[Test()]
        public void ValidCountWeekly()
        {
			string rule = "FREQ=Weekly;INTERVAL=2;COUNT=4;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=2;COUNT=4;", ruleResult); 
        }   
    }
}
