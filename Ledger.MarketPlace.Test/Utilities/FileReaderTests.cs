using Ledger.MarketPlace.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ledger.MarketPlace.Test.Utiities
{
    [Collection("Ledger.MarketPlace.Test")]
    public class FileReaderTests
    {
        [Fact]
        public void ReadTextFile_Success()
        {

            var  path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Utilities\\TestInput.txt";
            if (Directory.Exists(path))
            {
                var content = FileReader.ReadFile(path);

                Assert.Contains(content, c => c.Equals("BALANCE MBI Barry 12"));
            }
        }

        [Fact]
        public void ReadTextFile_ThrowsArgumentExceptionOnNonAbsolutePath()
        {
           Assert.Throws<ArgumentException>(() =>FileReader.ReadFile("Utilities\\TestInput.txt"));
        }
    }
}
