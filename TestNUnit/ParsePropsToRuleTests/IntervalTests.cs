using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestNUnit.ParsePropsToRuleTests
{
    [TestFixture()]
    public class IntervalTests
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
        public void ValidDaily1()
        {
            string rule = "FREQ=DAILY;INTERVAL=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=DAILY;INTERVAL=1;", ruleResult);
        }

        [Test()]
        public void ValidWeekly2()
        {
            string rule = "FREQ=Weekly;INTERVAL=2;BYDAY=TH;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=2;BYDAY=TH;", ruleResult);
        }

        [Test()]
        public void ValidMonthly3()
        {
            string rule = "FREQ=monthly;INTERVAL=3;BYMONTHDAY=31;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=MONTHLY;INTERVAL=3;BYMONTHDAY=31;", ruleResult);
        }

        [Test()]
        public void ValidYearly4()
        {
            string rule = "FREQ=YEARLY;INTERVAL=4;BYDAY=SU;BYSETPOS=2;BYMONTH=3;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=YEARLY;INTERVAL=4;BYDAY=SU;BYMONTH=3;BYSETPOS=2;", ruleResult);
        }

        [Test()]
        public void NoIntervalWeekly()
        {
            string rule = "FREQ=Weekly;BYDAY=TU,WE;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=WEEKLY;INTERVAL=1;BYDAY=TU,WE;", ruleResult);
        }

        [Test()]
        public void NoIntervalDaily()
        {
            string rule = "FREQ=DAily;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = parser.Convert(rule, startDate);

            string ruleResult = converter.Convert(props);

            Assert.AreEqual("FREQ=DAILY;INTERVAL=1;", ruleResult);
        }

    }
}
