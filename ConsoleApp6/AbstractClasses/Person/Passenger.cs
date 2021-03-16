using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class Passenger:Person,IPassenger
    {
        private double money;
        private IBuyingStrategy buyingStrategy;
        public Passenger(string name_, string surname_, double money_) : base(name_, surname_) 
        { money = money_; }
        public void SetStrategy(IBuyingStrategy buyingStrategy_) 
        {
            buyingStrategy = buyingStrategy_;
        }
        public double GetMoney() { return money; } 
        public void GiveMoney(double money_) 
        {
            money += money_;
            
        }
        public void TakeMoney(double money_) 
        {
            if (money < money_) { /*throw exception;*/}
            money -= money_;
        }

        /*Karta se kupuje tako sto src.GetFlightsTo(dst) da listu svih letova od 
         *izvorisnog do odredisnog aerodroma.Zatim buyingStrategy nalazi odgovarajucu 
         *kartu.Proverava se dali putnik ima dovoljno novca,i ako ima,skida mu se novac
         *i u kartu se upisuju njegovi podaci(Ime i Prezime)*/
        public bool BuyTicket(IAirport src, IAirport dst)         
        {
            ITicket t = buyingStrategy.FindBestTicket(src.GetFlightsTo(dst));
            if (t.GetPrice() > money) return false;
            money -= t.GetPrice();
            t.SetPassenger(this);
            return true;
        }
    }
}
