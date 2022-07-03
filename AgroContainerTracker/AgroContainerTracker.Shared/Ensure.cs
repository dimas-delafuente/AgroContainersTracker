namespace AgroContainerTracker.Shared
{
    public static class Ensure
    {
        public static void NotNull(this object obj, string objName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(objName);
            }
        }

        public static void Positive(this int number, string objName)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(objName);
            }
        }

        public static void NotEmpty(this string value, string stringName)
        {
            if (!string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(value, stringName);
            }
        }
    }
}