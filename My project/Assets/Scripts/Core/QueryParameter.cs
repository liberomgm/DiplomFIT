using System.Data;

namespace Core
{
    public class QueryParameter
    {
        public object Value { get; set; }
        
        public string ColumnName { get; set; }
        
        public DbType DbType { get; set; }
    }
}