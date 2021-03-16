using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6.Classes.Person.CrewMembers
{
    public class Copilot : CrewMember
    {
        public override string Role() { return "Copilot"; }
        public Copilot(string name, string surname) : base(name, surname) { }
    }
}
