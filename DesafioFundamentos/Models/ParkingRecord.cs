namespace DesafioFundamentos.Models
{
    public class ParkingRecord : IComparable<ParkingRecord>
    {
        public DateTime Entry { get; }
        public DateTime? Departure { get; private set; } = null;
        public Vehicle Vehicle { get; }

        public ParkingRecord(Vehicle vehicle)
        {
            Entry = DateTime.Now;
            Vehicle = vehicle;
        }

        public void RecordDeparture()
        {
            Departure ??= DateTime.Now;
        }

        public TimeSpan GetParkingDuration()
        {
            if (Departure is null)
                return DateTime.Now - Entry;
            else
                return Departure.Value - Entry;
        }

        public int CompareTo(ParkingRecord other)
        {
            return Vehicle.CompareTo(other.Vehicle);
        }
    }
}