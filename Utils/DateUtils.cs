namespace ClockInTimeWeb.Utils
{
    public static class DateUtils
    {
        public static DateOnly ParseStringToDateOnly(string data)
        {
            if (DateOnly.TryParse(data, out DateOnly dateOnly))
            {
                return dateOnly;
            }
            else
            {
                throw new ArgumentException("A string de data não está em um formato válido.");
            }
        }
    }
}
