using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public interface IValidadorDePlacas
    {
        public string FormatoPlaca { get; }

        bool PlacaEhValida(string placa);
    }

    public class ValidadorDePlacasBr : IValidadorDePlacas
    {
        public string FormatoPlaca => "ABC-1D23 | ABC-1234";

        private readonly Regex regexPlaca =
            new(@"^[A-Z]{3}-[0-9][A-Z0-9][0-9]{2}$");

        public bool PlacaEhValida(string placa)
        {
            if (string.IsNullOrEmpty(placa)) {
                return false;
            }
            return regexPlaca.IsMatch(placa);
        }
    }

    public class Estacionamento
    {
        private readonly decimal precoInicial;
        private readonly decimal precoPorHora;
        private readonly List<string> veiculos = new();
        private readonly IValidadorDePlacas validadorDePlaca;

        public Estacionamento(
            decimal precoInicial,
            decimal precoPorHora,
            IValidadorDePlacas validadorDePlaca
        )
        {
            if (precoInicial < 0 || precoPorHora < 0) {
                throw new ArgumentException("Preços não podem ser negativos");
            }
            if (validadorDePlaca == null) {
                throw new ArgumentNullException(nameof(validadorDePlaca));
            }
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            this.validadorDePlaca = validadorDePlaca;
        }

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            if (precoInicial < 0 || precoPorHora < 0) {
                throw new ArgumentException("Preços não podem ser negativos");
            }
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            this.validadorDePlaca = new ValidadorDePlacasBr();
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = LerPlacaDeCarro();

            if (veiculos.Contains(placa)) {
                Console.WriteLine("Veículo já está estacionado");
            }
            else {
                veiculos.Add(placa);
            }
        }

        public void RemoverVeiculo()
        {
            if (!veiculos.Any()) {
                Console.WriteLine("Não há veículos estacionados.");
                return;
            }
            ListarVeiculos();

            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = LerPlacaDeCarro();

            if (veiculos.Contains(placa)) {
                uint horasEstacionado = LerHorasEstacionado();
                decimal valorTotal = CalcularValorEstacionamento(horasEstacionado);

                veiculos.Remove(placa);

                Console.WriteLine($"O veículo {placa} foi removido.");
                Console.WriteLine($"O preço total foi de {valorTotal:C}.");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                veiculos.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private static uint LerHorasEstacionado()
        {
            string horasString;

            while (true)
            {
                Console.WriteLine("Digite a quantidade de horas que o " +
                                  "veículo permaneceu estacionado:");

                horasString = Console.ReadLine();
                if (uint.TryParse(horasString, out uint horas)) {
                    return horas;
                }
                Console.Error.WriteLine("Valor de horas inválido.");
            }
        }

        private string LerPlacaDeCarro()
        {
            string placa;

            while (true)
            {
                placa = Console.ReadLine().ToUpper();

                if (validadorDePlaca.PlacaEhValida(placa)) {
                    return placa;
                }
                Console.Error.WriteLine("Formato de placa inválido.");
                Console.WriteLine($"Formatos aceitos: {validadorDePlaca.FormatoPlaca}");
                Console.WriteLine("Tente novamente: ");
            }
        }

        private decimal CalcularValorEstacionamento(uint horasEstacionado) {
            return precoInicial + (precoPorHora * horasEstacionado);
        }
    }
}
