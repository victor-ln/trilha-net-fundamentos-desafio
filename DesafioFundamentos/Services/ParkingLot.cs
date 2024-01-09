using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Models;

namespace DesafioFundamentos.Services
{
    public class ParkingLot
    {
        private readonly IParkingRegistrationService parkingRegistration;
        private readonly IParkingCashier parkingCashier;
        private readonly ILicensePlateReader licensePlateReader;

        public ParkingLot(
            IParkingRegistrationService registrationService,
            IParkingCashier parkingCashier,
            ILicensePlateReader licensePlateReader
        )
        {
            this.parkingRegistration = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
            this.parkingCashier = parkingCashier ?? throw new ArgumentNullException(nameof(parkingCashier));
            this.licensePlateReader = licensePlateReader ?? throw new ArgumentNullException(nameof(licensePlateReader));
        }

        public void AddVehicle()
        {
            try
            {
                string licensePlate = ReadCarLicensePlate();

                parkingRegistration.RegisterVehicle(new Vehicle(licensePlate));
            }
            catch (ParkingRegistrationException e)
            {
                Console.Error.WriteLine(e.Message);
            }
            catch (InvalidLicensePlateException e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        public void RemoveVehicle()
        {
            if (parkingRegistration.IsRegistrationEmpty())
            {
                Console.WriteLine("Não há veículos estacionados.");
                return;
            }
            parkingRegistration.ListRegisteredVehicles();

            try
            {
                string licensePlate = ReadCarLicensePlate();
                Vehicle vehicle = new Vehicle(licensePlate);
                ParkingRecord record = parkingRegistration.RemoveRegisteredVehicle(vehicle);

                parkingCashier.PayParkingFee(record);
            }
            catch (ParkingRegistrationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidLicensePlateException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ListVehicles() => parkingRegistration.ListRegisteredVehicles();
        public void ShowParkingHistory() => parkingCashier.ShowParkingHistory();

        private string ReadCarLicensePlate()
        {
            Console.Write("Digite a placa do veículo: ");

            return licensePlateReader.ReadLicensePlate();
        }
    }
}
