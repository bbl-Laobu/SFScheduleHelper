using Kareke.SFScheduleHelper;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestUnit.RecurrencesCalculatorTests
{
    [TestFixture()]
    public class CurrentRecurrencesTests
    {
        RecurrencesCalculator calculator;

        [SetUp]
        public void Init()
        {
            calculator = new RecurrencesCalculator();
        }

        [Test()]
        public void Daily_count1_Before()
        {
            string rule = "FREQ=DAILY;";
            DateTime startDate = new DateTime(2017, 09, 01, 10, 0, 0);

            DateTime currentRecurrence = calculator.CurrentRecurrence(rule, startDate, new DateTime(2017, 08, 31, 10, 0, 0));

            Assert.AreEqual(DateTime.MinValue, currentRecurrence, "Pos: 0");
        }

        [Test()]
        public void Daily_count1_Current()
        {
            string rule = "FREQ=DAILY;";
            DateTime startDate = new DateTime(2017, 09, 01, 10, 0, 0);

            DateTime currentRecurrence = calculator.CurrentRecurrence(rule, startDate, new DateTime(2017, 09, 01, 10, 0, 0));

            Assert.AreEqual(new DateTime(2017, 09, 01, 10, 0, 0), currentRecurrence, "Pos: 0");
        }

        [Test()]
        public void Daily_count1_After()
        {
            string rule = "FREQ=DAILY;";
            DateTime startDate = new DateTime(2017, 09, 01, 10, 0, 0);

            DateTime currentRecurrence = calculator.CurrentRecurrence(rule, startDate, new DateTime(2017, 09, 01, 11, 0, 0));

            Assert.AreEqual(new DateTime(2017, 09, 01, 10, 0, 0), currentRecurrence, "Pos: 0");
        }

        [Test()]
        public void Daily_Count10_Middle()
        {
            string rule = "FREQ=DAILY;COUNT=10;";
            DateTime startDate = new DateTime(2017, 09, 01, 10, 0, 0);

            DateTime currentRecurrence = calculator.CurrentRecurrence(rule, startDate, new DateTime(2017, 09, 05, 15, 0, 0));

            Assert.AreEqual(new DateTime(2017, 09, 05, 10, 0, 0), currentRecurrence, "Pos: 0");
        }


        [Test()]
        public void Daily_UNTIL09012019_FinalDate()
        {
            string rule = "FREQ=DAILY;UNTIL=09/01/2019";
            DateTime startDate = new DateTime(2017, 09, 01, 10, 0, 0);
            DateTime LastRecurrence = calculator.FinalEndDate(rule, startDate, TimeSpan.FromHours(2));

            DateTime currentRecurrence = calculator.CurrentRecurrence(rule, startDate, LastRecurrence);

            Assert.AreEqual(LastRecurrence - TimeSpan.FromHours(2), currentRecurrence, "Pos: 0");
        }

        [Test()]
        public void Daily_UNTIL09012019_Middle()
        {
            string rule = "FREQ=DAILY;UNTIL=09/01/2019";
            DateTime startDate = new DateTime(2017, 09, 01, 10, 0, 0);

            DateTime currentRecurrence = calculator.CurrentRecurrence(rule, startDate, new DateTime(2018, 04, 01, 16, 11, 11));

            Assert.AreEqual(new DateTime(2018, 04, 01, 10, 0, 0), currentRecurrence, "Pos: 0");
        }

        [Test()]
        public void Weekly_Count10_ByDaySA_Second()
        {
            string rule = "FREQ=WEEKLY;COUNT=10;BYDAY=SA;";
            DateTime startDate = new DateTime(2017, 09, 01, 10, 0, 0);
            ObservableCollection<DateTime> recurrenceDates = calculator.AllRecurrenceDates(rule, startDate) as ObservableCollection<DateTime>;

            ObservableCollection<DateTime> nextRecurrences = calculator.NextRecurrences(rule, startDate, new DateTime(2017, 09, 02, 10, 0, 0), 1) as ObservableCollection<DateTime>;

            DateTime currentRecurrence = calculator.CurrentRecurrence(rule, startDate, nextRecurrences.First());

            Assert.AreEqual(new DateTime(2017, 09, 09, 10, 0, 0), currentRecurrence, "Pos: 0");
        }

        [Test()]
        public void Monthly_Until05012019_BYDAYMO_BYSETPOS1()
        {
            string rule = "FREQ=MONTHLY;UNTIL=05/01/2019;BYDAY=MO;BYSETPOS=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 11, 12);

            DateTime currentRecurrence = calculator.CurrentRecurrence(rule, startDate, new DateTime(2019, 01, 08, 10, 11, 12));

            Assert.AreEqual(new DateTime(2019, 01, 07, 10, 11, 12), currentRecurrence, "Pos: 0");
        }

        [Test()]
        public void Yearly_Until050121_BYMONTHDAY31_BYMONTH2()
        {
            string rule = "FREQ=YEARLY;UNTIL=05/01/2021;BYMONTHDAY=31;BYMONTH=2;";
            DateTime startDate = new DateTime(2018, 01, 01, 10, 11, 12);

            DateTime currentRecurrence = calculator.CurrentRecurrence(rule, startDate, new DateTime(2021, 01, 28, 10, 11, 12));

            Assert.AreEqual(new DateTime(2020, 02, 29, 10, 11, 12), currentRecurrence, "Pos: 0");
        }
    }
}

