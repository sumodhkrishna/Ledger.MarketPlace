using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledger.MarketPlace.Strategy
{
    public interface ICommandProcessorStrategy
    {
        public void Process(string[] values);
    }
}
