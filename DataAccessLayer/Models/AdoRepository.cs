using DataAccessLayer.Abstract;
using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Entities.Entities;

namespace DataAccessLayer.Models
{
    public class AdoRepository<T> : IGenericRepository<T> where T: TEntity
    {
        private string _connectionString;
        private Sql<T> _sql;
        private List<T> _list;

        public AdoRepository(string connectionString)
        {
            _list = new List<T>();
            _connectionString = connectionString;
            _sql = new Sql<T>();
            RefreshData();
        }

        public void RefreshData()
        {
            string sqlExpression = _sql.CreateSelectAllString();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                _list.Clear();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        _list.Add(GetEntity(reader));
                    }
                }

                reader.Close();
            }
        }
        public void Add(T item)
        {
            string sqlExpression = _sql.CreateInsertString(item);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public T FindById(int id)
        {
            string sqlExpression = _sql.CreateSelectByIdString(id);
            T entity = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    entity = GetEntity(reader);
                }
                reader.Close();
            }
            return entity;
        }
        public IEnumerable<T> GetAll()
        {
            return _list;
        }
        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _list.Where(predicate).ToList();
        }
        public void Remove(int id)
        {
            string sqlExpression = _sql.CreateDeleteString(id);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        public void Update(T item)
        {
            T entity = (T)item;
            string sqlExpression = _sql.CreateUpdateString(entity);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        private T GetEntity(SqlDataReader reader)
        {
            T entity = Activator.CreateInstance<T>();

            int number = 0;
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.PropertyType == typeof(int))
                {
                    entity.GetType().GetProperty(property.Name).SetValue(entity, int.Parse(reader.GetValue(number).ToString()));
                }
                if (property.PropertyType == typeof(string))
                {
                    entity.GetType().GetProperty(property.Name).SetValue(entity, reader.GetValue(number).ToString());
                }
                number++;
            }
            return entity;
        }
    }
}
