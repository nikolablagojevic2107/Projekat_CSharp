using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public abstract class IBuyingStrategy
    {
        public abstract ITicket FindBestTicket(List<IFlight> flights);
    }
}
