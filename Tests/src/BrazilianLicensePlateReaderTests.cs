using DesafioFundamentos.Services;

namespace ParkingProgramTests;

public class BrazilianLicensePlateReaderTests
{
    [Theory]
    [InlineData("ABC-1234")]
    [InlineData("XYZ-1561")]
    [InlineData("XYZ-1F61")]
    [InlineData("anc-1e11")]
    [InlineData("aaa-0a00")]
    [InlineData("PLQ-9876")]
    [InlineData("JKL-2345")]
    [InlineData("ZZZ-0000")]
    [InlineData("MNO-5555")]
    public void ReadLicensePlate_ValidLicensePlate_ShouldReturnLicensePlate(string input)
    {
        // Use a TextReader to simulate user input
        using var stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        BrazilianLicensePlateReader licensePlateReader = new();

        // Act
        string result = licensePlateReader.ReadLicensePlate();

        // Assert
        Assert.Equal(input.ToUpper(), result);
    }

    [Theory]
    [InlineData("InvalidPlate")]
    [InlineData("XYZ-15A1")]
    [InlineData("XYZ-1F6A")]
    [InlineData("142-1e11")]
    [InlineData("aaa0a00")]
    [InlineData("WrongFormat")]
    [InlineData("ABC-12")]
    [InlineData("PQR-12345")]
    [InlineData("ZZ-1234")]
    public void ReadLicensePlate_InvalidLicensePlate_ShouldThrowException(string invalidInput)
    {
        // Use a TextReader to simulate user input
        using var stringReader = new StringReader(invalidInput);
        Console.SetIn(stringReader);

        BrazilianLicensePlateReader licensePlateReader = new();

        // Act & Assert
        Assert.Throws<InvalidLicensePlateException>(licensePlateReader.ReadLicensePlate);
    }
}
