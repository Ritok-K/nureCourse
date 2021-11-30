using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB
{
    interface IDataFilter
    {
        IEnumerable<string> GetJoinClauses() { return Enumerable.Empty<string>(); }
        IEnumerable<string> GetWhereClauses() { return Enumerable.Empty<string>(); }
        IEnumerable<string> GetOrderClauses() { return Enumerable.Empty<string>(); }
        IEnumerable<string> GetGroupByClauses() { return Enumerable.Empty<string>(); }

        void AddCommandParameters(MySqlCommand command) {}
    }
}
