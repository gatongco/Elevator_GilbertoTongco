using Elevator_GilbertoTongco.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Elevator_GilbertoTongco
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IElevatorController, ElevatorController>(provider =>
                    new ElevatorController(4, 10))
                .AddSingleton<Random>()
                .BuildServiceProvider();

            var controller = serviceProvider.GetService<IElevatorController>();
            var random = serviceProvider.GetService<Random>();

            //Simulate people calling elevators up or down
            for (int i = 0; i < 20; i++)
            {
                int floor = random.Next(1, 10);
                ElevatorRequest elevatorRequest = GetIsUpRequest(floor, random)
                     ? new UpRequest(floor)
                     : new DownRequest(floor);

                ICommand command = new ElevatorCallCommand(controller, elevatorRequest);
                command.Execute();

                await controller.MoveElevators();
                Console.WriteLine();
            }
        }

        private static bool GetIsUpRequest(int floor, Random random)
        {
            return floor switch
            {
                1 => true,
                10 => false,
                _ => random.Next(0, 2) == 0
            };
        }
    }
}
