using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace MovieStore.DB.Filters
{
    class EmptyFilter : IDataFilter
    {
        public virtual IEnumerable<string> GetSelectClauses() { return Enumerable.Empty<string>(); }
        public virtual IEnumerable<string> GetJoinClauses() { return Enumerable.Empty<string>(); }
        public virtual IEnumerable<string> GetWhereClauses() { return Enumerable.Empty<string>(); }
        public virtual IEnumerable<string> GetOrderClauses() { return Enumerable.Empty<string>(); }
        public virtual IEnumerable<string> GetGroupByClauses() { return Enumerable.Empty<string>(); }

        public virtual void AddCommandParameters(MySqlCommand command) { }
    }
}
