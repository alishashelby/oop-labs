using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repository;

public class LabWorkRepository : IRepository<LabWork, Guid>
{
    private static LabWorkRepository? _instance;

    private readonly Dictionary<Guid, LabWork> _objects;

    public static LabWorkRepository Instance => _instance ??= new LabWorkRepository();

    private LabWorkRepository()
    {
        this._objects = new Dictionary<Guid, LabWork>();
    }

    public void Save(LabWork entity)
    {
        this._objects.Add(entity.Id, entity);
    }

    public LabWork? FindById(Guid id)
    {
        return this._objects.ContainsKey(id) ? this._objects[id] : null;
    }

    public ICollection<LabWork> GetAll()
    {
        return _objects.Values;
    }

    public bool RemoveById(Guid id)
    {
        return this._objects.Remove(id);
    }

    public void RemoveAll()
    {
        this._objects.Clear();
    }
}