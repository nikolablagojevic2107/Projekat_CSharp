using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public sealed partial class FlightDatabase:IFlightDatabase
    {
        //Private fields:
        private List<IFlight> Flights;
        SequenceGenerator Generator;

        //Singleton Design Pattern 
        // koristimo Thread Safe Singleton without using locks and no lazy instantiation
        private static readonly FlightDatabase instance = new FlightDatabase();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static FlightDatabase()
        {
        }

        private FlightDatabase()
        {
            Flights = new List<IFlight>();
        }

        public static FlightDatabase Instance
        {
            get
            {
                return instance;
            }
        }

        //public metode 
        public void SetGenerator(SequenceGenerator sg) { Generator = sg; }

        public int GetRandom() { return Generator.NextInt(); }
        public IFlight CreateFlight(int id, IAirport src, IAirport dst, IPlane plane, double pricePerUnit)
        {
            IFlight f = new Flight(id, src, dst, plane, pricePerUnit);
            return f;
        }
        public void InsertFlight(IFlight f) 
        {
            if (f == null) return;

            //Ako izvorisni i odredisni aerodromi leta nisu povezani baca se izuzetak
            if (!IAirport.Connected(f.GetDestAirport(), f.GetSrcAirport())) 
            { throw new AirportsNotConnectedException("Aerodromi nisu povezani"); }
            
            //proveravamo da li u listi letova postoji vec let sa istim identifikacionim brojem
            foreach(IFlight flight in Flights)
            {
                if(flight.GetUnique_ID()==f.GetUnique_ID())
                { throw new IdConflictException("Vec postoji let sa zadatim identifikacionim brojem"); }

            }
            Flights.Add(f); 
        
        }

        public void DeleteFlight(IFlight f) 
        {
            //prolazimo kroz listu svih letova,da bi smo nasli let koji treba da uklonimo
            //ako nema tog leta,baca se izuzetak
            if (f == null) return;
            int index = 0;
            foreach(Flight flight in Flights)
            {
                if (flight == f) break;
                index++;
            }
            if(index == Flights.Count) { throw new FlightNotInDatabaseException("dati let ne postoji u bazi letova"); } 
            Flights.RemoveAt(index);
        }

        public List<IFlight> GetFlightsList() { return Flights; }

        public List<IFlight> FindBySrcAirport(IAirport src) 
        {
            List<IFlight> flights_to_return = new List<IFlight>();
            foreach(Flight flight in Flights)
            {
                if(flight.GetSrcAirport()==src)
                {
                    flights_to_return.Add(flight);
                }
            }
            return flights_to_return;
        }

        public List<IFlight> FindByDestAirport(IAirport dest)
        {
            List<IFlight> flights_to_return = new List<IFlight>();
            foreach (Flight flight in Flights)
            {
                if (flight.GetDestAirport() == dest)
                {
                    flights_to_return.Add(flight);
                }
            }
            return flights_to_return;
        }

        public List<IFlight> FindByPlaneStatus(Status s) 
        {
            List<IFlight> flights = new List<IFlight>();
            foreach (Flight flight in Flights)
            {
                if (flight.GetPlane().Status==s)
                {
                    Flights.Add(flight);
                }
            }
            return flights;
        }

        public List<IFlight> FindByMaxPrice(double max)
        {
            List<IFlight> flights = new List<IFlight>();
            foreach (Flight flight in Flights)
            {
                if (flight.GetPrice()<max)
                {
                    Flights.Add(flight);
                }
            }
            return flights;
        }
    }
}
