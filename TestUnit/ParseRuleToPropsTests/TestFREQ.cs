using Kareke.SFScheduleHelper;
using NUnit.Framework;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit.ParseRuleToPropsTests
{
    [TestFixture()]
    public class TestFREQ
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
            string rule = "FREQ;;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("FREQ has non valid value ", parser.ErrorMessage);
        }

        [Test()]
        public void Invalid()
        {
            string rule = "FREQ=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("FREQ has non valid value ", parser.ErrorMessage);
        }

        [Test()]
        public void ValidDaily()
        {
            string rule = "FREQ=DAILY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType);
        }

        [Test()]
        public void ValidWeekly()
        {
            string rule = "FREQ=WEEKLY;BYDAY=TU;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType);
        }

        [Test()]
        public void ValidMonthly()
        {
            string rule = "FREQ=monthly;BYMONTHDAY=15;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType);
        }

        [Test()]
        public void ValidYearly()
        {
            string rule = "freq=yearly;BYMONTH=9;BYMONTHDAY=01";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(RecurrenceType.Yearly, props.RecurrenceType);
        }
    }
}
