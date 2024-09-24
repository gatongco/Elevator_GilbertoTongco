using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_GilbertoTongco
{
    public abstract class ElevatorRequest
    {
        public int RequestingFloor { get; }

        protected ElevatorRequest(int requestingFloor)
        {
            RequestingFloor = requestingFloor;
        }

        public abstract Direction RequestedDirection { get; }
    }

    public class UpRequest : ElevatorRequest
    {
        public UpRequest(int requestingFloor) : base(requestingFloor) { }

        public override Direction RequestedDirection => Direction.Up;
    }

    public class DownRequest : ElevatorRequest
    {
        public DownRequest(int requestingFloor) : base(requestingFloor) { }

        public override Direction RequestedDirection => Direction.Down;
    }
}
