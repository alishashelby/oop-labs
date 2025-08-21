using System;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repository;

public interface IRepository<T, TId>
{
    /// <summary>
    /// Saves entity to the repository.
    /// </summary>
    /// <param name="entity">The object which should be added.</param>
    void Save(T entity);

    /// <summary>
    /// Gets entity from the repository by id.
    /// </summary>
    /// <param name="id">The id of entity which should be returned.</param>
    /// <returns>The generic type parameter or null.</returns>
    T? FindById(Guid id);

    /// <summary>
    /// Gets all entities from the repository.
    /// </summary>
    /// <returns>A collection of entities.</returns>
    ICollection<T> GetAll();

    /// <summary>
    /// Removes entity by id.
    /// </summary>
    /// <returns>True if remove is successful.</returns>
    bool RemoveById(Guid id);

    /// <summary>
    /// Removes all entities from repository.
    /// </summary>
    void RemoveAll();
}