using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Ledger.MarketPlace.Test
{
    [Collection("Ledger.MarketPlace.Test")] 
    public class ProgramTest
    {
        [Fact]
        public static void TestProgram_Success()
        {
            var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Utilities\\TestInput.txt";
            if (Directory.Exists(path))
            {
                string[] inputVals = new string[] { path };
                var stringWriter = new StringWriter();
                Console.SetOut(stringWriter);

                Program.Main(inputVals);

                string actual = stringWriter.ToString();
                Assert.Equal("IDIDI Carry 1326 9\r\nIDIDI Carry 3652 4\r\nUON Billy 15856 3\r\nMBI Barry 9044 10\r\n", actual);
            }
        }
    }
}
