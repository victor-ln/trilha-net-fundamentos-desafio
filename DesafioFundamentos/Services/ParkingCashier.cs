using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Models;

namespace DesafioFundamentos.Services
{
    public class ParkingCashier : IParkingCashier
    {
        private readonly decimal initialPrice;
        private readonly decimal pricePerHour;
        private readonly List<ParkingRecord> vehicleHistory = new();

        public ParkingCashier(decimal initialPrice, decimal pricePerHour)
        {
            if (initialPrice < 0 || pricePerHour < 0)
            {
                throw new ArgumentException("Preços não podem ser negativos");
            }
            this.initialPrice = initialPrice;
            this.pricePerHour = pricePerHour;
        }

        public void PayParkingFee(ParkingRecord record)
        {
            vehicleHistory.Add(record);

            TimeSpan parkingDuration = record.GetParkingDuration();

            decimal totalPrice = CalculateParkingPrice(parkingDuration);

            Console.WriteLine($"O veículo {record.Vehicle.LicensePlate} foi removido.");
            Console.WriteLine($"Tempo estacionado: {parkingDuration}");
            Console.WriteLine($"Preço total: {totalPrice:C}.");
        }

        public void ShowParkingHistory()
        {
            Console.WriteLine("Histórico de Veículos:");

            foreach (var record in vehicleHistory)
            {
                var parkingDuration = record.GetParkingDuration();
                var parkingPrice = CalculateParkingPrice(parkingDuration);

                Console.Write($"Veículo: {record.Vehicle}, ");
                Console.Write($"Tempo Estacionado: {parkingDuration}, ");
                Console.Write($"Preço pago: {parkingPrice:C}");
                Console.WriteLine();
            }

            Console.WriteLine($"Total arrecadado: {CalculateTotalRevenue():C}");
        }

        private decimal CalculateParkingPrice(TimeSpan parkingDuration)
        {
            uint hoursParked = Convert.ToUInt32(
                Math.Floor(parkingDuration.TotalHours)
            );

            return initialPrice + (hoursParked * pricePerHour);
        }

        private decimal CalculateTotalRevenue()
        {
            return vehicleHistory.Sum(record =>
            {
                return CalculateParkingPrice(
                    record.GetParkingDuration()
                );
            });
        }
    }
}