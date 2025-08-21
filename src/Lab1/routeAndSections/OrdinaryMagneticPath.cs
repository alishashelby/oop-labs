// <copyright file="OrdinaryMagneticPath.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Itmo.ObjectOrientedProgramming.Lab1.Result;
using Itmo.ObjectOrientedProgramming.Lab1.Train;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteAndSections;

/// <summary>
/// Represents the first option of route section.
/// </summary>
public class OrdinaryMagneticPath : RouteSection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrdinaryMagneticPath"/> class.
    /// </summary>
    /// <param name="length">The length of a route section to write.</param>
    public OrdinaryMagneticPath(double length)
    {
        this.Length = length;
    }

    /// <summary>
    /// Train travels an ordinary magnetic path.
    /// </summary>
    /// <param name="train">An ordinary magnetic path.</param>
    /// <returns>An amount of time or -1 (if it's unsuccessful).</returns>
    public override ResultBase Travel(TrainBase train)
    {
        double sectionTravelTime = train.CalculateTheTotalTimeToTravelTheSection(this.Length);
        if (sectionTravelTime <= 0)
        {
            return new ResultBase.Failure();
        }

        this.Time = sectionTravelTime;

        return new ResultBase.Success();
    }
}