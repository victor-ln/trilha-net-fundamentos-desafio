using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Models;
using DesafioFundamentos.Services;
using Moq;

namespace ParkingProgramTests;

public class ParkingLotTests
{
    [Fact]
    public void AddVehicle_ShouldRegisterVehicle()
    {
        // Arrange
        Mock<IParkingRegistrationService> mockRegistrationService = new();
        Mock<IParkingCashier> mockParkingCashier = new();
        Mock<ILicensePlateReader> mockLicensePlateReader = new();

        ParkingLot parkingLot = new(
            mockRegistrationService.Object,
            mockParkingCashier.Object,
            mockLicensePlateReader.Object
        );

        mockLicensePlateReader.SetupSequence(r => r.ReadLicensePlate())
            .Returns("ABC-1234");

        // Act
        parkingLot.AddVehicle();

        // Assert
        mockRegistrationService.Verify(r => r.RegisterVehicle(It.IsAny<Vehicle>()), Times.Once);
    }

    [Fact]
    public void RemoveVehicle_ShouldRemoveVehicleAndPayFee()
    {
        // Arrange
        Mock<IParkingRegistrationService> mockRegistrationService = new();
        Mock<IParkingCashier> mockParkingCashier = new();
        Mock<ILicensePlateReader> mockLicensePlateReader = new();

        ParkingLot parkingLot = new(
            mockRegistrationService.Object,
            mockParkingCashier.Object,
            mockLicensePlateReader.Object
        );

        mockRegistrationService.Setup(r => r.IsRegistrationEmpty()).Returns(false);
        mockRegistrationService.Setup(r => r.RemoveRegisteredVehicle(It.IsAny<Vehicle>()))
            .Returns(new ParkingRecord(new Vehicle("ABC-1234")));

        mockLicensePlateReader.SetupSequence(r => r.ReadLicensePlate())
            .Returns("ABC-1234");

        // Act
        parkingLot.RemoveVehicle();

        // Assert
        mockParkingCashier.Verify(c => c.PayParkingFee(It.IsAny<ParkingRecord>()), Times.Once);
    }
}
