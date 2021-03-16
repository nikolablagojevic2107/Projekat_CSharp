using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class Plane : IPlane
    {
        private List<IPassenger> passengers = new List<IPassenger>();
        private List<CrewMember> crew = new List<CrewMember>();
        public Status Status { get; set; }
        public List<IPassenger> GetPassengers() { return passengers; }
        public List<CrewMember> GetCrew() { return crew; }
        public void AddPassenger(IPassenger passenger) { passengers.Add(passenger); }
        public void ClearPassenger() { passengers.Clear(); }

        /*Maksimalno moze biti jedan pilot i kopilot
         *Proverava se da li je clan posade koji se dodaje pilot ili kopilot
         *Ako jeste,i ako vec ima pilota ili kopilota,on nece biti dodat*/
        public void AddCrewMember(CrewMember crewMember)
        {
            bool isThereAPilot = false;
            bool isThereACoPilot = false;
            foreach (CrewMember member in crew)
            {
                if (member.Role() == "Pilot" || member.Role() == "pilot") { isThereAPilot = true; }
                if (member.Role() == "Copilot" || member.Role() == "copilot") { isThereACoPilot = true; }
            }

            bool isHePilot = (crewMember.Role() == "Pilot" || crewMember.Role() == "pilot");
            bool isHeCoPilot = (crewMember.Role() == "Copilot" || crewMember.Role() == "copilot");
            if (isThereAPilot && isHePilot) return;
            if (isThereACoPilot && isHeCoPilot) return;
            crew.Add(crewMember); 
        }
        public bool RemoveCrewMember(CrewMember crewMemeber) 
        {
            bool Found = false;
            int index = -1;
            for(int i = 0;i<crew.Count;i++)
            {
                if (crew[i] == crewMemeber)
                {
                    Found = true;
                    index = i;
                    break;
                }
            }
            if (index >= 0) crew.RemoveAt(index);
            return Found;
        }
    }
}
