using Elevator_GilbertoTongco;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ElevatorTests
    {
        [Fact]
        public void AddRandomDestination_ShouldAddValidDestination()
        {
            // Arrange
            int initialFloor = 1;
            int numberOfFloors = 10;
            var elevator = new Elevator(1, initialFloor);

            // Act
            int newDestination = elevator.AddRandomDestination(numberOfFloors);

            // Assert
            Assert.InRange(newDestination, 1, numberOfFloors);
            Assert.NotEqual(initialFloor, newDestination);
        }

        [Fact]
        public async Task Move_GoingUp_ShouldReachHigherFloor()
        {
            // Arrange
            int initialFloor = 1;
            int destinationFloor = 5;
            var elevator = new Elevator(1, initialFloor);
            elevator.AddDestination(destinationFloor);
            elevator.CurrentDirection = Direction.Up;

            // Act
            await elevator.Move();

            // Assert
            Assert.True(elevator.CurrentFloor > initialFloor);
        }
        [Fact]
        public async Task Move_GoingDown_ShouldReachLowerFloor()
        {
            // Arrange
            int initialFloor = 5;
            int destinationFloor = 2;
            var elevator = new Elevator(1, initialFloor);
            elevator.AddDestination(destinationFloor);
            elevator.CurrentDirection = Direction.Down;

            // Act
            await elevator.Move();

            // Assert
            Assert.True(elevator.CurrentFloor < initialFloor);
        }
        [Theory]
        [InlineData(2, 5)]
        [InlineData(1, 10)]
        [InlineData(9, 10)]
        [InlineData(1, 2)]
        public async Task Move_GoingUp_ShouldReachDestination(int initialFloor, int destinationFloor)
        {
            // Arrange
            var elevator = new Elevator(1, initialFloor);
            elevator.AddDestination(destinationFloor);
            elevator.CurrentDirection = Direction.Up;

            // Act
            do
            {
                await elevator.Move();
            }
            while (elevator.CurrentFloor < destinationFloor);


            // Assert
            Assert.Equal(destinationFloor, elevator.CurrentFloor);
        }
        [Theory]
        [InlineData(5, 2)]
        [InlineData(10, 1)]
        [InlineData(10, 9)]
        [InlineData(2, 1)]
        public async Task Move_GoingDown_ShouldReachDestination(int initialFloor, int destinationFloor)
        {
            // Arrange
            var elevator = new Elevator(1, initialFloor);
            elevator.AddDestination(destinationFloor);
            elevator.CurrentDirection = Direction.Down;

            // Act
            do
            {
                await elevator.Move();
            }
            while (elevator.CurrentFloor > destinationFloor);


            // Assert
            Assert.Equal(destinationFloor, elevator.CurrentFloor);
        }
        [Fact]
        public void AddRandomDestination_UpDirection_ShouldAddValidDestination()
        {
            // Arrange
            int initialFloor = 1;
            int numberOfFloors = 10;
            var elevator = new Elevator(1, initialFloor)
            {
                TargetDirection = Direction.Up
            };

            // Act
            int newDestination = elevator.AddRandomDestination(numberOfFloors);

            // Assert
            Assert.InRange(newDestination, initialFloor + 1, numberOfFloors);
            Assert.NotEqual(initialFloor, newDestination);
        }
        [Fact]
        public void AddRandomDestination_DownDirection_ShouldAddValidDestination()
        {
            // Arrange
            int initialFloor = 10;
            int numberOfFloors = 10;
            var elevator = new Elevator(1, initialFloor)
            {
                TargetDirection = Direction.Down
            };

            // Act
            int newDestination = elevator.AddRandomDestination(numberOfFloors);

            // Assert
            Assert.InRange(newDestination, 1, initialFloor - 1);
            Assert.NotEqual(initialFloor, newDestination);
        }
        [Fact]
        public void AddRandomDestination_UpDirection_ShouldNotExceedNumberOfFloors()
        {
            // Arrange
            int initialFloor = 8;
            int numberOfFloors = 10;
            var elevator = new Elevator(1, initialFloor)
            {
                TargetDirection = Direction.Up
            };

            // Act
            int newDestination = elevator.AddRandomDestination(numberOfFloors);

            // Assert
            Assert.InRange(newDestination, initialFloor + 1, numberOfFloors);
        }
        [Fact]
        public void AddRandomDestination_DownDirection_ShouldNotBeLessThanOne()
        {
            // Arrange
            int initialFloor = 3;
            int numberOfFloors = 10;
            var elevator = new Elevator(1, initialFloor)
            {
                TargetDirection = Direction.Down
            };

            // Act
            int newDestination = elevator.AddRandomDestination(numberOfFloors);

            // Assert
            Assert.InRange(newDestination, 1, initialFloor - 1);
        }
    }
}
