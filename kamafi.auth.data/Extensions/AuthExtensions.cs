namespace kamafi.auth.data.extensions
{
    public static class AuthExtensions
    {
        public static int? ToNullableInt(this string s)
        {
            int i;

            return int.TryParse(s, out i) 
                ? i
                : null;
        }

        public static Guid? ToNullableGuid(this string s)
        {
            Guid i;

            return Guid.TryParse(s, out i)
                ? i
                : null;
        }
    }
}
