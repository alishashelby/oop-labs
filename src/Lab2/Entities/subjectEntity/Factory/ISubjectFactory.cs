using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.SubjectEntity.Factory;

public interface ISubjectFactory
{
    ISubject Create(
        string name,
        ICollection<LabWork> labWorks,
        ICollection<LectureMaterials> lectureMaterials,
        User author);
}