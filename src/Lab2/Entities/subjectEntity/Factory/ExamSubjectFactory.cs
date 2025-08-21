using System;
using System.Collections.Generic;
using System.Linq;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.SubjectEntity.Factory;

public class ExamSubjectFactory : ISubjectFactory
{
    private readonly int _examPoints;

    public ExamSubjectFactory(int examPoints)
    {
        this._examPoints = examPoints;
    }

    public ISubject Create(
        string name,
        ICollection<LabWork> labWorks,
        ICollection<LectureMaterials> lectureMaterials,
        User author)
    {
        if (labWorks.Sum(lw => lw.Points) + this._examPoints != 100)
        {
            throw new ArgumentException("The number of points must be 100.");
        }

        return new SubjectWithExam(name, labWorks, lectureMaterials, author, this._examPoints);
    }
}