using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public abstract class Person
    {
        protected string Name;
        protected string Surname;
        public Person(string name,string surname) 
        {
            Name = name;
            Surname = surname;
        }
        public string GetName() { return Name; }
        public string GetSurname() { return Surname; }
    }
}
