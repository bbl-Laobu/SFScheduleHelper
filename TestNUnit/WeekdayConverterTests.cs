using Kareke.SFScheduleHelper;
using NUnit.Framework;
using System;
namespace TestNUnit
{
    [TestFixture()]
    public class WeekdayConverterTests
    {
        [Test()]
        public void SundayTo0()
        {
            string weekday = "SU";
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(0, weekdayInt);
        }

        [Test()]
        public void MondayTo1()
        {
            string weekday = "MO";
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(1, weekdayInt);
        }

        [Test()]
        public void TuesdayTo2()
        {
            string weekday = "TU";
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(2, weekdayInt);
        }

        [Test()]
        public void WednesdayTo3()
        {
            string weekday = "WE";
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(3, weekdayInt);
        }

        [Test()]
        public void ThursdayTo4()
        {
            string weekday = "TH";
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(4, weekdayInt);
        }

        [Test()]
        public void FridayTo5()
        {
            string weekday = "FR";
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(5, weekdayInt);
        }

        [Test()]
        public void SaturdayTo6()
        {
            string weekday = "SA";
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(6, weekdayInt);
        }

        [Test()]
        public void InvalidNull()
        {
            string weekday = null;
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(-1, weekdayInt);
        }

        [Test()]
        public void InvalidEmpty()
        {
            string weekday = string.Empty;
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(-1, weekdayInt);
        }

        [Test()]
        public void InvalidWrong()
        {
            string weekday = "bad";
            int weekdayInt = WeekdayConverter.Convert(weekday);
            Assert.AreEqual(-1, weekdayInt);
        }

        // TEST int => string
        [Test()]
        public void Test0toSU()
        {
            int weekdayInt = 0;
            string weekday = WeekdayConverter.Convert(weekdayInt);
            Assert.AreEqual("SU", weekday);
        }

        [Test()]
        public void Test1toMO()
        {
            int weekdayInt = 1;
            string weekday = WeekdayConverter.Convert(weekdayInt);
            Assert.AreEqual("MO", weekday);
        }

        [Test()]
        public void Test2toTU()
        {
            int weekdayInt = 2;
            string weekday = WeekdayConverter.Convert(weekdayInt);
            Assert.AreEqual("TU", weekday);
        }

        [Test()]
        public void Test3toWE()
        {
            int weekdayInt = 3;
            string weekday = WeekdayConverter.Convert(weekdayInt);
            Assert.AreEqual("WE", weekday);
        }

        [Test()]
        public void Test4toTH()
        {
            int weekdayInt = 4;
            string weekday = WeekdayConverter.Convert(weekdayInt);
            Assert.AreEqual("TH", weekday);
        }

        [Test()]
        public void Test5toFR()
        {
            int weekdayInt = 5;
            string weekday = WeekdayConverter.Convert(weekdayInt);
            Assert.AreEqual("FR", weekday);
        }

        [Test()]
        public void Test6toSA()
        {
            int weekdayInt = 6;
            string weekday = WeekdayConverter.Convert(weekdayInt);
            Assert.AreEqual("SA", weekday);
        }

        [Test()]
        public void InvalidNegative1()
        {
            int weekdayInt = -1;
            string weekday = WeekdayConverter.Convert(weekdayInt);
            Assert.AreEqual(string.Empty, weekday);
        }

        [Test()]
        public void Invalid27()
        {
            int weekdayInt = 27;
            string weekday = WeekdayConverter.Convert(weekdayInt);
            Assert.AreEqual(string.Empty, weekday);
        }
    }
}

