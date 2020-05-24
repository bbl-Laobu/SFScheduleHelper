using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestNUnit.ParseRuleToPropsTests
{
    [TestFixture()]
    public class TestCount
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
            string rule = "FREQ=WEEKLY;COUNT;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("COUNT has non valid value ", parser.ErrorMessage);
        }

        [Test()]
        public void Invalid()
        {
            string rule = "FREQ=WEEKLY;COUNT=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("COUNT has non valid value ", parser.ErrorMessage);
        }

        [Test()]
        public void ValidCountDaily()
        {
            string rule = "FREQ=DAILY;COUNT=2;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType);
            Assert.AreEqual(1, props.Interval);
            Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count);
            Assert.AreEqual(2, props.RecurrenceCount);
        }

        [Test()]
        public void ValidCountWeekly()
        {
            string rule = "FREQ=Weekly;INTERVAL=2;COUNT=4;BYDAY=SA,SU;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType);
            Assert.AreEqual(2, props.Interval);
            Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.Count);
            Assert.AreEqual(4, props.RecurrenceCount);
        }

        [Test()]
        public void NoCount()
        {
            string rule = "FREQ=DAILY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType);
            Assert.AreEqual(1, props.Interval);
            Assert.AreEqual(true, props.RecurrenceRange == RecurrenceRange.NoEndDate);
            Assert.AreEqual(1, props.RecurrenceCount);
        }

        [Test()]
        public void NegativeCountDaily()
        {
            string rule = "FREQ=DAily;COUNT=-1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            Assert.AreEqual(true, parser.HasError);
            StringAssert.Contains("COUNT has non valid value ", parser.ErrorMessage);
        }
    }
}
