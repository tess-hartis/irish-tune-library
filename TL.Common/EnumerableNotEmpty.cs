namespace TL.Common;

public static class EnumerableNotEmpty
{
    public static bool IsAny<T>(this IEnumerable<T>? data)
    {
        return data != null && data.Any();
    }

}