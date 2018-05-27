using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParseRuleToPropsTests
{
    [TestFixture()]
	public class TestByDayWeekly
    {
        ParseRuleToProps parser;

        [SetUp]
        public void Init()
        {
            parser = new ParseRuleToProps();
        }

		[Test()]
		public void InValidEmpty()
        {
			string rule = "FREQ=WEEKLY;BYDAY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYDAY has non valid value ", parser.ErrorMessage);
        }

		[Test()]
		public void InValidNoDay()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYDAY has non valid value ", parser.ErrorMessage);
        }  
        
		[Test()]
        public void Invalid()
        {
			string rule = "FREQ=WEEKLY;BYDAY=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYDAY has non valid value ", parser.ErrorMessage);
        }

		[Test()]
        public void InvalidWeekDay()
        {
			string rule = "FREQ=WEEKLY;BYDAY=MA";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYDAY has non valid value ", parser.ErrorMessage);
        }
        
		[Test()]
        public void NoByDay()
        {
            string rule = "FREQ=WEEKLY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(true, parser.HasError);
            Assert.AreEqual("BYDAY should be set for Weekly", parser.ErrorMessage);
        }

		[Test()]
		public void ValidSunday()
        {   
			string rule = "FREQ=WEEKLY;BYDAY=su";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(true, props.IsWeeklySunday);
            Assert.AreEqual(false, props.IsWeeklyMonday);
            Assert.AreEqual(false, props.IsWeeklyTuesday);
            Assert.AreEqual(false, props.IsWeeklyWednesday);
            Assert.AreEqual(false, props.IsWeeklyThursday);
            Assert.AreEqual(false, props.IsWeeklyFriday);
            Assert.AreEqual(false, props.IsWeeklySaturday);
        }   

		[Test()]
		public void ValidMonday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=mo";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(false, props.IsWeeklySunday);
            Assert.AreEqual(true, props.IsWeeklyMonday);
            Assert.AreEqual(false, props.IsWeeklyTuesday);
            Assert.AreEqual(false, props.IsWeeklyWednesday);
            Assert.AreEqual(false, props.IsWeeklyThursday);
            Assert.AreEqual(false, props.IsWeeklyFriday);
            Assert.AreEqual(false, props.IsWeeklySaturday);
        }  

		[Test()]
		public void ValidTuesday()
        {   
            string rule = "FREQ=WEEKLY;byday=TU";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(false, props.IsWeeklySunday);
			Assert.AreEqual(false, props.IsWeeklyMonday);
			Assert.AreEqual(true, props.IsWeeklyTuesday);
            Assert.AreEqual(false, props.IsWeeklyWednesday);
            Assert.AreEqual(false, props.IsWeeklyThursday);
            Assert.AreEqual(false, props.IsWeeklyFriday);
            Assert.AreEqual(false, props.IsWeeklySaturday);
        }  

		[Test()]
		public void ValidWednesday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=wE";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(false, props.IsWeeklySunday);
			Assert.AreEqual(false, props.IsWeeklyMonday);
            Assert.AreEqual(false, props.IsWeeklyTuesday);
			Assert.AreEqual(true, props.IsWeeklyWednesday);
            Assert.AreEqual(false, props.IsWeeklyThursday);
            Assert.AreEqual(false, props.IsWeeklyFriday);
            Assert.AreEqual(false, props.IsWeeklySaturday);
        }  

		[Test()]
		public void ValidThursday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=Th";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(false, props.IsWeeklySunday);
			Assert.AreEqual(false, props.IsWeeklyMonday);
            Assert.AreEqual(false, props.IsWeeklyTuesday);
            Assert.AreEqual(false, props.IsWeeklyWednesday);
			Assert.AreEqual(true, props.IsWeeklyThursday);
            Assert.AreEqual(false, props.IsWeeklyFriday);
            Assert.AreEqual(false, props.IsWeeklySaturday);
        }  

		[Test()]
		public void ValidFriday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(false, props.IsWeeklySunday);
			Assert.AreEqual(false, props.IsWeeklyMonday);
            Assert.AreEqual(false, props.IsWeeklyTuesday);
            Assert.AreEqual(false, props.IsWeeklyWednesday);
            Assert.AreEqual(false, props.IsWeeklyThursday);
			Assert.AreEqual(true, props.IsWeeklyFriday);
            Assert.AreEqual(false, props.IsWeeklySaturday);
        }  

		[Test()]
        public void ValidSaturday()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=SA";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(false, props.IsWeeklySunday);
			Assert.AreEqual(false, props.IsWeeklyMonday);
            Assert.AreEqual(false, props.IsWeeklyTuesday);
            Assert.AreEqual(false, props.IsWeeklyWednesday);
            Assert.AreEqual(false, props.IsWeeklyThursday);
            Assert.AreEqual(false, props.IsWeeklyFriday);
			Assert.AreEqual(true, props.IsWeeklySaturday);
        }  

		[Test()]
        public void Valid_MOWEFR()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=MO,WE,FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(false, props.IsWeeklySunday);
            Assert.AreEqual(true, props.IsWeeklyMonday);
            Assert.AreEqual(false, props.IsWeeklyTuesday);
			Assert.AreEqual(true, props.IsWeeklyWednesday);
            Assert.AreEqual(false, props.IsWeeklyThursday);
			Assert.AreEqual(true, props.IsWeeklyFriday);
			Assert.AreEqual(false, props.IsWeeklySaturday);
        } 

		[Test()]
        public void Valid_MOWEFR_x2()
        {   
			string rule = "FREQ=WEEKLY;BYDAY=MO,WE,FR,MO,WE,FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(false, props.IsWeeklySunday);
            Assert.AreEqual(true, props.IsWeeklyMonday);
            Assert.AreEqual(false, props.IsWeeklyTuesday);
            Assert.AreEqual(true, props.IsWeeklyWednesday);
            Assert.AreEqual(false, props.IsWeeklyThursday);
            Assert.AreEqual(true, props.IsWeeklyFriday);
            Assert.AreEqual(false, props.IsWeeklySaturday);
        } 

		[Test()]
        public void Valid_AllWeek()
        {   
            string rule = "FREQ=WEEKLY;BYDAY=MO,WE,FR,SU,SA,TU,TH";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
			Assert.AreEqual(true, props.IsWeeklySunday);
            Assert.AreEqual(true, props.IsWeeklyMonday);
			Assert.AreEqual(true, props.IsWeeklyTuesday);
            Assert.AreEqual(true, props.IsWeeklyWednesday);
			Assert.AreEqual(true, props.IsWeeklyThursday);
            Assert.AreEqual(true, props.IsWeeklyFriday);
			Assert.AreEqual(true, props.IsWeeklySaturday);
        }      
    }
}
