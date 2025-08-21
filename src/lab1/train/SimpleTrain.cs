// <copyright file="SimpleTrain.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Itmo.ObjectOrientedProgramming.Lab1.Result;

namespace Itmo.ObjectOrientedProgramming.Lab1.Train;

/// <summary>
/// Represents a train.
/// </summary>
public class SimpleTrain : TrainBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleTrain"/> class.
    /// </summary>
    /// <param name="weight">The weight of a train to write.</param>
    /// <param name="maxAcceptableForce">The maximum acceptable force of a train to write.</param>
    /// <param name="accuracy">The accuracy that is given to the train to write.</param>
    public SimpleTrain(double weight, double maxAcceptableForce, double accuracy)
    {
        this.Weight = weight;
        this.MaxAcceptableForce = maxAcceptableForce;
        this.Speed = 0;
        this.Acceleration = 0;
        this.Accuracy = accuracy;
    }

    /// <summary>
    /// Represents a Boolean (true or false) value.
    /// </summary>
    /// <param name="force">The new force which is applied to a train.</param>
    /// <returns>A Boolean (true or false) value.</returns>
    public override ResultBase TryApplyForce(double force)
    {
        if (double.Abs(force) > this.MaxAcceptableForce)
        {
            return new ResultBase.UnstoppableTrain();
        }

        this.Acceleration = force / this.Weight;
        return new ResultBase.Success();
    }

    /// <summary>
    /// Calculates the total time to travel the distance.
    /// </summary>
    /// <param name="distance">The distance which train has travelled.</param>
    /// <returns>A double value - the total time.</returns>
    public override double CalculateTheTotalTimeToTravelTheSection(double distance)
    {
        double time = 0;
        double resultingSpeed;
        while (distance > 0)
        {
            time += this.Accuracy;
            resultingSpeed = this.Speed + (this.Acceleration * this.Accuracy);
            if (resultingSpeed <= 0)
            {
                return -1;
            }

            this.Speed = resultingSpeed;
            distance -= resultingSpeed * this.Accuracy;
        }

        return time;
    }
}