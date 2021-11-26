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
            List<string> lines = new List<string>();
            string line;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }
    }
}
