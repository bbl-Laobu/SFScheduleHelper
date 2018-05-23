using Kareke.SFScheduleHelper;
using NUnit.Framework;
using System;
namespace TestUnit.RecurrencesCalculatorTests
{
    [TestFixture()]
    public class DailyTests
    {
        RecurrencesCalculator calculator;

        [SetUp]
        public void Init()
        {
            calculator = new RecurrencesCalculator();
        }

        [Test()]
        public void InterVal1()
        {
            string rule = "FREQ=DAILY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int pos = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(startDate, startDate, "Pos:" + pos);
                pos++;
            }
        }

        [Test()]
        public void Interval1_Count10()
        {
            string rule = "FREQ=DAILY;COUNT=10;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, startDate, "Pos:" + count);
                nextDate.AddDays(1);
                count++;
            }
            Assert.AreEqual(10, count, "Count");
        }

        [Test()]
        public void Interval1_Count10_Until10012018()
        {
            string rule = "FREQ=DAILY;COUNT=10;UNTIL=10/01/2018";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, startDate, "Pos:" + count);
                nextDate.AddDays(1);
                count++;
            }

            Assert.AreEqual(31, count, "Count");
        }

        [Test()]
        public void Interval1_Count30_Until09032018()
        {
            string rule = "FREQ=DAILY;COUNT=10;UNTIL=09/03/2018";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, startDate, "Pos:" + count);
                nextDate.AddDays(1);
                count++;
            }

            Assert.AreEqual(3, count, "Count");
        }
    }
}

