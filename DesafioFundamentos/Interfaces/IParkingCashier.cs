using DesafioFundamentos.Models;

namespace DesafioFundamentos.Interfaces
{
    public interface IParkingCashier
    {
        public void PayParkingFee(ParkingRecord record);
        public void ShowParkingHistory();
    }
}