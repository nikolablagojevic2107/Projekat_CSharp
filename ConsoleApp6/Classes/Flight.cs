using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public sealed partial class FlightDatabase
    {
        private partial class Flight:IFlight
        {
            //Private fields:
            private int Unique_ID;
            private IAirport SrcAirport;
            private IAirport DestAirport;
            private IPlane Plane;
            private double PricePerUnit;
            private List<ITicket> NewTickets; // novostvorene karte,koje su za prodaju(nemaju kupca)
            private List<ITicket> SoldTickets; // prodate karte,kad im dodelimo kupca
            /***************************************/
            //Constructor:
            public Flight(int id,IAirport src,IAirport dst,IPlane plane,double PricePerUnit_)
            {
                Unique_ID = id;
                SrcAirport = src;
                DestAirport = dst;
                Plane = plane;
                PricePerUnit = PricePerUnit_;
                NewTickets = new List<ITicket>();
                SoldTickets = new List<ITicket>();
            }

            //Public methods:
            public int GetUnique_ID() { return Unique_ID; }
            public IAirport GetSrcAirport() { return SrcAirport; }

            public IAirport GetDestAirport() { return DestAirport; }

            public IPlane GetPlane() { return Plane; }
            public double GetPrice()
            {
                //Cena je po jedinici udaljenosti,zato mnozimo sa distancom izmedju aerodroma
                return Airport.Distance(SrcAirport, DestAirport) * PricePerUnit;
            }
            public ITicket CreateTicket()
            {
                ITicket t = new Ticket(GetPrice());
                NewTickets.Add(t); //Dodajemo kartu u listu novih karata
                return t;
            }


            public List<ITicket> GetTickets()
            {   //iz liste novih karata,proveravamo sve koje imaju kupca i dodajemo ih u soldTickets
                //ovo radimo da kasnije ne bi puko program ako ticket nema kupca 
                foreach (ITicket ticket in NewTickets)
                {
                    if (ticket.GetPassenger() != null) { SoldTickets.Add(ticket); } 
                }
                return SoldTickets;

            }

        }
    }
}
