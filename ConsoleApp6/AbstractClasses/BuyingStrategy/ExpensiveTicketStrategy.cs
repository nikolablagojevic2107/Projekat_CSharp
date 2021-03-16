using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class ExpensiveTicketStrategy:IBuyingStrategy
    {
     public override ITicket FindBestTicket(List<IFlight> flights)
     {
           int index = 0;
           double maximum = flights[0].GetPrice();
           for(int i = 1;i<flights.Count;i++)
           { 
                   if(flights[i].GetPrice()>maximum)
                   {
                       maximum = flights[i].GetPrice();
                       index = i;
                   }
           }
           ITicket ticket = flights[index].CreateTicket();
           return ticket;
     }
    }
}
