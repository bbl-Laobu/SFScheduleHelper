﻿using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestNUnit.ParseRuleToPropsTests
{
    [TestFixture()]
    public class TestByMonthDayYearly
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
			string rule = "FREQ=YEARLY;BYMONTHDAY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }

		[Test()]
		public void InValid()
        {
			string rule = "FREQ=YEARLY;BYMONTHDAY=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }
        
		[Test()]
        public void InValid0()
        {
			string rule = "FREQ=YEARLY;BYMONTHDAY=0;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }  

		[Test()]
        public void InValid32()
        {
			string rule = "FREQ=YEARLY;BYMONTHDAY=32;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }  

		[Test()]
        public void InValiNegative1()
        {
			string rule = "FREQ=YEARLY;BYMONTHDAY=-1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }

		[Test()]
        public void InValiNegative()
        {
			string rule = "FREQ=YEARLY;BYMONTHDAY=-31;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);

			Assert.AreEqual(true, parser.HasError);
			StringAssert.Contains("BYMONTHDAY has non valid value ", parser.ErrorMessage);
        }

		[Test()]
		public void Valid1()
        {
            string rule = "FREQ=YEARLY;BYMONTH=8;BYMONTHDAY=1;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(false, props.IsMonthlySpecific);
			Assert.AreEqual(1, props.DayOfMonth);
        }   

		[Test()]
        public void Valid15()
        {
            string rule = "FREQ=YEARLY;BYMONTH=9;BYMONTHDAY=15;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(false, props.IsMonthlySpecific);
            Assert.AreEqual(15, props.DayOfMonth);
        }   

		[Test()]
        public void Valid31()
        {
            string rule = "FREQ=YEARLY;BYMONTH=9;BYMONTHDAY=31;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
			Assert.AreEqual(false, props.IsMonthlySpecific);
            Assert.AreEqual(31, props.DayOfMonth);
        } 
         
        [Test()]
		public void NoBYMONTHDAY()
        {
            string rule = "FREQ=YEARLY;BYMONTH=1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = parser.Convert(rule, startDate);
         
            Assert.AreEqual(true, parser.HasError);
            Assert.AreEqual("BYSETPOS/BYDAY or BYMONTHDAY should be set for Yearly", parser.ErrorMessage);
        } 
        
    }
}
