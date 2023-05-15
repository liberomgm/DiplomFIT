using System.Collections.Generic;
using Core;

namespace DataBase
{
    public class SelectConstructor
    {
        private const string Distinct = "distinct";
        private const string ColumnsName = "columns";
        private const string FromName = "from";
        private const string Union = "union";
        private const string WhereName = "where";
        private const string GroupName = "group";
        private const string HavingName = "having";
        private const string OrderName = "order";
        private const string LimitCount = "limitcount";
        private const string LimitOffset = "limitoffset";
        private const string ForUpdate = "forupdate";

        private const string InnerJoin = "inner join";
        private const string LeftJoin = "left join";
        private const string RightJoin = "right join";
        private const string FullJoin = "full join";
        private const string CrossJoin = "cross join";
        private const string NaturalJoin = "natural join";

        private const string SqlWildcard = "*";
        private const string SqlSelect = "SELECT";
        private const string SqlUnion = "UNION";
        private const string SqlUnionAll = "UNION ALL";
        private const string SqlFrom = "FROM";
        private const string SqlWhere = "WHERE";
        private const string SqlDistinct = "DISTINCT";
        private const string SqlGroupBy = "GROUP BY";
        private const string SqlOrderBy = "ORDER BY";
        private const string SqlHaving = "HAVING";
        private const string SqlForUpdate = "FOR UPDATE";
        private const string SqlAND = "AND";
        private const string SqlAs = "AS";
        private const string SqlOr = "OR";
        private const string SqlON = "ON";
        private const string SqlAsc = "ASC";
        private const string SqlDesc = "DESC";
        private const string SqlLimit = "LIMIT";


        private string from = string.Empty;
        private string columns = "*";
        private string where = string.Empty;
        private string group = string.Empty;
        private string having = string.Empty;
        private string order = string.Empty;
        private int limitCount = 0, limitOffset = 0;
        private string lastError = string.Empty;

        private readonly List<JoinObj> collectionJoin = new List<JoinObj>();


        public string SelectCommand => _constructor();


        public string LastError => lastError;

        public override string ToString()
        {
            return _constructor();
        }

        private class JoinObj
        {
            private readonly SqlJoinTypes type;


            public string Conditional { get; }

            public string Name { get; }

            public string SqlJoinType
            {
                get
                {
                    return type switch
                    {
                        SqlJoinTypes.InnerJoin => InnerJoin,
                        SqlJoinTypes.LeftJoin => LeftJoin,
                        SqlJoinTypes.RightJoin => RightJoin,
                        SqlJoinTypes.FullJoin => FullJoin,
                        SqlJoinTypes.CrossJoin => CrossJoin,
                        _ => ""
                    };
                }
            }

            public JoinObj(string name, string conditional, SqlJoinTypes type)
            {
                Name = name;
                Conditional = conditional;
                this.type = type;
            }
        }


        public SelectConstructor From(string tableName)
        {
            from = tableName;
            return this;
        }

        public SelectConstructor From(string tableName, string[] columns)
        {
            from = tableName;

            if (columns == null || columns.Length == 0)
            {
                this.columns = SqlWildcard;
            }
            else
                this.columns = ColumnsToLine(columns);

            return this;
        }

        public SelectConstructor From(string tableName, string columns)
        {
            from = tableName;

            if (string.IsNullOrEmpty(columns))
            {
                this.columns = SqlWildcard;
            }
            else
                this.columns = columns;

            return this;
        }

        public SelectConstructor Columns(string[] columns)
        {
            if (columns == null || columns.Length == 0)
            {
                this.columns = SqlWildcard;
            }
            else
                this.columns = ColumnsToLine(columns);

            return this;
        }

        public SelectConstructor Columns(string columns)
        {
            if (string.IsNullOrEmpty(columns))
            {
                this.columns = SqlWildcard;
            }
            else
                this.columns = columns;

            return this;
        }

        public SelectConstructor Where(string where)
        {
            this.where = " " + where;
            return this;
        }

        public SelectConstructor Join(string name, string conditional, SqlJoinTypes type)
        {
            collectionJoin.Add(new JoinObj(name, conditional, type));
            return this;
        }

        public SelectConstructor Group(string group)
        {
            this.group = group;
            return this;
        }

        public SelectConstructor Having(string having)
        {
            this.having = having;
            return this;
        }

        public SelectConstructor Order(string order)
        {
            this.order = order;
            return this;
        }

        public SelectConstructor Limit(int count)
        {
            limitCount = count;
            limitOffset = 0;
            return this;
        }

        public SelectConstructor Limit(int count, int offset)
        {
            limitCount = count;
            limitOffset = offset;
            return this;
        }

        private string _constructor()
        {
            var sqlCommand = string.Empty;
            if (from.Length == 0)
            {
                lastError = "�� ���������� ��� �������";
                return "";
            }

            sqlCommand = $"SELECT {columns} {SqlFrom} {from} ";
            foreach (var join in collectionJoin)
            {
                sqlCommand += $"{join.SqlJoinType} {join.Name} ON {join.Conditional} ";
            }
            
            sqlCommand += where.Length > 0 ? SqlWhere + " " + where : "";
            
            if (group.Length > 0)
            {
                sqlCommand += " " + SqlGroupBy + " " + group;
            }
            
            if (having.Length > 0)
            {
                sqlCommand += " " + SqlHaving + " " + having;
            }
            
            if (order.Length > 0)
            {
                sqlCommand += " " + SqlOrderBy + " " + order;
            }
            
            if (limitCount > 0)
            {
                if (limitOffset > 0)
                {
                    sqlCommand += " " + SqlLimit + " " + limitOffset;
                    sqlCommand += "," + limitCount;
                }
                else
                {
                    sqlCommand += " " + SqlLimit + " " + limitCount;
                }
            }


            return sqlCommand;
        }

        private string ColumnsToLine(string[] columns)
        {
            var textOfColumns = string.Empty;
            if (columns == null || columns.Length == 0)
                textOfColumns = "*";
            else
            {
                textOfColumns = string.Join(",", columns);
            }

            return textOfColumns;
        }
    }
}