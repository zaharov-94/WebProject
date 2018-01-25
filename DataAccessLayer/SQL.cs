using System.Reflection;

namespace DataAccessLayer
{
    class Sql<T> where T : class
    {
        public string CreateInsertString(T entity)
        {
            string sqlExpression = string.Empty;
            string thisType = entity.GetType().Name + "s";
            foreach (PropertyInfo propertyName in entity.GetType().GetProperties())
            {
                if ((propertyName.Name != "Id") && (sqlExpression != ""))
                {
                    sqlExpression += ", ";
                }
                if (propertyName.Name != "Id")
                {
                    sqlExpression += propertyName.Name;
                }
            }

            sqlExpression = "INSERT INTO " + thisType + " (" + sqlExpression + ") VALUES(";
            foreach (PropertyInfo propertyValue in entity.GetType().GetProperties())
            {
                if ((propertyValue.Name != "Id") && (propertyValue.GetValue(entity).GetType() == typeof(string)))
                {
                    sqlExpression += "'" + propertyValue.GetValue(entity) + "', ";
                }
                if ((propertyValue.Name != "Id") && (propertyValue.GetValue(entity).GetType() == typeof(int)))
                {
                    sqlExpression += propertyValue.GetValue(entity) + ", ";
                }
            }
            sqlExpression = sqlExpression.Substring(0, sqlExpression.Length - 2);
            sqlExpression += ")";
            return sqlExpression;
        }

        public string CreateUpdateString(T entity)
        {
            string thisType = entity.GetType().Name + "s";
            string sqlExpression = sqlExpression = "UPDATE " + thisType + " SET ";
            string id = string.Empty;
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {

                if (property.Name != "Id")
                {
                    sqlExpression += property.Name + "=";
                }
                if ((property.Name != "Id") && (property.GetValue(entity).GetType() == typeof(string)))
                {
                    sqlExpression += "'" + property.GetValue(entity) + "', ";
                }
                if ((property.Name != "Id") && (property.GetValue(entity).GetType() == typeof(int)))
                {
                    sqlExpression += property.GetValue(entity) + ", ";
                }
            }
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                if (property.Name == "Id")
                {
                    id += property.GetValue(entity);
                    break;
                }
            }
            sqlExpression = sqlExpression.Substring(0, sqlExpression.Length - 2);
            sqlExpression += " WHERE Id=" + id;
            return sqlExpression;
        }
    }
}
