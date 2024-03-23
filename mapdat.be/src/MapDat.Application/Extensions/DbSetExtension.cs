using MapDat.Application.Extensions;
using System.Linq.Expressions;

namespace MapDat.Persistance.Extensions
{
    public static class DbSetExtension
    {
        public static IQueryable<TEntity> ForSearchTerm<TEntity>(this IQueryable<TEntity> query, string? searchTerm, Func<string, Expression<Func<TEntity, bool>>> filterExpression)
        {
            if (searchTerm == null || searchTerm.Length == 0)
            {
                return query;
            }

            string[] words = searchTerm.Trim().Split(" ");
            //Expression<Func<TEntity, bool>> exp = c => true;

            foreach (var word in words)
            {
                query = query.Where(filterExpression(word.ToLikeExpression()));
            }

            return query;
        }
    }
}
