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

## License

Licensed under MIT, see license file. This is a derivative to Xamarin.Mobile's Media with a cross platform API and other enhancements.

//
//  Copyright 2011-2013, Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
