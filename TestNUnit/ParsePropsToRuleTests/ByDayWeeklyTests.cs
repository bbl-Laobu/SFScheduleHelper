using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestNUnit.ParsePropsToRuleTests
{
    [TestFixture()]
	public class ByDayWeeklyTests
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
			string rule = "FREQ=WEEKLY;BYDAY=su";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=SU;", ruleResult);
        }   

		[Test()]
		public void ValidMonday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=mo";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO;", ruleResult);
        }  

		[Test()]
		public void ValidTuesday()
        {   
            string rule = "FREQ=WEEKLY;byday=TU";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=TU;", ruleResult);
        }  

		[Test()]
		public void ValidWednesday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=wE";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=WE;", ruleResult);
        }  

		[Test()]
		public void ValidThursday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=Th";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=TH;", ruleResult);
        }  

		[Test()]
		public void ValidFriday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=FR;", ruleResult);
        }  

		[Test()]
        public void ValidSaturday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=SA";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=SA;", ruleResult);
        }  

		[Test()]
        public void Valid_MOWEFR()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=MO,WE,FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,WE,FR;", ruleResult);
        } 

		[Test()]
        public void Valid_MOWEFR_x2()
        {   
			string rule = "FREQ=WEEKLY;BYDAY=MO,WE,FR,MO,WE,FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,WE,FR;", ruleResult);
        } 

		[Test()]
        public void Valid_AllWeek()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=MO,WE,FR,SU,SA,TU,TH";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
            
			string ruleResult = converter.Convert(props);

			Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=SU,MO,TU,WE,TH,FR,SA;", ruleResult);
        }      
    }
}
