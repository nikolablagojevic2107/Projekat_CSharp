using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ConsoleApp6
{
    public interface IAirport
    {
        public List<IAirport> GetAirports();
        public Vector2 GetCoordinates();
        public int GetNumOfRunways();
        public static double Distance(IAirport a1, IAirport a2) { return Airport.Distance(a1,a2); }

        public static void Connect(IAirport a1, IAirport a2) { Airport.Connect(a1, a2); }

        public static bool Connected(IAirport a1, IAirport a2) { return Airport.Connected(a1,a2); }

        public List<IFlight> GetFlightsTo(IAirport dest);

    }
}
