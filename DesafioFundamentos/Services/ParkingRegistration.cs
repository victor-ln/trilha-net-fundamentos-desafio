using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Models;

namespace DesafioFundamentos.Services
{
    public class ParkingRegistrationException : Exception
    {
        public ParkingRegistrationException(string message) : base(message) { }
    }

    public class ParkingRegistration : IParkingRegistrationService
    {
        private readonly SortedSet<ParkingRecord> RegisteredVehicles = new();

        public void RegisterVehicle(Vehicle vehicle)
        {
            ParkingRecord vehicleRecord = new(vehicle);

            if (RegisteredVehicles.Contains(vehicleRecord))
                throw new ParkingRegistrationException("Veículo já estacionado.");

            RegisteredVehicles.Add(vehicleRecord);
        }

        public ParkingRecord RemoveRegisteredVehicle(Vehicle vehicle)
        {
            ParkingRecord record = RegisteredVehicles.FirstOrDefault(
                r => r.Vehicle.Equals(vehicle)
            );

            if (record is null)
                throw new ParkingRegistrationException("Veículo não está estacionado.");

            RegisteredVehicles.Remove(record);
            record.RecordDeparture();

            return record;
        }

        public bool IsRegistrationEmpty()
        {
            return RegisteredVehicles.Count == 0;
        }

        public void ListRegisteredVehicles()
        {
            if (IsRegistrationEmpty())
            {
                Console.WriteLine("Não há veículos estacionados.");
                return;
            }

            Console.WriteLine("Veículos estacionados:");
            foreach (var vehicleRecord in RegisteredVehicles)
            {
                Console.WriteLine(vehicleRecord.Vehicle);
            }
        }
    }
}
