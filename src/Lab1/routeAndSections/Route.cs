// <copyright file="Route.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteAndSections;

/// <summary>
/// Represents the whole route of the train.
/// </summary>
public class Route
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Route"/> class.
    /// </summary>
    /// <param name="speedLimit">The speed limit to write.</param>
    public Route(double speedLimit)
    {
        this.SpeedLimit = speedLimit;
        this.Sections = new List<RouteSection>();
        this.TimeAns = 0;
    }

    /// <summary>
    /// Gets length of a route section.
    /// </summary>
    public double SpeedLimit { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the train travelled the section successfully.
    /// </summary>
    public double TimeAns { get; set; }

    /// <summary>
    /// Gets The list of all route sections.
    /// </summary>
    internal ICollection<RouteSection> Sections { get; }

    /// <summary>
    /// Adds new section to the route.
    /// </summary>
    /// <param name="section">A new section of the route.</param>
    public void AddSection(RouteSection section)
    {
        this.Sections.Add(section);
    }
}