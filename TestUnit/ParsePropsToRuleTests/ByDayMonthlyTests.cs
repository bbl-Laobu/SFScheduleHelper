using Kareke.SFScheduleHelper;
using NUnit.Framework;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
	public class ByDayMonthlyTests
	{      
        [Test()]
        public void ValidSunday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=su";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			string ruleResult = RecurrenceConverter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=SU;", ruleResult);
        }

        [Test()]
        public void ValidMonday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=mo";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=MO;", ruleResult);
        }

        [Test()]
        public void ValidTuesday()
        {
			string rule = "FREQ=MONTHLY;byday=TU";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=TU;", ruleResult);
        }

        [Test()]
        public void ValidWednesday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=wE";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=WE;", ruleResult);
        }

        [Test()]
        public void ValidThursday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=Th";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=TH;", ruleResult);
        }

        [Test()]
        public void ValidFriday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			string ruleResult = RecurrenceConverter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=FR;", ruleResult);
        }

        [Test()]
        public void ValidSaturday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=SA";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			string ruleResult = RecurrenceConverter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=SA;", ruleResult);
        }

        [Test()]
        public void Valid_MOWEFR()
        {
			string rule = "FREQ=MONTHLY;BYDAY=WE,MO,FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			string ruleResult = RecurrenceConverter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=WE;", ruleResult);
        }

        [Test()]
        public void Valid_AllWeek()
        {
			string rule = "FREQ=MONTHLY;BYDAY=MO,WE,FR,SU,SA,TU,TH";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			string ruleResult = RecurrenceConverter.Convert(props);

			Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=MO;", ruleResult);
        }
    }
}
