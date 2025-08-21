using System;
using System.Collections.Generic;
using System.Linq;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.SubjectEntity.Factory;

public class ZachotSubjectFactory : ISubjectFactory
{
    private readonly int _minPoints;

    public ZachotSubjectFactory(int minPoints)
    {
        this._minPoints = minPoints;
    }

    public ISubject Create(
        string name,
        ICollection<LabWork> labWorks,
        ICollection<LectureMaterials> lectureMaterials,
        User author)
    {
        if (labWorks.Sum(lw => lw.Points) != 100)
        {
            throw new ArgumentException("The number of points must be 100.");
        }

        return new SubjectWithZachot(name, labWorks, lectureMaterials, author, this._minPoints);
    }
}