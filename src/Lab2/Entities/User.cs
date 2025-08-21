using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class User : IEntity
{
    public User(string name)
    {
        this.Name = name;
        this.Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public string Name { get; }
}