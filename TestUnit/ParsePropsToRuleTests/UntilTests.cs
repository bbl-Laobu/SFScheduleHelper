using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
    public class UntilTests
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
			string rule = "FREQ=DAILY;UNTIL=10/15/2018;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=DAILY;INTERVAL=1;UNTIL=10/15/2018;", ruleResult); 
        }   

		[Test()]
        public void ValidWeekly()
        {   
			string rule = "FREQ=Weekly;INTERVAL=2;COUNT=4;UNTIL=12/31/2018;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=2;COUNT=4;UNTIL=12/31/2018;", ruleResult);
        }      
    }
}
