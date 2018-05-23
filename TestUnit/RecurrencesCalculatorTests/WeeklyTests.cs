using Kareke.SFScheduleHelper;
using NUnit.Framework;
using System;
namespace TestUnit.RecurrencesCalculatorTests
{
    [TestFixture()]
    public class WeeklyTests
    {
        RecurrencesCalculator calculator;

        [SetUp]
        public void Init()
        {
            calculator = new RecurrencesCalculator();
        }

        [Test()]
        public void InterVal1_ByDaySA()
        {
            string rule = "FREQ=Weekly;BYDAY=SA;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int count = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(startDate, recDate, "Pos:" + count);
                count++;
            }
            Assert.AreEqual(1, count, "Count");
        }

        [Test()]
        public void Interval1_Count10_ByDaySA()
        {
            string rule = "FREQ=WEEKLY;COUNT=10;BYDAY=SA;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddDays(7);
                count++;
            }
            Assert.AreEqual(10, count, "Count");
        }

        [Test()]
        public void Interval1_Count10_Until10012018_ByDaySA()
        {
            string rule = "FREQ=WEEKLY;COUNT=10;UNTIL=10/01/2018;BYDAY=SA;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddDays(7);
                count++;
            }

            Assert.AreEqual(5, count, "Count");
        }

        [Test()]
        public void Interval1_Until09012019_ByDaySA()
        {
            string rule = "FREQ=WEEKLY;UNTIL=09/01/2019;BYDAY=SA;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddDays(7);
                count++;
            }

            Assert.AreEqual(53, count, "Count");
        }

        [Test()]
        public void Interval1_Count10_ByDayMOFR()
        {
            string rule = "FREQ=WEEKLY;COUNT=10;BYDAY=MO,FR;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate.AddDays(2);
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                if (nextDate.DayOfWeek == DayOfWeek.Monday) nextDate = nextDate.AddDays(4);
                else if (nextDate.DayOfWeek == DayOfWeek.Friday) nextDate = nextDate.AddDays(3);
                count++;
            }
            Assert.AreEqual(10, count, "Count");
        }

        [Test()]
        public void Interval2_Count30_ByDayMOFR()
        {
            string rule = "FREQ=WEEKLY;INTERVAL=2;COUNT=10;BYDAY=MO,FR;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate.AddDays(9);
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                if (nextDate.DayOfWeek == DayOfWeek.Monday) nextDate = nextDate.AddDays(4);
                else if (nextDate.DayOfWeek == DayOfWeek.Friday) nextDate = nextDate.AddDays(10);
                count++;
            }
            Assert.AreEqual(10, count, "Count");
        }

        [Test()]
        public void Interval1_Count20_ByDayAll()
        {
            string rule = "FREQ=WEEKLY;COUNT=10;BYDAY=MO,FR,SA,SU,TU,WE,TH;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.GetRecurrences(rule, startDate);

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
    }
}

