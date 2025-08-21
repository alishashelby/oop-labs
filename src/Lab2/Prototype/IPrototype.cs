namespace Itmo.ObjectOrientedProgramming.Lab2.Prototype;

/// <summary>
/// Represents the IPrototype.
/// </summary>
/// <typeparam name="T">The generic type parameter.</typeparam>
public interface IPrototype<out T>
{
    /// <summary>
    /// Clones entity.
    /// </summary>
    /// <returns>The generic type parameter.</returns>
    T Clone();
}