using Itmo.ObjectOrientedProgramming.Lab1;
using Itmo.ObjectOrientedProgramming.Lab1.Result;
using Itmo.ObjectOrientedProgramming.Lab1.RouteAndSections;
using Itmo.ObjectOrientedProgramming.Lab1.Train;
using Xunit;

namespace Lab1.Tests;

/// <summary>
/// The main test class.
/// </summary>
public class UnitTests
{
    /// <summary>
    /// 1st test case.
    /// Маршрут:
    /// силовой путь, ускоряющий поезд до допустимой скорости маршрута
    /// обычный путь.
    /// Результат: успех.
    /// </summary>
    [Fact]
    public void Case1()
    {
        // Arrange
        var route = new Route(100000);
        route.AddSection(new PowerMagneticPath(13, 5));
        route.AddSection(new OrdinaryMagneticPath(500));
        var train = new SimpleTrain(101.32, 13, 4.22);
        var simulator = new Simulator(train, route);

        // Act
        ResultBase simulatorRez = simulator.Run();

        // Assert
        Assert.True(simulatorRez is ResultBase.Success);
        Assert.Equal(143.48, route.TimeAns);
    }

    /// <summary>
    /// 2nd test case.
    /// Маршрут:
    /// силовой путь, ускоряющий поезд более допустимой скорости маршрута
    /// обычный путь
    /// Результат: неудача.
    /// </summary>
    [Fact]
    public void Case2()
    {
        // Arrange
        var route = new Route(2);
        route.AddSection(new PowerMagneticPath(130, 5)); // уже здесь неудача
        route.AddSection(new OrdinaryMagneticPath(500));
        var train = new SimpleTrain(101.32, 13, 4.22);
        var simulator = new Simulator(train, route);

        // Act
        ResultBase simulatorRez = simulator.Run();

        // Assert
        Assert.True(simulatorRez is ResultBase.Failure);
        Assert.Equal(0, route.TimeAns);
    }

    /// <summary>
    /// 3rd test case.
    /// Маршрут:
    /// силовой путь, ускоряющий поезд до допустимой скорости маршрута и станции
    /// обычный путь
    /// станция
    /// обычный путь.
    /// Результат: успех.
    /// </summary>
    [Fact]
    public void Case3()
    {
        // Arrange
        var route = new Route(10000);
        route.AddSection(new PowerMagneticPath(13, 5));
        route.AddSection(new OrdinaryMagneticPath(500));
        route.AddSection(new Station(100, 23));
        route.AddSection(new OrdinaryMagneticPath(50));
        var train = new SimpleTrain(101.32, 13, 4.22);
        var simulator = new Simulator(train, route);

        // Act
        ResultBase simulatorRez = simulator.Run();

        // Assert
        Assert.True(simulatorRez is ResultBase.Success);
        Assert.Equal(174.92, route.TimeAns);
    }

    /// <summary>
    /// 4th test case.
    /// Маршрут:
    /// силовой путь, ускоряющий поезд более допустимой скорости маршрута
    /// станция
    /// обычный путь
    /// Результат: неудача.
    /// </summary>
    [Fact]
    public void Case4()
    {
        // Arrange
        var route = new Route(2);
        route.AddSection(new PowerMagneticPath(130, 5)); // уже здесь неудача
        route.AddSection(new Station(100, 23));
        route.AddSection(new OrdinaryMagneticPath(500));
        var train = new SimpleTrain(101.32, 13, 4.22);
        var simulator = new Simulator(train, route);

        // Act
        ResultBase simulatorRez = simulator.Run();

        // Assert
        Assert.True(simulatorRez is ResultBase.Failure);
        Assert.Equal(0, route.TimeAns);
    }

    /// <summary>
    /// 5th test case.
    /// Маршрут:
    /// силовой путь, ускоряющий поезд более допустимой скорости маршрута, но до допустимой скорости станции
    /// обычный путь
    /// станция
    /// обычный путь
    /// Результат: неудача.
    /// </summary>
    [Fact]
    public void Case5()
    {
        // Arrange
        var route = new Route(74);
        route.AddSection(new PowerMagneticPath(1100, 506.6));
        route.AddSection(new OrdinaryMagneticPath(5000));
        route.AddSection(new Station(225.1, 23));
        route.AddSection(new OrdinaryMagneticPath(100));
        var train = new SimpleTrain(101.32, 1300, 15);
        var simulator = new Simulator(train, route);

        // Act
        ResultBase simulatorRez = simulator.Run();

        // Assert
        Assert.True(simulatorRez is ResultBase.Failure);
        Assert.Equal(0, route.TimeAns);
    }

    /// <summary>
    /// 6th test case.
    /// Маршрут:
    /// силовой путь, ускоряющий поезд более допустимой скорости станции
    /// обычный путь
    /// силовой путь, замедляющий поезд до допустимой скорости станции
    /// станция
    /// обычный путь
    /// силовой путь, ускоряющий поезд более допустимой скорости маршрута
    /// обычный путь
    /// силовой путь, замедляющий поезд до допустимой скорости маршрута
    /// Результат: успех.
    /// </summary>
    [Fact]
    public void Case6()
    {
        // Arrange
        var route = new Route(200);
        route.AddSection(new PowerMagneticPath(1100, 709.24));
        route.AddSection(new OrdinaryMagneticPath(500));
        route.AddSection(new PowerMagneticPath(7000, -202.64));
        route.AddSection(new Station(91, 23));
        route.AddSection(new OrdinaryMagneticPath(100));
        route.AddSection(new PowerMagneticPath(5000, 405.28));
        route.AddSection(new OrdinaryMagneticPath(10001));
        route.AddSection(new PowerMagneticPath(10000, -506.6));
        var train = new SimpleTrain(101.32, 1300, 15);
        var simulator = new Simulator(train, route);

        // Act
        ResultBase simulatorRez = simulator.Run();

        // Assert
        Assert.True(simulatorRez is ResultBase.Success);
        Assert.Equal(263, route.TimeAns);
    }

    /// <summary>
    /// 7th test case.
    /// Маршрут:
    /// обычный путь.
    /// Результат: неудача.
    /// </summary>
    [Fact]
    public void Case7()
    {
        // Arrange
        var route = new Route(100000);
        route.AddSection(new OrdinaryMagneticPath(5000));
        var train = new SimpleTrain(101.32, 13, 4.22);
        var simulator = new Simulator(train, route);

        // Act
        ResultBase simulatorRez = simulator.Run();

        // Assert
        Assert.True(simulatorRez is ResultBase.Failure);
        Assert.Equal(0, route.TimeAns);
    }

    /// <summary>
    /// 8th test case.
    /// Маршрут:
    /// силовой путь длины X, прикладывающий силу Y
    /// силовой путь длины X, прикладывающий силу -2Y
    /// Результат: неудача.
    /// </summary>
    [Fact]
    public void Case8()
    {
        // Arrange
        var route = new Route(100000);
        route.AddSection(new PowerMagneticPath(130, 24));
        route.AddSection(new PowerMagneticPath(130, -48));
        var train = new SimpleTrain(101.32, 13, 4.22);
        var simulator = new Simulator(train, route);

        // Act
        ResultBase simulatorRez = simulator.Run();

        // Assert
        Assert.True(simulatorRez is ResultBase.UnstoppableTrain);
        Assert.Equal(0, route.TimeAns);
    }
}