using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MovieStore.DB.Filters
{
    class CurrentUserOrderList : EmptyFilter
    {
        string Field => MovieDB.BuildFieldName(MovieDB.c_OrdersTable, MovieDB.c_UserIdColumn);
        string Parameter => MovieDB.BuildParameterName(MovieDB.c_OrdersTable, MovieDB.c_UserIdColumn);

        public override IEnumerable<string> GetWhereClauses() 
        {
            var where = $"{Field} = {Parameter}";
            return new string[] { where }; 
        }

        public override void AddCommandParameters(MySqlCommand command)
        {
            command.Parameters.AddWithValue(Parameter, Program.DB.CurrentUser.Id);
        }
    }
}
