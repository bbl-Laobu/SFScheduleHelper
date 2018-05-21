using System;
using System.Runtime.CompilerServices;
using Syncfusion.SfSchedule.XForms;

namespace Kareke.SFScheduleHelper
{
    public static class RecurrenceConverter
    {
		public static bool HasError { get; set; }
        public static string ErrorMessage { get; set; }

		public static RecurrenceProperties Convert(string rule, DateTime startDate)
        {
			RecurrenceProperties props =  ParseRuleToProps.Convert(rule, startDate);
			HasError = ParseRuleToProps.HasError;
			ErrorMessage = ParseRuleToProps.ErrorMessage;
			return props;
        }
        
		public static string Convert(RecurrenceProperties properties)
        {
			string rule = ParsePropsToRule.Convert(properties);
			HasError = ParsePropsToRule.HasError;
			ErrorMessage = ParsePropsToRule.ErrorMessage;
			return rule;
        }
    }
}
