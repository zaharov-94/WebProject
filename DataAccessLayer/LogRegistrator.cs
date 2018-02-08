using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public static class LogRegistrator
    {
        public static void Write(Exception ex)
        {
            try
            {
                string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                if (!Directory.Exists(pathToLog))
                    Directory.CreateDirectory(pathToLog);
                string filename = string.Format("{0:dd.MM.yyy}.log", DateTime.Now);
                string fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] [{1}()] {2}\r\n",
                DateTime.Now, ex.TargetSite.DeclaringType, ex.Message);
                File.WriteAllText(pathToLog+filename, fullText);
            }
            catch
            {
            }
        }
    }
}
