// <copyright file="TrainBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Itmo.ObjectOrientedProgramming.Lab1.Result;

namespace Itmo.ObjectOrientedProgramming.Lab1.Train;

/// <summary>
/// Represents a base class of a train.
/// </summary>
public abstract class TrainBase
{
    /// <summary>
    /// Gets or sets speed of a train.
    /// </summary>
    public double Speed { get; protected set; }

    /// <summary>
    /// Gets or sets acceleration of a train.
    /// </summary>
    protected double Acceleration { get; set; }

    /// <summary>
    /// Gets the accuracy that is given to the train.
    /// </summary>
    protected double Accuracy { get; init; }

    /// <summary>
    /// Gets weight of a train.
    /// </summary>
    protected double Weight { get; init; }

    /// <summary>
    /// Gets maximum acceptable force of a train.
    /// </summary>
    protected double MaxAcceptableForce { get; init; }

    /// <summary>
    /// Represents a Boolean (true or false) value.
    /// </summary>
    /// <param name="force">The new force which is applied to a train.</param>
    /// <returns>A Boolean (true or false) value.</returns>
    public abstract ResultBase TryApplyForce(double force);

    /// <summary>
    /// Calculates the total time to travel the distance.
    /// </summary>
    /// <param name="distance">The distance which train has travelled.</param>
    /// <returns>A double value - the total time.</returns>
    public abstract double CalculateTheTotalTimeToTravelTheSection(double distance);
}