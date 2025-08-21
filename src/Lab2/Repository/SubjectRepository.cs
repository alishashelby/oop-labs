using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.SubjectEntity;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repository;

public class SubjectRepository : IRepository<ISubject, Guid>
{
    private static SubjectRepository? _instance;

    private readonly Dictionary<Guid, ISubject> _objects;

    public static SubjectRepository Instance => _instance ??= new SubjectRepository();

    private SubjectRepository()
    {
        this._objects = new Dictionary<Guid, ISubject>();
    }

    public void Save(ISubject entity)
    {
        this._objects.Add(entity.Id, entity);
    }

    public ISubject? FindById(Guid id)
    {
        return this._objects.ContainsKey(id) ? this._objects[id] : null;
    }

    public ICollection<ISubject> GetAll()
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