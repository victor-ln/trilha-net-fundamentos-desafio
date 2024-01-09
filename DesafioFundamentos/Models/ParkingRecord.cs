namespace DesafioFundamentos.Models
{
    public class ParkingRecord : IComparable<ParkingRecord>
    {
        public DateTime Entry { get; }
        public DateTime? Departure { get; private set; } = null;
        public TimeSpan ParkingDuration
        {
            get => Departure.HasValue ? Departure.Value - Entry : DateTime.Now - Entry;
        }
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

        public int CompareTo(ParkingRecord other)
        {
            return Vehicle.CompareTo(other.Vehicle);
        }
    }
}