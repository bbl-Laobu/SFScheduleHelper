using Kareke.SFScheduleHelper;
using NUnit.Framework;
using Syncfusion.SfSchedule.XForms;
using System;
namespace TestUnit
{
    [TestFixture()]
    public class TestFREQ
    {
		[Test()]
		public void InValidEmpty()
        {
			string rule = "FREQ;;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.Convert(rule, startDate);

			Assert.AreEqual(true, RuleToPropertiesConverter.HasError);
			StringAssert.Contains("FREQ has non valid value ", RuleToPropertiesConverter.ErrorMessage);
        }

		[Test()]
        public void Invalid()
        {
            string rule = "FREQ=wrong#;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.Convert(rule, startDate);

            Assert.AreEqual(true, RuleToPropertiesConverter.HasError);
			StringAssert.Contains("FREQ has non valid value ", RuleToPropertiesConverter.ErrorMessage);
        }

        [Test()]
        public void ValidDaily()
        {
			string rule = "FREQ=DAILY;";
			DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
			RecurrenceProperties props = RuleToPropertiesConverter.Convert(rule, startDate);
         
			Assert.AreEqual(RecurrenceType.Daily, props.RecurrenceType);
        }

		[Test()]
		public void ValidWeekly()
        {
			string rule = "FREQ=WEEKLY;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.Convert(rule, startDate);
            
			Assert.AreEqual(RecurrenceType.Weekly, props.RecurrenceType);
        }

		[Test()]
        public void ValidMonthly()
        {
            string rule = "FREQ=monthly;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Monthly, props.RecurrenceType);
        }

		[Test()]
        public void ValidYearly()
        {
            string rule = "freq=yearly;";
            DateTime startDate = new DateTime(2018, 09, 01, 10, 0, 0);
            RecurrenceProperties props = RuleToPropertiesConverter.Convert(rule, startDate);

			Assert.AreEqual(RecurrenceType.Yearly, props.RecurrenceType);
        }
    }
}
