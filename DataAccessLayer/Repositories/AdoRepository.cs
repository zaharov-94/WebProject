using DataAccessLayer.Abstract;
using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Entities.Entities;
using Library.DAL;

namespace DataAccessLayer.Models
{
    public class AdoRepository<T> : IGenericRepository<T> where T: TEntity
    {
        private string _connectionString;
        private SqlStringCreator<T> _sql;
        private List<T> _list;

        public AdoRepository(string connectionString)
        {
            _list = new List<T>();
            _connectionString = connectionString;
            _sql = new SqlStringCreator<T>();
            RefreshData();
        }

        public void RefreshData()
        {
            string sqlExpression = _sql.CreateSelectAllString(); 
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    _list.Clear();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _list.Add(GetEntity(reader));
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
            }
        }
        public void Add(T item)
        {
            try
            {
                string sqlExpression = _sql.CreateInsertString(item);
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
            }
        }

        public T FindById(int id)
        { 
            try
            {
                string sqlExpression = _sql.CreateSelectByIdString(id);
                T entity = null;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        entity = GetEntity(reader);
                    }
                    reader.Close();
                }
                return entity;
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
                return null;
            }
        }
        public IEnumerable<T> GetAll()
        {
            
            try
            {
                return _list;
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
                return null;
            }
        }
        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            try
            {
                return _list.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
                return null;
            }
        }
        public void Remove(int id)
        {
            try
            {
                string sqlExpression = _sql.CreateDeleteString(id);
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
            }
        }
        public void Update(T item)
        {
            T entity = (T)item;
            try
            {
                string sqlExpression = _sql.CreateUpdateString(entity);
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
            } 
        }
        private T GetEntity(SqlDataReader reader)
        {
            try
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
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
                return null;
            }
            
        }
    }
}
