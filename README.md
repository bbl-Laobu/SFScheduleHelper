# SFScheduleHelper
Xamarin.Forms Syncfusion SFSchedule Helper methods for recurring appointments

## Functions
Fully unit tested Rule <=> Properties converter for reccuring Appointments.

## How to use
Rule=>Properties:<br/> 
RecurrenceProperties properties = RecurrenceConverter.Convert(recurenceRule, startDate);<br/>
<br/>
Properties=>Rule:<br/> 
string rule = RecurrenceConverter.Convert(recurrenceProperties);<br/> 

## INSTALL
option 1: Create Nuget Package and import into your project<br/>
Option 1: Copy and Paste the RuleToProperties Class into your project 

## Links
Appointment Recurrence Docs: https://help.syncfusion.com/xamarin/sfschedule/data-bindings#recurrence-appointment
Plugin Docs: https://help.syncfusion.com/xamarin/sfschedule/overview
