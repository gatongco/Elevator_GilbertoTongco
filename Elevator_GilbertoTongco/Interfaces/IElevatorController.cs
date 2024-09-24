using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_GilbertoTongco.Interfaces
{
    public interface IElevatorController
    {
        public void HandleCall(ElevatorRequest request); 
        public Task MoveElevators();
    }
}
