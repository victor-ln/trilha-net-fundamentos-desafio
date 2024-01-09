namespace DesafioFundamentos.Models
{
    public class Vehicle : IComparable<Vehicle>
    {
        public string LicensePlate { get; }

        public Vehicle(string licensePlate)
        {
            LicensePlate = licensePlate;
        }

        public int CompareTo(Vehicle other)
        {
            return string.Compare(LicensePlate, other.LicensePlate, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return obj is Vehicle vehicle && LicensePlate == vehicle.LicensePlate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LicensePlate);
        }

        public override string ToString()
        {
            return LicensePlate;
        }
    }
}