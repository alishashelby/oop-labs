// <copyright file="Simulator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Itmo.ObjectOrientedProgramming.Lab1.Result;
using Itmo.ObjectOrientedProgramming.Lab1.RouteAndSections;
using Itmo.ObjectOrientedProgramming.Lab1.Train;

namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <summary>
/// Represents a simulator.
/// </summary>
public class Simulator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Simulator"/> class.
    /// </summary>
    /// <param name="train">The train to run.</param>
    /// <param name="route">The route which train should run.</param>
    public Simulator(TrainBase train, Route route)
    {
        this.Train = train;
        this.Route = route;
    }

    /// <summary>
    /// Gets weight of a train.
    /// </summary>
    private TrainBase Train { get; init; }

    /// <summary>
    /// Gets maximum acceptable force of a train.
    /// </summary>
    private Route Route { get; init; }

    /// <summary>
    /// Runs th simulator.
    /// </summary>
    /// <returns>A Result type.</returns>
    public ResultBase Run()
    {
        double finalTime = 0;
        foreach (RouteSection section in this.Route.Sections)
        {
            ResultBase rez = section.Travel(this.Train);
            if (rez is ResultBase.Failure || rez is ResultBase.UnstoppableTrain)
            {
                return rez;
            }

            finalTime += section.Time;
        }

        if (this.Train.Speed > this.Route.SpeedLimit)
        {
            return new ResultBase.Failure();
        }

        this.Route.TimeAns = finalTime;
        return new ResultBase.Success();
    }
}