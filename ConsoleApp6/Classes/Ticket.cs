using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp6;

namespace ConsoleApp6
{
    //Zbog toga sto Ticket moze biti napravljen jedino od strane Flight objekta
    //I Flight moze biti napravljen jedino od strane FlightDatabase objekta
    //Stavicemo Ticket kao privatnu klasu Flight-a(koja implementira ITicket interfejs) i 
    //Flight kao privatnu klasu FlightDatabase-a(koja implementira IFlight interfejs)
    //Time dobijamo da jedino spoljna klasa moze da inicijalizuje objekat
    //a svi mogu da ga korist(kroz interfejs)
    public sealed partial class FlightDatabase
    {
        private partial class Flight
        {
            private class Ticket : ITicket
            {
                private readonly double Price;
                private IPassenger Passenger;
                public IPassenger GetPassenger() { return Passenger; }

                public void SetPassenger(IPassenger passenger) { Passenger = passenger; }

                public double GetPrice() { return Price; }

                public Ticket(double price) { Price = price; }
            }
        }

    }
}
