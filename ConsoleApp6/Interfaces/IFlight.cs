using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public interface IFlight
    {

        public int GetUnique_ID();
        public IAirport GetSrcAirport();

        public IAirport GetDestAirport();

        public IPlane GetPlane();

        public ITicket CreateTicket();

        public double GetPrice();

        public List<ITicket> GetTickets();

    }
}
