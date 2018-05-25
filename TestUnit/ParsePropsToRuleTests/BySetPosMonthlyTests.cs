using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
	public class BySetPosMonthlyTests
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
			string rule = "FREQ=MONTHLY;BYSETPOS=1;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=SU;BYSETPOS=1;", ruleResult);
        }   

		[Test()]
        public void Valid26()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=26;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=SU;BYSETPOS=26;", ruleResult);
        }   

		[Test()]
        public void Valid52()
        {
			string rule = "FREQ=MONTHLY;BYSETPOS=52;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=SU;BYSETPOS=52;", ruleResult);
        }  
    }
}
