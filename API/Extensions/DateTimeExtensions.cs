namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly bd)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - bd.Year;

            // Si el cumpleaños aún no ha pasado este año, decrementamos la edad
            if (bd > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}