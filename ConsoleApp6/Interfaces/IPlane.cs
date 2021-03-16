using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public enum Status { READY = 1,ONFFLIGHT,FINISHED}
    public interface IPlane
    {
        public Status Status { get; set; }
        public void AddPassenger(IPassenger passenger);
        public void ClearPassenger();
        public void AddCrewMember(CrewMember crewMember);
        public bool RemoveCrewMember(CrewMember crewMemeber);
        public List<IPassenger> GetPassengers();
        public List<CrewMember> GetCrew();
    }
}
