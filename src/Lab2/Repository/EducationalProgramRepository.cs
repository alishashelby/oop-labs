using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repository;

public class EducationalProgramRepository : IRepository<EducationalProgram, Guid>
{
    private static EducationalProgramRepository? _instance;

    private readonly Dictionary<Guid, EducationalProgram> _objects;

    public static EducationalProgramRepository Instance => _instance ??= new EducationalProgramRepository();

    private EducationalProgramRepository()
    {
        this._objects = new Dictionary<Guid, EducationalProgram>();
    }

    public void Save(EducationalProgram entity)
    {
        this._objects.Add(entity.Id, entity);
    }

    public EducationalProgram? FindById(Guid id)
    {
        return this._objects.ContainsKey(id) ? this._objects[id] : null;
    }

    public ICollection<EducationalProgram> GetAll()
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