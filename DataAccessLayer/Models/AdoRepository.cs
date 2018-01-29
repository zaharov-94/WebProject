using DataAccessLayer.Abstract;
using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DataAccessLayer.Models
{
    public class AdoRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private string _connectionString;
        private Sql<TEntity> _sql;
        private List<TEntity> _list;

        public AdoRepository(string connectionString)
        {
            _list = new List<TEntity>();
            _connectionString = connectionString;
            _sql = new Sql<TEntity>();
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
        public void Add(TEntity item)
        {
            string sqlExpression = _sql.CreateInsertString(item);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public TEntity FindById(int id)
        {
            string sqlExpression = _sql.CreateSelectByIdString(id);
            TEntity entity = null;
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
        public IEnumerable<TEntity> GetAll()
        {
            return _list;
        }
        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
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
        public void Update(TEntity item)
        {
            string sqlExpression = _sql.CreateUpdateString(item);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        private TEntity GetEntity(SqlDataReader reader)
        {
            TEntity entity = Activator.CreateInstance<TEntity>();

            int number = 0;
            foreach (PropertyInfo property in typeof(TEntity).GetProperties())
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
