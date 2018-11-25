using System.Collections.Generic;

namespace Mints.BLayer.Helpers
{
    static class CollectionHelper
    {
        public static HashSet<TModel> ToSet<TModel> (this IEnumerable<TModel> models) =>
            new HashSet<TModel>(models);


        public static HashSet<TModel> ToSet<TModel> (this ISet<TModel> models) =>
            new HashSet<TModel>(models);


        public static HashSet<TModel> ToSet<TModel> (this ICollection<TModel> models) =>
            new HashSet<TModel>(models);
    }
}
