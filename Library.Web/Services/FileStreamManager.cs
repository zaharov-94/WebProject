using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Library.Web
{
    public class FileStreamManager
    {
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private string _absolutePathToFile;

        public FileStreamManager()
        {
            string appDataPath = System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data");
            string fileName = "Books.txt";
            _absolutePathToFile = Path.Combine(appDataPath, fileName);
        }
        public void Write(Book book)
        {
            _streamWriter = new StreamWriter(_absolutePathToFile, true);
            string text = String.Format("Author:{0} Name:{1} Year:{2}", book.Author, book.Name, book.YearOfPublishing);
            _streamWriter.WriteLine(text);
            _streamWriter.Close();
        }
        public List<Book> Read()
        {
            _streamReader = new StreamReader(_absolutePathToFile);
            string currentString = "";
            string pattern = @"\w*\u003A";
            string[] stringParts;
            List<Book> books = new List<Book>();
            while (currentString != null)
            {
                currentString = _streamReader.ReadLine();
                if (currentString != null)
                {
                    stringParts = Regex.Split(currentString, pattern);
                    books.Add(new Book { Author = stringParts[1], Name = stringParts[2], YearOfPublishing = int.Parse(stringParts[3]) });
                }  
            }
            _streamReader.Close();
            return books;
        }
    }
}