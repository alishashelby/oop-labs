using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.SubjectEntity;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.SubjectEntity.Factory;
using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace Lab2.Tests;

/// <summary>
/// The main test class.
/// </summary>
public class UnitTests
{
    /// <summary>
    /// 1st test case.
    /// Попытки изменения сущностей не автором – возвращают ошибки - Лабораторная работа.
    /// </summary>
    [Fact]
    public void UpdateLabWorkNotByAuthorReturnsFalse()
    {
        // Arrange
        var author1 = new User("Author1");
        var author2 = new User("Author2");

        LabWork lab = LabWork.Builder.SetAuthor(author1)
            .SetTitle("Lab 1")
            .SetDescription("Lab 1 Description.")
            .SetEvaluationCriteria("Each exercise costs 1 point.")
            .SetPoints(15)
            .Build();

        // Act
        bool isSuccessful = lab.TryUpdatePoints(30, author2);

        // Assert
        Assert.False(isSuccessful);
        Assert.Equal(15, lab.Points);
    }

    /// <summary>
    /// 2nd test case.
    /// Попытки изменения сущностей не автором – возвращают ошибки - Лекционные материалы.
    /// </summary>
    [Fact]
    public void UpdateLectureMaterialsNotByAuthorReturnsFalse()
    {
        // Arrange
        var author1 = new User("Author1");
        var author2 = new User("Author2");

        LectureMaterials lectures = LectureMaterials.Builder.SetAuthor(author1)
            .SetTitle("1st OOP lection")
            .SetShortDescription("OOP-basics")
            .SetContent("OOP Concepts")
            .Build();

        // Act
        bool isSuccessful = lectures.TryUpdateContent("BlA-BLA-BLA", author2);

        // Assert
        Assert.False(isSuccessful);
        Assert.Equal("OOP Concepts", lectures.Content);
    }

    /// <summary>
    /// 3rd test case.
    /// Попытки изменения сущностей не автором – возвращают ошибки - Предмет с экзаменом.
    /// </summary>
    [Fact]
    public void UpdateExamSubjectNotByAuthorReturnsFalse()
    {
        // Arrange
        var author1 = new User("Author1");
        var author2 = new User("Author2");

        LabWork lab = LabWork.Builder.SetAuthor(author1)
            .SetTitle("Lab 1")
            .SetDescription("Lab 1 Description.")
            .SetEvaluationCriteria("Each exercise costs 1 point.")
            .SetPoints(80)
            .Build();
        LabWorkRepository labs = LabWorkRepository.Instance;
        labs.Save(lab);

        LectureMaterials lecture = LectureMaterials.Builder.SetAuthor(author1)
            .SetTitle("1st OOP lection")
            .SetShortDescription("OOP-basics")
            .SetContent("OOP Concepts")
            .Build();
        LectureMaterialsRepository lectures = LectureMaterialsRepository.Instance;
        lectures.Save(lecture);

        var factory = new ExamSubjectFactory(20);

        LabWork? labEntity = labs.FindById(lab.Id);
        LectureMaterials? lectureEntity = lectures.FindById(lecture.Id);
        Assert.NotNull(labEntity);
        Assert.NotNull(lectureEntity);

        ISubject subjectExam = factory.Create("OOP", new List<LabWork> { labEntity }, new List<LectureMaterials> { lectureEntity }, author1);

        // Act
        bool isSuccessful = subjectExam.TryUpdateName("Maths", author2);

        // Assert
        Assert.False(isSuccessful);
        Assert.Equal("OOP", subjectExam?.Name);
    }

