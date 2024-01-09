namespace ParkingProgramTests;

using DesafioFundamentos.Models;
using DesafioFundamentos.Services;
using Xunit;

public class ParkingRegistrationTests
{
    [Fact]
    public void RemoveRegisteredVehicle_ShouldAddRemoveVehicleAndReturnRecord()
    {
        // Arrange
        ParkingRegistration parkingRegistration = new();
        Vehicle vehicle = new("ABC-1234");

        parkingRegistration.RegisterVehicle(vehicle);

        // Act
        ParkingRecord record = parkingRegistration.RemoveRegisteredVehicle(vehicle);

        // Assert
        Assert.True(parkingRegistration.IsRegistrationEmpty());
        Assert.NotNull(record);
        Assert.Equal(vehicle, record.Vehicle);
    }
}
