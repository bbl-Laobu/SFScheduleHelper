using System;
using Syncfusion.SfSchedule.XForms;

namespace Kareke.SFScheduleHelper
{
    public class RecurrenceConverter
    {
		public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

		public RecurrenceProperties Convert(string rule, DateTime startDate)
        {
            ParseRuleToProps parser = new ParseRuleToProps();
            RecurrenceProperties props =  parser.Convert(rule, startDate);
            HasError = parser.HasError;
            ErrorMessage = parser.ErrorMessage;
			return props;
        }
        
		public string Convert(RecurrenceProperties properties)
        {
            ParsePropsToRule parser = new ParsePropsToRule();
            string rule = parser.Convert(properties);
            HasError = parser.HasError;
            ErrorMessage = parser.ErrorMessage;
			return rule;
        }
    }
}
