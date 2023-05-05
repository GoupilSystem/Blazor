using System.Text.RegularExpressions;

namespace Birk.Client.Bestilling.Utils.Validator
{
    public static class FødselsnummerValidator
    {
        private static readonly int[] _k1Digits = { 3, 7, 6, 1, 8, 9, 4, 5, 2 };
        private static readonly int[] _k2Digits = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
        private static int _kontroll1;
        private static int _kontroll2;

        private static int _dagUfiltrert;
        private static int _dag;
        private static int _måned;
        private static int _år;
        public static int _individsifre;

        static List<ValidRange> ValidRanges = new List<ValidRange>()
        {
            new ValidRange(0, 499, 1900, 00, 99),
            new ValidRange(500, 749, 1800, 54, 99),
            new ValidRange(500, 999, 2000, 00, 39),
            new ValidRange(900, 999, 1900, 40, 99),
        };

        public static bool IsNumeric(string fnr)
        {
            return !string.IsNullOrEmpty(fnr) && Regex.IsMatch(fnr, "^[0-9]*$");
        }

        public static bool Is11Digits(string fnr)
        {
            return Regex.IsMatch(fnr, "^[0-9]{11}$");
        }

        public static bool IsValid(string fnr)
        {
            _dagUfiltrert = int.Parse(fnr.Substring(0, 2));
            _dag = _dagUfiltrert % 40;
            _måned = int.Parse(fnr.Substring(2, 2));
            _år = int.Parse(fnr.Substring(4, 2));
            _individsifre = int.Parse(fnr.Substring(6, 3));

            var fullYear = GetFullYear();
            if (fullYear >= 2100) return false;

            var birthdate = GetBirthdate();
            if (birthdate == null) return false;

            var digits9 = GetDigits(fnr);

            _kontroll1 = int.Parse(fnr.Substring(9, 1));
            var k1 = 11 - _k1Digits.Select((d, i) => d * digits9[i]).Sum() % 11;
            if (k1 == 11) k1 = 0;
            if (k1 < 0 || k1 > 9 || k1 != _kontroll1) return false;

            _kontroll2 = int.Parse(fnr.Substring(10, 1));
            var digits10 = digits9.Concat(new[] { k1 }).ToArray();
            var k2 = 11 - _k2Digits.Select((d, i) => d * digits10[i]).Sum() % 11;
            if (k2 == 11) k2 = 0;
            if (k2 < 0 || k2 > 9 || k2 != _kontroll2) return false;

            return true;
        }

        private static int GetFullYear()
        {
            var range = ValidRanges.FirstOrDefault(r => r.IsInside(_år, _individsifre));
            if (range == null) return 2100 + _år;
            return range.Century + _år;
        }

        private static DateTime? GetBirthdate()
        {
            DateTime birthdate;
            if (DateTime.TryParse($"{GetFullYear()}-{_måned}-{_dag}", out birthdate))
            {
                return birthdate;
            }
            return null;
        }

        private static int[] GetDigits(string str)
        {
            return str.Select(c => c - '0').ToArray();
        }

        private class ValidRange
        {
            public int Century { get; }
            private int _start { get; }
            private int _end { get; }
            private int _startYear { get; }
            private int _endYear { get; }

            public ValidRange(int start, int end, int century, int startYear, int endYear)
            {
                _start = start;
                _end = end;
                Century = century;
                _startYear = startYear;
                _endYear = endYear;
            }

            public bool IsInside(int year, int individSifre)
            {
                return individSifre <= _end && individSifre >= _start &&
                       year <= _endYear && year >= _startYear;
            }
        }
    }
}
