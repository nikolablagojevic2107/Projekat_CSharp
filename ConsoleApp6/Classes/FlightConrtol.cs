using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp6.Classes;

namespace ConsoleApp6
{
    public partial class FlightControl : IFlightControl
    {
        //Private fields: 
        private IAirport Airport;
        private int NumOfRunways;
        private List<IPlane> Planes = new List<IPlane>();
        //public methods:
        public IAirport GetAirport() { return Airport; }
        public int GetNumOfRunways() { return NumOfRunways; }
        public FlightControl(IAirport a) { Airport = a; NumOfRunways = Airport.GetNumOfRunways(); }
        public void SetOnRunway(IPlane plane)
        {
            //Proveravamo da li ima slobodnih pista da postavimo avion
            //Proveravamo da li je avion u letu vec 
            if(NumOfRunways == 0) { throw new NoFreeRunwayException("Nema dovoljno pista "); }
            if (plane.Status == Status.ONFFLIGHT) { throw new PlaneNotOnGroundException("avion je u letu: "); }

            //kada postavimo avion smanjujemo broj slobodnih pista za 1 i postavljamo status aviona na READY
            NumOfRunways--;
            plane.Status = Status.READY;

            //Nalazimo sve letove koji imaju kao izvorisni aerodrom onaj za koga je vezana kontrola leta
            IFlightDatabase database = FlightDatabase.Instance;
            List<IFlight> flights = database.FindBySrcAirport(Airport);
            IFlight theFlight = null;

            //Onaj let koji je vezan za nas dati avion(plane)
            //je let koji nam treba zbog liste Ticket-a
            //svaki ticket sadrzi referencu na putnika,
            //koji treba da se ubaci u avion
            foreach(IFlight flight in flights)
            {
                if(flight.GetPlane() == plane)
                {
                    theFlight = flight;
                }
            }
            if (theFlight != null)
            { 
                List<ITicket> tickets = theFlight.GetTickets();
                foreach(ITicket ticket in tickets)
                {
                    if(ticket.GetPassenger()!= null)
                    {
                        plane.AddPassenger(ticket.GetPassenger());
                    }

                }
            }
            else
            {   //ako nije pronadjen avion baca se izuzetak
                throw new PlaneNotSuitableException("Ne postoji let za ovaj avion: ");
            }
            if (plane != null) Planes.Add(plane);
            
        }
        public void RemoveFromRunway(IPlane plane) 
        {
            if (plane != null)
            {
                if (!Planes.Contains(plane)) { throw new PlaneNotOnRunwayException("Avion nije na pisti "); }
                NumOfRunways++; // oslobodila se jedna pista kako smo skloniki avion 
                Planes.Remove(plane);
            }
        }
        public void AllowTakeOff(IPlane plane) 
        {
            if (plane != null)
            {
                if(plane.Status != Status.READY) { throw new PlaneNotReadyException("Avion nije spreman za poletanje"); }
                //brojimo clanove posade,treba minimum: 1 pilot,1 copilot,5 ostalih clanova posade
                List<CrewMember> crew = plane.GetCrew();
                int copilot = 0;
                int pilot = 0;
                int other = 0;
                foreach(CrewMember member in crew)
                {
                    if (member.Role() == "Pilot") pilot++;
                    if (member.Role() == "Copilot") copilot++;
                    if (member.Role() == "Other") other++;
                }
                if((copilot == 0)||(pilot == 0)||(other<5)) { throw new NoCrewException("Nema dovoljno clanova posade"); }
                
                plane.Status = Status.ONFFLIGHT; //postavljamo status aviona da je u letu 
            }
        }
        public void AllowLanding(IPlane plane) 
        {
            if (plane == null) return;
            IFlightDatabase database = FlightDatabase.Instance;
            //Nalazimo sve letove koji slecu na aerodrom vezan za ovu FlightControl-u
            //Ako nema jednog leta medju njima koji ima zadati avion(plane) baca se izuzetak
            List<IFlight> flights = database.FindByDestAirport(Airport);
            IFlight theFlight = null;
            foreach (IFlight flight in flights)
            {
                if (flight.GetPlane() == plane)
                {
                    theFlight = flight;
                }
            }
            if (theFlight != null)
            {
                List<ITicket> tickets = theFlight.GetTickets();
                foreach (ITicket ticket in tickets)
                {
                    ticket.SetPassenger(null);
                }
            }
            else { throw new PlaneCanNotLandException("Avion ne slece na taj aerodrom"); }
            plane.Status = Status.FINISHED; //Avion je sleteo
            plane.ClearPassenger(); //brisu se putnicima karte 

        }
        public List<IPlane> ReadyForTakeOff() 
        {
            IFlightDatabase database = FlightDatabase.Instance;
            //nalazimo sve letove kojima je odredisni aerodrom,Aerodrom iz ove IFlightControl-e
            List<IFlight> flights = database.FindBySrcAirport(Airport);
            List <IPlane> ready = new List<IPlane>();

            //Za svaki od tih letova,proveravamo da li je avion spreman za poletanje
            foreach(IFlight flight in flights)
            {
                if(flight.GetPlane().Status == Status.READY)
                {
                    flights.Add(flight);
                }
            }
            return ready;

        }

    }
}
