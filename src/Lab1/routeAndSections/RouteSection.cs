// <copyright file="RouteSection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Itmo.ObjectOrientedProgramming.Lab1.Result;
using Itmo.ObjectOrientedProgramming.Lab1.Train;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteAndSections;

/// <summary>
/// Represents a section of the route - base class.
/// </summary>
public abstract class RouteSection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RouteSection"/> class.
    /// </summary>
    protected RouteSection()
    {
        this.Length = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RouteSection"/> class.
    /// </summary>
    /// <param name="length">The length of a route section to write.</param>
    protected RouteSection(double length)
    {
        this.Length = length;
    }

    /// <summary>
    /// Gets length of a route section.
    /// </summary>
    public double Length { get; init; }

    /// <summary>
    /// Gets or sets length of a route section.
    /// </summary>
    public double Time { get; protected set; }

    /// <summary>
    /// Train travels a section.
    /// </summary>
    /// <param name="train">A train.</param>
    /// <returns>An amount of time or -1 (if it's unsuccessful).</returns>
    public abstract ResultBase Travel(TrainBase train);
}