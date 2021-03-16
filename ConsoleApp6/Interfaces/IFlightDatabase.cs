using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public interface IFlightDatabase
    {
        void SetGenerator(SequenceGenerator sg);
        int GetRandom();
        IFlight CreateFlight(int id, IAirport src, IAirport dst, IPlane plane, double pricePerUnit);
        void InsertFlight(IFlight f);

        void DeleteFlight(IFlight f);

        List<IFlight> GetFlightsList();

        List<IFlight> FindBySrcAirport(IAirport src);

        List<IFlight> FindByDestAirport(IAirport dst);

        List<IFlight> FindByPlaneStatus(Status s);

        List<IFlight> FindByMaxPrice(double max);

    }
}
