using System.Text.RegularExpressions;
using DesafioFundamentos.Interfaces;

namespace DesafioFundamentos.Services
{
    public class InvalidLicensePlateException : Exception
    {
        public InvalidLicensePlateException(string message) : base(message) { }
    }

    public class BrazilianLicensePlateReader : ILicensePlateReader
    {
        private readonly string PlateFormat = "ABC-1D23 | ABC-1234";

        private readonly Regex plateRegex =
            new(@"^[A-Z]{3}-[0-9][A-Z0-9][0-9]{2}$");

        private bool IsPlateValid(string plate)
        {
            if (string.IsNullOrEmpty(plate))
            {
                return false;
            }
            return plateRegex.IsMatch(plate);
        }

        public string ReadLicensePlate()
        {
            string plate = Console.ReadLine().ToUpper();

            if (!IsPlateValid(plate))
                throw new InvalidLicensePlateException($"Formato de placa inválido. Formatos aceitos: {PlateFormat}");

            return plate;
        }
    }
}