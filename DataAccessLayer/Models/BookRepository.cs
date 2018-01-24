using Library.Web.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace Library.Web.Models
{
    public class BookRepository
    {
        private string _connectionString;

        public List<Book> Books
        {
            get;
            set;
        }
        public BookRepository(string connectionString)
        {
            Books = new List<Book>();
            _connectionString = connectionString;
            RefreshData();
        }
        public void RefreshData()
        {
            string sqlExpression = "SELECT * FROM Books";
            Books.Clear();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                Books.Clear();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Books.Add(new Book { Id = int.Parse(reader.GetValue(0).ToString()), Name = reader.GetValue(1).ToString(),
                            Author = reader.GetValue(2).ToString(), YearOfPublishing = int.Parse(reader.GetValue(3).ToString()) });
                    }
                }

                reader.Close();
            }
        }
        public Book GetById(int id)
        {
            string sqlExpression = string.Format("SELECT * FROM Books WHERE Id={0}", id);
            Book book = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    book = new Book
                    {
                        Id = int.Parse(reader.GetValue(0).ToString()),
                        Name = reader.GetValue(1).ToString(),
                        Author = reader.GetValue(2).ToString(),
                        YearOfPublishing = int.Parse(reader.GetValue(3).ToString())
                    };
                }
                reader.Close();
            }
            return book;
        }
        public void Add(Book book)
        {
            string sqlExpression = string.Empty;
            foreach(string property in GetProperties())
            {
                if (sqlExpression.Length != 0)
                {
                    sqlExpression += ", " + property;
                }
                if (sqlExpression.Length == 0)
                {
                    sqlExpression += property;
                }    
            }

            sqlExpression = string.Format("INSERT INTO Books ({0}) VALUES ('{1}', '{2}', {3})", sqlExpression, book.Name, book.Author, book.YearOfPublishing);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        public void Update(Book book)
        {
            string sqlExpression = string.Format("UPDATE Books SET Name='{0}', Author='{1}', YearOfPublishing={2} WHERE Id={3}", book.Name, book.Author, book.YearOfPublishing, book.Id);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = string.Format("DELETE FROM Books WHERE Id={0}", id);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        private List<string> GetProperties()
        {
            List<string> properties = new List<string>();
            foreach (PropertyInfo myPropInfo in typeof(Book).GetProperties())
            {
                if (myPropInfo.Name != "Id")
                {
                    properties.Add(myPropInfo.Name);
                }
            }
            return properties;
        }
    }
}