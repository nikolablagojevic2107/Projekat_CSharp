using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class Pilot:CrewMember
    {
        public override string Role() { return "Pilot"; }
        public Pilot(string name, string surname) : base(name, surname) { }
    }
}
