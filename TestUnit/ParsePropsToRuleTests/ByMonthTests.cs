using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
	public class ParsePropsToRuleTests
    {      
		[Test()]
		public void Valid1()
        {
			string rule = "FREQ=YEARLY;BYMONTH=1;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
         
			string ruleResult = RecurrenceConverter.Convert(props);

			Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTH=1;", ruleResult);
        }   

		[Test()]
        public void Valid6()
        {
			string rule = "FREQ=YEARLY;BYMONTH=6;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);

			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTH=6;", ruleResult);
        }   

		[Test()]
        public void Valid12()
        {
			string rule = "FREQ=YEARLY;BYMONTH=12;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
         
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=YEARLY;INTERVAL=1;BYMONTH=12;", ruleResult);
        } 
    }
}
