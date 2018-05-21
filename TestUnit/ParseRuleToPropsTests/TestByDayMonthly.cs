using Kareke.SFScheduleHelper;
using NUnit.Framework;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParseRuleToPropsTests
{
    [TestFixture()]
	public class TestByDayMonthly
	{
        [Test()]
		public void InValidEmpty()
        {
			string rule = "FREQ=MONTHLY;BYDAY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);

			Assert.AreEqual(true, ParseRuleToProps.HasError);
			StringAssert.Contains("BYDAY has non valid value ", ParseRuleToProps.ErrorMessage);
        }

        [Test()]
        public void InValidNoDay()
        {
			string rule = "FREQ=MONTHLY;BYDAY=";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);

			Assert.AreEqual(true, ParseRuleToProps.HasError);
			StringAssert.Contains("BYDAY has non valid value ", ParseRuleToProps.ErrorMessage);
        }      

        [Test()]
        public void Invalid()
        {
			string rule = "FREQ=MONTHLY;BYDAY=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			Assert.AreEqual(true, ParseRuleToProps.HasError);
			StringAssert.Contains("BYDAY has non valid value ", ParseRuleToProps.ErrorMessage);
        }

        [Test()]
        public void InvalidWeekDay()
        {
			string rule = "FREQ=MONTHLY;BYDAY=MA";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
             
			Assert.AreEqual(true, ParseRuleToProps.HasError);
			StringAssert.Contains("BYDAY has non valid value ", ParseRuleToProps.ErrorMessage);
        }

		[Test()]
        public void NoByDay()
        {
            string rule = "FREQ=MONTHLY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType);
        }

        [Test()]
        public void ValidSunday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=su";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
			Assert.AreEqual(1, props.MonthlyWeekDay);
        }

        [Test()]
        public void ValidMonday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=mo";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
            Assert.AreEqual(2, props.MonthlyWeekDay);
        }

        [Test()]
        public void ValidTuesday()
        {
			string rule = "FREQ=MONTHLY;byday=TU";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
            Assert.AreEqual(3, props.MonthlyWeekDay);
        }

        [Test()]
        public void ValidWednesday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=wE";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
            Assert.AreEqual(4, props.MonthlyWeekDay);
        }

        [Test()]
        public void ValidThursday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=Th";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
            Assert.AreEqual(5, props.MonthlyWeekDay);
        }

        [Test()]
        public void ValidFriday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
            Assert.AreEqual(6, props.MonthlyWeekDay);
        }

        [Test()]
        public void ValidSaturday()
        {
			string rule = "FREQ=MONTHLY;BYDAY=SA";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
            Assert.AreEqual(7, props.MonthlyWeekDay);
        }

        [Test()]
        public void Valid_MOWEFR()
        {
			string rule = "FREQ=MONTHLY;BYDAY=WE,MO,FR";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
            Assert.AreEqual(4, props.MonthlyWeekDay);
        }

        [Test()]
        public void Valid_AllWeek()
        {
			string rule = "FREQ=MONTHLY;BYDAY=MO,WE,FR,SU,SA,TU,TH";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = ParseRuleToProps.Convert(rule, startDate);
            
            Assert.AreEqual(2, props.MonthlyWeekDay);
        }
    }
}
