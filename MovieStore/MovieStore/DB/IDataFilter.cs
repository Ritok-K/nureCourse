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
        IEnumerable<string> GetSelectClauses();
        IEnumerable<string> GetJoinClauses();
        IEnumerable<string> GetWhereClauses();
        IEnumerable<string> GetOrderClauses();
        IEnumerable<string> GetGroupByClauses();

        void AddCommandParameters(MySqlCommand command);
    }
}
