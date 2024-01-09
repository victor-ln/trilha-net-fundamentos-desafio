using DesafioFundamentos.Models;

namespace DesafioFundamentos.Interfaces
{
    public interface IParkingRegistrationService
    {
        public void RegisterVehicle(Vehicle vehicle);
        public ParkingRecord RemoveRegisteredVehicle(Vehicle vehicle);
        public bool IsRegistrationEmpty();
        public void ListRegisteredVehicles();
    }
}