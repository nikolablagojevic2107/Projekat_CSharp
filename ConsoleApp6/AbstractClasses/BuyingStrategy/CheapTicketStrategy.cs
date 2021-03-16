using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp6
{
    public class CheapTicketStrategy:IBuyingStrategy
    {
        public override ITicket FindBestTicket(List<IFlight> flights)
        {
            int index = 0;
            double minimum = flights[0].GetPrice();
            for(int i = 1;i<flights.Count;i++)
            { 
                    if(flights[i].GetPrice()<minimum)
                    {
                        minimum = flights[i].GetPrice();
                        index = i;
                    }
            }
            ITicket ticket = flights[index].CreateTicket();
            return ticket;
        }
    }
}