    /// <summary>
    /// 4th test case.
    /// Попытки изменения сущностей не автором – возвращают ошибки - Предмет с зачетом.
    /// </summary>
    [Fact]
    public void UpdateZachotSubjectNotByAuthorReturnsFalse()
    {
        // Arrange
        var author1 = new User("Author1");
        var author2 = new User("Author2");

        LabWork lab = LabWork.Builder.SetAuthor(author1)
            .SetTitle("Lab 1")
            .SetDescription("Lab 1 Description.")
            .SetEvaluationCriteria("Each exercise costs 1 point.")
            .SetPoints(100)
            .Build();
        LabWorkRepository labs = LabWorkRepository.Instance;
        labs.RemoveAll();
        labs.Save(lab);

        LectureMaterials lecture = LectureMaterials.Builder.SetAuthor(author1)
            .SetTitle("1st OOP lection")
            .SetShortDescription("OOP-basics")
            .SetContent("OOP Concepts")
            .Build();
        LectureMaterialsRepository lectures = LectureMaterialsRepository.Instance;
        lectures.RemoveAll();
        lectures.Save(lecture);

        var factory = new ZachotSubjectFactory(60);
        ISubject subjectZachot = factory.Create("OOP", labs.GetAll(), lectures.GetAll(), author1);

        // Act
        bool isSuccessful = subjectZachot.TryUpdateName("Maths", author2);

        // Assert
        Assert.False(isSuccessful);
        Assert.Equal("OOP", subjectZachot.Name);
    }

    /// <summary>
    /// 5th test case.
    /// После создания сущностей на основе существующих,
    /// копии должны содержать идентификаторы исходника - Лабораторная работа.
    /// </summary>
    [Fact]
    public void ClonedLabWorkShouldContainBaseID()
    {
        // Arrange
        var author = new User("Author1");

        LabWork lab = LabWork.Builder.SetAuthor(author)
            .SetTitle("Lab 1")
            .SetDescription("Lab 1 Description.")
            .SetEvaluationCriteria("Each exercise costs 1 point.")
            .SetPoints(15)
            .Build();

        // Act
        LabWork clonedLab = lab.Clone();

        // Assert
        Assert.Equal(lab.Id, clonedLab.BaseLabId);
    }

    /// <summary>
    /// 6th test case.
    /// После создания сущностей на основе существующих,
    /// копии должны содержать идентификаторы исходника - Лекционные материалы.
    /// </summary>
    [Fact]
    public void ClonedLectureMaterialsShouldContainBaseID()
    {
        // Arrange
        var author = new User("Author");

        LectureMaterials lectures = LectureMaterials.Builder.SetAuthor(author)
            .SetTitle("1st OOP lection")
            .SetShortDescription("OOP-basics")
            .SetContent("OOP Concepts")
            .Build();

        // Act
        LectureMaterials clonedLectures = lectures.Clone();

        // Assert
        Assert.Equal(lectures.Id, clonedLectures.BaseLectureMaterialsId);
    }

    /// <summary>
    /// 7th test case.
    /// После создания сущностей на основе существующих,
    /// копии должны содержать идентификаторы исходника - Предмет с экзаменом.
    /// </summary>
    [Fact]
    public void ClonedExamSubjectShouldContainBaseID()
    {
        // Arrange
        var author = new User("Author");

        LabWork lab = LabWork.Builder.SetAuthor(author)
            .SetTitle("Lab 1")
            .SetDescription("Lab 1 Description.")
            .SetEvaluationCriteria("Each exercise costs 1 point.")
            .SetPoints(80)
            .Build();
        LabWorkRepository labs = LabWorkRepository.Instance;
        labs.RemoveAll();
        labs.Save(lab);

        LectureMaterials lecture = LectureMaterials.Builder.SetAuthor(author)
            .SetTitle("1st OOP lection")
            .SetShortDescription("OOP-basics")
            .SetContent("OOP Concepts")
            .Build();
        LectureMaterialsRepository lectures = LectureMaterialsRepository.Instance;
        lectures.RemoveAll();
        lectures.Save(lecture);

        var factory = new ExamSubjectFactory(20);
        ISubject subjectExam = factory.Create("OOP", labs.GetAll(), lectures.GetAll(), author);

        // Act
        var clonedSubjectExam = subjectExam?.Clone() as SubjectWithExam;

        // Assert
        Assert.Equal(subjectExam?.Id, clonedSubjectExam?.BaseSubjectId);
    }

