using Kareke.SFScheduleHelper;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestUnit.RecurrencesCalculatorTests
{
    [TestFixture()]
    public class NextRecurrencesTests
    {
        RecurrencesCalculator calculator;

        [SetUp]
        public void Init()
        {
            calculator = new RecurrencesCalculator();
        }

        [Test()]
        public void Daily_count1_Past_Found0()
        {
            string rule = "FREQ=DAILY;Count=1";
            DateTime startDate = new DateTime(2017, 09, 01, 10, 0, 0);
            TimeSpan duration = TimeSpan.FromHours(2);

            ObservableCollection<DateTime> nextRecurrences = calculator.NextRecurrences(rule, startDate, DateTime.Now) as ObservableCollection<DateTime>;

            Assert.AreEqual(0, nextRecurrences.Count, "Pos: 0");
        }

        [Test()]
        public void Daily_count1_Current_Found1()
        {
            string rule = "FREQ=DAILY;Count=1";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            TimeSpan duration = TimeSpan.FromHours(2);

            ObservableCollection<DateTime> nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2018, 09, 01, 10, 0, 0)) as ObservableCollection<DateTime>;

            Assert.AreEqual(0, nextRecurrences.Count, "Pos: 0");
        }

        [Test()]
        public void Daily_count1_Future_Found1()
        {
            string rule = "FREQ=DAILY;Count=1";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            TimeSpan duration = TimeSpan.FromHours(2);

            ObservableCollection<DateTime> nextRecurrences = calculator.NextRecurrences(rule, startDate, DateTime.Now) as ObservableCollection<DateTime>;

            Assert.AreEqual(1, nextRecurrences.Count, "Pos: 0");
        }

        [Test()]
        public void Daily_Count10_Found5()
        {
            string rule = "FREQ=DAILY;COUNT=10;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            TimeSpan duration = TimeSpan.FromHours(2);

            ObservableCollection<DateTime> nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2018, 09, 05, 10, 0, 0)) as ObservableCollection<DateTime>;

            Assert.AreEqual(5, nextRecurrences.Count, "Pos: 0");
        }


        [Test()]
        public void Daily_UNTIL09012019_WithfinalDate_Found0()
        {
            string rule = "FREQ=DAILY;UNTIL=09/01/2019";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            TimeSpan duration = TimeSpan.FromHours(2);

            DateTime finalDate = calculator.FinalEndDate(rule, startDate, duration);

            ObservableCollection<DateTime> nextRecurrences = calculator.NextRecurrences(rule, startDate, finalDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(0, nextRecurrences.Count, "Pos: 0");
        }

        [Test()]
        public void Weekly_Count10_ByDaySA_Found10()
        {
            string rule = "FREQ=WEEKLY;COUNT=10;BYDAY=SA;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            TimeSpan duration = TimeSpan.FromHours(2);

            ObservableCollection<DateTime> nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2018, 08, 31, 10, 0, 0)) as ObservableCollection<DateTime>;

            Assert.AreEqual(10, nextRecurrences.Count, "Pos: 0");
        }

        [Test()]
        public void Monthly_Until05012019_BYDAYMO_BYSETPOS1_Found4()
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

            TimeSpan duration = TimeSpan.FromHours(2);

            ObservableCollection<DateTime> nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2018, 12, 03, 10, 11, 12)) as ObservableCollection<DateTime>;

            Assert.AreEqual(4, nextRecurrences.Count, "Pos: 0");
            Assert.AreEqual(new DateTime(2019, 01, 07, 10, 11, 12), nextRecurrences[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2019, 02, 04, 10, 11, 12), nextRecurrences[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2019, 03, 04, 10, 11, 12), nextRecurrences[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2019, 04, 01, 10, 11, 12), nextRecurrences[3], "Pos: 3");
        }

        [Test()]
        public void Yearly_Until050121_BYMONTHDAY31_BYMONTH2()
        {
            string rule = "FREQ=YEARLY;UNTIL=05/01/2021;BYMONTHDAY=31;BYMONTH=2;";
            DateTime startDate = new DateTime(2018, 01, 01, 10, 11, 12);

            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            Assert.AreEqual(new DateTime(2018, 02, 28, 10, 11, 12), recurrenceDates[0], "Pos: 0");
            Assert.AreEqual(new DateTime(2019, 02, 28, 10, 11, 12), recurrenceDates[1], "Pos: 1");
            Assert.AreEqual(new DateTime(2020, 02, 29, 10, 11, 12), recurrenceDates[2], "Pos: 2");
            Assert.AreEqual(new DateTime(2021, 02, 28, 10, 11, 12), recurrenceDates[3], "Pos: 3");
            Assert.AreEqual(4, recurrenceDates.Count, "Count");

            TimeSpan duration = TimeSpan.FromHours(2);

            ObservableCollection<DateTime> nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2018, 02, 27, 10, 11, 12)) as ObservableCollection<DateTime>;
            Assert.AreEqual(4, nextRecurrences.Count, "Pos: 0");

            nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2018, 02, 28, 10, 11, 12)) as ObservableCollection<DateTime>;
            Assert.AreEqual(3, nextRecurrences.Count, "Pos: 1");

            nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2019, 02, 28, 10, 11, 12)) as ObservableCollection<DateTime>;
            Assert.AreEqual(2, nextRecurrences.Count, "Pos: 2");

            nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2020, 02, 29, 10, 11, 12)) as ObservableCollection<DateTime>;
            Assert.AreEqual(1, nextRecurrences.Count, "Pos: 3");

            nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2021, 02, 28, 10, 11, 12)) as ObservableCollection<DateTime>;
            Assert.AreEqual(0, nextRecurrences.Count, "Pos: 4");
        }


    }
}

