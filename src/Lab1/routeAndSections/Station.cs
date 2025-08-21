// <copyright file="Station.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Itmo.ObjectOrientedProgramming.Lab1.Result;
using Itmo.ObjectOrientedProgramming.Lab1.Train;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteAndSections;

/// <summary>
/// Represents the third option of route section.
/// </summary>
public class Station : RouteSection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Station"/> class.
    /// </summary>
    /// <param name="speedLimit">The speed limit to write.</param>
    /// /// <param name="stationPassageTime">The time which is taken to pass the station to write.</param>
    public Station(double speedLimit, double stationPassageTime)
    {
        this.SpeedLimit = speedLimit;
        this.StationPassageTime = stationPassageTime;
    }

    /// <summary>
    /// Gets length of a route section.
    /// </summary>
    public double SpeedLimit { get; }

    /// <summary>
    /// Gets the time to pass the station a route section.
    /// </summary>
    public double StationPassageTime { get; }

    /// <summary>
    /// Train travels a station.
    /// </summary>
    /// <param name="train">A power magnetic path.</param>
    /// <returns>An amount of time or -1 (if it's unsuccessful).</returns>
    public override ResultBase Travel(TrainBase train)
    {
        if (train.Speed > this.SpeedLimit)
        {
            return new ResultBase.Failure();
        }

        this.Time += this.StationPassageTime;
        return new ResultBase.Success();
    }
}