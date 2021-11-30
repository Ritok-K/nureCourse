using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class CurrentUserOredrList : IDataFilter
    {
        string Field => MovieDB.BuildFieldName(MovieDB.c_OrdersTable, MovieDB.c_UserIdColumn);
        string Parameter => MovieDB.BuildParameterName(MovieDB.c_OrdersTable, MovieDB.c_UserIdColumn);

        IEnumerable<string> IDataFilter.GetWhereClauses() 
        {
            var where = $"{Field} = {Parameter}";
            return new string[] { where }; 
        }

        void IDataFilter.AddCommandParameters(MySqlCommand command)
        {
            command.Parameters.AddWithValue(Parameter, Program.DB.CurrentUser.Id);
        }
    }
}
