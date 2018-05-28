namespace Kareke.SFScheduleHelper
{
    public static class WeekdayConverter
    {
        public static int Convert(string weekDay)
        {
            weekDay = string.IsNullOrEmpty(weekDay) ? string.Empty : weekDay.ToUpper().Trim();
            switch (weekDay)
            {
                case "SU": return 0;
                case "MO": return 1;
                case "TU": return 2;
                case "WE": return 3;
                case "TH": return 4;
                case "FR": return 5;
                case "SA": return 6;
                default: return -1;
            }
        }

        public static string Convert(int weekDay)
        {
            switch (weekDay)
            {
                case 0 : return "SU";
                case 1 : return "MO";
                case 2 : return "TU";
                case 3 : return "WE";
                case 4 : return "TH";
                case 5 : return "FR";
                case 6 : return "SA";
                default: return string.Empty;
            }
        }
    }
}
