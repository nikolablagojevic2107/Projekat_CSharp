using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public interface IPassenger
    {
        public double GetMoney();
        public string GetName();
        public string GetSurname();
        public void SetStrategy(IBuyingStrategy buyingStrategy);
        public void GiveMoney(double money);
        public void TakeMoney(double money);
        public bool BuyTicket(IAirport src,IAirport dst);
        //Buys the ticket from one airport to another using one of the buying strategies
    }
}
