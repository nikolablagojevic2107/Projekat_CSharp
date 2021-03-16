using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleApp6
{
    public class PlaneNotSuitableException : Exception
    {
        public PlaneNotSuitableException(String msg) : base(msg) { }
    }
    public class PlaneNotOnGroundException: Exception
    {
        public PlaneNotOnGroundException(String msg): base(msg) { }
    }
    public class NoFreeRunwayException: Exception 
    {
        public NoFreeRunwayException(String msg): base(msg) { }
    }
    public class PlaneNotOnRunwayException: Exception
    {
        public PlaneNotOnRunwayException(String msg): base(msg) { }

    }
    public class PlaneNotReadyException: Exception
    {
        public PlaneNotReadyException(String msg) : base(msg) { }
    }
        
    public class NoCrewException:Exception
    {
        public NoCrewException(String msg): base(msg) { }
    }
    public class PlaneCanNotLandException : Exception 
    { 
        public PlaneCanNotLandException(String msg) : base(msg) { }
    }
    public class AirportsNotConnectedException:Exception
    {
        public AirportsNotConnectedException(String msg): base(msg) { }
    }
    public class IdConflictException:Exception
    {
        public IdConflictException(String msg):base(msg) { }
    }
    public class FlightNotInDatabaseException:Exception
    {
        public FlightNotInDatabaseException(String msg):base(msg) { }
    }
}
