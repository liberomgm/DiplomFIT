using System.Collections;
using System.Data;

namespace Core
{
    public sealed class QueryParametersCollection : CollectionBase
    {
        public void Add(QueryParameter param)
        {
            List.Add(param);
        }
        
        public void Add(string columnName, object value, DbType dbType)
        {
            var iparam = new QueryParameter();
            iparam.ColumnName = columnName;
            iparam.Value = value;
            iparam.DbType = dbType;
            List.Add(iparam);
        }
        
        public QueryParameter this[int Index] => (QueryParameter)List[Index];
    }
}