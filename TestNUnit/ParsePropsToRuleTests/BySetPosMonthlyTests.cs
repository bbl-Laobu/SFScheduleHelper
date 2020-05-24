using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestNUnit.ParsePropsToRuleTests
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
			string rule = "FREQ=MONTHLY;BYSETPOS=1;BYDAY=MO,WE,FR";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=MO;BYSETPOS=1;", ruleResult);
        }   

		[Test()]
        public void Valid2()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=2;BYDAY=SU;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=SU;BYSETPOS=2;", ruleResult);
        }   

		[Test()]
        public void Valid3()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=3;BYDAY=WE;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=WE;BYSETPOS=3;", ruleResult);
        }  

        [Test()]
        public void Valid4()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=4;BYDAY=FR;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
         
            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=FR;BYSETPOS=4;", ruleResult);
        } 

        [Test()]
        public void Valid5()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=5;BYDAY=SU;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
         
            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=1;BYDAY=SU;BYSETPOS=5;", ruleResult);
        } 
    }
}
