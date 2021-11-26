using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledger.MarketPlace.Utilities
{
    public static class FileReader
    {
        public static List<string> ReadFile(string path)
        {
            if (System.IO.Path.IsPathRooted(path) && Directory.Exists(path))
                return System.IO.File.ReadAllLines(path).ToList();
            throw new ArgumentException(@"The given "+path+" is not a valid absolute path");
        }
    }
}
