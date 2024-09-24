using Elevator_GilbertoTongco.Interfaces;

namespace Elevator_GilbertoTongco
{
    // Concrete Command for Elevator Call
    public class ElevatorCallCommand : ICommand
    {
        private readonly IElevatorController _controller;
        private readonly ElevatorRequest _request;

        public ElevatorCallCommand(IElevatorController controller, ElevatorRequest request)
        {
            _controller = controller;
            _request = request;
        }

        public void Execute()
        {
            _controller.HandleCall(_request);
        }
    }
}
