using System;
using System.Collections;

namespace Repository.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsPrimitive ||
                ((IList)new[]
                {
                    typeof(long?),
                    typeof(string),
                    typeof(decimal),
                    typeof(decimal?),
                    typeof(DateTime),
                    typeof(DateTime?),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid),
                }).Contains(type) ||
                type.IsEnum ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetGenericArguments()[0]));
        }
    }
}