using Kareke.SFScheduleHelper;
using NUnit.Framework;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParsePropsToRuleTests
{
    [TestFixture()]
	public class ByDayMonthlyTests
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
        public void ValidSunday()
        {
            string rule = "FREQ=MONTHLY;BYDAY=su;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=SU;BYSETPOS=1;", ruleResult);
        }

        [Test()]
        public void ValidMonday()
        {
            string rule = "FREQ=MONTHLY;BYDAY=mo;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=MO;BYSETPOS=1;", ruleResult);
        }

        [Test()]
        public void ValidTuesday()
        {
            string rule = "FREQ=MONTHLY;byday=TU;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=TU;BYSETPOS=1;", ruleResult);
        }

        [Test()]
        public void ValidWednesday()
        {
            string rule = "FREQ=MONTHLY;BYDAY=wE;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=WE;BYSETPOS=1;", ruleResult);
        }

        [Test()]
        public void ValidThursday()
        {
            string rule = "FREQ=MONTHLY;BYDAY=Th;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=TH;BYSETPOS=1;", ruleResult);
        }

        [Test()]
        public void ValidFriday()
        {
            string rule = "FREQ=MONTHLY;BYDAY=FR;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=FR;BYSETPOS=1;", ruleResult);
        }

        [Test()]
        public void ValidSaturday()
        {
            string rule = "FREQ=MONTHLY;BYDAY=SA;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=SA;BYSETPOS=1;", ruleResult);
        }

        [Test()]
        public void Valid_MOWEFR()
        {
            string rule = "FREQ=MONTHLY;BYDAY=WE,MO,FR;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=WE;BYSETPOS=1;", ruleResult);
        }

        [Test()]
        public void Valid_AllWeek()
        {
            string rule = "FREQ=MONTHLY;BYDAY=MO,WE,FR,SU,SA,TU,TH;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=MO;BYSETPOS=1;", ruleResult);
        }
    }
}
