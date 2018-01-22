using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Library.Web
{
    public class TxtWriter
    {
        StreamWriter _streamWriter;
        public TxtWriter(string fileName)
        {
            _streamWriter = new StreamWriter(fileName);
        }
        public void Write()
        {
            string[] st = new string[] { "fff", "aaa", "kkk" };

            foreach (var node in st)
            {
                _streamWriter.WriteLine(node + Environment.NewLine);
            }

            _streamWriter.Close();
        }
    }
}