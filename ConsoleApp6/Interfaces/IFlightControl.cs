using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    interface IFlightControl
    {
        public IAirport GetAirport();
        public int GetNumOfRunways();
        public void SetOnRunway(IPlane plane);
        public void RemoveFromRunway(IPlane plane);
        public void AllowTakeOff(IPlane plane);
        public void AllowLanding(IPlane plane);
        public List<IPlane> ReadyForTakeOff();
    }
}