    /// <summary>
    /// 8th test case.
    /// После создания сущностей на основе существующих,
    /// копии должны содержать идентификаторы исходника - Предмет с зачетом.
    /// </summary>
    [Fact]
    public void ClonedZachotSubjectShouldContainBaseID()
    {
        // Arrange
        var author = new User("Author");

        LabWork lab = LabWork.Builder.SetAuthor(author)
            .SetTitle("Lab 1")
            .SetDescription("Lab 1 Description.")
            .SetEvaluationCriteria("Each exercise costs 1 point.")
            .SetPoints(100)
            .Build();
        LabWorkRepository labs = LabWorkRepository.Instance;
        labs.RemoveAll();
        labs.Save(lab);

        LectureMaterials lecture = LectureMaterials.Builder.SetAuthor(author)
            .SetTitle("1st OOP lection")
            .SetShortDescription("OOP-basics")
            .SetContent("OOP Concepts")
            .Build();
        LectureMaterialsRepository lectures = LectureMaterialsRepository.Instance;
        lectures.RemoveAll();
        lectures.Save(lecture);

        var factory = new ZachotSubjectFactory(60);
        ISubject subjectZachot = factory.Create("OOP", labs.GetAll(), lectures.GetAll(), author);

        // Act
        var clonedSubjectZachot = subjectZachot?.Clone() as SubjectWithZachot;

        // Assert
        Assert.Equal(subjectZachot?.Id, clonedSubjectZachot?.BaseSubjectId);
    }

    /// <summary>
    /// 9th test case.
    /// При создании предмета,
    /// с количеством баллов не равное 100 – возвращается ошибка.
    /// Предмет с экзаменом.
    /// </summary>
    [Fact]
    public void CreateExamSubjectWithNot100PointsThrowsArgumentException()
    {
        // Arrange
        var author = new User("Author");

        LabWork lab = LabWork.Builder.SetAuthor(author)
            .SetTitle("Lab 1")
            .SetDescription("Lab 1 Description.")
            .SetEvaluationCriteria("Each exercise costs 1 point.")
            .SetPoints(75)
            .Build();
        LabWorkRepository labs = LabWorkRepository.Instance;
        labs.Save(lab);

        LectureMaterials lecture = LectureMaterials.Builder.SetAuthor(author)
            .SetTitle("1st OOP lection")
            .SetShortDescription("OOP-basics")
            .SetContent("OOP Concepts")
            .Build();
        LectureMaterialsRepository lectures = LectureMaterialsRepository.Instance;
        lectures.Save(lecture);

        var factory = new ExamSubjectFactory(20);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(()
            => factory.Create("OOP", labs.GetAll(), lectures.GetAll(), author));

        // Assert
        Assert.Equal("The number of points must be 100.", exception.Message);
    }

    /// <summary>
    /// 10th test case.
    /// При создании предмета,
    /// с количеством баллов не равное 100 – возвращается ошибка.
    /// Предмет с зачетом.
    /// </summary>
    [Fact]
    public void CreateZachotSubjectWithNot100PointsThrowsArgumentException()
    {
        // Arrange
        var author = new User("Author");

        LabWork lab = LabWork.Builder.SetAuthor(author)
            .SetTitle("Lab 1")
            .SetDescription("Lab 1 Description.")
            .SetEvaluationCriteria("Each exercise costs 1 point.")
            .SetPoints(90)
            .Build();
        LabWorkRepository labs = LabWorkRepository.Instance;
        labs.Save(lab);

        LectureMaterials lecture = LectureMaterials.Builder.SetAuthor(author)
            .SetTitle("1st OOP lection")
            .SetShortDescription("OOP-basics")
            .SetContent("OOP Concepts")
            .Build();
        LectureMaterialsRepository lectures = LectureMaterialsRepository.Instance;
        lectures.Save(lecture);

        var factory = new ZachotSubjectFactory(60);

        // Act
        ArgumentException exception = Assert.Throws<ArgumentException>(()
            => factory.Create("OOP", labs.GetAll(), lectures.GetAll(), author));

        // Assert
        Assert.Equal("The number of points must be 100.", exception.Message);
    }
}