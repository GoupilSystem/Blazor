using System.Globalization;

namespace Birk.Client.Bestilling.Utils.Helpers
{
    public static class StringHelper
    {
        public static string ConvertFødt(DateTime? født)
        {
            if (født == null) return "";

            return $"{født.Value.Day}/{født.Value.Month}/{født.Value.Year}";
        }

        public static DateTime? ConvertFnrToFødt(string fnr)
        {
            if (string.IsNullOrEmpty(fnr)) return null;

            string strFødt = fnr.Substring(0, 6);
            
            if (DateTime.TryParseExact(strFødt, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime født))
            {
                return født;
            }

            return null;
        }

        public static string CalculateAge(DateTime birthdate)
        {
            DateTime today = DateTime.Today;
            int ageInYears = today.Year - birthdate.Year;
            int months = today.Month - birthdate.Month;
            if (birthdate > today.AddYears(-ageInYears))
            {
                ageInYears--;
                months = 12 - birthdate.Month + today.Month;
            }

            if (months == 12)
            {
                ageInYears++;
                months = 0;
            }

            string age = $"{ageInYears} år{(ageInYears != 1 ? "" : "e")}";

            if (months > 0)
            {
                age += $" og {months} måned{(months != 1 ? "er" : "")}";
            }

            return age;
        }

        public static string ConvertGender(int numericGender)
        {
            string gender = "";
            switch (numericGender)
            {
                case 1:
                    gender = "Gutt";
                    break;
                case 2:
                    gender = "Jente";
                    break;
                case 3:
                    gender = "Ukjent";
                    break;
                default:
                    break;
            }
            return gender;
        }
    }
}
