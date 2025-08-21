using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Prototype;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.SubjectEntity;

public interface ISubject : IEntity, IPrototype<ISubject>
{
    string Name { get; }

    ICollection<LabWork> LabWorks { get; }

    ICollection<LectureMaterials> LectureMaterials { get; }

    User Author { get; }

    Guid? BaseSubjectId { get; init; }

    /// <summary>
    /// Updates name of subject.
    /// </summary>
    /// <param name="newName">The new name which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>True if successful.</returns>
    bool TryUpdateName(string newName, User author);

    /// <summary>
    /// Updates name of labWork.
    /// </summary>
    /// <param name="newLabWorks">The collection of new labWorks which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>A resultType.</returns>
    bool TryUpdateLabs(ICollection<LabWork> newLabWorks, User author);

    /// <summary>
    /// Updates name of LectureMaterials.
    /// </summary>
    /// <param name="newLectureMaterials">The collection of new labWorks which should be set.</param>
    /// <param name="author">The author which should change the labWork.</param>
    /// <returns>A resultType.</returns>
    bool TryUpdateLectures(ICollection<LectureMaterials> newLectureMaterials, User author);
}