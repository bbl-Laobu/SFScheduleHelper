using System;
using Syncfusion.SfSchedule.XForms;

namespace Kareke.SFScheduleHelper
{
    public class RecurrenceConverter
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Kareke.SFScheduleHelper.RecurrenceConverter"/> has error.
        /// </summary>
        /// <value><c>true</c> if has error; otherwise, <c>false</c>.</value>
		public bool HasError { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Convert the specified rule and startDate into a recurrence property object.
        /// </summary>
        /// <returns>A recurrence property object. On conversion error: return null, HasError and ErrorMessage are set</returns>
        /// <param name="rule">Rule.</param>
        /// <param name="startDate">Start date.</param>
		public RecurrenceProperties Convert(string rule, DateTime startDate)
        {
            ParseRuleToProps parser = new ParseRuleToProps();
            RecurrenceProperties props =  parser.Convert(rule, startDate);
            HasError = parser.HasError;
            ErrorMessage = parser.ErrorMessage;
			return props;
        }

        /// <summary>
        /// Convert the specified properties into a Recurrence Rule.
        /// </summary>
        /// <returns>A recurrence rule. On conversion error: return null, HasError and ErrorMessage are set</returns>>
        /// <param name="properties">Properties.</param>
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
