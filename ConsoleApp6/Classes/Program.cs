using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using ConsoleApp6.Classes.Person.CrewMembers;

namespace ConsoleApp6
{ 
    class Program
    {
        static void Main()
        {
            //Testovi za IPassenger
            /******************************************************************************/
            Console.WriteLine("\n/*************************************************************************/\n");
            Console.WriteLine("Testovi za IPassenger-a\n");
            IPassenger passenger1 = new Passenger("Lebron", "James" ,1000);
            Console.WriteLine(passenger1.GetName()+" "+passenger1.GetSurname());
            Console.WriteLine(passenger1.GetMoney());
            passenger1.TakeMoney(755.55);
            Console.WriteLine(passenger1.GetMoney().ToString("F2"));
            passenger1.GiveMoney(836.677);
            Console.WriteLine(passenger1.GetMoney().ToString("F4"));
            Console.WriteLine("\n/*************************************************************************/\n");
            /*******************************************************************************/







            //Testiranje ITicket
            /*******************************************************************************/
            Console.WriteLine("\n/*************************************************************************/\n");
            Console.WriteLine("Testovi za ITicket\n");

            // ITicket = new Ticket(100);   //Izbacuje gresku,sto i treba
                                            //Jer Ticket sme da napravi samo Flight
            // IFlight f = new Flight();    //Analogno vazi i za Flight,moze ga stvoriti samo 
                                            //FlightDatabase
            IFlightDatabase database = FlightDatabase.Instance;
            database.SetGenerator(new SequenceGenerator());
            Vector2 pos1 = new Vector2(0,0);
            Vector2 pos2 = new Vector2(3, 4);
            IAirport src1 = new Airport(pos1,5);
            IAirport dest1 = new Airport(pos2,4);
            IPlane plane1 = new Plane();
            double Price1 = 10;
            IFlight f1 = database.CreateFlight(database.GetRandom(), src1, dest1, plane1, Price1);
            ITicket t1 = f1.CreateTicket();
            Console.WriteLine("price of ticket t1 is: {0}",t1.GetPrice());
            Console.WriteLine("\n/*************************************************************************/\n");
            /********************************************************************************/







            //Testiranje 1) metode ButTicket(IAirport src,IAirport dst) iz IPassanger-a
            //           2) metoda SetPassenger i GetPassenger iz ITicket-a
            //           3) IBuyingStrategy-a
            /********************************************************************************/
            Console.WriteLine("\n/*************************************************************************/\n");
            Console.WriteLine("Testovi za ITicket,IPassenger,IbuyingTicket\n");
            IBuyingStrategy Cheap = new CheapTicketStrategy();
            IBuyingStrategy Expensive = new ExpensiveTicketStrategy();
            IBuyingStrategy Average = new AverageTicketStrategy();
            double Price2 = 20;
            double Price3 = 11;
            double Price4 = 15;
            IFlight f2 = database.CreateFlight(database.GetRandom(), src1, dest1, plane1, Price2);
            IFlight f3 = database.CreateFlight(database.GetRandom(), src1, dest1, plane1, Price3);
            IFlight f4 = database.CreateFlight(database.GetRandom(), src1, dest1, plane1, Price4);
            IAirport.Connect(src1, dest1);//moramo prvo da povezemo dva aerodroma
            database.InsertFlight(f1);
            database.InsertFlight(f2);
            database.InsertFlight(f3);
            database.InsertFlight(f4);
            ITicket t2 = f2.CreateTicket();
            ITicket t3 = f3.CreateTicket();
            ITicket t4 = f4.CreateTicket();
            passenger1.SetStrategy(Cheap);
            passenger1.BuyTicket(src1,dest1);
            IPassenger passenger2 = new Passenger("Michael", "Jordan", 1100);
            IPassenger passenger3 = new Passenger("Garry", "Payton", 500);
            passenger2.SetStrategy(Expensive);
            passenger2.BuyTicket(src1, dest1);
            passenger3.SetStrategy(Average);
            passenger3.BuyTicket(src1, dest1); 
            ITicket t11 = f1.GetTickets()[0];
            if (t11 != null) //Karta koju je kupio Lebron James,Jeftina 
            {
                Console.WriteLine(t11.GetPassenger().GetName()+" "+t11.GetPassenger().GetSurname());
                Console.WriteLine("Price of the ticket is {0}", t1.GetPrice());
            }
            ITicket t22 = f2.GetTickets()[0];
            if (t22 != null) //Karta koju je kupio Michael Jordan,Najskuplja 
            {
                Console.WriteLine(t22.GetPassenger().GetName() + " " + t22.GetPassenger().GetSurname());
                Console.WriteLine("Price of the ticket is {0}", t22.GetPrice());
            }

            ITicket t33 = f4.GetTickets()[0];
            if (t33 != null) //Karta koju je kupio Garry Payton,Prosecna 
            {
                Console.WriteLine(t33.GetPassenger().GetName() + " " + t33.GetPassenger().GetSurname());
                Console.WriteLine("Price of the ticket is {0}", t33.GetPrice());
            }

            //Garry Payton je kupio kartu najblize proseku,uzeo je t4(75),a ne t3(55),(prosek je 70)
            //Average Ticket dakle radi

            Console.WriteLine("\n/*************************************************************************/\n");
            /*****************************************************************************************/











            //Testiranje IPlane 
            /****************************************************************************************/
            Console.WriteLine("\n/*************************************************************************/\n");
            Console.WriteLine("\n Testiranje za IPlane\n\n");
            IPlane plane2 = new Plane();
            plane2.AddPassenger(passenger1);
            plane2.AddPassenger(passenger2);
            plane2.AddPassenger(passenger3);
            foreach(IPassenger passenger in plane2.GetPassengers())
            {
                Console.WriteLine(passenger.GetName() + " " + passenger.GetSurname());
            }
            plane2.ClearPassenger();
            if (plane2.GetPassengers().Count == 0) { Console.WriteLine("ClearPassenger works !**"); }
            CrewMember pilot1 = new Pilot("Michael", "Schumaher");
            CrewMember copilot1 = new Copilot("Manuel","Fangio");
            CrewMember crewMember1 = new Other("Michael", "Johnson");
            CrewMember crewMember2 = new Other("David","Figuererdo");
            CrewMember crewMember3 = new Other("Anabela", "Mischkin");
            CrewMember crewMember4 = new Other("Stephen", "Wonderboy");
            CrewMember crewMember5 = new Other("Michael", "Franceze");
            plane2.AddCrewMember(pilot1);
            plane2.AddCrewMember(copilot1);
            plane2.AddCrewMember(crewMember1);
            plane2.AddCrewMember(crewMember2);
            plane2.AddCrewMember(crewMember3);
            foreach (CrewMember member in plane2.GetCrew()) 
            { Console.WriteLine(member.Role() + " " + member.GetName() + " " + member.GetSurname()); }
            plane2.RemoveCrewMember(crewMember1);
            plane2.RemoveCrewMember(pilot1);
            Console.WriteLine();
            Console.WriteLine();
            foreach (CrewMember member in plane2.GetCrew())
            { Console.WriteLine(member.Role() + " " + member.GetName() + " " + member.GetSurname()); }

            Console.WriteLine("\n/*************************************************************************/\n");
            /*******************************************************************************************/








            //Testiranje IFlightControl
            /*******************************************************************************************/
            Console.WriteLine("\n/*************************************************************************/");
            Console.WriteLine("Testing IFlightControl");
            IPassenger Stefan = new Passenger("Stefan","Jakovlejvic",1200);
            IPassenger Nikola = new Passenger("Nikola", "Novakovic", 1000);
            IPassenger Slaki = new Passenger("Slavisa", "Blesic", 1000);
            IPassenger Boki = new Passenger("Bojan", "Jovanovic", 300);
            IPassenger Milos = new Passenger("Milos", "Jovanovic", 1300);
            IPassenger Miki = new Passenger("Milisav", "Jovanovic", 1600);
            IPassenger Nevena = new Passenger("Nevena", "Preradovic", 900);
            IPassenger Teodora = new Passenger("Teodora", "Jovanov", 800);

            Stefan.SetStrategy(Expensive);
            Nikola.SetStrategy(Average);
            Slaki.SetStrategy(Cheap);
            Boki.SetStrategy(Cheap);
            Milos.SetStrategy(Expensive);
            Miki.SetStrategy(Average);
            Nevena.SetStrategy(Average);
            Teodora.SetStrategy(Expensive);

            Vector2 start = new Vector2(0, 0);
            Vector2 finish = new Vector2(6, 8);
            int NumOfRunwaysBelgrade = 3;
            int NumOfRunwaysFrankfurt = 5;
            IAirport Belgrade = new Airport(start, NumOfRunwaysBelgrade);
            IAirport Frankfurt = new Airport(finish, NumOfRunwaysFrankfurt);
            IPlane AirSerbia = new Plane();
            IPlane Lufthansa = new Plane();
            IPlane Elite = new Plane();
            IAirport.Connect(Belgrade, Frankfurt);
            IFlightControl BelgradeControl = new FlightControl(Belgrade);
            IFlightControl FrankfurtControl = new FlightControl(Frankfurt);


            IFlight flight1 = database.CreateFlight(database.GetRandom(),Belgrade,Frankfurt,AirSerbia,50);
            IFlight flight2 = database.CreateFlight(database.GetRandom(),Belgrade,Frankfurt,Lufthansa,75);
            IFlight flight3 = database.CreateFlight(database.GetRandom(), Belgrade, Frankfurt, Elite, 100);

            database.DeleteFlight(f1);
            database.DeleteFlight(f2);
            database.DeleteFlight(f3);
            database.DeleteFlight(f4);

            database.InsertFlight(flight1);
            database.InsertFlight(flight2);
            database.InsertFlight(flight3);
            // brisemo prethodne letove iz baze i dodajemo ove


            Stefan.BuyTicket(Belgrade, Frankfurt);
            Nikola.BuyTicket(Belgrade, Frankfurt);
            Slaki.BuyTicket(Belgrade, Frankfurt);
            Boki.BuyTicket(Belgrade, Frankfurt);
            Miki.BuyTicket(Belgrade, Frankfurt);
            Milos.BuyTicket(Belgrade, Frankfurt);

            Console.WriteLine("Num of Runways on Belgrade Airport {0}",BelgradeControl.GetNumOfRunways());
            BelgradeControl.SetOnRunway(AirSerbia);
            Console.WriteLine("Num of Runways on Belgrade Airport {0}", BelgradeControl.GetNumOfRunways());
            int i1 = 0;
            Console.WriteLine();
            Console.WriteLine("passengers in flight1\n");
            BelgradeControl.SetOnRunway(Lufthansa);
            BelgradeControl.SetOnRunway(Elite);  //Bez ovoga se nece ispisati putnici u trecem avionu:
            foreach (IPassenger passenger in flight1.GetPlane().GetPassengers())
            {
                i1++;
                Console.WriteLine("Passenger" + i1 + ": " + passenger.GetName() + " " + passenger.GetSurname());
                Console.WriteLine("Price of his ticket is "+flight1.GetTickets()[i1 - 1].GetPrice());
                Console.WriteLine("\n");
            }
            i1 = 0;
            Console.WriteLine("\npassengers in flight2");
            foreach (IPassenger passenger in flight2.GetPlane().GetPassengers())
            {
                i1++;
                Console.WriteLine("Passenger" + i1 + ": " + passenger.GetName() + " " + passenger.GetSurname());
                Console.WriteLine("Price of his ticket is " + flight2.GetTickets()[i1 - 1].GetPrice());
                Console.WriteLine("\n");
            }
            i1 = 0;
            Console.WriteLine("\npassengers in flight3\n");
            foreach (IPassenger passenger in flight3.GetPlane().GetPassengers())
            {
                i1++;
                Console.WriteLine("Passenger" + i1 + ": " + passenger.GetName() + " " + passenger.GetSurname());
                Console.WriteLine("Price of his ticket is " + flight3.GetTickets()[i1 - 1].GetPrice());
                Console.WriteLine("\n");
            }
            Console.WriteLine("Flight1 Status: "+flight1.GetPlane().Status);
            Console.WriteLine("Flight2 Status: "+flight2.GetPlane().Status);
            Console.WriteLine("Flight3 Status: "+flight3.GetPlane().Status);

            //Dodajemo posadu 
            AirSerbia.AddCrewMember(pilot1);
            AirSerbia.AddCrewMember(copilot1);
            AirSerbia.AddCrewMember(crewMember1);
            AirSerbia.AddCrewMember(crewMember2);
            AirSerbia.AddCrewMember(crewMember3);
            AirSerbia.AddCrewMember(crewMember4);
            AirSerbia.AddCrewMember(crewMember5);

            BelgradeControl.AllowTakeOff(AirSerbia);
            Console.WriteLine("\n\n\nAirserbia Status: {0}", AirSerbia.Status);
            FrankfurtControl.AllowLanding(AirSerbia); //Ako zakomentarisemo ovu liniju i otkomentarisemo sledecu
            //AirSerbia.Status=Status.FINISHED;       //Pojavice se i Slaki na povratku,sto ne bi trebalo
                                                      //Sto znaci da f-ja uspesno brise IPassengerima karte
            Console.WriteLine("\n\n\nAirserbia Status: {0}", AirSerbia.Status);
            Console.WriteLine("\n\ndodajemo jos dva putnika sa aerodroma u Frankfurtu: \n");

 
            IFlight flight4 = database.CreateFlight(database.GetRandom(), Frankfurt, Belgrade, AirSerbia, 80);
            database.InsertFlight(flight4);

            Teodora.BuyTicket(Frankfurt, Belgrade);
            Nevena.BuyTicket(Frankfurt, Belgrade);

            FrankfurtControl.SetOnRunway(AirSerbia);

            i1 = 0;
            Console.WriteLine("\npassengers in flight4\n");
            foreach (IPassenger passenger in flight4.GetPlane().GetPassengers())
            {
                i1++;
                Console.WriteLine("Passenger" + i1 + ": " + passenger.GetName() + " " + passenger.GetSurname());
                Console.WriteLine("Price of his ticket is " + flight4.GetTickets()[i1 - 1].GetPrice());
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n/*************************************************************************/\n");
            /*******************************************************************************************/





            /*******************************************************************************************/
            Console.WriteLine("\n/*************************************************************************/");
            Console.WriteLine("Testing Exceptions\n\n\n");

            IAirport NewYork = new Airport(start, NumOfRunwaysBelgrade);
            IAirport Ottawa = new Airport(finish, NumOfRunwaysFrankfurt);

            IPlane AirCanada = new Plane();

            int random = database.GetRandom();
            IFlight TestFlight = database.CreateFlight(random, NewYork, Ottawa, AirCanada, 100);
               
            IFlightControl NewYorkControl = new FlightControl(NewYork);
            IFlightControl OttawaControl = new FlightControl(Ottawa);

            //Izuztak pri ubacivanju leta u bazu gde dva aviona nisu povezana 
            try
            { 
                database.InsertFlight(TestFlight);
            }catch(AirportsNotConnectedException e)
            {
                Console.WriteLine("\n---- Exception1: ----\n");
                Console.WriteLine(e.Message);
            }

            IAirport.Connect(NewYork, Ottawa);  
            database.InsertFlight(TestFlight);

            //Izuzetak pri ubacivanju leta gde su dva Unique_ID ista 
            try
            {
                IFlight TestFlight2 = database.CreateFlight(random, NewYork, Ottawa, AirCanada, 100);
                database.InsertFlight(TestFlight2);
            }
            catch (IdConflictException e)
            {
                Console.WriteLine("\n---- Exception2: ----\n");
                Console.WriteLine(e.Message);
            }

            //Izuzetak pri brisanju leta ako taj let nije u bazi
            try 
            {
                IFlight TestFlight2 = database.CreateFlight(random, NewYork, Ottawa, AirCanada, 100);
                database.DeleteFlight(TestFlight2);            
            }catch(FlightNotInDatabaseException e)
            {
                Console.WriteLine("\n---- Exception3: ----\n");
                Console.WriteLine(e.Message);
            }

            //Izuzetak ako ne postoji let sa tog aerodroma za dati avion
            try
            {
                NewYorkControl.SetOnRunway(AirSerbia);
            }catch(PlaneNotSuitableException e)
            {
                Console.WriteLine("\n---- Exception4: ----\n");
                Console.WriteLine(e.Message);
            }

            IFlight TestFlight_ = null;
            //izuzetak ako nema slobodne piste pri postavljanju aviona
            try
            {
                Vector2 spoint = new Vector2(0, 0);
                Vector2 fpoint = new Vector2(1, 1);
                IAirport TestAirport = new Airport(spoint, 0); //Ako umesto 0 stavimo 1 ne baca se izuzetak
                IAirport TestAirport2 = new Airport(fpoint, 0);
                IAirport.Connect(TestAirport, TestAirport2);
                TestFlight_ = database.CreateFlight(-7, TestAirport, TestAirport2, AirCanada,100);
                IFlightControl TestFlightControl = new FlightControl(TestAirport);
                database.InsertFlight(TestFlight_);
                TestFlightControl.SetOnRunway(AirCanada);
            }
            catch (NoFreeRunwayException e)
            {
                database.DeleteFlight(TestFlight_);
                Console.WriteLine("\n---- Exception5: ----\n");
                Console.WriteLine(e.Message);
            }

            //izuzetak ako je avion u letu a mi hocemo da ga postavimo na pistu 
            try
            {
                Vector2 spoint = new Vector2(0, 0);
                Vector2 fpoint = new Vector2(1, 1);
                IAirport TestAirport = new Airport(spoint, 1); 
                IAirport TestAirport2 = new Airport(fpoint, 0);
                IAirport.Connect(TestAirport, TestAirport2);
                TestFlight_ = database.CreateFlight(-7, TestAirport, TestAirport2, AirCanada, 100);
                IFlightControl TestFlightControl = new FlightControl(TestAirport);
                database.InsertFlight(TestFlight_);
                AirCanada.Status = Status.ONFFLIGHT;
                TestFlightControl.SetOnRunway(AirCanada);
            }
            catch (PlaneNotOnGroundException e)
            {
                database.DeleteFlight(TestFlight_);
                Console.WriteLine("\n---- Exception6: ----\n");
                Console.WriteLine(e.Message);
            }
            //Izuzetak pri sklanjanju aviona sa piste,ako avion nije na pisti
            try
            {
                Vector2 spoint = new Vector2(0, 0);
                Vector2 fpoint = new Vector2(1, 1);
                IAirport TestAirport = new Airport(spoint, 1);
                IAirport TestAirport2 = new Airport(fpoint, 0);
                IAirport.Connect(TestAirport, TestAirport2);
                TestFlight_ = database.CreateFlight(-7, TestAirport, TestAirport2, AirCanada, 100);
                IFlightControl TestFlightControl = new FlightControl(TestAirport);
                database.InsertFlight(TestFlight_);
                AirCanada.Status = Status.READY;
                //TestFlightControl.SetOnRunway(AirCanada);
                TestFlightControl.RemoveFromRunway(AirCanada);
            }
            catch (PlaneNotOnRunwayException e)
            {
                database.DeleteFlight(TestFlight_);
                Console.WriteLine("\n---- Exception7: ----\n");
                Console.WriteLine(e.Message);
            }

            //Izuzetak ako nema dovoljno clanova posade 
            try
            {
                Vector2 spoint = new Vector2(0, 0);
                Vector2 fpoint = new Vector2(1, 1);
                IAirport TestAirport = new Airport(spoint, 1);
                IAirport TestAirport2 = new Airport(fpoint, 0);
                IAirport.Connect(TestAirport, TestAirport2);
                TestFlight_ = database.CreateFlight(-7, TestAirport, TestAirport2, AirCanada, 100);
                IFlightControl TestFlightControl = new FlightControl(TestAirport);
                database.InsertFlight(TestFlight_);
                AirCanada.Status = Status.READY;
                TestFlightControl.SetOnRunway(AirCanada);
                TestFlightControl.AllowTakeOff(AirCanada);
            }
            catch (NoCrewException e)
            {
                database.DeleteFlight(TestFlight_);
                Console.WriteLine("\n---- Exception7: ----\n");
                Console.WriteLine(e.Message);
            }

            //izuzetak ako avion nije spreman za poletanje
            try
            {
                Vector2 spoint = new Vector2(0, 0);
                Vector2 fpoint = new Vector2(1, 1);
                IAirport TestAirport = new Airport(spoint, 1);
                IAirport TestAirport2 = new Airport(fpoint, 0);
                IAirport.Connect(TestAirport, TestAirport2);
                TestFlight_ = database.CreateFlight(-7, TestAirport, TestAirport2, AirCanada, 100);
                IFlightControl TestFlightControl = new FlightControl(TestAirport);
                database.InsertFlight(TestFlight_);
                AirCanada.Status = Status.READY;
                TestFlightControl.SetOnRunway(AirCanada);
                AirCanada.AddCrewMember(pilot1);
                AirCanada.AddCrewMember(copilot1);
                AirCanada.AddCrewMember(crewMember1);
                AirCanada.AddCrewMember(crewMember2);
                AirCanada.AddCrewMember(crewMember3);
                AirCanada.AddCrewMember(crewMember4);
                AirCanada.AddCrewMember(crewMember5);
                AirCanada.Status = Status.FINISHED;
                TestFlightControl.AllowTakeOff(AirCanada);
            }
            catch (PlaneNotReadyException e)
            {
                database.DeleteFlight(TestFlight_);
                Console.WriteLine("\n---- Exception8: ----\n");
                Console.WriteLine(e.Message);
            }


            Console.WriteLine("\n/*************************************************************************/\n");
            /*******************************************************************************************/

        }
    }
}
