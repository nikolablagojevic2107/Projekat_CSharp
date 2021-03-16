using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp6
{
    public class SequenceGenerator
    {
        public int NextInt() 
        {
            Random rnd = new Random();
            return rnd.Next(1, 2000);
        }
    }
}
