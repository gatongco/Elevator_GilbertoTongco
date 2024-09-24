using Elevator_GilbertoTongco.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator_GilbertoTongco
{
    public enum Direction
    {
        Up,
        Down,
        Idle
    }

    // Receiver
    public class Elevator : IElevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public List<int> Destinations { get; set; }
        public Direction CurrentDirection { get; set; }
        public Direction TargetDirection { get; set; }
        public bool IsMoving { get; set; }

        public Elevator(int id, int initialFloor)
        {
            Id = id;
            CurrentFloor = initialFloor;
            Destinations = new List<int>();
            CurrentDirection = Direction.Idle;
            IsMoving = false;
        }

        public async Task Move()
        {
            if (Destinations.Count == 0) return;

            IsMoving = true;

            // Move the elevator to the next floor
            switch (CurrentDirection)
            {
                case Direction.Up:
                    CurrentFloor++;
                    break;
                case Direction.Down:
                    CurrentFloor--;
                    break;
            }

            // Simulate time to move between floors
            await Task.Delay(5000); 

            IsMoving = false;
        }

        public void AddDestination(int destinationFloor)
        {
            if (!Destinations.Contains(destinationFloor))
            {
                Destinations.Add(destinationFloor);
                Destinations.Sort();

                if (destinationFloor > CurrentFloor)
                {
                    CurrentDirection = Direction.Up;
                }
                else if (destinationFloor < CurrentFloor)
                { 
                    CurrentDirection = Direction.Down;
                }
                Console.WriteLine($"Car {Id} heading {CurrentDirection} to new destination floor {string.Join(", ", Destinations)}");
            }
        }

        public int AddRandomDestination(int numberOfFloors)
        {
            Random random = new Random();
            int? newDestination = null;

            if (TargetDirection == Direction.Up && CurrentFloor < numberOfFloors)
            {
                newDestination = random.Next(CurrentFloor + 1, numberOfFloors);
            }
            else if (TargetDirection == Direction.Down && CurrentFloor > 1)
            {
                newDestination = random.Next(1, CurrentFloor - 1);
            }
            else
            {
                CurrentDirection = Direction.Idle;
                TargetDirection = Direction.Idle;
                Destinations.Clear();
            }
            if (newDestination.HasValue)
            {
                AddDestination(newDestination.Value);
            }
            return newDestination ?? 0;
        }
    }
}
