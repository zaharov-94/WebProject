using DataAccessLayer;
using Library.Web.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Web.Models
{
    public class MagazineRepository
    {
        private string _connectionString;
        private Sql<Book> _sql;

        public List<Magazine> Magazines
        {
            get;
            set;
        }
        public MagazineRepository(string connectionString)
        {
            Magazines = new List<Magazine>();
            _connectionString = connectionString;
            _sql = new Sql<Book>();
            RefreshData();
        }
        public void RefreshData()
        {
            string sqlExpression = "SELECT * FROM Magazines";
            Magazines.Clear();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                Magazines.Clear();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Magazines.Add(new Magazine
                        {
                            Id = int.Parse(reader.GetValue(0).ToString()),
                            Name = reader.GetValue(1).ToString(),
                            Number = int.Parse(reader.GetValue(2).ToString()),
                            YearOfPublishing = int.Parse(reader.GetValue(3).ToString())
                        });
                    }
                }

                reader.Close();
            }
        }
        public Magazine GetById(int id)
        {
            string sqlExpression = string.Format("SELECT * FROM Magazines WHERE Id={0}", id);
            Magazine magazine = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    magazine = new Magazine
                    {
                        Id = int.Parse(reader.GetValue(0).ToString()),
                        Name = reader.GetValue(1).ToString(),
                        Number = int.Parse(reader.GetValue(2).ToString()),
                        YearOfPublishing = int.Parse(reader.GetValue(3).ToString())
                    };
                }
                reader.Close();
            }
            return magazine;
        }
        public void Add(Magazine magazine)
        {
            string sqlExpression = _sql.CreateInsertString(magazine);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        public void Update(Magazine magazine)
        {
            string sqlExpression = _sql.CreateUpdateString(magazine);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = string.Format("DELETE FROM Magazines WHERE Id={0}", id);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}