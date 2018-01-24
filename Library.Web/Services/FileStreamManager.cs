using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace Library.Web
{
    public class FileStreamManager
    {
        private string _absolutePathToFile;

        public FileStreamManager()
        {
            string appDataPath = System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data");
            string fileName = "Books.txt";
            _absolutePathToFile = Path.Combine(appDataPath, fileName);
        }
        public void Write(Object entity)
        {
            Book book;
            Magazine magazine;
            XElement xElement = null;

            XDocument xmlDocument = XDocument.Load(_absolutePathToFile);
            if (entity is Book)
            {
                book = (Book)entity;
                xElement = new XElement("book",
                            new XElement("name", book.Name),
                            new XElement("author", book.Author),
                            new XElement("yearOfPublish", book.YearOfPublishing.ToString()));
            }
            if (entity is Magazine)
            {
                magazine = (Magazine)entity;
                xElement = new XElement("magazine",
                            new XElement("name", magazine.Name),
                            new XElement("number", magazine.Number.ToString()),
                            new XElement("yearOfPublish", magazine.YearOfPublishing.ToString()));
            }
            XElement xElementParent = new XElement(xmlDocument.Element("publications"));
            xElementParent.Add(xElement);
            xmlDocument.Root.ReplaceAll(xElementParent.Elements());

            xmlDocument.Save(_absolutePathToFile);
        }

        public List<Object> Read(Type type)
        {
            XDocument xmlDocument = XDocument.Load(_absolutePathToFile);
            List<Object> list = new List<Object>();

            if (type == typeof(Book))
            {
                foreach (XElement el in xmlDocument.Root.Elements().Where(x =>x.Name == "book"))
                {
                    //выводим в цикле названия всех дочерних элементов и их значения
                    list.Add(new Book { Name = el.Elements().ElementAt(0).Value, Author = el.Elements().ElementAt(1).Value,
                        YearOfPublishing = int.Parse(el.Elements().ElementAt(2).Value) });
                }
                return list;
            }
            if (type == typeof(Magazine))
            {
                foreach (XElement el in xmlDocument.Root.Elements().Where(x => x.Name == "magazine"))
                {
                    list.Add(new Magazine { Name = el.Elements().ElementAt(0).Value, Number = int.Parse(el.Elements().ElementAt(1).Value),
                        YearOfPublishing = int.Parse(el.Elements().ElementAt(2).Value) });
                }
                return list;
            }
            foreach (XElement el in xmlDocument.Root.Elements())
            {
                if (el.Name == "book")
                {
                    list.Add(new Publication {Name = el.Elements().ElementAt(0).Value, Type = "Book" });
                }
                if (el.Name == "magazine")
                {
                    list.Add(new Publication { Name = el.Elements().ElementAt(0).Value, Type = "Magazine"});
                }
            }
            return list;
        }
    }
}