using MinecraftSpelunking.Common;

namespace System.Linq
{
    public static class IQueryableExtensions
    {
        public static Page<T> Page<T>(this IQueryable<T> queryable, int number, int size)
        {
            int count = (number - 1) * size;
            return new Page<T>()
            {
                Number = number,
                Size = size,
                Total = (queryable.Count() + size - 1) / size,
                Items = queryable.Skip(count).Take(size).ToArray()
            };
        }
    }
}
