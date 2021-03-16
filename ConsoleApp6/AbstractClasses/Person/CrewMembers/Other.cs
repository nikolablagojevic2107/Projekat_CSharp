using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class Other : CrewMember
    {
        public override string Role() { return "Other"; }
        public Other(string name, string surname) : base(name, surname) { }
    }
}
