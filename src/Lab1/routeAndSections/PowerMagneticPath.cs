// <copyright file="PowerMagneticPath.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Itmo.ObjectOrientedProgramming.Lab1.Result;
using Itmo.ObjectOrientedProgramming.Lab1.Train;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteAndSections;

/// <summary>
/// Represents the second option of route section.
/// </summary>
public class PowerMagneticPath : RouteSection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PowerMagneticPath"/> class.
    /// </summary>
    /// <param name="length">The length of a route section to write.</param>
    /// <param name="force">The force which is applied to a train in this section.</param>
    public PowerMagneticPath(double length, double force)
    {
        this.Length = length;
        this.Force = force;
    }

    /// <summary>
    /// Gets force of a route section.
    /// </summary>
    public double Force { get; }

    /// <summary>
    /// Train travels a power magnetic path.
    /// </summary>
    /// <param name="train">A power magnetic path.</param>
    /// <returns>An amount of time or -1 (if it's unsuccessful).</returns>
    public override ResultBase Travel(TrainBase train)
    {
        if (train.TryApplyForce(this.Force) is ResultBase.UnstoppableTrain)
        {
            return new ResultBase.UnstoppableTrain();
        }

        this.Time = train.CalculateTheTotalTimeToTravelTheSection(this.Length);
        return new ResultBase.Success();
    }
}