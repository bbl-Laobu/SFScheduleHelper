using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
    public class IntervalTests
    {
		[Test()]
        public void ValidDaily1()
        {
			string rule = "FREQ=DAILY;INTERVAL=1;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
         
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=DAILY;INTERVAL=1;", ruleResult); 
        }      

		[Test()]
        public void ValidWeekly2()
        {
            string rule = "FREQ=Weekly;INTERVAL=2;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
         
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=2;", ruleResult);
        }

		[Test()]
        public void ValidMonthly3()
        {
            string rule = "FREQ=monthly;INTERVAL=3;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
         
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=3;", ruleResult);
        } 

		[Test()]
        public void ValidYearly4()
        {
            string rule = "FREQ=YEARLY;INTERVAL=4;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
         
			string ruleResult = RecurrenceConverter.Convert(props);

			Assert.AreEqual("FREQ=YEARLY;INTERVAL=4;", ruleResult);
        } 

		[Test()]
        public void NoIntervalWeekly()
        {
            string rule = "FREQ=Weekly;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
         
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;", ruleResult);
        } 

        [Test()]
        public void NoIntervalDaily()
        {
            string rule = "FREQ=DAily;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
         
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=DAILY;INTERVAL=1;", ruleResult); 
        } 
       
    }
}
