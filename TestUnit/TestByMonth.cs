using NUnit.Framework;
using Kareke.SFScheduleHelper;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit
{
    [TestFixture()]
    public class TestByMonth
    {
		[Test()]
		public void InValidEmpty()
        {
			string rule = "FREQ=YEARLY;BYMONTH;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);

			Assert.AreEqual(true, RuleToPropertiesConverter.HasError);
			StringAssert.Contains("BYMONTH has non valid value ", RuleToPropertiesConverter.ErrorMessage);
        }

		[Test()]
		public void InValid()
        {
			string rule = "FREQ=YEARLY;BYMONTH=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);
            
			Assert.AreEqual(true, RuleToPropertiesConverter.HasError);
			StringAssert.Contains("BYMONTH has non valid value ", RuleToPropertiesConverter.ErrorMessage);
        }
        
		[Test()]
        public void InValid0()
        {
			string rule = "FREQ=YEARLY;BYMONTH=0;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);

            Assert.AreEqual(true, RuleToPropertiesConverter.HasError);
			StringAssert.Contains("BYMONTH has non valid value ", RuleToPropertiesConverter.ErrorMessage);
        }  

		[Test()]
        public void InValid13()
        {
			string rule = "FREQ=YEARLY;BYMONTH=13;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);

            Assert.AreEqual(true, RuleToPropertiesConverter.HasError);
			StringAssert.Contains("BYMONTH has non valid value ", RuleToPropertiesConverter.ErrorMessage);
        }  

		[Test()]
        public void InValiNegative1()
        {
			string rule = "FREQ=YEARLY;BYMONTH=-1;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);

            Assert.AreEqual(true, RuleToPropertiesConverter.HasError);
			StringAssert.Contains("BYMONTH has non valid value ", RuleToPropertiesConverter.ErrorMessage);
        }

		[Test()]
        public void InValiNegative()
        {
			string rule = "FREQ=YEARLY;BYMONTH=-12;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);

            Assert.AreEqual(true, RuleToPropertiesConverter.HasError);
			StringAssert.Contains("BYMONTH has non valid value ", RuleToPropertiesConverter.ErrorMessage);
        }

		[Test()]
		public void Valid1()
        {
			string rule = "FREQ=YEARLY;BYMONTH=1;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);
         
			Assert.AreEqual(true, props.IsYearlySpecific);
			Assert.AreEqual(1, props.YearlySpecificMonth);
        }   

		[Test()]
        public void Valid6()
        {
			string rule = "FREQ=YEARLY;BYMONTH=6;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);

			Assert.AreEqual(true, props.IsYearlySpecific);
			Assert.AreEqual(6, props.YearlySpecificMonth);
        }   

		[Test()]
        public void Valid12()
        {
			string rule = "FREQ=YEARLY;BYMONTH=12;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);
         
			Assert.AreEqual(true, props.IsYearlySpecific);
			Assert.AreEqual(12, props.YearlySpecificMonth);
        } 
         
        [Test()]
		public void NoByMonth()
        {
			string rule = "FREQ=YEARLY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.ConvertRule(rule, startDate);
         
			Assert.AreEqual(false, props.IsMonthlySpecific);
            Assert.AreEqual(0, props.MonthlySpecificMonthDay);
        } 
        
    }
}
