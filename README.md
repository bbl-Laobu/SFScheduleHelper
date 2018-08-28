# SFScheduleHelper
Xamarin.Forms Syncfusion SFSchedule Helper methods for recurring appointments

## Functions
Rule <=> Properties converter for recurring appointments.

## How to use
Rule=>Properties:<br/> 
RecurrenceConverter converter = new RecurrenceConverter();<br/>
RecurrenceProperties properties = converter.Convert(recurenceRule, startDate);<br/>
<br/>
Properties=>Rule:<br/>
RecurrenceConverter converter = new RecurrenceConverter();<br/>
string rule = converter.Convert(recurrenceProperties); <br/> 

## Install
Option 1: Copy and Paste the classes into your project <br/>
Option 2: Create Nuget Package and import into your project <br/>

## Links
Appointment Recurrence Docs: https://help.syncfusion.com/xamarin/sfschedule/data-bindings#recurrence-appointment
Plugin Docs: https://help.syncfusion.com/xamarin/sfschedule/overview
