using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mints.BLayer.Extensions
{
    public static class DateFilterExtension
    {
        public static IQueryable<T> FilterByDateFrom<T>(this IQueryable<T> query, DateTime? dateFrom) where T : BaseEntity
        {
            if(dateFrom.HasValue)
            {
                return query.Where(t => t.DateCreated.Date >= dateFrom.Value.Date);
            }
            return query;
        }

        public static IQueryable<T> FilterByDateTo<T>(this IQueryable<T> query, DateTime? dateTo) where T : BaseEntity
        {
            if (dateTo.HasValue)
            {
                return query.Where(t => t.DateCreated.Date <= dateTo.Value.Date);
            }
            return query;
        }

        public static IQueryable<T> FilterBySpecificDate<T>(this IQueryable<T> query, DateTime? dateFor) where T : BaseEntity
        {
            if (dateFor.HasValue)
            {
                return query.Where(t => t.DateCreated.Date == dateFor.Value.Date);
            }
            return query;
        }

        public static IEnumerable<T> FilterByDateFrom<T>(this IEnumerable<T> query, DateTime? dateFrom) where T : BaseEntity
        {
            if (dateFrom.HasValue)
            {
                return query.Where(t => t.DateCreated.Date >= dateFrom.Value.Date);
            }
            return query;
        }

        public static IEnumerable<T> FilterByDateTo<T>(this IEnumerable<T> query, DateTime? dateTo) where T : BaseEntity
        {
            if (dateTo.HasValue)
            {
                return query.Where(t => t.DateCreated.Date <= dateTo.Value.Date);
            }
            return query;
        }

        public static IEnumerable<T> FilterBySpecificDate<T>(this IEnumerable<T> query, DateTime? dateFor) where T : BaseEntity
        {
            if (dateFor.HasValue)
            {
                return query.Where(t => t.DateCreated.Date == dateFor.Value.Date);
            }
            return query;
        }
    }
}
