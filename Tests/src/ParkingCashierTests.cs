namespace ParkingProgramTests;

using DesafioFundamentos.Models;
using DesafioFundamentos.Services;
using System;
using Xunit;

public class ParkingCashierTests
{
    [Fact]
    public void PayParkingFee_ShouldAddRecordAndPrintDetails()
    {
        // Arrange
        decimal initialPrice = 5.0m;
        decimal pricePerHour = 2.0m;
        ParkingCashier parkingCashier = new(initialPrice, pricePerHour);

        Vehicle vehicle = new("ABC-1234");
        ParkingRecord parkingRecord = new(vehicle);

        // Redirect Console.Out to a StringWriter
        using StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        // Act
        parkingCashier.PayParkingFee(parkingRecord);

        // Restore the standard output stream
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));

        // Get the captured console output
        string consoleOutput = stringWriter.ToString().Trim();

        // Assert the console output as needed
        Assert.Contains($"Preço total: {5:C}.", consoleOutput);
    }

    [Fact]
    public void ShowParkingHistory_ShouldPrintHistoryAndTotalRevenue()
    {
        // Arrange
        decimal initialPrice = 5.0m;
        decimal pricePerHour = 2.0m;
        ParkingCashier parkingCashier = new(initialPrice, pricePerHour);

        Vehicle vehicle1 = new("ABC-1234");
        Vehicle vehicle2 = new("XYZ-5678");

        ParkingRecord parkingRecord1 = new(vehicle1);
        ParkingRecord parkingRecord2 = new(vehicle2);

        // Redirect Console.Out to a StringWriter
        using StringWriter stringWriter = new();
        Console.SetOut(stringWriter);

        parkingCashier.PayParkingFee(parkingRecord1);
        parkingCashier.PayParkingFee(parkingRecord2);

        parkingCashier.ShowParkingHistory();

        // Restore the standard output stream
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));

        // Get the captured console output
        string consoleOutput = stringWriter.ToString().Trim();

        // Assert
        Assert.Contains($"{5:C}.", consoleOutput);
        Assert.Contains($"{10:C}", consoleOutput);
    }
}
