using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
namespace ConsoleApp6
{
    public class Airport:IAirport
    {
        private List<IAirport> Airports; //Aerodromi sa kojima je povezan 
        private Vector2 Coordinates;
        private int NumOfRunways; //Maksimalan broj pista na aerodromu 

        public List<IAirport> GetAirports() { return Airports; }
        public Vector2 GetCoordinates() { return Coordinates; }
        public int GetNumOfRunways() { return NumOfRunways; }

        public Airport(Vector2 coordinates_, int numOfRunways_) 
            { 
              Coordinates = coordinates_; 
              NumOfRunways = numOfRunways_; 
              Airports = new List<IAirport>(); 
            }

        public static double Distance(IAirport a1, IAirport a2) 
           {
            double x = a1.GetCoordinates().X - a2.GetCoordinates().X;
            double y = a1.GetCoordinates().Y - a2.GetCoordinates().Y;
            double distance = Math.Sqrt(x*x + y*y);
            return distance;
           }

        public static void Connect(IAirport a1, IAirport a2) //povezujemo dva aerodroma tako sto ih dodamo medjusobno 
        {                                                    //u listu njihovih povezanih aerodroma
            a1.GetAirports().Add(a2); 
            a2.GetAirports().Add(a1); 
        }

            public static bool Connected(IAirport a1, IAirport a2) 
            {
                bool connected = false;
                    foreach(IAirport airport in a1.GetAirports())
                {
                    if (airport == a2) { connected = true; break; }
                }
                return connected;
            }   

            public List<IFlight> GetFlightsTo(IAirport dest) 
            {
                //AllFlights sadrzi sve letove u bazi podataka,
                //i ispitujemo svaki let da li ima IAirport dest kao svoj odredisni aerodrom
                IFlightDatabase database = FlightDatabase.Instance;
                List<IFlight> AllFlights = database.GetFlightsList();
                List<IFlight> FlightsToDest = new List<IFlight>();
                foreach(IFlight flight in AllFlights)
                {
                    if((flight.GetSrcAirport()==this)&&(flight.GetDestAirport() == dest))
                    {
                        FlightsToDest.Add(flight);            
                    }
                }
                return FlightsToDest;
            }
    }
}

