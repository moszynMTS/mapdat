namespace MapDat.Application.Extensions
{
    public static class StringExtension
    {
        public static string ToLikeExpression(this string value) => $"%{value}%";
    }
}
