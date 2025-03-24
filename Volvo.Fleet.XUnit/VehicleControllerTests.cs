using Moq;
using Volvo.Fleet.Domain.Enums;
using Volvo.Fleet.Domain.Models.Vehicle;
using Volvo.Fleet.Domain.Services;
using Volvo.Fleet.WebApi.Controllers;

public class VehicleControllerTests
{
    private readonly VehicleController _controller;
    private readonly Mock<IVehicleService> _vehicleServiceMock;

    public VehicleControllerTests()
    {
        _vehicleServiceMock = new Mock<IVehicleService>();
        _controller = new VehicleController(_vehicleServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnListOfVehicles()
    {
        _vehicleServiceMock.Setup(s => s.GetAll()).ReturnsAsync(new List<VehicleDetail> { new VehicleDetail() });
        var result = await _controller.GetAll();
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetById_ShouldReturnVehicle()
    {
        var vehicleId = "JHMEJ6630NS-18263355";
        _vehicleServiceMock.Setup(s => s.GetById(vehicleId)).ReturnsAsync(new VehicleDetail());
        var result = await _controller.GetById(vehicleId);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetById_InvalidId_ShouldThrowException()
    {
        _vehicleServiceMock.Setup(s => s.GetById(It.IsAny<string>())).ThrowsAsync(new Exception("Vehicle not found"));
        await Assert.ThrowsAsync<Exception>(() => _controller.GetById("invalid"));
    }

    [Fact]
    public async Task Create_ShouldReturnId()
    {
        var model = new VehicleModel { ChassisSeries = "ABC", ChassisNumber = 123, Type = VehicleTypeEnum.Car, Color = "Blue" };
        _vehicleServiceMock.Setup(s => s.CreateAsync(model)).ReturnsAsync("ABC-123");
        var result = await _controller.Create(model);
        Assert.Equal("ABC-123", result);
    }

    [Fact]
    public async Task Create_NullModel_ShouldThrowException()
    {
        await Assert.ThrowsAsync<Exception>(() => _controller.Create(null));
    }

    [Fact]
    public async Task Create_DuplicateVehicle_ShouldThrowException()
    {
        var model = new VehicleModel { ChassisSeries = "JHMEJ6630NS", ChassisNumber = 18263355, Type = VehicleTypeEnum.Truck, Color = "Red" };
        _vehicleServiceMock.Setup(s => s.CreateAsync(model)).ThrowsAsync(new Exception("Vehicle already exists"));
        await Assert.ThrowsAsync<Exception>(() => _controller.Create(model));
    }

    [Fact]
    public async Task Create_InvalidColor_ShouldThrowException()
    {
        var model = new VehicleModel { ChassisSeries = "JHMEJ6630NS", ChassisNumber = 18263355, Type = VehicleTypeEnum.Truck };
        _vehicleServiceMock.Setup(s => s.CreateAsync(model)).ThrowsAsync(new Exception("Color is required"));
        await Assert.ThrowsAsync<Exception>(() => _controller.Create(model));
    }

    [Fact]
    public async Task Edit_ShouldCompleteWithoutError()
    {
        var model = new VehicleEditModel { Color = "Red" };
        await _controller.Edit("JHMEJ6630NS-18263355", model);
        _vehicleServiceMock.Verify(s => s.EditAsync("JHMEJ6630NS-18263355", model), Moq.Times.Once);
    }

    [Fact]
    public async Task Edit_InvalidId_ShouldThrowException()
    {
        var model = new VehicleEditModel { Color = "Red" };
        _vehicleServiceMock.Setup(s => s.EditAsync(It.IsAny<string>(), It.IsAny<VehicleEditModel>()))
                           .ThrowsAsync(new Exception("Vehicle not found"));
        await Assert.ThrowsAsync<Exception>(() => _controller.Edit("invalid", model));
    }

    [Fact]
    public async Task Edit_EmptyColor_ShouldThrowException()
    {
        var model = new VehicleEditModel { Color = "" };
        _vehicleServiceMock.Setup(s => s.EditAsync(It.IsAny<string>(), model)).ThrowsAsync(new Exception("Color is required."));
        await Assert.ThrowsAsync<Exception>(() => _controller.Edit("JHMEJ6630NS-18263355", model));
    }
}
