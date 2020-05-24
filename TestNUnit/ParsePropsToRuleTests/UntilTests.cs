using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestNUnit.ParsePropsToRuleTests
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
			string rule = "FREQ=DAILY;UNTIL=20181015;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=DAILY;INTERVAL=1;UNTIL=20181015;", ruleResult); 
        }   

		[Test()]
        public void ValidWeekly()
        {   
            string rule = "FREQ=Weekly;INTERVAL=2;COUNT=4;UNTIL=20181231;BYDAY=WE;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=2;UNTIL=20181231;BYDAY=WE;", ruleResult);
        }      
    }
}
