using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public interface ITicket
    {
        public double GetPrice();
        public IPassenger GetPassenger();

        public void SetPassenger(IPassenger passenger);

    }


}
