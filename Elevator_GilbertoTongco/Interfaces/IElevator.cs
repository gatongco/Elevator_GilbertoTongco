using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_GilbertoTongco.Interfaces
{
    public interface IElevator
    {
        int Id { get; }
        int CurrentFloor { get; }
        Direction CurrentDirection { get; }
        List<int> Destinations { get; }
        public void AddDestination(int destinationFloor);
        public int AddRandomDestination(int numberOfFloors);
        public Task Move();
    }
}
