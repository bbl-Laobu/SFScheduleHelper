using Kareke.SFScheduleHelper;
using NUnit.Framework;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParseRuleToPropsTests
{
    [TestFixture()]
	public class TestByDayMonthly
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
			string rule = "FREQ=MONTHLY;BYDAY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYDAY has non valid value ", parser.ErrorMessage);
        }

        [Test()]
        public void InValidNoDay()
        {
			string rule = "FREQ=MONTHLY;BYDAY=";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYDAY has non valid value ", parser.ErrorMessage);
        }      

        [Test()]
        public void Invalid()
        {
			string rule = "FREQ=MONTHLY;BYDAY=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYDAY has non valid value ", parser.ErrorMessage);
        }

        [Test()]
        public void InvalidWeekDay()
        {
			string rule = "FREQ=MONTHLY;BYDAY=MA";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
             
            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("BYDAY has non valid value ", parser.ErrorMessage);
        }

		[Test()]
        public void NoByDay()
        {
            string rule = "FREQ=MONTHLY;BYMONTHDAY=01; ";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType);
        }

        [Test()]
        public void ValidSunday()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=4;BYDAY=su";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(0, props.DayOfWeek);
        }

        [Test()]
        public void ValidMonday()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=2;BYDAY=mo";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(1, props.DayOfWeek);
        }

        [Test()]
        public void ValidTuesday()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=1;byday=TU";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(2, props.DayOfWeek);
        }

        [Test()]
        public void ValidWednesday()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=1;BYDAY=wE";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(3, props.DayOfWeek);
        }

        [Test()]
        public void ValidThursday()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=1;BYDAY=Th";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(4, props.DayOfWeek);
        }

        [Test()]
        public void ValidFriday()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=1;BYDAY=FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(5, props.DayOfWeek);
        }

        [Test()]
        public void ValidSaturday()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=1;BYDAY=SA";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(6, props.DayOfWeek);
        }

        [Test()]
        public void Valid_MOWEFR()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=1;BYDAY=WE,MO,FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(3, props.DayOfWeek);
        }

        [Test()]
        public void Valid_AllWeek()
        {
            string rule = "FREQ=MONTHLY;BYSETPOS=1;BYDAY=MO,WE,FR,SU,SA,TU,TH";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);
            
            Assert.AreEqual(1, props.DayOfWeek);
        }
    }
}
