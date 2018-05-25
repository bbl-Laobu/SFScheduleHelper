using Kareke.SFScheduleHelper;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;

namespace TestUnit.RecurrencesCalculatorTests
{
    [TestFixture()]
    public class YearlyTests
    {
        RecurrencesCalculator calculator;

        [SetUp]
        public void Init()
        {
            calculator = new RecurrencesCalculator();
        }

        [Test()]
        public void Interval1_BYMONTHDAY1_BYMONTH12()
        {
            string rule = "FREQ=YEARLY;BYMONTHDAY=1;BYMONTH=12;";
            DateTime startDate = new DateTime(2018, 01, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.GetRecurrences(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2018, 12, 01, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(1, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Count5_BYMONTHDAY15_BYMONTH12()
        {
            string rule = "FREQ=YEARLY;COUNT=5;BYMONTHDAY=15;BYMONTH=12;";
            DateTime startDate = new DateTime(2018, 01, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.GetRecurrences(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2018, 12, 15, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2019, 12, 15, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2020, 12, 15, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2021, 12, 15, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2022, 12, 15, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(5, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Count5_BYMONTHDAY31_BYMONTH2()
        {
            string rule = "FREQ=YEARLY;COUNT=5;BYMONTHDAY=31;BYMONTH=2;";
            DateTime startDate = new DateTime(2018, 01, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.GetRecurrences(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2018, 02, 28, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2019, 02, 28, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2020, 02, 29, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2021, 02, 28, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2022, 02, 28, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(5, recurrenceDates.Count, "Count");
        }


        [Test()]
        public void Interval1_Count5_BYMONTHDAY15_BYMONTH4_StartdayBigger()
        {
            string rule = "FREQ=YEARLY;COUNT=5;BYMONTHDAY=15;BYMONTH=4;";
            DateTime startDate = new DateTime(2018, 05, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.GetRecurrences(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2019, 04, 15, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2020, 04, 15, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2021, 04, 15, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2022, 04, 15, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2023, 04, 15, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(5, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval2_Count5_BYMONTHDAY31_BYMONTH2()
        {
            string rule = "FREQ=YEARLY;INTERVAL=2;COUNT=5;BYMONTHDAY=31;BYMONTH=2;";
            DateTime startDate = new DateTime(2018, 01, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.GetRecurrences(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2018, 02, 28, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2020, 02, 29, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2022, 02, 28, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2024, 02, 29, 10, 11, 12), recurrenceDates[3], "Pos: 2");
            Assert.AreEqual(new DateTime(2026, 02, 28, 10, 11, 12), recurrenceDates[4], "Pos: 2");
            Assert.AreEqual(5, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Until050121_BYMONTHDAY31_BYMONTH2()
        {
            string rule = "FREQ=YEARLY;UNTIL=05/01/2021;BYMONTHDAY=31;BYMONTH=2;";
            DateTime startDate = new DateTime(2018, 01, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.GetRecurrences(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2018, 02, 28, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2019, 02, 28, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2020, 02, 29, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2021, 02, 28, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(4, recurrenceDates.Count, "Count");
        }


        [Test()]
        public void Interval1_Until050121_BYDAYSA_BYSETPOS3_BYMONTH2()
        {
            string rule = "FREQ=YEARLY;UNTIL=05/01/2019;BYDAY=SA;BYSETPOS=3;BYMONTH=2;";
            DateTime startDate = new DateTime(2018, 01, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.GetRecurrences(rule, startDate) as ObservableCollection<DateTime>;

            // check weekday consistent
            int count = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(6, (int)recDate.DayOfWeek, "Pos:" + count);
                count++;
            }
            // Check dates
            Assert.AreEqual(new DateTime(2018, 02, 24, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2019, 02, 23, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(2, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Until050121_BYDAYMO_BYSETPOS5_BYMONTH4()
        {
            string rule = "FREQ=YEARLY;UNTIL=05/01/2021;BYDAY=SU;BYSETPOS=5;BYMONTH=4;";
            DateTime startDate = new DateTime(2018, 01, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.GetRecurrences(rule, startDate) as ObservableCollection<DateTime>;

            // check weekday consistent
            int count = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(0, (int)recDate.DayOfWeek, "Pos:" + count);
                count++;
            }
            // Check dates
            Assert.AreEqual(new DateTime(2018, 04, 29, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(1, recurrenceDates.Count, "Count");
        }


    }
}

