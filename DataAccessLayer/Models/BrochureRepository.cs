using DataAccessLayer;
using Library.Web.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Web.Models
{
    public class BrochureRepository
    {
        private string _connectionString;
        private Sql<Brochure> _sql;

        public List<Brochure> Brochures
        {
            get;
            set;
        }
        public BrochureRepository(string connectionString)
        {
            Brochures = new List<Brochure>();
            _connectionString = connectionString;
            _sql = new Sql<Brochure>();
            RefreshData();
        }
        public void RefreshData()
        {
            string sqlExpression = "SELECT * FROM Brochures";
            Brochures.Clear();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                Brochures.Clear();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Brochures.Add(new Brochure
                        {
                            Id = int.Parse(reader.GetValue(0).ToString()),
                            Name = reader.GetValue(1).ToString(),
                            TypeOfCover = reader.GetValue(2).ToString(),
                            NumberOfPages = int.Parse(reader.GetValue(3).ToString())
                        });
                    }
                }

                reader.Close();
            }
        }
        public Brochure GetById(int id)
        {
            string sqlExpression = string.Format("SELECT * FROM Brochures WHERE Id={0}", id);
            Brochure brochure = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    brochure = new Brochure
                    {
                        Id = int.Parse(reader.GetValue(0).ToString()),
                        Name = reader.GetValue(1).ToString(),
                        TypeOfCover = reader.GetValue(2).ToString(),
                        NumberOfPages = int.Parse(reader.GetValue(3).ToString())
                    };
                }
                reader.Close();
            }
            return brochure;
        }
        public void Add(Brochure brochure)
        {
            string sqlExpression = _sql.CreateInsertString(brochure);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        public void Update(Brochure brochure)
        {
            string sqlExpression = _sql.CreateUpdateString(brochure);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = string.Format("DELETE FROM Brochures WHERE Id={0}", id);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}