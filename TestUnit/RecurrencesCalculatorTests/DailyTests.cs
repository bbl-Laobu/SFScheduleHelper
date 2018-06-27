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

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate, 1);

            int pos = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(startDate, recDate, "Pos:" + pos);
                pos++;
            }
        }

        [Test()]
        public void Interval1_Count10()
        {
            string rule = "FREQ=DAILY;COUNT=10;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddDays(1);
                count++;
            }
            Assert.AreEqual(10, count, "Count");
        }

        [Test()]
        public void Interval1_Count10_Until10012018()
        {
            string rule = "FREQ=DAILY;COUNT=10;UNTIL=20181001";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddDays(1);
                count++;
            }

            Assert.AreEqual(31, count, "Count");
        }

        [Test()]
        public void Interval1_Until09012019()
        {
            string rule = "FREQ=DAILY;UNTIL=20190901";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddDays(1);
                count++;
            }

            Assert.AreEqual(200, count, "Count");
        }

        [Test()]
        public void Interval1_Until09012019_max201()
        {
            string rule = "FREQ=DAILY;UNTIL=20190901";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate, 210);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddDays(1);
                count++;
            }

            Assert.AreEqual(210, count, "Count");
        }

        [Test()]
        public void Interval1_Until11052018()
        {
            string rule = "FREQ=DAILY;UNTIL=20181105";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddDays(1);
                count++;
            }

            Assert.AreEqual(66, count, "Count");
        }


    }
}

