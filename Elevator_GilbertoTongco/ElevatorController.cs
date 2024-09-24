using Elevator_GilbertoTongco.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator_GilbertoTongco
{
    // Invoker
    public class ElevatorController : IElevatorController
    {
        private readonly Random random;
        private List<Elevator> Elevators;
        private int NumberOfFloors;

        public ElevatorController(int numberOfElevators, int numberOfFloors)
        {
            Elevators = new List<Elevator>(numberOfElevators);
            NumberOfFloors = numberOfFloors;
            random = new Random();

            InitializeElevators(numberOfElevators);
        }

        private void InitializeElevators(int numberOfElevators)
        {
            for (int i = 0; i < numberOfElevators; i++)
            {
                int initialFloor = random.Next(1, NumberOfFloors + 1);
                var elevator = new Elevator(i + 1, initialFloor);
                Elevators.Add(elevator);
                Console.WriteLine($"Car {elevator.Id} is idle at floor {elevator.CurrentFloor}");
            }
            Console.WriteLine();
        }

        public void HandleCall(ElevatorRequest request)
        {
            Console.WriteLine($"{request.RequestedDirection} request on floor {request.RequestingFloor} received.");

            IElevator? closestElevator = FindClosestElevator(request);

            if (closestElevator == null)
            {
                Console.WriteLine("All elevators are busy. Please wait.");
                return;
            }

            if (request.RequestingFloor != closestElevator.CurrentFloor)
            {
                Console.WriteLine($"Closest available elevator is car {closestElevator.Id}");
                closestElevator.AddDestination(request.RequestingFloor);
            }
            else
            {
                Console.WriteLine($"Car {closestElevator.Id} is already at {request.RequestingFloor}");
                closestElevator.AddRandomDestination(NumberOfFloors);
            }
        }

        private IElevator? FindClosestElevator(ElevatorRequest request)
        {
            IElevator? closestElevator = null;
            int closestDistance = NumberOfFloors;

            foreach (var elevator in Elevators)
            {
                int distance = Math.Abs(elevator.CurrentFloor - request.RequestingFloor);
                if (distance < closestDistance
                    && (elevator.CurrentDirection == request.RequestedDirection || elevator.CurrentDirection == Direction.Idle))
                {
                    if ((elevator.CurrentDirection == Direction.Up && request.RequestingFloor > elevator.CurrentFloor) ||
                        (elevator.CurrentDirection == Direction.Down && request.RequestingFloor < elevator.CurrentFloor) ||
                        elevator.CurrentDirection == Direction.Idle)
                    {
                        elevator.TargetDirection = request.RequestedDirection;
                        closestDistance = distance;
                        closestElevator = elevator;
                    }
                }
            }
            return closestElevator;
        }

        public async Task MoveElevators()
        {
            foreach (var elevator in Elevators)
            {
                await elevator.Move();
                Console.WriteLine();

                if (elevator.Destinations.Contains(elevator.CurrentFloor))
                {
                    await HandleArrival(elevator);
                }
                else
                {
                    string status = elevator.CurrentDirection == Direction.Idle
                        ? $"Car {elevator.Id} is {elevator.CurrentDirection} on floor {elevator.CurrentFloor}"
                        : $"Car {elevator.Id} is at floor {elevator.CurrentFloor} going {elevator.CurrentDirection} to floor {string.Join(", ", elevator.Destinations)}";
                    Console.WriteLine(status);
                }
            }
        }

        private async Task HandleArrival(Elevator elevator)
        {
            Console.WriteLine($"Car {elevator.Id} has arrived at floor {elevator.CurrentFloor}");
            elevator.Destinations.Remove(elevator.CurrentFloor);

            // Simulate time for passengers to enter/leave
            await Task.Delay(5000);

            elevator.AddRandomDestination(NumberOfFloors);
        }
    }

}
