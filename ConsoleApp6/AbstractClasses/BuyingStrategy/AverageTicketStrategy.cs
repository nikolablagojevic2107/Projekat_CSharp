using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class AverageTicketStrategy:IBuyingStrategy
    {

        public override ITicket FindBestTicket(List<IFlight> flights)
        {
            double average = 0;
            foreach (IFlight flight in flights) { average += flight.GetPrice(); }
            average /= flights.Count;
            int index = 0;
            double closestToAverage = Math.Abs(average - flights[0].GetPrice());
            for (int i = 1; i < flights.Count; i++)
            {
                double diff = Math.Abs(average - flights[i].GetPrice());
                if (diff < closestToAverage)
                {
                    closestToAverage = diff;
                    index = i;
                }
            }
            ITicket ticket = flights[index].CreateTicket();
            return ticket;
        }
    }
}
