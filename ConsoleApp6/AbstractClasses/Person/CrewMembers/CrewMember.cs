using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public abstract class CrewMember:Person
    {
        public abstract string Role();
        public CrewMember(string name, string surname):base(name,surname) { }
    }
}

