using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public static class CommonFunctions
    {
        public static string ReadFromFile(string fileName)
        {
            return System.IO.File.ReadAllText(fileName);
        }
        public static void WriteToFile(string fileName, string str)
        {
            File.WriteAllText(fileName, str);
        }
    }
}
