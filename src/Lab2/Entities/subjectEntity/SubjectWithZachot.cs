using System;
using System.Collections.Generic;
using System.Linq;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.SubjectEntity;

public class SubjectWithZachot : ISubject
{
    internal SubjectWithZachot(
        string name,
        ICollection<LabWork> labWorks,
        ICollection<LectureMaterials> lectureMaterials,
        User author,
        int minPoints)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.LabWorks = labWorks;
        this.LectureMaterials = lectureMaterials;
        this.Author = author;
        this.MinPoints = minPoints;
    }

    public Guid Id { get; }

    public string Name { get; private set; }

    public ICollection<LabWork> LabWorks { get;  private set; }

    public ICollection<LectureMaterials> LectureMaterials { get;  private set; }

    public User Author { get; }

    public Guid? BaseSubjectId { get;  init; }

    public int MinPoints { get; private set; }

    /// <summary>
    /// Updates name of subject.
    /// </summary>
    /// <param name="newName">The new name which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    public bool TryUpdateName(string newName, User author)
    {
        if (this.Author.Id != author.Id)
        {
            return false;
        }

        this.Name = newName;
        return true;
    }

    /// <summary>
    /// Updates name of labWork.
    /// </summary>
    /// <param name="newLabWorks">The collection of new labWorks which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    public bool TryUpdateLabs(ICollection<LabWork> newLabWorks, User author)
    {
        if (this.Author.Id != author.Id || newLabWorks.Sum(lw => lw.Points) != 100)
        {
            return false;
        }

        this.LabWorks = newLabWorks.Select(lw => lw.Clone()).ToList();
        return true;
    }

    /// <summary>
    /// Updates name of LectureMaterials.
    /// </summary>
    /// <param name="newLectureMaterials">The collection of new labWorks which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    public bool TryUpdateLectures(ICollection<LectureMaterials> newLectureMaterials, User author)
    {
        if (this.Author.Id != author.Id)
        {
            return false;
        }

        this.LectureMaterials = newLectureMaterials.Select(lw => lw.Clone()).ToList();
        return true;
    }

    /// <summary>
    /// Clones entity.
    /// </summary>
    /// <returns>The subject object.</returns>
    public ISubject Clone()
    {
        var newSubject = new SubjectWithZachot(
            this.Name,
            this.LabWorks.Select(lw => lw.Clone()).ToList(),
            this.LectureMaterials.Select(lw => lw.Clone()).ToList(),
            this.Author,
            this.MinPoints)
        {
            BaseSubjectId = this.Id,
        };
        return newSubject;
    }
}