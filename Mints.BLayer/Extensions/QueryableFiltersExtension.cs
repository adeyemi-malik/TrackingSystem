using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mints.BLayer.Extensions
{
    public static class QueryableFiltersExtension
    {
           
        public static IQueryable<Farmer> FilterByEmail(this IQueryable<Farmer> query, string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                return query;
            }
            else
            {
                return query.Where(f => f.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
        
        }

        public static IQueryable<User> FilterByEmail(this IQueryable<User> query, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return query;
            }
            else
            {
                return query.Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }

        }
    }
}
