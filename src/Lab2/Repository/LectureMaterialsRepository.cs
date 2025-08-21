using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repository;

public class LectureMaterialsRepository : IRepository<LectureMaterials, Guid>
{
    private static LectureMaterialsRepository? _instance;

    private readonly Dictionary<Guid, LectureMaterials> _objects;

    public static LectureMaterialsRepository Instance => _instance ??= new LectureMaterialsRepository();

    private LectureMaterialsRepository()
    {
        this._objects = new Dictionary<Guid, LectureMaterials>();
    }

    public void Save(LectureMaterials entity)
    {
        this._objects.Add(entity.Id, entity);
    }

    public LectureMaterials? FindById(Guid id)
    {
        return this._objects.ContainsKey(id) ? this._objects[id] : null;
    }

    public ICollection<LectureMaterials> GetAll()
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