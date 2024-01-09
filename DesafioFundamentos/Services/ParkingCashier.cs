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

            TimeSpan parkingDuration = record.ParkingDuration;

            decimal totalPrice = CalculateParkingPrice(parkingDuration);

            Console.WriteLine($"Tempo estacionado: {parkingDuration}");
            Console.WriteLine($"Preço total: {totalPrice:C}.");
        }

        public void ShowParkingHistory()
        {
            Console.WriteLine("| Veículo            | Horário de Entrada | Horário de Saída   | Tempo Estacionado  | Preço Pago         |");
            Console.WriteLine("+--------------------+--------------------+--------------------+--------------------+--------------------+");

            decimal totalRevenue = 0;

            foreach (var record in vehicleHistory)
            {
                var parkingDuration = record.ParkingDuration;
                var parkingPrice = CalculateParkingPrice(parkingDuration);

                Console.Write($"| {record.Vehicle.LicensePlate,-18} ");
                Console.Write($"| {record.Entry,-18:HH:mm:ss} ");
                Console.Write($"| {record.Departure,-18:HH:mm:ss} ");
                Console.Write($"| {parkingDuration,-18:hh\\:mm\\:ss} ");
                Console.Write($"| {parkingPrice,-18:C} |\n");

                totalRevenue += parkingPrice;
            }

            Console.WriteLine("+--------------------+--------------------+--------------------+--------------------+--------------------+");
            Console.WriteLine($"| Total              |                    |                    |                    | {totalRevenue,-18:C} |");
            Console.WriteLine("+--------------------+--------------------+--------------------+--------------------+--------------------+");
        }

        private decimal CalculateParkingPrice(TimeSpan parkingDuration)
        {
            uint hoursParked = Convert.ToUInt32(
                Math.Floor(parkingDuration.TotalHours)
            );

            return initialPrice + (hoursParked * pricePerHour);
        }
    }
}