using Kareke.SFScheduleHelper;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;

namespace TestUnit.RecurrencesCalculatorTests
{
    [TestFixture()]
    public class MonthlyTests
    {
        RecurrencesCalculator calculator;

        [SetUp]
        public void Init()
        {
            calculator = new RecurrencesCalculator();
        }

        [Test()]
        public void Interval1_BYMONTHDAY1()
        {
            string rule = "FREQ=MONTHLY;COUNT=1;BYMONTHDAY=1;";
            DateTime startDate = new DateTime(2019, 01, 01, 10, 11, 12);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate);

            int count = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(startDate, recDate, "Pos:" + count);
                count++;
            }
            Assert.AreEqual(1, count, "Count");
        }

        [Test()]
        public void Interval1_Count10_BYMONTHDAY1()
        {
            string rule = "FREQ=MONTHLY;COUNT=10;BYMONTHDAY=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddMonths(1);
                count++;
            }
            Assert.AreEqual(10, count, "Count");
        }

        [Test()]
        public void Interval1_Count10_BYMONTHDAY1_StartdayBigger()
        {
            string rule = "FREQ=MONTHLY;COUNT=10;BYMONTHDAY=15;";
            DateTime startDate = new DateTime(2018, 09, 30, 10, 0, 0);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate.AddDays(15);
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddMonths(1);
                count++;
            }
            Assert.AreEqual(10, count, "Count");
        }

        [Test()]
        public void Interval2_Count10_BYMONTHDAY15()
        {
            string rule = "FREQ=MONTHLY;INTERVAL=2;COUNT=10;BYMONTHDAY=15;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate.AddDays(14);
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddMonths(2);
                count++;
            }
            Assert.AreEqual(10, count, "Count");
        }

        [Test()]
        public void Interval1_Count10_Until10012019_BYMONTHDAY28()
        {
            string rule = "FREQ=MONTHLY;COUNT=10;UNTIL=10/01/2019;BYMONTHDAY=28;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 11, 12);

            var recurrenceDates = calculator.AllRecurrenceDates(rule, startDate);

            int count = 0;
            DateTime nextDate = startDate.AddDays(27);
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(nextDate, recDate, "Pos:" + count);
                nextDate = nextDate.AddMonths(1);
                count++;
            }

            Assert.AreEqual(13, count, "Count");
        }

        [Test()]
        public void Interval1_Until05012019_BYMONTHDAY30()
        {
            string rule = "FREQ=MONTHLY;COUNT=10;UNTIL=05/01/2019;BYMONTHDAY=30;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2018, 09, 30, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2018, 10, 30, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2018, 11, 30, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2018, 12, 30, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2019, 01, 30, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(new DateTime(2019, 02, 28, 10, 11, 12), recurrenceDates[5], "Pos: 5");
            Assert.AreEqual(new DateTime(2019, 03, 30, 10, 11, 12), recurrenceDates[6], "Pos: 6");
            Assert.AreEqual(new DateTime(2019, 04, 30, 10, 11, 12), recurrenceDates[7], "Pos: 7");
            Assert.AreEqual(8, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Until05012019_BYMONTHDAY31()
        {
            string rule = "FREQ=MONTHLY;COUNT=10;UNTIL=05/01/2019;BYMONTHDAY=31;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2018, 09, 30, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2018, 10, 31, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2018, 11, 30, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2018, 12, 31, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2019, 01, 31, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(new DateTime(2019, 02, 28, 10, 11, 12), recurrenceDates[5], "Pos: 5");
            Assert.AreEqual(new DateTime(2019, 03, 31, 10, 11, 12), recurrenceDates[6], "Pos: 6");
            Assert.AreEqual(new DateTime(2019, 04, 30, 10, 11, 12), recurrenceDates[7], "Pos: 7");
            Assert.AreEqual(8, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Until05012019_BYMONTHDAY31_LeapYEAR()
        {
            string rule = "FREQ=MONTHLY;COUNT=10;UNTIL=05/01/2020;BYMONTHDAY=31;";
            DateTime startDate = new DateTime(2019, 09, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2019, 09, 30, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2019, 10, 31, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2019, 11, 30, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2019, 12, 31, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2020, 01, 31, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(new DateTime(2020, 02, 29, 10, 11, 12), recurrenceDates[5], "Pos: 5");
            Assert.AreEqual(new DateTime(2020, 03, 31, 10, 11, 12), recurrenceDates[6], "Pos: 6");
            Assert.AreEqual(new DateTime(2020, 04, 30, 10, 11, 12), recurrenceDates[7], "Pos: 7");
            Assert.AreEqual(8, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval2_Until05012019_BYMONTHDAY31_LeapYEAR_StartdayBigger()
        {
            string rule = "FREQ=MONTHLY;INTERVAL=2;COUNT=10;UNTIL=05/01/2020;BYMONTHDAY=30;";
            DateTime startDate = new DateTime(2019, 01, 31, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2019, 02, 28, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2019, 04, 30, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2019, 06, 30, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2019, 08, 30, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2019, 10, 30, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(new DateTime(2019, 12, 30, 10, 11, 12), recurrenceDates[5], "Pos: 5");
            Assert.AreEqual(new DateTime(2020, 02, 29, 10, 11, 12), recurrenceDates[6], "Pos: 6");
            Assert.AreEqual(new DateTime(2020, 04, 30, 10, 11, 12), recurrenceDates[7], "Pos: 7");
            Assert.AreEqual(8, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Until05012019_BYDAYMO_BYSETPOS2()
        {
            string rule = "FREQ=MONTHLY;UNTIL=05/01/2019;BYDAY=MO;BYSETPOS=2;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            // check weekday consistent
            int count = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(1, (int)recDate.DayOfWeek, "Pos:" + count); 
                count++;
            }
            // Check dates
            Assert.AreEqual(new DateTime(2018, 09, 10, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2018, 10, 08, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2018, 11, 12, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2018, 12, 10, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2019, 01, 14, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(new DateTime(2019, 02, 11, 10, 11, 12), recurrenceDates[5], "Pos: 5");
            Assert.AreEqual(new DateTime(2019, 03, 11, 10, 11, 12), recurrenceDates[6], "Pos: 6");
            Assert.AreEqual(new DateTime(2019, 04, 08, 10, 11, 12), recurrenceDates[7], "Pos: 7");
            Assert.AreEqual(8, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Until05012019_BYDAYMO_BYSETPOS1()
        {
            string rule = "FREQ=MONTHLY;UNTIL=05/01/2019;BYDAY=MO;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            // check weekday consistent
            int count = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(1, (int)recDate.DayOfWeek, "Pos:" + count);
                count++;
            }
            // Check dates
            Assert.AreEqual(new DateTime(2018, 09, 03, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2018, 10, 01, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2018, 11, 05, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2018, 12, 03, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2019, 01, 07, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(new DateTime(2019, 02, 04, 10, 11, 12), recurrenceDates[5], "Pos: 5");
            Assert.AreEqual(new DateTime(2019, 03, 04, 10, 11, 12), recurrenceDates[6], "Pos: 6");
            Assert.AreEqual(new DateTime(2019, 04, 01, 10, 11, 12), recurrenceDates[7], "Pos: 7");
            Assert.AreEqual(8, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Until05012019_BYDAYMO_BYSETPOS5()
        {
            string rule = "FREQ=MONTHLY;UNTIL=05/01/2019;BYDAY=MO;BYSETPOS=5;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            // check weekday consistent
            int count = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(1, (int)recDate.DayOfWeek, "Pos:" + count);
                count++;
            }
            // Check dates
            Assert.AreEqual(new DateTime(2018, 10, 29, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2018, 12, 31, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2019, 04, 29, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(3, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Until05012019_BYDAYSA_BYSETPOS4()
        {
            string rule = "FREQ=MONTHLY;UNTIL=05/01/2019;BYDAY=SA;BYSETPOS=4;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            // check weekday consistent
            int count = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(6, (int)recDate.DayOfWeek, "Pos:" + count);
                count++;
            }
            // Check dates
            Assert.AreEqual(new DateTime(2018, 09, 22, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2018, 10, 27, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2018, 11, 24, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2018, 12, 22, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2019, 01, 26, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(new DateTime(2019, 02, 23, 10, 11, 12), recurrenceDates[5], "Pos: 5");
            Assert.AreEqual(new DateTime(2019, 03, 23, 10, 11, 12), recurrenceDates[6], "Pos: 6");
            Assert.AreEqual(new DateTime(2019, 04, 27, 10, 11, 12), recurrenceDates[7], "Pos: 7");

            Assert.AreEqual(8, recurrenceDates.Count, "Count");
        }

        [Test()]
        public void Interval1_Until05012019_BYDAYMO_BYSETPOS1_Skip1stMonth()
        {
            string rule = "FREQ=MONTHLY;UNTIL=05/01/2019;BYDAY=MO;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 30, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            // check weekday consistent
            int count = 0;
            foreach (var recDate in recurrenceDates)
            {
                Assert.AreEqual(1, (int)recDate.DayOfWeek, "Pos:" + count);
                count++;
            }
            // Check dates
            //Assert.AreEqual(new DateTime(2018, 09, 03, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2018, 10, 01, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2018, 11, 05, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2018, 12, 03, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2019, 01, 07, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(new DateTime(2019, 02, 04, 10, 11, 12), recurrenceDates[4], "Pos: 4");
            Assert.AreEqual(new DateTime(2019, 03, 04, 10, 11, 12), recurrenceDates[5], "Pos: 5");
            Assert.AreEqual(new DateTime(2019, 04, 01, 10, 11, 12), recurrenceDates[6], "Pos: 6");

            Assert.AreEqual(7, recurrenceDates.Count, "Count");
        }
    }
}