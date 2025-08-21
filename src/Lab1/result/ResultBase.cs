// <copyright file="ResultBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Itmo.ObjectOrientedProgramming.Lab1.Result;

/// <summary>
/// Represents the result base.
/// </summary>
public abstract record ResultBase
{
    public sealed record Success : ResultBase;

    public sealed record Failure : ResultBase;

    public sealed record UnstoppableTrain : ResultBase;
}